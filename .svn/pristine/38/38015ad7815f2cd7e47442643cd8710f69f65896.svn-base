using System.Diagnostics;
using System.Reflection;

namespace Framework.Utility.ToolsClass
{
    /// <summary>
    /// 用于正则表达式验证的KEY
    /// </summary>
    public static class RegexKey
    {
        /// <summary>
        /// 验证Email地址
        /// </summary>
        public const string CheckEmailValidate = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 只能输入数字
        /// </summary>
        public const string CKDigitalOnly = @"^[0-9]*$";

        /// <summary>
        /// 只能输入n位的数字
        /// </summary>
        /// <param name="n">数字个数</param>
        /// <returns>返回n个的数据正则匹配字符串</returns>
        public static string CKDNumberCount(int n)
        {
            return @"^\d{" + n + "}$";
        }

        /// <summary>
        /// 只能输入至少n位的数字
        /// </summary>
        /// <param name="n">数字个数</param>
        /// <returns>返回n个的数据正则匹配字符串</returns>
        public static string CKNumLeastLength(int n)
        {
            return @"^\d{" + n + ",}$";
        }

        /// <summary>
        /// 只能输入n~m位的数字(n要小于m)
        /// </summary>
        /// <param name="n">个数n</param>
        /// <param name="m">个数m</param>
        /// <returns></returns>
        public static string CKDNumberRange(int n, int m)
        {
            
            Debug.Assert(n < m);
            return string.Format(@"^\d{{{0},{1}}}$", n, m);
        }

        /// <summary>
        /// 只能输入零和非零开头的数字
        /// </summary>
        public const string CKNonZeroBegin = @"^(0|[1-9][0-9]*)$";

        /// <summary>
        /// 只能输入有两位小数的正实数
        /// </summary>
        public const string CKDeciminal = @"^[0-9]+(.[0-9]{2})?$";

        /// <summary>
        /// 只能输入有1~3位小数的正实数
        /// </summary>
        public const string CKTenThousands = @"^[0-9]+(.[0-9]{1,3})?$";

        /// <summary>
        /// 只能输入非零的正整数
        /// </summary>
        public const string CKNonZero = @"^\+?[1-9][0-9]*$";

        /// <summary>
        /// 只能输入非零的负整数
        /// </summary>
        public const string CKNoZeroNegative = @"^\-?[1-9][0-9]*$";

        /// <summary>
        /// 只能输入长度为3的字符
        /// </summary>
        public const string CKStrLen3 = @"^.{3}$";

        /// <summary>
        /// 只能输入由26个英文字母组成的字符串
        /// </summary>
        public const string CKStrAlpha = @"^[A-Za-z]+$";

        /// <summary>
        /// 只能输入由26个大写英文字母组成的字符串
        /// </summary>
        public const string CKStrOnlyChar = @"^[A-Z]+$";

        /// <summary>
        /// 只能输入由26个小写英文字母组成的字符串
        /// </summary>
        public const string CKStr = @"^[a-z]+$";

        /// <summary>
        /// 只能输入由数字和26个英文字母组成的字符串
        /// </summary>
        public const string CheckKey14 = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// 只能输入由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        public const string CheckKey15 = @"^\w+$";

        /// <summary>
        /// 验证用户密码：正确格式为：以字母开头，长度在6~18之间，只能包含字符、数字和下划线。
        /// </summary>
        public const string CheckKey16 = @"^[a-zA-Z]\w{5,17}$";

        /// <summary>
        /// 验证是否含有^%&',;=?$\"等字符
        /// </summary>
        public const string CheckKey17 = @"[^%&',;=?$\x22]+";

        /// <summary>
        /// 只能输入汉字
        /// </summary>
        public const string CheckKey18 = @"^[\u4e00-\u9fa5]{0,}$";

        /// <summary>
        /// 验证InternetURL
        /// </summary>
        public const string CheckKey19 = @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$";

        /// <summary>
        /// 验证电话号码正确格式为："XXX-XXXXXXX"、"XXXX-XXXXXXXX"、"XXX-XXXXXXX"、"XXX-XXXXXXXX"、"XXXXXXX"和"XXXXXXXX"。
        /// </summary>
        public const string CheckKey20 = @"^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$";

        /// <summary>
        /// 验证身份证号（15位或18位数字）
        /// </summary>
        public const string CheckKey21 = @"^\d{15}|\d{18}$";

        /// <summary>
        /// 匹配1个以上的数子字符串.
        /// </summary>
        public const string CheckKey22 = "[0-9]{1,}";


    }
}