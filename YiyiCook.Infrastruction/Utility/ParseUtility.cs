using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YiyiCook.Infrastruction.Utility
{
    public static class ParseUtility
    {
		public static double ParseDouble(string num)
		{
			if (num.Contains("/"))
			{
				String[] str = num.Split("/");
				return Double.Parse(str[0]) / Double.Parse(str[1]);
			}
			else
			{
				return Double.Parse(num);
			}

		}
		public static double MatchNum(string num,out bool matchCountSuccess,out string matchPartStr)
		{
            Match match;
            matchCountSuccess = false;
            matchPartStr = "";
            double count = 0;
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "0.[0-9]+");
                if (match.Success)
                {
                    matchPartStr = match.Groups[0].Value;
                    count = double.Parse(matchPartStr);
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "[1-9]+/[1-9]+");
                if (match.Success)
                {
                    matchPartStr = match.Groups[0].Value;
                    count = ParseUtility.ParseDouble(matchPartStr);
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "[1-9]+");
                if (match.Success)
                {
                    matchPartStr = match.Groups[0].Value;
                    count = double.Parse(matchPartStr);
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "半");
                if (match.Success)
                {
                    matchPartStr = "半";
                    count = 0.5;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "一");
                if (match.Success)
                {
                    matchPartStr = "一";
                    count = 1;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "二");
                if (match.Success)
                {
                    matchPartStr = "二";
                    count = 2;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "两");
                if (match.Success)
                {
                    matchPartStr = "两";
                    count = 2;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "三");
                if (match.Success)
                {
                    matchPartStr = "三";
                    count = 3;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "四");
                if (match.Success)
                {
                    matchPartStr = "四";
                    count = 4;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "五");
                if (match.Success)
                {
                    matchPartStr = "五";
                    count = 5;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "六");
                if (match.Success)
                {
                    matchPartStr = "六";
                    count = 6;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "七");
                if (match.Success)
                {
                    matchPartStr = "七";
                    count = 7;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "八");
                if (match.Success)
                {
                    matchPartStr = "八";
                    count = 8;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "九");
                if (match.Success)
                {
                    matchPartStr = "九";
                    count = 9;
                    matchCountSuccess = true;
                }
            }
            if (!matchCountSuccess)
            {
                match = Regex.Match(num, "十");
                if (match.Success)
                {
                    matchPartStr = "十";
                    count = 10;
                    matchCountSuccess = true;
                }
            }
            
            return count;
        }
       
    }
}
