using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace yitiji_ma.util
{
    /// <summary>
    /// 验证用户输入
    /// </summary>
    class ValidateUtil
    {
        /// <summary>
        /// 验证电话号码格式
        /// </summary>
        /// <param name="param">待验证的字符串</param>
        /// <returns></returns>
        public static bool ValidateTelNumber(string param) 
        {
            string pattnern = "^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\\d{8}$";
            Regex r = new Regex(pattnern);
            if (r.IsMatch(param))
            {
                return true;
            }
            return false;
        }

    }
}
