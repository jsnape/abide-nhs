#region Copyright (c) 2013 James Snape
// <copyright file="Options.cs" company="James Snape">
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
    using System.ComponentModel;
    using System.IO;

    /// <summary>
    /// Command line options holder.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Default download folder.
        /// </summary>
        private const string DefaultDownloadPath = "nhs-data";

        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class.
        /// </summary>
        public Options()
        {
            this.DownloadPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                DefaultDownloadPath);
        }

        /// <summary>
        /// Gets or sets a value indicating whether help should be shown.
        /// </summary>
        [Description("Shows command line help and options")]
        public bool Help { get; set; }

        /// <summary>
        /// Gets or sets the download path.
        /// </summary>
        [Description("Source data path")]
        public string DownloadPath { get; set; }
    }
}
