using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DashBoardBLL
{
    public class BOValidation
    {
        public static bool IsEmail(string inputEmail)
        {
            string strRedex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRedex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if (n <= 47 || n >= 58)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsDecimal(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if (n < 46 || n >= 58)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNumericPhone(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if ((n <= 47 || n >= 58) && (n != 45))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsTime(string value)
        {
            string time = Convert.ToString(Convert.ToDateTime(value));
            bool isTime = Regex.IsMatch(time, "^((0?[1-9]|1[012])(:[0-5]\\d){0,2}(\\ [AP]M))$|^([01]\\d|2[0-3])(:[0-5]\\d){0,2}$");
            return isTime;
        }

        public static bool IsDate(string value)
        {
            string date = Convert.ToString(Convert.ToDateTime(value));
            bool isDate = Regex.IsMatch(date, "^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$");
            return isDate;
        }
    }
}
