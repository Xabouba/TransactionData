using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentTransaction.App_Code
{
    abstract class ErrorCode
    {
        public static int TestCSVLine(string[] values)
        {
            //Test if format correspond to database type
            if (values[0].Length > 100 || values[1].Length > 100 || values[2].Length > 3 || values[3].Length > 18)
            {
                return 1;
            }

            // Test if non empty input
            foreach (string val in values)
            {
                if (String.IsNullOrEmpty(val))
                {
                    return 2;
                }
            }

            // Test Currency Code ISO 4217
            if (values[2].Length != 3)
            {
                if (values[2].Any(x => !char.IsLetter(x)))
                {
                    if (!Iso4217Lookup.LookupByCode(values[2]).Found)
                    {
                        return 3;
                    }
                }

            }

            // Test amount is decimal
            decimal num;
            if (!decimal.TryParse(values[3], out num))
            {
                return 4;
            }


            return 0;
        }

        
        public static string TestIntDetails(int TestCode)
        {
            string details = "";
            if (TestCode == 1)
            {
                details = "One or more input values are wrongly typed";
            }
            if (TestCode == 2)
            {
                details = "One or more input values are null or empty";
            }
            else if (TestCode == 3)
            {
                details = "The currency code is not ISO 4217 valid";
            }
            else if (TestCode == 4)
            {
                details = "The amount is not a valid number";
            }
            return details;
        }

    }
}