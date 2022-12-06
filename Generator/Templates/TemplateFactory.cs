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
                    return new TemplateEfCore2();

                case TemplateType.EfCore3:
                    return new TemplateEfCore3();
                
                case TemplateType.EfCore5:
                    return new TemplateEfCore5();

                case TemplateType.EfCore6:
                    return new TemplateEfCore6();

                case TemplateType.EfCore7:
                    return new TemplateEfCore7();

                case TemplateType.FileBasedCore2:
                case TemplateType.FileBasedCore3:
                case TemplateType.FileBasedCore5:
                case TemplateType.FileBasedCore6:
                case TemplateType.FileBasedCore7:
                    return new TemplateFileBased();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
