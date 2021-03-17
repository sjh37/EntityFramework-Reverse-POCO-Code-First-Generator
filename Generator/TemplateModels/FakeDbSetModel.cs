namespace Efrpg.TemplateModels
{
    public class FakeDbSetModel
    {
        public string DbContextClassModifiers { get; set; }
        public bool DbContextClassIsPartial   { get; set; }
        public bool IsEfCore2                 { get; set; }
        public bool IsEfCore3                 { get; set; }
        public bool IsEfCore5                 { get; set; }
        public bool IsEfCore3Plus             { get; set; }
    }
}