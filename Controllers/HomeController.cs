using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RationalNumbers.Models;
using System.IO;

namespace RationalNumbers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string str)
        {
            try
            {
                string input, add, sub, mul, div;
                List<Rational> rationals = new List<Rational>();
                Rational rat1, rat2;
                string r1, r2;
 
                input = Request["txtInput"].ToString().TrimStart().TrimEnd();    
                
                rationals = splitString(input, 2);

                rat1 = rationals[0];
                rat2 = rationals[1];
                
                add = rat1.add(rat2).Simplify();
                sub = rat1.sub(rat2).Simplify();
                mul = rat1.mul(rat2).Simplify();
                div = rat1.div(rat2).Simplify();

                r1 = rat1.Simplify();
                r2 = rat2.Simplify();

                if(add != "Inf")
                    add = r1 + " + " + r2 + " = " + add;

                if (sub != "Inf")
                    sub = r1 + " - " + r2 + " = " + sub;

                if (mul != "Inf") 
                    mul = r1 + " * " + r2 + " = " + mul;

                if (div != "Inf") 
                    div = r1 + " / " + r2 + " = " + div;

                ViewData["Result"] = add + "\r\n" + sub + "\r\n" + mul + "\r\n" + div;                               

            }
            catch (Exception e)
            {
                if (e.Message.Equals("Invalid Format"))
                    ViewData["Result"] = e.Message;
                else
                    ViewData["Result"] = "There has been a problem with the Application";                
            }

            return View();

        }



        /// <summary>
        /// Validates and split the required string
        /// </summary>
        /// <param name="str">String to split</param>
        /// <param name="splitter">Separator</param>
        /// <param name="validNumber">Valid number of items</param>
        /// <returns></returns>
        private List<Rational> splitString(string str, int validNumber)
        {
            List<string> list = new List<string>();
            List<Rational> rationals = new List<Rational>();
            Rational rat;
            long numerator, denominator;
            int count = 0;

            count = str.Split(' ').Count();

            if(count != validNumber )
                throw new Exception("Invalid Format");

            else
            {
                foreach (string item in str.Split(' '))
                {
                    count = item.Split('/').Count();
                    
                    if (count != validNumber)
                        throw new Exception("Invalid Format");
                    else
                    {
                        long.TryParse(item.Split('/')[0], out numerator);
                        long.TryParse(item.Split('/')[1], out denominator);
                        rat = new Rational(numerator, denominator);
                        rationals.Add(rat);
                    }                                  
                }                   
            }
           
            return rationals;
        }
     
       
    }
}
