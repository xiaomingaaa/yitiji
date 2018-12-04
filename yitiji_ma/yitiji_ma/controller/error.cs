using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yitiji_ma.controller
{
    public enum Error
    {
        GUASHI_ERROR,//学生卡挂失失败
        VALIDATE_ERROR,//用户输入（电话号码）验证失败
        HTTP_ERROR,//http请求数据错误
        GUASHIED,//此卡已经挂失
        UPDATECARD_ERROR,//变更用户卡状态失败
        INSERT_BLACKNAME_ERROR,//插入黑名单失败
        GUASHI_SUCCESS,//挂失成功·
        HTTP_NONE,//无此人数据
        BUKA_SUCCESS
    }
    public class error 
    {
        public static string errorMessage(Error e)
        {
            string temp="";
            switch (e)
            {
                case Error.GUASHI_ERROR:
                    temp = "挂失失败！";
                    break;
                case Error.GUASHI_SUCCESS:
                    temp = "挂失成功！";
                    break;
                case Error.GUASHIED:
                    temp = "卡已经挂失！";
                    break;
                case Error.HTTP_ERROR:
                    temp = "Http请求出现错误，请检查网络！";
                    break;
                case Error.HTTP_NONE:
                    temp = "没有查找到学生信息！";
                    break;
                case Error.INSERT_BLACKNAME_ERROR:
                    temp = "学生挂失信息存储失败！";
                    break;
                case Error.UPDATECARD_ERROR:
                    temp = "学生卡状态变更失败！";
                    break;
                case Error.VALIDATE_ERROR:
                    temp = "请输入正确的电话号码格式！";
                    break;
                default:
                    temp = "未知错误！";
                    break;
            }
            return temp;
        }
    }
}
