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

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
