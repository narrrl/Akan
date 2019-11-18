using System;

namespace Akan.Services
{
    class Methods
    {
        static public string GetWeekDay(DateTime? dateTemp)
        {
            DateTime date = dateTemp ?? default;
            string weekDay = date.DayOfWeek.ToString();
            return weekDay;
        }

        static public string GetStringTime(DateTime? dateTemp)
        {
            if (dateTemp == null)
            {
                return "unknown";
            }
            else
            {
                DateTime date = dateTemp ?? default;
                string retur = "";
                string weekDay;
                if((date.Day < 28 && date.Month < 10) || (date.Month > 3))
                {
                    date.AddHours(-7);
                }
                else
                {
                    date.AddHours(-8);
                }
                weekDay = date.DayOfWeek.ToString();
                string time = date.ToString("HH:mm");
                retur = weekDay + " " + time + " " + date.Day + "." + date.Month + "." + date.Year;
                return retur;
            }
        }

        static public string getTime(DateTime? dateTemp)
        {
            if (dateTemp == null)
            {
                return "unknown";
            }
            else
            {
                DateTime date = dateTemp ?? default;
                string time = date.ToString("HH:mm");
                return time;
            }
        }

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

        static public string RegionalIndicatorConverter(string str)
        {
            string b = str;
            string d = "";
            int asciiWert;
            foreach (char c in b)
            {
                asciiWert = c;

                if (asciiWert == 32)
                {
                    d = d + "\n";
                }

                else if ((asciiWert >= 97) && (asciiWert <= 122))
                {
                    d = d + ":regional_indicator_" + c + ": ";
                }

                else if ((asciiWert >= 48) && (asciiWert <= 57))
                {
                    switch (asciiWert)
                    {
                        case (48):
                            d = d + ":zero: ";
                            break;

                        case (49):
                            d = d + ":one: ";
                            break;

                        case (50):
                            d = d + ":two: ";
                            break;

                        case (51):
                            d = d + ":three: ";
                            break;

                        case (52):
                            d = d + ":four: ";
                            break;

                        case (53):
                            d = d + ":five: ";
                            break;

                        case (54):
                            d = d + ":six: ";
                            break;

                        case (55):
                            d = d + ":seven: ";
                            break;

                        case (56):
                            d = d + ":eight: ";
                            break;

                        case (57):
                            d = d + ":nine: ";
                            break;
                    }
                }

                else if (asciiWert == 33)
                {
                    d = d + ":exclamation: ";
                }

                else if (asciiWert == 63)
                {
                    d = d + ":question: ";
                }

                else if ((asciiWert >= 35) && (asciiWert <= 36))
                {
                    switch (asciiWert)
                    {
                        case (35):
                            d = d + ":hash: ";
                            break;

                        case (36):
                            d = d + ":heavy_dollar_sign: ";
                            break;
                    }
                }

                else if ((asciiWert >= 42) && (asciiWert <= 43))
                {
                    switch (asciiWert)
                    {
                        case (42):
                            d = d + ":heavy_multiplication_x: ";
                            break;

                        case (43):
                            d = d + ":heavy_plus_sign: ";
                            break;
                    }
                }

                else if (asciiWert == 45)
                {
                    d = d + ":heavy_minus_sign: ";
                }

                else if (asciiWert == 47)
                {
                    d = d + ":heavy_division_sign: ";
                }

                else if (c == 'ä' || c == 'ö' || c == 'ü' || c == 'ß')
                {
                    switch (c)
                    {
                        case 'ä':
                            d = d + ":regional_indicator_" + "a" + ": " + ":regional_indicator_" + "e" + ": ";
                            break;
                        case 'ö':
                            d = d + ":regional_indicator_" + "o" + ": " + ":regional_indicator_" + "e" + ": ";
                            break;
                        case 'ü':
                            d = d + ":regional_indicator_" + "u" + ": " + ":regional_indicator_" + "e" + ": ";
                            break;
                        case 'ß':
                            d = d + ":regional_indicator_" + "s" + ": " + ":regional_indicator_" + "s" + ": ";
                            break;
                        default:
                            break;
                    }
                }

                else
                {
                    d = d + c;
                }
            }

            return d;
        }

    }
}
