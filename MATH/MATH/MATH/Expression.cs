using System;
using System.Collections.Generic;
using System.Text;

namespace MATH
{
    class Expression
    {
        private static readonly string symbols = "()-+/*><||&&^%$~!=";
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