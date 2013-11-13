#region Copyright (c) 2013 James Snape
// <copyright file="SourceFile.cs" company="James Snape">
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
    using System.Net;

    /// <summary>
    /// SourceFile class definition.
    /// </summary>
    public class SourceFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceFile"/> class.
        /// </summary>
        /// <param name="url">Source URL.</param>
        /// <param name="description">Source description.</param>
        public SourceFile(string url, string description)
        {
            this.Uri = new Uri(url);
            this.Description = description;
        }

        /// <summary>
        /// Gets the source URI.
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <remarks>The absolute path includes a leading slash.</remarks>
        public string FileName
        {
            get { return WebUtility.UrlDecode(this.Uri.AbsolutePath.Substring(1)); }
        }
    }
}
