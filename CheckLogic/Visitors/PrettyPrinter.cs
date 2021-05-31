using System;
using System.Text;


namespace CheckLogic {

    public enum Precedence {
        Lowest = 0,
        BiImplication = 1,
        Implication = 2,
        Disjunction = 3,
        Conjunction = 4,
        Negation = 5,
        Highest = int.MaxValue
    }

    public enum Associativity {
        Associative,
        LeftAssociative,
        RightAssociative
    }

    public class PrettyPrintVisitor : Visitor {
        private StringBuilder _buffer;
        private PrecedenceVisitor _precedence;

        public PrettyPrintVisitor() {
            _buffer = new StringBuilder();
            _precedence = new PrecedenceVisitor();
        }

        public override string ToString()
        {
            return _buffer.ToString();
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public override void visit(Expression expression)
        {
            throw new NotImplementedException();
        }

        public override void visit(Proposition proposition)
        {
            _buffer.Append(proposition.Label);
        }

        public override void visit(BinaryOperator binaryOperator)
        {
            throw new NotImplementedException();
        }

        public override void visit(Conjunction conjunction)
        {
            left(conjunction, Precedence.Conjunction);
            _buffer.Append(" ∧ ");
            right(conjunction, Precedence.Conjunction);
        }

        public override void visit(Disjunction disjunction)
        {
            left(disjunction, Precedence.Disjunction);
            _buffer.Append(" ∨ ");
            right(disjunction, Precedence.Disjunction);
        }

        public override void visit(RightImplication implication)
        {
            left(implication, Precedence.Implication, Associativity.RightAssociative);
            _buffer.Append(" → ");
            right(implication, Precedence.Implication, Associativity.RightAssociative);
        }

        public override void visit(LeftImplication implication)
        {
            left(implication, Precedence.Implication, Associativity.LeftAssociative);
            _buffer.Append(" ← ");
            right(implication, Precedence.Implication, Associativity.LeftAssociative);
        }

        public override void visit(BiImplication biImplication)
        {
            left(biImplication, Precedence.BiImplication);
            _buffer.Append(" ↔ ");
            right(biImplication, Precedence.BiImplication);
        }

        public override void visit(Negation negation)
        {
            _buffer.Append('¬');
            Precedence childPrecedence = getPrecedence(negation.Operand);
            if (childPrecedence < Precedence.Negation) {
                _buffer.Append('(');
                negation.Operand.accept(this);
                _buffer.Append(')');
            }
            else {
                negation.Operand.accept(this);
            }
        }

        public override void visit(ForAll forAll)
        {
            _buffer.Append($"∀{forAll.Variable}:");
            forAll.Operand.accept(this);
        }

        public override void visit(ThereExists thereExists)
        {
            _buffer.Append($"∃{thereExists.Variable}:");
            thereExists.Operand.accept(this);
        }

        public override void visit(Predicate predicate)
        {
            _buffer.Append($"{predicate.Name}({String.Join(',', predicate.Arguments)})");
        }

        private void left(
                BinaryOperator node,
                Precedence precedence,
                Associativity associativity = Associativity.Associative
            ) {
            Precedence childPrecedence = getPrecedence(node.LeftHandSide);

            // Parentheses required if:
            // - child precedence is lower than parent precedence.
            // - child precedence is equal to parent precedence and
            //   this operation is right-associative.
            bool parens = childPrecedence < precedence || (
                childPrecedence == precedence && (
                    node.LeftHandSide.GetType() != node.GetType() ||
                    associativity == Associativity.RightAssociative
                )
            );

            if (parens) {
                _buffer.Append('(');
                node.LeftHandSide.accept(this);
                _buffer.Append(')');
            }
            else {
                node.LeftHandSide.accept(this);
            }
        }

        private void right(
                BinaryOperator node,
                Precedence precedence,
                Associativity associativity = Associativity.Associative
            ) {
            Precedence childPrecedence = getPrecedence(node.RightHandSide);

            // Parentheses required if:
            // - child precedence is lower than parent precedence.
            // - child precedence is equal to parent precedence and
            //   this operation is left-associative.
            bool parens = childPrecedence < precedence || (
                childPrecedence == precedence && (
                    node.RightHandSide.GetType() != node.GetType() ||
                    associativity == Associativity.LeftAssociative
                )
            );

            if (parens) {
                _buffer.Append('(');
                node.RightHandSide.accept(this);
                _buffer.Append(')');
            }
            else {
                node.RightHandSide.accept(this);
            }
        }

        private Precedence getPrecedence(Visitable node) {
            node.accept(_precedence);
            return _precedence.Result;
        }

    }

    class PrecedenceVisitor : Visitor
    {
        public Precedence Result { get; set; }

        public override void visit(Proposition proposition)
        {
            Result = Precedence.Highest; 
        }

        public override void visit(Conjunction conjunction)
        {
            Result = Precedence.Conjunction;
        }

        public override void visit(Disjunction disjunction)
        {
            Result = Precedence.Disjunction;
        }

        public override void visit(RightImplication implication)
        {
            Result = Precedence.Implication;
        }

        public override void visit(LeftImplication implication)
        {
            Result = Precedence.Implication;
        }

        public override void visit(BiImplication biImplication)
        {
            Result = Precedence.BiImplication;
        }

        public override void visit(Negation negation)
        {
            Result = Precedence.Negation;
        }

        public override void visit(Expression expression)
        {
            throw new NotImplementedException();
        }

        public override void visit(BinaryOperator binaryOperator)
        {
            throw new NotImplementedException();
        }

        public override void visit(ForAll forAll)
        {
            Result = Precedence.Lowest;
        }

        public override void visit(ThereExists thereExists)
        {
            Result = Precedence.Lowest;
        }

        public override void visit(Predicate predicate)
        {
            Result = Precedence.Highest;
        }
    }
}