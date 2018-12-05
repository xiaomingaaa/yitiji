using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yitiji_ma.entity;
using yitiji_ma.util;
namespace yitiji_ma.controller
{
    /// <summary>
    /// 处理补卡的相关逻辑
    /// </summary>
    class BukaController
    {
        public Error Buka(string tel1,string tel2)
        {
            if (!ValidateUtil.ValidateTelNumber(tel1) || !ValidateUtil.ValidateTelNumber(tel2))
            {
                return Error.VALIDATE_ERROR;//用户输入验证错误
            }
            else
            {

                return Error.BUKA_SUCCESS;
            }
        }
    }
}
