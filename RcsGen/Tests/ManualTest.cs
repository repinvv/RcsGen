namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;

    [TestClass]
    public class ManualTest
    {
        string source = @"@visibility internal
@inherits FileGenerator
@using StormGenerator.Models StormGenerator.Settings GeneratorHelpers
@constructor(GenOptions options, EntityModel model)
@member{public override string FileName => model.Name + "".main"";}

namespace @model.GetModelNamespace(options)
{
    @options.Visibility partial class @model.Name
    {
      @foreach(var field in model.Model.Fields)
      {
        @[ModelField(field, options)]
        @if(!field.Equals(model.Model.Fields.Last())){@newline}
      }
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
