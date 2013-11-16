#region Copyright (c) 2013 James Snape
// <copyright file="PracticeRepositoryFacts.cs" company="James Snape">
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

namespace Abide.Data.Tests
{
    using System.Linq;
    using Xunit;
    
    /// <summary>
    /// PracticeRepository Facts
    /// </summary>
    public static class PracticeRepositoryFacts
    {
        /// <summary>
        /// Data source path.
        /// </summary>
        private const string SourcePath = @"C:\Users\James\Desktop\nhs-data";

        /// <summary>
        /// Repository returns all the records.
        /// </summary>
        [Fact]
        public static void RepositoryReturnsAllTheRecords()
        {
            var repository = new PracticeRepository(SourcePath, "*REXT.CSV");
            var recordCount = repository.List().Count();

            Assert.True(recordCount > 10000);
        }
    }
}
