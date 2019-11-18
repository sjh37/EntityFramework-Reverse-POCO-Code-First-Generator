using System.Collections.Generic;
using Efrpg.Generators;

namespace Efrpg.TemplateModels
{
    public class StoredProcReturnModel
    {
        public string ResultClassModifiers                                 { get; set; }
        public string WriteStoredProcReturnModelName                       { get; set; }
        public bool SingleModel                                            { get; set; }
        public List<string> SingleModelReturnColumns                       { get; set; }
        public List<MultipleModelReturnColumns> MultipleModelReturnColumns { get; set; }
    }
}