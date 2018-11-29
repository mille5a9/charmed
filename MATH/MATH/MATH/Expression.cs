using System;
using System.Collections.Generic;
using System.Text;

namespace MATH
{
    class Expression
    {
        public Expression(string expression)
        {
            _expression = expression;
        }
        public Expression Solve()
        {
            _expression.Replace(" ", "");

            for (int i = 0; i < _expression.Length; i++)
            {
                if (numbers.Contains(_expression[i]))
                {
                    for (int j = i; j < _expression.Length; j++)
                    {
                        if (numbers.Contains(_expression[j]) == false)
                        {
                            Int32.TryParse(_expression.Substring(i, j - i - 1), out int n);
                            _values.Add(new Constant(n) { Start = i, End = j - i - 1 });
                        }
                    }
                }
                if (letters.Contains(_expression[i]))
                {
                    for (int j = i; j < _expression.Length; j++)
                    {
                        if (letters.Contains(_expression[j]) == false)
                        {
                            Int32.TryParse(_expression.Substring(i, j - i - 1), out int n);
                            _values.Add(new Variable(_expression.Substring(i, j - i - 1)) { Start = i, End = j - i - 1 });
                        }
                    }
                }
            }

            //check for open paren
            for (int i = 0; i < _expression.Length; i++)
            {
                if (_expression[i] == '(')
                {
                    for (int j = i; j < _expression.Length; j++)
                    {
                        //check for close paren and call recursively
                        if (_expression[j] == ')')
                        {
                            Expression x = new Expression(_expression.Substring(i, j - i));
                            x = x.Solve();
                            _expression.Replace(_expression.Substring(i, j - i), x._expression);
                        }
                    }
                }
            }

            //check for exponents
        }
        private string _expression;
        private List<AValue> _values;
        private static List<Variable> _assignments;

        private static readonly string symbols = "()-+/*><|&^%~!";
        private static bool IsSymbol(char x) { return symbols.Contains(x); }
        private static readonly string numbers = "1234.567890";
        private static bool IsNumber(char x) { return numbers.Contains(x); }
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyz";
        private static bool IsLetter(char x) { return letters.Contains(x); }
    }
}

/*
 Parenthesis Processing Procedure:
    -Read input left to right for parenthesis
    -If open paren found, replace with variable that is equal
        to the solution of the smaller problem within
        (possible ParenProc function)
    -Continue processing
     */