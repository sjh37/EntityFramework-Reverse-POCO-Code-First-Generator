namespace Efrpg.Gui
{
    /// <summary>
    ///     The five booleans on FilterSettings that switch whole categories of object on or off. The member names
    ///     are the names in the .tt, because they are written there.
    /// </summary>
    public enum FilterFlag
    {
        IncludeViews,
        IncludeSynonyms,
        IncludeStoredProcedures,
        IncludeTableValuedFunctions,
        IncludeScalarValuedFunctions
    }
}
