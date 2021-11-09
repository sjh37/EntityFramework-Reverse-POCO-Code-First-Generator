using System.Text;
using Efrpg.TemplateModels;
using Efrpg.Templates;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    public class MustacheUtfTest
    {
        [Test]
        [TestCase("hello")]
        [TestCase("用户@例子.广告")]
        [TestCase("अजय@डाटा.भारत")]
        [TestCase("квіточка@пошта.укр")]
        [TestCase("θσερ@εχαμπλε.ψομ")]
        [TestCase("Dörte@.example.com")]
        [TestCase("аджай@экзампл.рус")]
        [TestCase("अज य@ डा टाभार त")]
        [TestCase("用户 @例 子广 告")]
        [TestCase("\"Joe.\\Blow\"@example.com")]
        [TestCase("\"Fred Bloggs\"@example.com")]
        [TestCase("\"Abc@def\"@example.com")]
        [TestCase("user+mailbox/department=shipping@example.com")]
        public void Test(string name)
        {
            const string template = @"{{Name}}";
            var data = new PocoModel { Name = name };
            var result = Template.Transform(template, data);
            Assert.AreEqual(name, result);
        }
    }
}