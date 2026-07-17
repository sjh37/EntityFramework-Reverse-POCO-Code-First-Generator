namespace Efrpg.TemplateModels
{
    public class FakeDbSetModel
    {
        public string DbContextClassModifiers { get; set; }
        public bool DbContextClassIsPartial { get; set; }

        /// <summary>
        ///     The EF Core provider assembly name as a ready-to-emit C# literal, so it is either a quoted string such as
        ///     "Microsoft.EntityFrameworkCore.SqlServer" (quotes included) or the bare word null. Mustache cannot turn a
        ///     null model value into the null keyword, so the quoting is done here rather than in the template.
        ///     See <see cref="Settings.DatabaseProviderAssemblyName" />.
        /// </summary>
        public string DatabaseProviderNameLiteral { get; set; }
    }
}
