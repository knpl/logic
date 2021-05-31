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
        public void AcceptPrettyPrinter_NotPrecedence_ExpectParentheses()
        {
            // Arrange
            var p = new Proposition("p");
            var q = new Proposition("q");

            ValueTuple<Expression, string>[] cases = {
                (new Disjunction(p, q), "¬(p ∨ q)"),
                (new Conjunction(p, q), "¬(p ∧ q)"),
                (new RightImplication(p, q), "¬(p → q)"),
                (new LeftImplication(p, q), "¬(p ← q)"),
                (new BiImplication(p, q), "¬(p ↔ q)")
            };

            var visitor = new PrettyPrintVisitor();

            foreach (var (expression, result) in cases) {
                // Act
                new Negation(expression).accept(visitor);

                // Assert
                Assert.That(visitor.ToString(), Is.EqualTo(result));
                visitor.Clear();
            }
        }
    }
}