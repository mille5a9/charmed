using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MATH
{
    class Expression
    {
        public Expression(string expression)
        {
            _expression = expression;
        }
        public Variable Solve()
        {

            #region String Dressing
            //_expression.Replace(" ", null);
            _expression = RemoveWhitespace(_expression);
            _expression.Replace(")(", ")*(");
            bool needsnewline = false;
            for (int i = 0; i < _expression.Length; i++)
            {
                while (i < _expression.Length && numbers.Contains(_expression.ElementAt(i)))
                {
                    i++;
                    needsnewline = true;
                }
                if (needsnewline) _expression = _expression.Insert(i, "\n");
                needsnewline = false;
                while (i < _expression.Length && ( _expression.ElementAt(i) == '(' || _expression.ElementAt(i) == ')'))
                {
                    i++;
                    needsnewline = true;
                }
                if (needsnewline) _expression = _expression.Insert(i, "\n");
                needsnewline = false;
                while (i < _expression.Length && letters.Contains(_expression[i]))
                {
                    i++;
                    needsnewline = true;
                }
                if (needsnewline) _expression = _expression.Insert(i, "\n");
                needsnewline = false;
                while (i < _expression.Length && symbols.Contains(_expression[i]))
                {
                    i++;
                    needsnewline = true;
                }
                if (needsnewline) _expression = _expression.Insert(i, "\n");
                needsnewline = false;
            }
            #endregion

            //string is now "abc\n*\n123.4\n/\nvar\n+\n3\n"
            //now convert line contents into useable objects in a list of objects

            #region List Population
            List<object> contents = new List<object>();
            StringReader observer = new StringReader(_expression);
            while (observer.Peek().ToString() != null)
            {
                string current = observer.ReadLine();
                if (current == null) break;
                if (numbers.Contains(current[0]))
                {
                    Double.TryParse(current, out double n);
                    contents.Add(new Constant(n));
                }
                else if (letters.Contains(current[0]))
                {
                    //check for identical named variable in assignments list,
                    //else throw a big fat error
                    try
                    {
                        bool exists = false;
                        foreach (Variable x in _assignments) if (x.Name == current)
                            {
                                exists = true;
                                contents.Add(x);
                            }
                        if (!exists) throw new ArgumentException();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Encountered unassigned variable...");
                    }
                }
                else
                {
                    contents.Add(current); //if contents object is string, its an operator
                }
            }
            #endregion

            //now arguments and operators should alternate, unless there is
            //a unary operator or implied multiplication like with adjacent parenthesis
            //now call helper for recursive fun with parenthesis
            return (Variable)SolveHelper(contents);
        }

        public AValue SolveHelper(List<object> contents)
        {
            int end = 0;
            List<object> processing = new List<object>();

            #region Parenthesis Solving
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] is string && (string)contents[i] == "(")
                {
                    for (int j = 0; j < contents.Count; j++)
                    {
                        if (contents[j] is string && (string)contents[j] == ")") end = j - i - 1;
                    }
                    processing.Add((Variable)SolveHelper(contents.GetRange(i + 1, end)));
                    i += (end + 1);
                }
                else
                {
                    processing.Add(contents[i]);
                }
            }
            #endregion
            //replaces an "(" in contents with a constant representing the result of the parens

            //now there is theoretically guaranteed to be no parenthesis unchecked,
            //time to cover all of the cases of how the operators could be called against
            //the constants and variables. Also all variable have valid values, as guaranteed
            //by the try/catch in the original Solve()
            Variable result = new Variable("ans", 0);
            List<object> temp = UseOperators(processing, 0);
            result.Value = ((AValue)temp[0]).Value;
            return result;
        }

        public static List<object> UseOperators(List<object> temp, int m)
        {
            List<object> processing = temp;
            if (m > 10 || temp.Count == 1) return processing;
            temp = new List<object>();
            processing.Add("end");
            bool binaryoperated = false;
            for (int i = 0; i < processing.Count; i++)
            {
                if (processing[i] is string && (string)processing[i] == "end") break;
                if (processing[i + 1] is string && (string)processing[i + 1] == "end")
                {
                    if (!binaryoperated) temp.Add(processing[i]);
                    break;
                }
                if (processing[i] == processing[i + 1]) continue;

                switch (m)
                {
                    #region Unary
                    case 0: // Satisfy Unary Operators (Find them first)
                        if (processing[i] is string && (i == 0 || processing[i - 1] is string))
                        {
                            switch ((string)processing[i]) // use operator on o which should be AValue
                            {
                                case "+":
                                    if (processing[i + 1] is Variable) processing[i] = +(Variable)processing[i + 1];
                                    else processing[i] = +(Constant)processing[i + 1];
                                    break;
                                case "-":
                                    if (processing[i + 1] is Variable) processing[i] = new Variable(((Variable)processing[i + 1]).Name, -((Variable)processing[i + 1]).Value);
                                    else processing[i] = new Constant(-((Constant)processing[i + 1]).Value);
                                    break;
                                case "!":
                                    if (processing[i + 1] is Variable) processing[i] = !(Variable)processing[i + 1];
                                    else processing[i] = !(Constant)processing[i + 1];
                                    break;
                                case "~":
                                    if (processing[i + 1] is Variable) processing[i] = ~(Variable)processing[i + 1];
                                    else processing[i] = ~(Constant)processing[i + 1];
                                    break;
                                case "!!":
                                    if (processing[i + 1] is Variable) processing[i] = AValue.Factorial((Variable)processing[i + 1]);
                                    else processing[i] = AValue.Factorial((Constant)processing[i + 1]);
                                    break;
                                case "++":
                                    if (processing[i + 1] is Variable)
                                    {
                                        AValue x = (Variable)processing[i + 1];
                                        processing[i] = x++;
                                    }
                                    else
                                    {
                                        AValue x = (Constant)processing[i + 1];
                                        processing[i] = x++;
                                    }
                                    break;
                                case "--":
                                    if (processing[i + 1] is Variable)
                                    {
                                        AValue x = (Variable)processing[i + 1];
                                        processing[i] = x--;
                                    }
                                    else
                                    {
                                        AValue x = (Constant)processing[i + 1];
                                        processing[i] = x--;
                                    }
                                    break;
                            }
                        }
                        break;
                    #endregion
                    #region Exponentiation
                    case 1: // Calls for Exponentiation
                        if (processing[i] is string && (string)processing[i] == "^^")
                        {
                            processing[i] = AValue.Exponentiation((AValue)processing[i - 1], (AValue)processing[i + 1]);
                            processing.RemoveAt(i - 1);
                            processing.RemoveAt(i);
                            i--;
                            temp.RemoveAt(temp.Count - 1);
                            binaryoperated = true;
                        }
                        break;
                    #endregion
                    #region Multiplication, Division, Modulo
                    case 2: // Multiplication, Division, and Modulo
                        if (processing[i] is string)
                        {
                            switch ((string)processing[i])
                            {
                                case "*":
                                    processing[i] = (AValue)processing[i - 1] * (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "/":
                                    processing[i] = (AValue)processing[i - 1] / (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "%":
                                    processing[i] = (AValue)processing[i - 1] % (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                            }
                        }
                        break;
                    #endregion
                    #region Addition and Subtraction
                    case 3: // Addition and Subtraction
                        if (processing[i] is string)
                        {
                            switch ((string)processing[i])
                            {
                                case "+":
                                    processing[i] = (AValue)processing[i - 1] + (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "-":
                                    processing[i] = (AValue)processing[i - 1] - (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                            }
                        }
                        break;
                    #endregion
                    #region Inequality Comparisons
                    case 4: // Inequality Comparisons
                        if (processing[i] is string)
                        {
                            switch ((string)processing[i])
                            {
                                case ">=":
                                    processing[i] = (AValue)processing[i - 1] >= (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "<=":
                                    processing[i] = (AValue)processing[i - 1] <= (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case ">":
                                    processing[i] = (AValue)processing[i - 1] > (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "<":
                                    processing[i] = (AValue)processing[i - 1] < (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                            }
                        }
                        break;
                    #endregion
                    #region Equality Comparisons
                    case 5: // Equality Comparisons
                        if (processing[i] is string)
                        {
                            switch ((string)processing[i])
                            {
                                case "==":
                                    processing[i] = (AValue)processing[i - 1] == (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                                case "!=":
                                    processing[i] = (AValue)processing[i - 1] != (AValue)processing[i + 1];
                                    processing.RemoveAt(i - 1);
                                    processing.RemoveAt(i);
                                    i--;
                                    temp.RemoveAt(temp.Count - 1);
                                    binaryoperated = true;
                                    break;
                            }
                        }
                        break;
                    #endregion
                    #region Bitwise AND
                    case 6: // Bitwise AND
                        if (processing[i] is string)
                        {
                            if ((string)processing[i] == "&")
                            {
                                processing[i] = (AValue)processing[i - 1] & (AValue)processing[i + 1];
                                processing.RemoveAt(i - 1);
                                processing.RemoveAt(i);
                                i--;
                                temp.RemoveAt(temp.Count - 1);
                                binaryoperated = true;
                            }
                        }
                        break;
                    #endregion
                    #region Bitwise XOR
                    case 7: // Bitwise XOR
                        if (processing[i] is string)
                        {
                            if ((string)processing[i] == "^")
                            {
                                processing[i] = (AValue)processing[i - 1] ^ (AValue)processing[i + 1];
                                processing.RemoveAt(i - 1);
                                processing.RemoveAt(i);
                                i--;
                                temp.RemoveAt(temp.Count - 1);
                                binaryoperated = true;
                            }
                        }
                        break;
                    #endregion
                    #region Bitwise OR
                    case 8: // Bitwise OR
                        if (processing[i] is string)
                        {
                            if ((string)processing[i] == "|")
                            {
                                processing[i] = (AValue)processing[i - 1] | (AValue)processing[i + 1];
                                processing.RemoveAt(i - 1);
                                processing.RemoveAt(i);
                                i--;
                                temp.RemoveAt(temp.Count - 1);
                                binaryoperated = true;
                            }
                        }
                        break;
                    #endregion
                    #region Boolean AND
                    case 9: // Boolean AND
                        if (processing[i] is string)
                        {
                            if ((string)processing[i] == "&&")
                            {
                                processing[i] = (AValue)processing[i - 1] && (AValue)processing[i + 1];
                                processing.RemoveAt(i - 1);
                                processing.RemoveAt(i);
                                i--;
                                temp.RemoveAt(temp.Count - 1);
                                binaryoperated = true;
                            }
                        }
                        break;
                    #endregion
                    #region Boolean OR
                    case 10:// Boolean OR
                        if (processing[i] is string)
                        {
                            if ((string)processing[i] == "||")
                            {
                                processing[i] = (AValue)processing[i - 1] && (AValue)processing[i + 1];
                                processing.RemoveAt(i - 1);
                                processing.RemoveAt(i);
                                i--;
                                temp.RemoveAt(temp.Count - 1);
                                binaryoperated = true;
                            }
                        }
                        break;
                        #endregion
                }
                temp.Add(processing[i]);
            }
            return UseOperators(temp, ++m);
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        private string _expression;
        private static List<Variable> _assignments;

        /*literal list of ops:
            Binary: + - * / % ^ & | ^^(Exponentiation())
            Unary: + - ! ~ !!(Factorial()) ++ --
            Relational: == != > < <= >=
              */
        private static readonly string symbols = "-+/*><|&^%~!="; //careful of double-char operators
        private static readonly string numbers = "1234.567890";
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string PEMDAS = "All Unary, ^^, */%, +-, <>, ==!=, &, ^, |, &&, ||";
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