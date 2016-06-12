namespace RcsGen.SyntaxTree.States.AtStates.ForStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.Expect;
    using RcsGen.SyntaxTree.States.BracketStates;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class ForConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly string keyword;
        private readonly NodeStore nodes;
        private readonly BracketStateFactory factory;
        private NodeStore childNodes;

        public ForConditionState(StateMachine stateMachine,
            IState previous,
            string keyword,
            NodeStore nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.keyword = keyword;
            this.nodes = nodes;
            this.childNodes = new NodeStore();
            factory = new BracketStateFactory(stateMachine, this, BracketStateFactory.AllBrackets);
        }

        public override void ProcessToken(string token)
        {
            if (token != ")")
            {
                Accumulate(token);
                factory.TryBracket(token);
                return;
            }

            stateMachine
                .Expect("{", previous)
                .SuccessState
                = new SingleLineChildNodesState(stateMachine, childNodes, this)
                {
                    ReturnAction = CreateNode
                };
        }

        private void CreateNode()
        {
            var hasEol = childNodes.HasEol();
            nodes.Add(new ForNode(keyword, Accumulated, childNodes), hasEol);
            stateMachine.State = previous;
        }

        public override void Finish()
        {
            CreateNode();
            previous.Finish();
        }
    }
}
