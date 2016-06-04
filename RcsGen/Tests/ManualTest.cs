namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;

    [TestClass]
    public class ManualTest
    {
        string source = @"@using Models System.Collections.Generic
@visibility internal 
@inherits TemplateBase<List<EntityModel>>
@member{public string FileName { get; set; }}
@{FileName = Options.ContextName;}
namespace @Options.OutputNamespace
{
    using LinqToDB;

    public partial class @Options.ContextName
	{
    @foreach(var model in Model){@Resolve.With(model).Run<ContextTableLine>()}
	}	
}
";
        [TestMethod]
        public void A_ManualTest()
        {
            var result = new Generator().Generate(source, "SomeNamespace", "SomeName");
        }
    }
}
