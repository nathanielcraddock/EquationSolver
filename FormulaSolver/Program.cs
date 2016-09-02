using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prompt Loop
            while (true)
            {
                //Try equation to prevent any run time errors
                try
                {
                    Console.WriteLine("Please enter your function: ");
                    //Get user input
                    var equation = Console.ReadLine();

                    //Passes equation if factorial or power of operators are used due
                    //to them not being supported by datatable
                    if (equation.Contains("^") || equation.Contains("!"))
                    {
                        if (equation.Contains("^"))
                        {
                            string powerEquation = equation.ToString();
                            var answer = new DataTable().Compute(toPower(powerEquation), null);
                            Console.Write("The answer is: ");
                            Console.WriteLine(answer);
                        }
                        if (equation.Contains("!"))
                        {
                            string factorialEquation = equation.ToString();
                            var answer = new DataTable().Compute(toFactorial(factorialEquation), null);
                            Console.Write("The answer is: ");
                            Console.WriteLine(answer);
                        }
                    }

                    //If no special operators are typed, calculate simple expression using a datatbale
                    else
                    {
                        var answer = new DataTable().Compute(equation, null);
                        Console.Write("The answer is: ");
                        Console.WriteLine(answer);
                    }
                }
                
                //Simple catch for back to back operators
                catch
                {
                    Console.WriteLine("Back to back Operators entered.");
                }
            }
        }

        public static string toPower(string power)
        {
            //Declare variables needed
            string current = "";
            string oper = "";
            int test;
            int count = 1;
            double num1 = 0;
            double num2 = 0;
            List<string> totalEQ = new List<string>();

            //Cycles through equation to calculate power of
            foreach (char c in power)
            {
                if (count == 1)
                {
                    if (c == '^')
                    {
                        num1 = num1 + Convert.ToInt32(current);
                        count = 2;
                        oper = "^";
                        current = "";
                    }
                    else if (!int.TryParse(c.ToString(), out test))
                    {
                        totalEQ.Add(current);
                        current = c.ToString();
                        totalEQ.Add(current);
                        current = "";
                    }
                    else
                    {
                        current = current + c;
                    }
                }
                
                //Will be used for exponential equations
                else
                {
                    if (!int.TryParse(c.ToString(), out test))
                    {
                        num2 = num2 + Convert.ToInt32(current);
                        current = Convert.ToString(Math.Pow(num1, num2));
                        totalEQ.Add(current);
                        count = 1;
                    }
                    else
                    {
                        current = current + c;
                    }

                }
            }
            
            //Calculates power of using System.Math if exponential equation is not used
            if (oper == "^")
            {
                num2 = num2 + Convert.ToInt32(current);
                current = Convert.ToString(Math.Pow(num1, num2));
                totalEQ.Add(current);
            }

            //Returns equation to be easily computed by datatable
            return power = string.Join("", totalEQ.ToArray());
        }

        public static string toFactorial(string factorial)
        {
            //Declare variables
            string current = "";
            int factorialNum = 0;
            int runningTotal = 0;
            int num1 = 0;
            int test;
            string oper = "";
            List<string> totalEQ = new List<string>();

            //Loop to cycle through equation
            foreach (char c in factorial)
            {
                if (c == '!')
                {
                    //Declare variables for factorial equation
                    factorialNum = Convert.ToInt32(current);
                    runningTotal = factorialNum;
                    num1 = factorialNum;

                    //Loop to calculate factorial sequence
                    for (int i = 1; i < num1; i++)
                    {
                        runningTotal = runningTotal*(factorialNum - 1);
                        factorialNum = factorialNum - 1;
                    }


                    oper = "!";
                    current = runningTotal.ToString();
                    totalEQ.Add(current);
                    current = "";
                }

                else if (!int.TryParse(c.ToString(), out test))
                {
                    totalEQ.Add(current);
                    current = c.ToString();
                    totalEQ.Add(current);
                    current = "";
                }
                else
                {
                    current = current + c;
                }
            }

            if (oper == "!")
            {
                totalEQ.Add(current);
            }

            //Return simplified equation if neccessary 
            return factorial = string.Join("", totalEQ.ToArray());
        }


    }
}
