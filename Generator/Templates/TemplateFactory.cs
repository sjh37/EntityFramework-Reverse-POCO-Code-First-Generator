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

                case TemplateType.EfCore8:
                case TemplateType.EfCore9:
                    return new TemplateEfCore8();

                case TemplateType.FileBasedEf6:
                case TemplateType.FileBasedCore8:
                case TemplateType.FileBasedCore9:
                    return new TemplateFileBased();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
