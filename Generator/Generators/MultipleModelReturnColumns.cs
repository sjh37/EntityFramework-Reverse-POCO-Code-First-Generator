using System.Collections.Generic;

namespace Efrpg.Generators
{
    public class MultipleModelReturnColumns
    {
        public int Model { get; }
        public List<string> ReturnColumns { get; }

        public MultipleModelReturnColumns(int model, List<string> returnColumns)
        {
            Model = model;
            ReturnColumns = returnColumns;
        }
    }
}