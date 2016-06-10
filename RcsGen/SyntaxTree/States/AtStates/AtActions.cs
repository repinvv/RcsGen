namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.ForStates;
    using RcsGen.SyntaxTree.States.AtStates.IfStates;
    using static RcsGen.SyntaxTree.States.BracketStates.BracketStateFactory;

    internal class AtActions
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly NodeStore nodes;

        public AtActions(StateMachine stateMachine, IState previous, NodeStore nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
        }

        public void CreateLiteral(string content)
        {
            nodes.Add(new ContentNode("@", NodeType.Literal));
            stateMachine.State = previous;
        }

        public void CreateCodeExpression(string content) 
            => nodes.Add(new ContentNode(content, NodeType.CodeExpression));

        public void GotoCodeExpression() => stateMachine
            .State = new ContentState(stateMachine, "}", CreateCodeExpression, previous, AllBrackets);

        public void CreateWriteExpression(string content)
            => nodes.Add(new ContentNode(content, NodeType.WriteExpression));

        public void GotoExplicitWriteExpression() => stateMachine
            .State = new ContentState(stateMachine, ")", CreateWriteExpression, previous, "<", "(");

        public void SkipAtAndReenterToken(string token)
        {
            stateMachine.State = previous;
            previous.ProcessToken(token);
        }

        public void CreateAtTokenWithEol()
        {
            nodes.Add(new ContentNode("@", NodeType.Literal));
            nodes.Add(new Node(NodeType.Eol));
            stateMachine.State = previous;
        }

        public void Previous() => stateMachine.State = previous;

        public void GotoComment() => stateMachine.State = new CommentState(Previous);

        public void GotoIf()
            => stateMachine
                .Expect("(", previous)
                .SuccessState = new IfConditionState(stateMachine, previous, nodes);

        public void GotoFor(string token)
            => stateMachine
                .Expect("(", previous)
                .SuccessState = new ForConditionState(stateMachine, previous, token, nodes);

        public void GotoImplicitWrite(string token)
        {
            var state = new ImplicitWriteState(nodes, stateMachine, previous);
            stateMachine.State = state;
            state.ProcessToken(token);
        }

        private void CreatePartialNode(string content) 
            => nodes.Add(new ContentNode(content, NodeType.Partial));

        public void GotoPartial() 
            => stateMachine.State = new ContentState(stateMachine, "]", CreatePartialNode, previous, AllBrackets);
    }
}
