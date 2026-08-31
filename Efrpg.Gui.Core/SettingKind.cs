namespace Efrpg.Gui
{
    /// <summary>
    ///     What kind of editor a setting needs, as recorded in settings-metadata by BuildTT.
    /// </summary>
    /// <remarks>
    ///     The distinction that matters is editable versus not. A callback is a lambda the user wrote, a complex
    ///     setting is a list of objects built in code, and neither can be represented in a form without throwing
    ///     away what is there - so they are shown, labelled, and left alone.
    /// </remarks>
    public enum SettingKind
    {
        /// <summary>Anything this version does not recognise. Treated as read-only, which is the safe default.</summary>
        Unknown,

        Text,
        Boolean,
        Number,
        Character,
        Enumeration,

        /// <summary>A List&lt;string&gt; or string[] built in code.</summary>
        StringList,

        /// <summary>A lambda: Func, Action or a named method.</summary>
        Callback,

        /// <summary>A list of objects assembled in code, such as Enumerations or HiLoSequences.</summary>
        Complex
    }
}
