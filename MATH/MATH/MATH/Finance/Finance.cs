namespace MATH
{
    public class Finance
    {
        //Returns a total amount from given amount, rate, number of compounds per year, and years
        public static double? CompoundInterest(double Principle, double rate, int years, int compounds)
        {
            Expression x = new Expression("" + Principle + "*(1+" + rate + "/" + compounds + ")^^(" + compounds + "*" + years + ")");
            return x.Solve().Value;
        }

        //Converts a present amount to what it will be in the future given a rate of interest or inflation
        public static double? PresentToFutureValue(double Present, double rate, double years)
        {
            Expression x = new Expression("" + Present + "*(1+" + rate + ")^^" + years);
            return x.Solve().Value;
        }

        //Converts an interest rate that is compounded at some distinct interval into an equivalent annual rate
        public static double? EffectiveAnnualRate(double rate, int compounds)
        {
            Expression x = new Expression("(((1+(" + rate + "/" + compounds + "))^^" + compounds + ")-1)*100");
            return x.Solve().Value;
        }

        //Divides 72 by the percentage value of an interest rate to learn how many years an amount would approximately double
        public static double? SeventyTwoRule(double rate)
        {
            return (72 / (rate * 100));
        }

        //Finds the effective annual rate needed to convert one amount to another in a given number of years
        public static double? CompoundedAnnualGrowthRate(double FutureValue, double PresentValue, int years)
        {
            Expression x = new Expression("((" + FutureValue + "/" + PresentValue + ")^^(1/" + years + "))-1");
            return x.Solve().Value;
        }

        //Finds a constant payment amount to pay off a given amount at a certain effective annual rate over a number of months
        public static double? EquatedMonthlyInstallments(double LoanAmount, double annualrate, int months)
        {
            annualrate /= 12; //must function as a monthly rate
            Expression x = new Expression("((" + LoanAmount + "*" + annualrate + ")*((1+" + annualrate + ")^^" + months + "))/(((1+" + annualrate + ")^^" + months + ")-1)");
            return x.Solve().Value;
        }

        //Finds an investment's future value after an amount of time given a rate and a constant recurring payment into it on a monthly basis
        public static double? FuturePlanValue(double Payment, double annualrate, int months)
        {
            annualrate /= 12; //must function as a monthly rate
            Expression x = new Expression("" + Payment + "*((((1+" + annualrate + ")^^" + months + ")-1)/" + annualrate + ")");
            return x.Solve().Value;
        }
    }
}
