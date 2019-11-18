/* BidirectionalDictionary
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
    // <summary>
    // This class provide service for both the singularization and pluralization, it takes the word pairs
    // in the ctor following the rules that the first one is singular and the second one is plural.
    // </summary>
    internal class BidirectionalDictionary<TFirst, TSecond>
    {
        internal Dictionary<TFirst, TSecond> FirstToSecondDictionary { get; set; }
        internal Dictionary<TSecond, TFirst> SecondToFirstDictionary { get; set; }

        internal BidirectionalDictionary()
        {
            FirstToSecondDictionary = new Dictionary<TFirst, TSecond>();
            SecondToFirstDictionary = new Dictionary<TSecond, TFirst>();
        }

        internal BidirectionalDictionary(Dictionary<TFirst, TSecond> firstToSecondDictionary)
            : this()
        {
            foreach (var key in firstToSecondDictionary.Keys)
            {
                AddValue(key, firstToSecondDictionary[key]);
            }
        }

        internal virtual bool ExistsInFirst(TFirst value)
        {
            if (FirstToSecondDictionary.ContainsKey(value))
            {
                return true;
            }
            return false;
        }

        internal virtual bool ExistsInSecond(TSecond value)
        {
            if (SecondToFirstDictionary.ContainsKey(value))
            {
                return true;
            }
            return false;
        }

        internal virtual TSecond GetSecondValue(TFirst value)
        {
            if (ExistsInFirst(value))
            {
                return FirstToSecondDictionary[value];
            }
            else
            {
                return default(TSecond);
            }
        }

        internal virtual TFirst GetFirstValue(TSecond value)
        {
            if (ExistsInSecond(value))
            {
                return SecondToFirstDictionary[value];
            }
            else
            {
                return default(TFirst);
            }
        }

        internal void AddValue(TFirst firstValue, TSecond secondValue)
        {
            FirstToSecondDictionary.Add(firstValue, secondValue);

            if (!SecondToFirstDictionary.ContainsKey(secondValue))
            {
                SecondToFirstDictionary.Add(secondValue, firstValue);
            }
        }
    }
}
