#region Copyright (c) 2013 James Snape
// <copyright file="PracticeRepository.cs" company="James Snape">
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

namespace Abide.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CsvHelper;

    /// <summary>
    /// PracticeRepository class definition.
    /// </summary>
    public sealed class PracticeRepository
    {
        /// <summary>
        /// Source path.
        /// </summary>
        private readonly string sourcePath;

        /// <summary>
        /// File pattern.
        /// </summary>
        private readonly string searchPattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="PracticeRepository"/> class.
        /// </summary>
        /// <param name="sourcePath">Source path for practice files.</param>
        /// <param name="searchPattern">Source file pattern.</param>
        public PracticeRepository(string sourcePath, string searchPattern)
        {
            if (sourcePath == null)
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (!Directory.Exists(sourcePath))
            {
                throw new ArgumentException("Source path does not exist", "sourcePath");
            }

            if (searchPattern == null)
            {
                throw new ArgumentNullException("searchPattern");
            }

            this.sourcePath = sourcePath;
            this.searchPattern = searchPattern;
        }

        /// <summary>
        /// Enumerates the source files and returns all the relevant records.
        /// </summary>
        /// <returns>A sequence of practice records.</returns>
        public IEnumerable<PracticeRecord> List()
        {
            var files = Directory.EnumerateFiles(this.sourcePath, this.searchPattern);

            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<PracticeRecordMap>();

                    while (csv.Read())
                    {
                        yield return csv.GetRecord<PracticeRecord>();
                    }
                }
            }
        }
    }
}
