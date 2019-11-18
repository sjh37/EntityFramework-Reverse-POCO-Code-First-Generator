using System;

namespace Efrpg.Templates
{
    public static class TemplateFactory
    {
        public static Template Create()
        {
            switch (Settings.TemplateType)
            {
                case TemplateType.Ef6:
                    return new TemplateEf6();

                case TemplateType.EfCore2:
                case TemplateType.EfCore3:
                    return new TemplateEfCore();

                case TemplateType.FileBased:
                    return new TemplateFileBased();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
