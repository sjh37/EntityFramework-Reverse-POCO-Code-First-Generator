using Efrpg.Gui;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    /// <summary>
    ///     The GUI's tool gate and the T4 template's XML reader must reject exactly the same tools.
    /// </summary>
    /// <remarks>
    ///     They cannot share the constant. EfrpgResultXmlReader has to stay plain source under Generator/ because
    ///     BuildTT concatenates it into the .ttinclude, which pins it to net48; Efrpg.Gui.Core is netstandard2.0 so
    ///     that the net48 VSIX and a modern test project can both consume it, and netstandard cannot reference net48.
    ///
    ///     So the two hold a copy each, and this is what stops them drifting. Drift would not be a compiler error: the
    ///     gate would cheerfully pass a tool the reader then refuses, and the user would be told everything is fine
    ///     immediately before generation failed.
    /// </remarks>
    [TestFixture]
    [Category(Constants.CI)]
    public class ToolGateSchemaFloorTests
    {
        [Test]
        public void ToolGate_RequiresTheSameSchemaVersionAsTheReader()
        {
            Assert.That(EfrpgToolGate.RequiredSchemaVersion, Is.EqualTo(EfrpgResultXmlReader.RequiredSchemaVersion),
                "EfrpgToolGate.RequiredSchemaVersion and EfrpgResultXmlReader.RequiredSchemaVersion have diverged. " +
                "The gate would then approve a tool the template refuses, or refuse one it would have accepted. " +
                "Whichever was bumped, bump the other.");
        }
    }
}
