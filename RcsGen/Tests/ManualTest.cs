namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;

    [TestClass]
    public class ManualTest
    {
        string source = @"@using Models
@visibility Internal 
@inherits TemplateBase<List<EntityModel>>
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
        public void A_ManualTest()
        {
            var result = new Generator().Generate(source, "SomeNamespace", "SomeName");
        }
    }
}
