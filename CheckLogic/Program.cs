using System;
using System.Text;

namespace CheckLogic
{
    abstract class Visitor {
        public abstract void visit(Expression expression);
        public abstract void visit(Proposition proposition);
        public abstract void visit(BinaryOperator binaryOperator);
        public abstract void visit(Conjunction conjunction);
        public abstract void visit(Disjunction disjunction);
        public abstract void visit(Implication implication);
        public abstract void visit(BiImplication biImplication);
        public abstract void visit(Negation negation);
    }

    class PrettyPrinter : Visitor
    {

        public StringBuilder Buffer;

        public PrettyPrinter() {
            Buffer = new StringBuilder();
        }

        public override void visit(Expression expression)
        {
            throw new NotImplementedException();
        }

        public override void visit(Proposition proposition)
        {
            Buffer.Append(proposition.Label);
        }

        public override void visit(BinaryOperator binaryOperator)
        {
            throw new NotImplementedException();
        }

        public override void visit(Conjunction conjunction)
        {
            Buffer.Append('(');
            conjunction.LeftHandSide.accept(this);
            Buffer.Append(" ^ ");
            conjunction.RightHandSide.accept(this);
            Buffer.Append(')');
        }

        public override void visit(Disjunction disjunction)
        {
            Buffer.Append('(');
            disjunction.LeftHandSide.accept(this);
            Buffer.Append(" v ");
            disjunction.RightHandSide.accept(this);
            Buffer.Append(')');
        }

        public override void visit(Implication implication)
        {
            Buffer.Append('(');
            implication.LeftHandSide.accept(this);
            Buffer.Append(" => ");
            implication.RightHandSide.accept(this);
            Buffer.Append(')');
        }

        public override void visit(BiImplication biImplication)
        {
            Buffer.Append('(');
            biImplication.LeftHandSide.accept(this);
            Buffer.Append(" <=> ");
            biImplication.RightHandSide.accept(this);
            Buffer.Append(')');
        }

        public override void visit(Negation negation)
        {
            Buffer.Append('~');
            negation.Operand.accept(this);
        }
    }

    abstract class Visitable {
        public abstract void accept(Visitor visitor);
    }

    abstract class Expression : Visitable {
        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class Proposition : Expression {

        public string Label { get; }

        public Proposition(string label) {
            Label = label;
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    abstract class BinaryOperator : Expression {
        public Expression LeftHandSide { get; }
        public Expression RightHandSide { get; }

        public BinaryOperator(Expression lhs, Expression rhs) {
            LeftHandSide = lhs;
            RightHandSide = rhs;
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class Disjunction : BinaryOperator {
        public Disjunction(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class Conjunction : BinaryOperator {
        public Conjunction(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class Implication : BinaryOperator {
        public Implication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class BiImplication : BinaryOperator {
        public BiImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class Negation : Expression {

        public Expression Operand { get; }
        public Negation(Expression operand) {
            Operand = operand;
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class ExpressionBuilder {

        private Expression? Expr { get; set; }

        public ExpressionBuilder() {
            Expr = null;
        }
    }

    static class ExpressionExtensions {
        public static string Stringify(this Expression expr) {
            PrettyPrinter visitor = new PrettyPrinter();
            expr.accept(visitor);
            return visitor.Buffer.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Propositions.
            var p = new Proposition("P");
            var q = new Proposition("Q");
            var r = new Proposition("R");

            // Law of the excluded middle.
            var excludedMiddle = new Disjunction(p, new Negation(p));
            Console.WriteLine($"Excluded middle: {excludedMiddle.Stringify()}");

            // Contradiction.
            var contradiction = new Conjunction(q, new Negation(q));
            Console.WriteLine($"Contradiction: {contradiction.Stringify()}");

            // Implication.
            var implication = new Implication(
                new Proposition("P"),
                new Proposition("Q")
            );
            Console.WriteLine($"Implication: {implication.Stringify()}");

            // Tautology.
            var tautology = new BiImplication(
                new Implication(p, q),
                new Disjunction(new Negation(p), q)
            );
            Console.WriteLine($"Tautology: {tautology.Stringify()}");
        }
    }
}
