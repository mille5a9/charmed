using System;
using System.Collections.Generic;
using System.Text;

namespace MATH
{
    public class AValue
    {
        public double? Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        #region Binary Operators
        public static AValue operator +(AValue one, AValue two) //addition
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value + two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value + two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value + two.Value);
            else return new Constant(one.Value + two.Value);
        }
        public static AValue operator -(AValue one, AValue two) //subtraction
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value - two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value - two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value - two.Value);
            else return new Constant(one.Value - two.Value);
        }
        public static AValue operator *(AValue one, AValue two) //multiplication
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value * two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value * two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value * two.Value);
            else return new Constant(one.Value * two.Value);
        }
        public static AValue operator /(AValue one, AValue two) //division
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value / two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value / two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value / two.Value);
            else return new Constant(one.Value / two.Value);
        }
        public static AValue operator %(AValue one, AValue two) //modulo
        {
            while (one.Value > two.Value) one.Value -= two.Value;
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value);
            else return new Constant(one.Value);
        }
        public static AValue operator ^(AValue one, AValue two) //bitwise XOR
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, (int)one.Value ^ (int)two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, (int)one.Value ^ (int)two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, (int)one.Value ^ (int)two.Value);
            else return new Constant((int)one.Value ^ (int)two.Value);
        }
        public static AValue operator &(AValue one, AValue two) //bitwise AND
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, (int)one.Value & (int)two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, (int)one.Value & (int)two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, (int)one.Value & (int)two.Value);
            else return new Constant((int)one.Value & (int)two.Value);
        }
        public static AValue operator |(AValue one, AValue two) //bitwise OR
        {
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, (int)one.Value | (int)two.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, (int)one.Value | (int)two.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, (int)one.Value | (int)two.Value);
            else return new Constant((int)one.Value | (int)two.Value);
        }
        #endregion

        #region Unary Operators
        public static AValue operator +(AValue one) //what is unary plus for anyway
        {
            return one;
        }
        public static AValue operator -(AValue one) //negation
        {
            if (one is Variable) return new Variable(((Variable)one).Name, one.Value * (-1));
            else return new Constant(one.Value * (-1));
        }
        public static bool operator !(AValue one) //inverse boolean
        {
            return (one.Value == 0 ? true : false);
        }
        public static AValue operator ~(AValue one) //bitwise flips
        {
            if (one is Variable) return new Variable(((Variable)one).Name, ~((int)one.Value));
            else return new Constant(~((int)one.Value));
        }
        public static AValue operator ++(AValue one) //increment
        {
            if (one is Variable) return new Variable(((Variable)one).Name, ++one.Value);
            else return new Constant(++one.Value);
        }
        public static AValue operator --(AValue one) //decrement
        {
            if (one is Variable) return new Variable(((Variable)one).Name, --one.Value);
            else return new Constant(--one.Value);
        }
        #endregion

        #region Relational Operators
        public static bool operator ==(AValue one, AValue two)
        {
            return (one.Value == two.Value);
        }
        public static bool operator !=(AValue one, AValue two)
        {
            return (one.Value != two.Value);
        }
        public static bool operator >(AValue one, AValue two)
        {
            return (one.Value > two.Value);
        }
        public static bool operator <(AValue one, AValue two)
        {
            return (one.Value < two.Value);
        }
        public static bool operator <=(AValue one, AValue two)
        {
            return (one.Value <= two.Value);
        }
        public static bool operator >=(AValue one, AValue two)
        {
            return (one.Value >= two.Value);
        }
        #endregion
        #region Casting
        public static bool operator true(AValue one)
        {
            return one.Value > 0;
        }
        public static bool operator false(AValue one)
        {
            return one.Value <= 0;
        }
        #endregion
        #region Function Operations
        public static AValue Exponentiation(AValue one, AValue two) //exponentiation
        {
            two.Value--;
            double? baseval = one.Value;
            while (two.Value > 1)
            {
                one.Value *= baseval;
                two.Value--;
            }
            one.Value += (baseval * two.Value);
            if (one is Variable && two is Variable) return new Variable(((Variable)one).Name, one.Value);
            if (one is Variable && two is Constant) return new Variable(((Variable)one).Name, one.Value);
            if (one is Constant && two is Variable) return new Variable(((Variable)two).Name, one.Value);
            else return new Constant(one.Value);
        }
        public static AValue Factorial(AValue one) //exponentiation
        {
            double? i = (int)one.Value;
            while (one.Value > 1) one.Value *= i--;
            if (one is Variable) return new Variable(((Variable)one).Name, one.Value);
            else return new Constant(one.Value);
        }
        #endregion
    }

    public class Constant : AValue
    {
        public Constant(double? value)
        {
            Value = value;
        }
        public double? Value { get; set; }
    }

    public class Variable : AValue
    {
        public Variable(string name, double? value)
        {
            Name = name;
            Value = value;
        }
        public Variable(string name)
        {
            Name = name;
            Value = null;
        }
        public string Name { get; set; }
        public double? Value { get; set; }
    }
}
