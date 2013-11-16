#region Copyright (c) 2013 James Snape
// <copyright file="PracticeRecordMap.cs" company="James Snape">
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
    using CsvHelper.Configuration;

    /// <summary>
    /// PracticeRecord mapping definition.
    /// </summary>
    [CLSCompliant(false)]
    public class PracticeRecordMap : CsvClassMap<PracticeRecord>
    {
        /// <summary>
        /// Called to create the mappings.
        /// </summary>
        public override void CreateMap()
        {
            Map(m => m.DateKey).Index(0);
            Map(m => m.Code).Index(1);
            Map(m => m.Name).Index(2);
            Map(m => m.Group).Index(3);
            Map(m => m.Street).Index(4);
            Map(m => m.City).Index(5);
            Map(m => m.County).Index(6);
            Map(m => m.Postcode).Index(7);
        }
    }
}
