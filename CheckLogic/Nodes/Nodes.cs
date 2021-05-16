namespace CheckLogic {
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

    class RightImplication : BinaryOperator {
        public RightImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    class LeftImplication : BinaryOperator {
        public LeftImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
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
}