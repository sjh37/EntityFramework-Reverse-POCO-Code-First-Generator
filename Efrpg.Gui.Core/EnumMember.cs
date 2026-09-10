namespace Efrpg.Gui
{
    /// <summary>
    ///     One member of an enum setting, as BuildTT found it by reflecting over the generator's own enums.
    /// </summary>
    public sealed class EnumMember
    {
        public EnumMember(string name, int value)
        {
            Name  = name;
            Value = value;
        }

        public string Name { get; }

        /// <summary>The declared value. Needed for flags settings, where members are combined with |.</summary>
        public int Value { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
