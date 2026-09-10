namespace Efrpg.Gui
{
    /// <summary>
    ///     One row in the object picker: a database object, whether it is currently going to be generated, and
    ///     whether the user may change that here.
    /// </summary>
    public sealed class ObjectChoice
    {
        public ObjectChoice(DatabaseObject databaseObject, bool? isSelected, bool canChange, string reason)
        {
            Object     = databaseObject;
            IsSelected = isSelected;
            CanChange  = canChange;
            Reason     = reason ?? string.Empty;
        }

        public DatabaseObject Object { get; }

        /// <summary>
        ///     True when the generator will produce code for it, false when it will not, and null when the .tt
        ///     holds a filter this dialog cannot evaluate, so it honestly does not know.
        /// </summary>
        public bool? IsSelected { get; }

        /// <summary>
        ///     False when a filter the user wrote decides this object, in which case <see cref="Reason"/> says
        ///     which one. The picker never overrides a hand-written filter; it shows it.
        /// </summary>
        public bool CanChange { get; }

        public string Reason { get; }

        public override string ToString()
        {
            return Object + (IsSelected == true ? " [x]" : IsSelected == false ? " [ ]" : " [?]");
        }
    }
}
