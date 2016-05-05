namespace RcsGen
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;

    [TestClass]
    public class UnitTest1
    {
        string source = @"@TypeVisibility Internal
@Imports: Models
@Inherits TemplateBase<List<EntityModel>>
@{FileName = Options.ContextName;}
namespace @Options.OutputNamespace
{
    using LinqToDB;

    public partial class @Options.ContextName
    {
        @foreach(var model in Model)
	    {
		    @Resolve.With(model).Run<ContextTableLine>()
        }
	}
}";
        [TestMethod]
        public void TestMethod1()
        {
            var node = Parser.Parse(source);
        }
    }
}
