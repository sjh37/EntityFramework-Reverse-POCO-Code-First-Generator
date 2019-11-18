/* IPluralizationService
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

namespace Efrpg.Pluralization
{
    /// <summary>
    /// Pluralization services to be used by the EF runtime implement this interface.
    /// </summary>
    public interface IPluralizationService
    {
        /// <summary>
        /// Pluralize a word using the service.
        /// </summary>
        /// <param name="word">The word to pluralize.</param>
        /// <returns>The pluralized word </returns>
        string Pluralize(string word);

        /// <summary>
        /// Singularize a word using the service.
        /// </summary>
        /// <param name="word">The word to singularize.</param>
        /// <returns>The singularized word.</returns>
        string Singularize(string word);
    }
}