using System.Collections.Generic;

namespace Efrpg.TemplateModels
{
    public class OwnedEntityClassModel
    {
        public string ClassModifier { get; set; }
        public string ClassName { get; set; }
        public List<OwnedEntityPropertyItem> Properties { get; set; }
    }

    public class OwnedEntityPropertyItem
    {
        public string WrappedType { get; set; }
        public string PropertyName { get; set; }
        public string PropertyInitialiser { get; set; }
    }
}
