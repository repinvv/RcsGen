namespace RcsGen.SyntaxTree.States.AtStates.ForStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class ForConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly string keyword;
        private readonly NodeStore nodes;
        private readonly BracketStateFactory factory;

        public ForConditionState(StateMachine stateMachine, 
            IState previous, 
            string keyword, 
            NodeStore nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.keyword = keyword;
            this.nodes = nodes;
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

            var childNodes = new NodeStore();
            stateMachine
                .Expect("{", previous)
                .SuccessState = new SingleLineChildNodesState(stateMachine, childNodes)
                                {
                                    ReturnAction = () =>
                                    {
                                        var hasEol = childNodes.Nodes.Any(x => x.NodeType == NodeType.Eol);
                                        nodes.Add(new ForNode(keyword, Accumulated, childNodes), hasEol);
                                        stateMachine.State = previous;
                                    }
                                };
        }

        public override void Finish() { }
    }
}
