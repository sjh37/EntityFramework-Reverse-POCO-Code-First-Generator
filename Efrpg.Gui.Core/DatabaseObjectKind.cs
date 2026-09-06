namespace Efrpg.Gui
{
    /// <summary>
    ///     What a database object is, to the extent the GUI cares. Deliberately coarser than the generator's own
    ///     view: the picker groups by these five and nothing else.
    /// </summary>
    /// <remarks>
    ///     The two function kinds are separate because the generator switches them on separately -
    ///     FilterSettings.IncludeTableValuedFunctions and IncludeScalarValuedFunctions - and both default to off. A
    ///     single Function kind could not say which flag to write.
    /// </remarks>
    public enum DatabaseObjectKind
    {
        Table,
        View,
        StoredProcedure,
        TableValuedFunction,
        ScalarValuedFunction
    }
}
