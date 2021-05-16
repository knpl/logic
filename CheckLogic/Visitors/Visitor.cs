namespace CheckLogic {
    abstract class Visitor {
        public abstract void visit(Expression expression);
        public abstract void visit(Proposition proposition);
        public abstract void visit(BinaryOperator binaryOperator);
        public abstract void visit(Conjunction conjunction);
        public abstract void visit(Disjunction disjunction);
        public abstract void visit(RightImplication implication);
        public abstract void visit(LeftImplication implication);
        public abstract void visit(BiImplication biImplication);
        public abstract void visit(Negation negation);
    }

    abstract class Visitable {
        public abstract void accept(Visitor visitor);
    }

}