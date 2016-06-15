namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	using System;
@newline
    @options.Visibility partial class @model.Model.Name
    {	
	  @{var fields = model.Model.Fields.Where(x => x.IsEnabled).ToList();}
      @foreach (var field in fields){
        @[ModelField(field, options)]
		@if(!field.Equals(fields.Last())){@newline}
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
