using System;
using System.Collections;
using NUnit.Framework;
using CheckLogic;

namespace CheckLogic.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AcceptPrettyPrinter_NegationPrecedence()
        {
            // Arrange
            var p = new Proposition("p");
            var q = new Proposition("q");

            ValueTuple<Expression, string>[] cases = {
                (new Negation(new Negation(p)), "¬¬p"),
                (new Negation(new Conjunction(p, q)), "¬(p ∧ q)"),
                (new Negation(new Disjunction(p, q)), "¬(p ∨ q)"),
                (new Negation(new RightImplication(p, q)), "¬(p → q)"),
                (new Negation(new LeftImplication(p, q)), "¬(p ← q)"),
                (new Negation(new BiImplication(p, q)), "¬(p ↔ q)")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }

        [Test]
        public void AcceptPrettyPrinter_ConjunctionPrecedence()
        {
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");

            ValueTuple<Expression, string>[] cases = {
                (new Conjunction(p, new Negation(r)), "p ∧ ¬r"),
                (new Conjunction(p, new Conjunction(q, r)), "p ∧ q ∧ r"),
                (new Conjunction(p, new Disjunction(q, r)), "p ∧ (q ∨ r)"),
                (new Conjunction(p, new RightImplication(q, r)), "p ∧ (q → r)"),
                (new Conjunction(p, new LeftImplication(q, r)), "p ∧ (q ← r)"),
                (new Conjunction(p, new BiImplication(q, r)), "p ∧ (q ↔ r)"),
                (new Conjunction(new Negation(p), r), "¬p ∧ r"),
                (new Conjunction(new Conjunction(p, q), r), "p ∧ q ∧ r"),
                (new Conjunction(new Disjunction(p, q), r), "(p ∨ q) ∧ r"),
                (new Conjunction(new RightImplication(p, q), r), "(p → q) ∧ r"),
                (new Conjunction(new LeftImplication(p, q), r), "(p ← q) ∧ r"),
                (new Conjunction(new BiImplication(p, q), r), "(p ↔ q) ∧ r")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }

        [Test]
        public void AcceptPrettyPrinter_DisjunctionPrecedence()
        {
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");

            ValueTuple<Expression, string>[] cases = {
                (new Disjunction(p, new Negation(r)), "p ∨ ¬r"),
                (new Disjunction(p, new Conjunction(q, r)), "p ∨ q ∧ r"),
                (new Disjunction(p, new Disjunction(q, r)), "p ∨ q ∨ r"),
                (new Disjunction(p, new RightImplication(q, r)), "p ∨ (q → r)"),
                (new Disjunction(p, new LeftImplication(q, r)), "p ∨ (q ← r)"),
                (new Disjunction(p, new BiImplication(q, r)), "p ∨ (q ↔ r)"),
                (new Disjunction(new Negation(p), r), "¬p ∨ r"),
                (new Disjunction(new Conjunction(p, q), r), "p ∧ q ∨ r"),
                (new Disjunction(new Disjunction(p, q), r), "p ∨ q ∨ r"),
                (new Disjunction(new RightImplication(p, q), r), "(p → q) ∨ r"),
                (new Disjunction(new LeftImplication(p, q), r), "(p ← q) ∨ r"),
                (new Disjunction(new BiImplication(p, q), r), "(p ↔ q) ∨ r")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }

        [Test]
        public void AcceptPrettyPrinter_RightImplicationPrecedence()
        {
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");

            ValueTuple<Expression, string>[] cases = {
                (new RightImplication(p, new Negation(r)), "p → ¬r"),
                (new RightImplication(p, new Conjunction(q, r)), "p → q ∧ r"),
                (new RightImplication(p, new Disjunction(q, r)), "p → q ∨ r"),
                (new RightImplication(p, new RightImplication(q, r)), "p → q → r"),
                (new RightImplication(p, new LeftImplication(q, r)), "p → (q ← r)"),
                (new RightImplication(p, new BiImplication(q, r)), "p → (q ↔ r)"),
                (new RightImplication(new Negation(p), r), "¬p → r"),
                (new RightImplication(new Conjunction(p, q), r), "p ∧ q → r"),
                (new RightImplication(new Disjunction(p, q), r), "p ∨ q → r"),
                (new RightImplication(new RightImplication(p, q), r), "(p → q) → r"),
                (new RightImplication(new LeftImplication(p, q), r), "(p ← q) → r"),
                (new RightImplication(new BiImplication(p, q), r), "(p ↔ q) → r")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }

        [Test]
        public void AcceptPrettyPrinter_LeftImplicationPrecedence()
        {
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");

            ValueTuple<Expression, string>[] cases = {
                (new LeftImplication(p, new Negation(r)), "p ← ¬r"),
                (new LeftImplication(p, new Conjunction(q, r)), "p ← q ∧ r"),
                (new LeftImplication(p, new Disjunction(q, r)), "p ← q ∨ r"),
                (new LeftImplication(p, new RightImplication(q, r)), "p ← (q → r)"),
                (new LeftImplication(p, new LeftImplication(q, r)), "p ← (q ← r)"),
                (new LeftImplication(p, new BiImplication(q, r)), "p ← (q ↔ r)"),
                (new LeftImplication(new Negation(p), r), "¬p ← r"),
                (new LeftImplication(new Conjunction(p, q), r), "p ∧ q ← r"),
                (new LeftImplication(new Disjunction(p, q), r), "p ∨ q ← r"),
                (new LeftImplication(new RightImplication(p, q), r), "(p → q) ← r"),
                (new LeftImplication(new LeftImplication(p, q), r), "p ← q ← r"),
                (new LeftImplication(new BiImplication(p, q), r), "(p ↔ q) ← r")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }

        [Test]
        public void AcceptPrettyPrinter_BiImplicationPrecedence()
        {
            var p = new Proposition("p");
            var q = new Proposition("q");
            var r = new Proposition("r");

            ValueTuple<Expression, string>[] cases = {
                (new BiImplication(p, new Negation(r)), "p ↔ ¬r"),
                (new BiImplication(p, new Conjunction(q, r)), "p ↔ q ∧ r"),
                (new BiImplication(p, new Disjunction(q, r)), "p ↔ q ∨ r"),
                (new BiImplication(p, new RightImplication(q, r)), "p ↔ q → r"),
                (new BiImplication(p, new LeftImplication(q, r)), "p ↔ q ← r"),
                (new BiImplication(p, new BiImplication(q, r)), "p ↔ q ↔ r"),
                (new BiImplication(new Negation(p), r), "¬p ↔ r"),
                (new BiImplication(new Conjunction(p, q), r), "p ∧ q ↔ r"),
                (new BiImplication(new Disjunction(p, q), r), "p ∨ q ↔ r"),
                (new BiImplication(new RightImplication(p, q), r), "p → q ↔ r"),
                (new BiImplication(new LeftImplication(p, q), r), "p ← q ↔ r"),
                (new BiImplication(new BiImplication(p, q), r), "p ↔ q ↔ r")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                expression.accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }
    }
}