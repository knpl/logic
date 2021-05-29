namespace CheckLogic {
    public abstract class Visitor {
        public abstract void visit(Expression expression);
        public abstract void visit(Proposition proposition);
        public abstract void visit(BinaryOperator binaryOperator);
        public abstract void visit(Conjunction conjunction);
        public abstract void visit(Disjunction disjunction);
        public abstract void visit(RightImplication implication);
        public abstract void visit(LeftImplication implication);
        public abstract void visit(BiImplication biImplication);
        public abstract void visit(Negation negation);
        public abstract void visit(ForAll forAll);
        public abstract void visit(ThereExists thereExists);
        public abstract void visit(Predicate predicate);

    }

    public abstract class Visitable {
        public abstract void accept(Visitor visitor);
    }

}