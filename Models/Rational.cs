using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RationalNumbers.Models
{
    public class Rational 
    {
        private long numerator;
        private long denominator;

        public Rational(long num, long den)
        {        
            numerator = num;
            denominator = den;
        }

        public Rational add (Rational r) 
        {
            return new Rational(numerator * r.denominator + denominator * r.numerator, denominator * r.denominator);
        }

        public Rational sub(Rational r)
        {
            return new Rational(numerator * r.denominator - denominator * r.numerator, denominator * r.denominator);
        }

        public Rational mul(Rational r) 
        {
            return new Rational(numerator * r.numerator, denominator * r.denominator);
        }

        public Rational div (Rational r) 
        {
            return new Rational(numerator * r.denominator, denominator * r.numerator);
        }

        public string Simplify()
        {
            long remainder, result, gcd;
            string str;

            //If the denominator in the division is zero, output ""Inf""
            if (this.denominator == 0)
                return "Inf";
        
            result = Math.DivRem(this.numerator, this.denominator, out remainder);

            //If it is possible to simplify the rational number, it does.
            if (remainder != 0 && this.denominator != 0)
            {
                gcd = greatestCommonDivisor(Math.Abs(remainder), Math.Abs(this.denominator));
                remainder = remainder / gcd;
                this.denominator = this.denominator / gcd;
            }            
            
            //if result is different from 0 it shows the integer part and does not show the negative sign from the rational part
            if (result != 0)
            {
                //if remainder (numerator) is 0, it just shows the integer part
                if (remainder == 0)
                    str = result.ToString();
                else
                    str = result.ToString() + " " + Math.Abs(remainder).ToString() + "/" + Math.Abs(this.denominator).ToString();
            }
            else
            { 
                if (this.denominator < 0)
                    str = "-" + Math.Abs(remainder).ToString() + "/" + Math.Abs(this.denominator).ToString();
                else
                    str = remainder.ToString() + "/" + this.denominator.ToString(); 
            }
                

            //if the numerator is negative it close the result string with ()
            if (this.numerator < 0 || this.denominator < 0)
                str = "(" + str + ")";

            return str;
        }


        private long greatestCommonDivisor(long a, long b)
        {           
            while (b > 0)
            {
                long rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }   
        
    }

}