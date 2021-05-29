namespace CheckLogic {
    public abstract class Expression : Visitable {
        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class Proposition : Expression {

        public string Label { get; }

        public Proposition(string label) {
            Label = label;
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public abstract class BinaryOperator : Expression {
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

    public class Disjunction : BinaryOperator {
        public Disjunction(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class Conjunction : BinaryOperator {
        public Conjunction(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class RightImplication : BinaryOperator {
        public RightImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class LeftImplication : BinaryOperator {
        public LeftImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class BiImplication : BinaryOperator {
        public BiImplication(Expression lhs, Expression rhs) : base(lhs, rhs) {
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class Negation : Expression {

        public Expression Operand { get; }
        public Negation(Expression operand) {
            Operand = operand;
        }

        public override void accept(Visitor visitor) {
            visitor.visit(this);
        }
    }

    public class ForAll : Expression {
        public string Variable { get; }
        public Expression Operand { get; }

        public ForAll(string variable, Expression operand) {
            Variable = variable;
            Operand = operand;
        }

        public override void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class ThereExists : Expression {
        public string Variable { get; }
        public Expression Operand { get; }

        public ThereExists(string variable, Expression operand) {
            Variable = variable;
            Operand = operand;
        }

        public override void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class Predicate : Expression {
        public string Name { get; }
        public string[] Arguments { get; }

        public Predicate(string name, params string[] arguments) {
            Name = name;
            Arguments = arguments;
        }

        public override void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
    }
}