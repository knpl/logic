using System;

namespace CheckLogic
{

    static class ExpressionExtensions {
        public static string Stringify(this Expression expr) {
            PrettyPrintVisitor visitor = new PrettyPrintVisitor();
            expr.accept(visitor);
            return visitor.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Propositions.
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");
            var s = new Proposition("s");

            // Law of the excluded middle.
            Console.WriteLine(new Disjunction(p, new Negation(p)).Stringify());

            // Contradiction.
            Console.WriteLine(new Conjunction(q, new Negation(q)).Stringify());

            // Implication.
            Console.WriteLine(new RightImplication(p, q).Stringify());

            // Tautology.
            Console.WriteLine(
                new BiImplication(
                    new RightImplication(p, q),
                    new Disjunction(new Negation(p), q)
                ).Stringify()
            );

            Console.WriteLine(new Negation(new Disjunction(p, q)).Stringify());
            Console.WriteLine(new Negation(new Conjunction(p, q)).Stringify());
            Console.WriteLine(new Negation(new RightImplication(p, q)).Stringify());
            Console.WriteLine(
                new Conjunction(
                    new Disjunction(p, q),
                    new RightImplication(q, r)
                ).Stringify()
            );
            Console.WriteLine(
                new BiImplication(
                    new Disjunction(p, q),
                    new Conjunction(q, r)
                ).Stringify()
            );
            Console.WriteLine(new Conjunction(new Conjunction(new Conjunction(p, q), r), s).Stringify());
            Console.WriteLine(new Conjunction(p, new Conjunction(q, new Conjunction(r, s))).Stringify());
            Console.WriteLine(new RightImplication(new BiImplication(p, q), r).Stringify());
            Console.WriteLine(new BiImplication(p, new RightImplication(q, r)).Stringify());
            Console.WriteLine(new RightImplication(new RightImplication(new RightImplication(p, q), r), s).Stringify());
            Console.WriteLine(new RightImplication(p, new RightImplication(q, new RightImplication(r, s))).Stringify());
            Console.WriteLine(new RightImplication(new RightImplication(p, new RightImplication(q, r)), s).Stringify());
            Console.WriteLine(new RightImplication(p, new RightImplication(new RightImplication(q, r), s)).Stringify());
            Console.WriteLine(new Negation(new Negation(new Negation(p))).Stringify());
            
            Console.WriteLine(new RightImplication(p, new LeftImplication(q, r)).Stringify());
            Console.WriteLine(new LeftImplication(new RightImplication(p, q), r).Stringify());
            Console.WriteLine(new LeftImplication(p, new RightImplication(q, r)).Stringify());
            Console.WriteLine(new RightImplication(new LeftImplication(p, q), r).Stringify());

            Console.WriteLine(new LeftImplication(new LeftImplication(new LeftImplication(p, q), r), s).Stringify());
            Console.WriteLine(new LeftImplication(p, new LeftImplication(q, new LeftImplication(r, s))).Stringify());
            Console.WriteLine(new LeftImplication(new LeftImplication(p, new LeftImplication(q, r)), s).Stringify());
            Console.WriteLine(new LeftImplication(p, new LeftImplication(new LeftImplication(q, r), s)).Stringify());
            
        }
    }
}
