/* StringBidirectionalDictionary
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

using System.Collections.Generic;

namespace Efrpg.Pluralization
{
    internal class StringBidirectionalDictionary : BidirectionalDictionary<string, string>
    {
        internal StringBidirectionalDictionary()
        {
        }

        internal StringBidirectionalDictionary(Dictionary<string, string> firstToSecondDictionary)
            : base(firstToSecondDictionary)
        {
        }

        internal override bool ExistsInFirst(string value)
        {
            return base.ExistsInFirst(value.ToLowerInvariant());
        }

        internal override bool ExistsInSecond(string value)
        {
            return base.ExistsInSecond(value.ToLowerInvariant());
        }

        internal override string GetFirstValue(string value)
        {
            return base.GetFirstValue(value.ToLowerInvariant());
        }

        internal override string GetSecondValue(string value)
        {
            return base.GetSecondValue(value.ToLowerInvariant());
        }
    }
}