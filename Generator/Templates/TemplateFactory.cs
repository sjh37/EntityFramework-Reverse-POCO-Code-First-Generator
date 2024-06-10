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

                case TemplateType.EfCore6:
                    return new TemplateEfCore6();

                case TemplateType.EfCore7:
                    return new TemplateEfCore7();

                case TemplateType.EfCore8:
                    return new TemplateEfCore8();

                case TemplateType.FileBasedEf6:
                case TemplateType.FileBasedCore6:
                case TemplateType.FileBasedCore7:
                case TemplateType.FileBasedCore8:
                    return new TemplateFileBased();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
