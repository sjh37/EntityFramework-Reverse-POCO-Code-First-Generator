namespace Efrpg.Gui
{
    /// <summary>
    ///     What a database object is, to the extent the GUI cares. Deliberately coarser than the generator's own
    ///     view: the picker groups by these four and nothing else.
    /// </summary>
    public enum DatabaseObjectKind
    {
        Table,
        View,
        StoredProcedure,
        Function
    }
}
