/* CustomPluralizationEntry
Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
Microsoft Open Technologies would like to thank its contributors, a list of whom
are at http://aspnetwebstack.codeplex.com/wikipage?title=Contributors.

Licensed under the Apache License, Version 2.0 (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied. See the License for the specific language governing permissions
and limitations under the License.
*/

using System;

namespace Efrpg.Pluralization
{
    /// <summary>
    /// Represents a custom pluralization term to be used by the <see cref="EnglishPluralizationService" />
    /// </summary>
    public class CustomPluralizationEntry
    {
        /// <summary>
        /// Get the singular.
        /// </summary>
        public string Singular { get; private set; }

        /// <summary>
        /// Get the plural.
        /// </summary>
        public string Plural { get; private set; }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="singular">A non null or empty string representing the singular.</param>
        /// <param name="plural">A non null or empty string representing the plural.</param>
        public CustomPluralizationEntry(string singular, string plural)
        {
            if (singular == null) throw new ArgumentNullException("singular");
            if (plural == null) throw new ArgumentNullException("plural");

            Singular = singular;
            Plural = plural;
        }
    }
}
