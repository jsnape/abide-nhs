#region Copyright (c) 2013 James Snape
// <copyright file="PracticeRecord.cs" company="James Snape">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// PracticeRecord class definition.
    /// </summary>
    public class PracticeRecord : IEquatable<PracticeRecord>
    {
        /// <summary>
        /// Practice code
        /// </summary>
        private string code;

        /// <summary>
        /// Practice name
        /// </summary>
        private string name;

        /// <summary>
        /// Practice group
        /// </summary>
        private string group;

        /// <summary>
        /// Practice street
        /// </summary>
        private string street;

        /// <summary>
        /// Practice city
        /// </summary>
        private string city;

        /// <summary>
        /// Practice county
        /// </summary>
        private string county;

        /// <summary>
        /// Practice postcode
        /// </summary>
        private string postcode;
        
        /// <summary>
        /// Gets or sets the Date Key.
        /// </summary>
        public int DateKey { get; set; }

        /// <summary>
        /// Gets or sets the practice code.
        /// </summary>
        public string Code 
        {
            get { return this.code; }
            set { this.code = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the practice name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the medical group.
        /// </summary>
        public string Group
        {
            get { return this.group; }
            set { this.group = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string Street
        {
            get { return this.street; }
            set { this.street = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City
        {
            get { return this.city; }
            set { this.city = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        public string County
        {
            get { return this.county; }
            set { this.county = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        public string Postcode
        {
            get { return this.postcode; }
            set { this.postcode = value == null ? value : value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <remarks>Does not consider Source or DateKey as structural members for comparison.</remarks>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(PracticeRecord other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.City != other.City)
            {
                return false;
            }

            if (this.Code != other.Code)
            {
                return false;
            }
            
            if (this.County != other.County)
            {
                return false;
            }

            if (this.Group != other.Group)
            {
                return false;
            }

            if (this.Name != other.Name)
            {
                return false;
            }

            if (this.Postcode != other.Postcode)
            {
                return false;
            }

            if (this.Street != other.Street)
            {
                return  false;
            }

            return true;
        }
    }
}
