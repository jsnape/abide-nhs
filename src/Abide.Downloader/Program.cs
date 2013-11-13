#region Copyright (c) 2013 James Snape
// <copyright file="Program.cs" company="James Snape">
// Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// </copyright>
#endregion

namespace Abide.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Args;
    using Args.Help.Formatters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Main program entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// JSON metadata URL.
        /// </summary>
        private const string MetadataUrl = @"http://data.gov.uk/api/2/rest/package/gp-practice-prescribing-data";

        /// <summary>
        /// Download throttle.
        /// </summary>
        private static readonly SemaphoreSlim Throttler = new SemaphoreSlim(initialCount: 3);

        /// <summary>
        /// Gets the global options.
        /// </summary>
        public static Options Options { get; private set; }

        /// <summary>
        /// Main program entry point.
        /// </summary>
        /// <param name="args">Program arguments</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Parsing arguments");
            Program.Options = ParseArguments(args);

            EnsurePathExists(Program.Options.DownloadPath);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) => cancellationTokenSource.Cancel();

            Run(cancellationTokenSource.Token).Wait(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Parses the command line arguments into 
        /// </summary>
        /// <param name="args">Array of command line arguments.</param>
        /// <returns>A set of options.</returns>
        private static Options ParseArguments(string[] args)
        {
            var optionModel = Configuration.Configure<Options>();
            var options = optionModel.CreateAndBind(args);

            if (options.Help)
            {
                var help = new Args.Help.HelpProvider().GenerateModelHelp(optionModel);
                var formatter = new ConsoleHelpFormatter();

                Console.WriteLine(formatter.GetHelp(help));
            }

            return options;
        }

        /// <summary>
        /// Ensures the specified path exists.
        /// </summary>
        /// <param name="path">Path to check and create if missing.</param>
        private static void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }            
        }

        /// <summary>
        /// Main execution function.
        /// </summary>
        /// <remarks>This code could go in the main function except it can't be asynchronous.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the completion of this function.</returns>
        private static async Task Run(CancellationToken cancellationToken)
        {
            var json = await FetchMetadata(cancellationToken);
            var sources = EnumerateFiles(json);

            var downloadTasks = new List<Task>();

            foreach (var source in sources)
            {
                Console.WriteLine("Found {0} - {1}", source.Description, source.Uri);

                downloadTasks.Add(Retry(source, cancellationToken));
            }

            await Task.WhenAll(downloadTasks);
        }

        /// <summary>
        /// Fetches the JSON metadata.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A parsed <c>JSON</c> object.</returns>
        private static async Task<JObject> FetchMetadata(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                string body = await client.GetStringAsync(MetadataUrl);

                return JObject.Parse(body);
            }            
        }

        /// <summary>
        /// Lists all the relevant files in the metadata.
        /// </summary>
        /// <param name="json">JSON object to enumerate.</param>
        /// <returns>A sequence of source files.</returns>
        private static IEnumerable<SourceFile> EnumerateFiles(JObject json)
        {
            var query = from d in json["resources"]
                        where d["format"].ToString() == "exe" && d["date"].ToString().Contains("2012")
                        select new SourceFile(
                            d["url"].ToString(),
                            d["description"].ToString().Trim());

            return query;
        }

        /// <summary>
        /// Retries the downloads.
        /// </summary>
        /// <param name="source">Source file.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the completion of this function.</returns>
        private static async Task Retry(SourceFile source, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Throttle(source, cancellationToken);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Throttles the downloads.
        /// </summary>
        /// <param name="source">Source file.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the completion of this function.</returns>
        private static async Task Throttle(SourceFile source, CancellationToken cancellationToken)
        {
            Console.WriteLine("Waiting to download {0}", source.FileName);

            await Throttler.WaitAsync();
            
            try
            {
                await DownloadFile(source, cancellationToken);
            }
            finally
            {
                Throttler.Release();
            }
        }

        /// <summary>
        /// Downloads a single file.
        /// </summary>
        /// <param name="source">Source file.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the completion of this function.</returns>
        private static async Task DownloadFile(SourceFile source, CancellationToken cancellationToken)
        {
            var path = Path.Combine(Program.Options.DownloadPath, source.FileName);

            Console.WriteLine("Downloading {0}", path);

            using (var client = new HttpClient())
            {
                using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var stream = await client.GetStreamAsync(source.Uri))
                {
                    await stream.CopyToAsync(output);
                }
            }

            Console.WriteLine("Completed {0}", path);
        }
    }
}
