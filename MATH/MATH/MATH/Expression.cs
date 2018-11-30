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
                        if (contents[j] is string && (string)contents[j] == ")") end = j;
                    }
                    processing.Add((Constant)SolveHelper(contents.GetRange(i, end)));
                    processing.Add(contents.GetRange(end, contents.Count - end));
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
            object last = null, current = null;
            Variable result = new Variable("ans", 0);
            List<object> temp = processing;
            for (int m = 0; m < 11; m++) // PEMDAS minus the P
            {
                processing = temp;
                temp = new List<object>();
                current = processing[0];
                processing.Add("end");
                foreach (object o in processing)
                {
                    if (o is string && (string)o == "end")
                    {
                        temp.Add(current);
                        break;
                    }

                    object pass = current;
                    if (current == o) continue;

                    //NOTE: an AValue obj casted as a string is always null

                    switch (m)
                    {
                        #region Unary
                        case 0: // Satisfy Unary Operators (Find them first)
                            if (current is string && last is string)
                            {
                                switch ((string)current) // use operator on o which should be AValue
                                {
                                    case "+":
                                        if (o is Variable) pass = +(Variable)o;
                                        else pass = +(Constant)o;
                                        break;
                                    case "-":
                                        if (o is Variable) pass = new Variable(((Variable)o).Name, -((Variable)o).Value);
                                        else pass = new Constant(-((Constant)o).Value);
                                        break;
                                    case "!":
                                        if (o is Variable) pass = !(Variable)o;
                                        else pass = !(Constant)o;
                                        break;
                                    case "~":
                                        if (o is Variable) pass = ~(Variable)o;
                                        else pass = ~(Constant)o;
                                        break;
                                    case "!!":
                                        if (o is Variable) pass = AValue.Factorial((Variable)o);
                                        else pass = AValue.Factorial((Constant)o);
                                        break;
                                    case "++":
                                        if (o is Variable)
                                        {
                                            AValue x = (Variable)o;
                                            pass = x++;
                                        }
                                        else
                                        {
                                            AValue x = (Constant)o;
                                            pass = x++;
                                        }
                                        break;
                                    case "--":
                                        if (o is Variable)
                                        {
                                            AValue x = (Variable)o;
                                            pass = x--;
                                        }
                                        else
                                        {
                                            AValue x = (Constant)o;
                                            pass = x--;
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion
                        #region Exponentiation
                        case 1: // Calls for Exponentiation
                            if (current is string && (string)current == "^^")
                            {
                                pass = AValue.Exponentiation((AValue)last, (AValue)o);
                                temp.RemoveAt(temp.Count - 1);
                            }
                            break;
                        #endregion
                        #region Multiplication, Division, Modulo
                        case 2: // Multiplication, Division, and Modulo
                            if (current is string)
                            {
                                switch ((string)current)
                                {
                                    case "*":
                                        pass = (AValue)last * (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "/":
                                        pass = (AValue)last / (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "%":
                                        pass = (AValue)last % (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                }
                            }
                            break;
                        #endregion
                        #region Addition and Subtraction
                        case 3: // Addition and Subtraction
                            if (current is string)
                            {
                                switch ((string)current)
                                {
                                    case "+":
                                        pass = (AValue)last + (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "-":
                                        pass = (AValue)last - (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                }
                            }
                            break;
                        #endregion
                        #region Inequality Comparisons
                        case 4: // Inequality Comparisons
                            if (current is string)
                            {
                                switch ((string)current)
                                {
                                    case ">=":
                                        pass = (AValue)last >= (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "<=":
                                        pass = (AValue)last <= (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case ">":
                                        pass = (AValue)last > (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "<":
                                        pass = (AValue)last < (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                }
                            }
                            break;
                        #endregion
                        #region Equality Comparisons
                        case 5: // Equality Comparisons
                            if (current is string)
                            {
                                switch ((string)current)
                                {
                                    case "==":
                                        pass = (AValue)last == (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                    case "!=":
                                        pass = (AValue)last != (AValue)o;
                                        temp.RemoveAt(temp.Count - 1);
                                        break;
                                }
                            }
                            break;
                        #endregion
                        #region Bitwise AND
                        case 6: // Bitwise AND
                            if (current is string)
                            {
                                if ((string)current == "&")
                                {
                                    pass = (AValue)last & (AValue)o;
                                    temp.RemoveAt(temp.Count - 1);
                                }
                            }
                            break;
                        #endregion
                        #region Bitwise XOR
                        case 7: // Bitwise XOR
                            if (current is string)
                            {
                                if ((string)current == "^")
                                {
                                    pass = (AValue)last ^ (AValue)o;
                                    temp.RemoveAt(temp.Count - 1);
                                }
                            }
                            break;
                        #endregion
                        #region Bitwise OR
                        case 8: // Bitwise OR
                            if (current is string)
                            {
                                if ((string)current == "|")
                                {
                                    pass = (AValue)last | (AValue)o;
                                    temp.RemoveAt(temp.Count - 1);
                                }
                            }
                            break;
                        #endregion
                        #region Boolean AND
                        case 9: // Boolean AND
                            if (current is string)
                            {
                                if ((string)current == "&&")
                                {
                                    pass = (AValue)last && (AValue)o;
                                    temp.RemoveAt(temp.Count - 1);
                                }
                            }
                            break;
                        #endregion
                        #region Boolean OR
                        case 10:// Boolean OR
                            if (current is string)
                            {
                                if ((string)current == "||")
                                {
                                    pass = (AValue)last && (AValue)o;
                                    temp.RemoveAt(temp.Count - 1);
                                }
                            }
                            break;
                        #endregion
                    }

                    last = current;
                    current = o;
                    temp.Add(pass);
                }
            }
            result.Value = ((AValue)temp[0]).Value;
            return result;
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
        private static readonly string symbols = "()-+/*><|&^%~!="; //careful of double-char operators
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