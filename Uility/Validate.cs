﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Utility.ToolsClass
{
    public static class Validate
    {
        /// <summary>
        /// 验证Value是否在minValue和maxValue之间
        /// </summary>
        /// <param name="value">要验证的数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool IsInRange(int value, int minValue, int maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>
        /// 验证Value是否在minValue和maxValue之间
        /// </summary>
        /// <param name="value">要验证的数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool IsInRange(float value, float minValue, float maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>
        /// 验证Value是否在minValue和maxValue之间
        /// </summary>
        /// <param name="value">要验证的数值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool IsInRange(double value, double minValue, double maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>
        /// 验证是否为Email地址
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool IsEmailValue(string value)
        {
            return RegexValidate(RegexKey.CheckEmailValidate, value);
        }

        /// <summary>
        /// 验证是否为身份证号
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool IsIDCard(string value)
        {
            bool IsOk = false;

            if (value.Length == 15 || value.Length == 18)
            {
                long Temp;
                IsOk = long.TryParse(value, out Temp);

                if (!IsOk)
                {
                    if (value.Substring(value.Length - 1, 1) == "X")
                    {
                        IsOk = true;
                    }
                }
            }

            return IsOk;
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="Key">为正则表达式验证关健字 RegexKey 类中有常用关键字</param>
        /// <param name="Value">为操作的字符串</param>
        /// <returns>成功返回true,失败返回false</returns>
        /// <example>C#代码示例
        /// 验证是否为Email格式RegexKey.CheckKey0为Email正则表达式条件
        /// <code>
        /// CheckValue.CheckRegex(RegexKey.CheckKey0, "asdfsas@163.com");
        /// </code>
        /// </example>
        public static bool RegexValidate(string Key, string Value)
        {
            Regex RegexStr = new Regex(Key);
            Match Math = RegexStr.Match(Value);
             
            return Math.Success;
        }

        /// <summary>
        /// 取字符串中的相同类型数据列表
        /// </summary>
        /// <param name="Key">为正则表达式验证关健字 RegexKey 类中有常用关键字</param>
        /// <param name="Value">为操作的字符串</param>
        /// <returns>成功返回List,失败返回null</returns>
        /// <example>C#代码示例
        /// 把字符串中的数子提取出来 RegexKey.CheckKey22为Email正则表达式条件   "~~!!we2323()__*34.434343jksldfasl.2323~!)("    返回2323,34,434343,2323四个成员的List
        /// <code>
        ///  CheckValue.GetList(RegexKey.CheckKey22, "~~!!we2323()__*34.434343jksldfasl.2323~!)(");
        /// </code>
        /// </example>
        public static List<string> GetList(string Key, string Value)
        {
            List<string> Temp = new List<string>();
            try
            {
                Regex RegexStr = new Regex(Key);
               
                MatchCollection Maths = RegexStr.Matches(Value);

                foreach (Match Item in Maths)
                {
                    if (!string.IsNullOrEmpty(Item.Value.Trim()))
                    {
                        Temp.Add(Item.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Temp;
        }
    }
}