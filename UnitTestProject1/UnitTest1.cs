using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
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
        }
    }
}
