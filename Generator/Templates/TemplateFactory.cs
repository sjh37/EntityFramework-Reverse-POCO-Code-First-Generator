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
                case TemplateType.EfCore10:
                    return new TemplateEfCore8();

                case TemplateType.FileBasedEf6:
                    return new TemplateFileBased();

                case TemplateType.FileBasedCore8:
                case TemplateType.FileBasedCore9:
                case TemplateType.FileBasedCore10:
                    return new TemplateFileBased();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
