namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CascadedForTest
    {
        string source = "@for(smth1){@foreach(smth2){@if(smth3){inside}}}";
    }
}
