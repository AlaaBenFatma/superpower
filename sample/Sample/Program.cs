﻿using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using System;

namespace Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string input = "(abc (234) 567 defgh) ";
            Console.WriteLine($"Input is: {input}");

            var tok = new SExpressionTokenizer();
            foreach (var token in tok.Tokenize(input))
            {
                Console.WriteLine(token);
            }

            var number = Parse.Token(SExpressionToken.Number)
                              .Apply(t => Numerics.IntegerInt32);

            var number1 = Parse.Token(SExpressionToken.Number)
                               .Apply(Numerics.IntegerInt32);

            var number2 = Parse.Token(SExpressionToken.Number)
                               .Select(t => int.Parse(t.Value));

            var atom = Parse.Token(SExpressionToken.Atom).Or(Parse.Token(SExpressionToken.LParen)).Select(t => t.Value);

            var numbers = number.AtLeastOnce().AtEnd();

            var alt = number.Then(n => atom.Select(a => $"({n}, {a})")).AtLeastOnce().AtEnd();

            var stream = tok.Tokenize(" 1 abc 23 456");

            var result = alt.TryParse(stream);

            if (result.HasValue)
            {
                foreach (var n in result.Value)
                {
                    Console.WriteLine(n);
                }
            }
            else
            {
                Console.WriteLine(result);
            }
        }
    }
}
