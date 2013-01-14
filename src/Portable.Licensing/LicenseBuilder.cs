﻿﻿//
// Copyright © 2012 - 2013 Nauck IT KG     http://www.nauck-it.de
//
// Author:
//  Daniel Nauck        <d.nauck(at)nauck-it.de>
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Portable.Licensing.Model;

namespace Portable.Licensing
{
    /// <summary>
    /// Implementation of the <see cref="ILicenseBuilder"/>, a fluent api
    /// to create new licenses.
    /// </summary>
    internal class LicenseBuilder : ILicenseBuilder
    {
        private readonly License license;

        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseBuilder"/> class.
        /// </summary>
        public LicenseBuilder()
        {
            license = new License();
        }

        /// <summary>
        /// Sets the unique identifier of the <see cref="ILicense"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the <see cref="ILicense"/>.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder WithUniqueIdentifier(Guid id)
        {
            license.Id = id;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="LicenseType"/> of the <see cref="ILicense"/>.
        /// </summary>
        /// <param name="type">The <see cref="LicenseType"/> of the <see cref="ILicense"/>.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder As(LicenseType type)
        {
            license.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the expiration date of the <see cref="ILicense"/>.
        /// </summary>
        /// <param name="date">The expiration date of the <see cref="ILicense"/>.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder ExpiresAt(DateTime date)
        {
            license.Expiration = date.ToUniversalTime();
            return this;
        }

        /// <summary>
        /// Sets the maximum utilization of the <see cref="ILicense"/>.
        /// This can be the quantity of developers for a "per-developer-license".
        /// </summary>
        /// <param name="utilization">The maximum utilization of the <see cref="ILicense"/>.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder WithMaximumUtilization(int utilization)
        {
            license.Quantity = utilization;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="ICustomer">license holder</see> of the <see cref="ILicense"/>.
        /// </summary>
        /// <param name="name">The name of the license holder.</param>
        /// <param name="email">The email of the license holder.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder LicensedTo(string name, string email)
        {
            license.Customer.Name = name;
            license.Customer.Email = email;
            return this;
        }

        /// <summary>
        /// Sets the licensed product features of the <see cref="ILicense"/>.
        /// </summary>
        /// <param name="productFeatures">The licensed product features of the <see cref="ILicense"/>.</param>
        /// <returns>The <see cref="ILicenseBuilder"/>.</returns>
        public ILicenseBuilder WithProductFeatures(IDictionary<string, string> productFeatures)
        {
            license.ProductFeatures.AddAll(productFeatures);
            return this;
        }

        /// <summary>
        /// Create and sign a new <see cref="ILicense"/> with the specified
        /// private encryption key.
        /// </summary>
        /// <param name="privateKey">The private encryption key for the signature.</param>
        /// <returns>The signed <see cref="ILicense"/>.</returns>
        public ILicense CreateAndSignWithPrivateKey(string privateKey)
        {
            license.Sign(privateKey);
            return license;
        }
    }
}