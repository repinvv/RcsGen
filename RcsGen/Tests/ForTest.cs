namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ForTest
    {
        string source = "@for(smth){smthMore}";
        string source2 = @"@for(smth){
    smthMore}
    }";
        string source3 = @"@for(smth)
        {
        smthMore}
        }";

        string source4 = "@for (smth) {smthMore}";
        string source5 = @"@for (smth) {
    smthMore}
    }";
        string source6 = @"@for (smth)
        {
        smthMore}
        }";


    }
}
