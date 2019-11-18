/* PluralizationServiceUtil
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Efrpg.Pluralization
{
    internal static class PluralizationServiceUtil
    {
        internal static bool DoesWordContainSuffix(string word, IEnumerable<string> suffixes, CultureInfo culture)
        {
            return suffixes.Any(s => word.EndsWith(s, true, culture));
        }

        internal static bool TryGetMatchedSuffixForWord(
            string word, IEnumerable<string> suffixes, CultureInfo culture, out string matchedSuffix)
        {
            matchedSuffix = null;
            if (DoesWordContainSuffix(word, suffixes, culture))
            {
                matchedSuffix = suffixes.First(s => word.EndsWith(s, true, culture));
                return true;
            }
            return false;
        }

        internal static bool TryInflectOnSuffixInWord(
            string word, IEnumerable<string> suffixes, Func<string, string> operationOnWord, CultureInfo culture,
            out string newWord)
        {
            newWord = null;
            string matchedSuffixString;

            if (TryGetMatchedSuffixForWord(
                word,
                suffixes,
                culture,
                out matchedSuffixString))
            {
                newWord = operationOnWord(word);
                return true;
            }
            return false;
        }
    }
}
