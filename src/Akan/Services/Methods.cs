using System;
using System.Collections.Generic;
using System.Text;

namespace Akan.Services
{
    class Methods
    {
        static public int getYear()
        {
            DateTime thisDay = DateTime.Today;
            string str = thisDay.Year.ToString();
            return Convert.ToInt32(str);
        }


        static public string getSeason()
        {
            DateTime thisDay = DateTime.Today;
            int month = thisDay.Month;
            string monthStr = "";
            switch (month)
            {
                case 1:
                    monthStr = "winter";
                    break;
                case 2:
                    monthStr = "winter";
                    break;
                case 3:
                    monthStr = "winter";
                    break;
                case 4:
                    monthStr = "spring";
                    break;
                case 5:
                    monthStr = "spring";
                    break;
                case 6:
                    monthStr = "spring";
                    break;
                case 7:
                    monthStr = "summer";
                    break;
                case 8:
                    monthStr = "summer";
                    break;
                case 9:
                    monthStr = "summer";
                    break;
                case 10:
                    monthStr = "fall";
                    break;
                case 11:
                    monthStr = "fall";
                    break;
                case 12:
                    monthStr = "fall";
                    break;

            }
            return monthStr;
        }

        static public string getNextSeason()
        {
            DateTime thisDay = DateTime.Today;
            int month = thisDay.Month + 3;
            if(month > 12)
            {
                month = 1;
            }
            string monthStr = "";
            switch (month)
            {
                case 1:
                    monthStr = "winter";
                    break;
                case 2:
                    monthStr = "winter";
                    break;
                case 3:
                    monthStr = "winter";
                    break;
                case 4:
                    monthStr = "spring";
                    break;
                case 5:
                    monthStr = "spring";
                    break;
                case 6:
                    monthStr = "spring";
                    break;
                case 7:
                    monthStr = "summer";
                    break;
                case 8:
                    monthStr = "summer";
                    break;
                case 9:
                    monthStr = "summer";
                    break;
                case 10:
                    monthStr = "fall";
                    break;
                case 11:
                    monthStr = "fall";
                    break;
                case 12:
                    monthStr = "fall";
                    break;

            }
            return monthStr;
        }
    }
}
