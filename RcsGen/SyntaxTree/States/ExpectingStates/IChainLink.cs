namespace RcsGen.SyntaxTree.States.ExpectingStates
{
    using System;

    internal interface ICanChain
    {
        ICanChainAndReject Expect(string value, Func<IState> successFunc);

        ICanChainAndReject Expect(string value, Action<ICanChain> successAction);

        ICanChainAndReject Await(string value, Func<IState> successFunc);

        ICanChainAndReject Await(string value, Action<ICanChain> successAction);
    }
    
    internal interface ICanChainAndReject : ICanChain
    {
        void Otherwize(Func<IState> rejectFunc);
    }
}