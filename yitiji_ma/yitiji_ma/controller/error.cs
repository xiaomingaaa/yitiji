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
        NOTGUASHIED,
        UPDATECARD_ERROR,//变更用户卡状态失败
        INSERT_BLACKNAME_ERROR,//插入黑名单失败
        GUASHI_SUCCESS,//挂失成功·
        HTTP_NONE,//无此人数据
        BUKA_SUCCESS,
        WRITE_CARD_ERROR,
        SEND_CARD_ERROR,
        DETECT_CARD_ERROR,
        PWD_LOAD_ERROR,
        READ_PHYID_ERROR,
        SEND_CARD_SUCCESS,
        NOTEXIST_ERROR,
        UPDATE_INFO_ERROR
        
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
                case Error.BUKA_SUCCESS:
                    temp = "补卡成功！";
                    break;
                case Error.DETECT_CARD_ERROR:
                    temp = "寻卡失败，请检查机器内是否有卡";
                    break;
                case Error.NOTEXIST_ERROR:
                    temp = "学生信息不存在！";
                    break;
                case Error.NOTGUASHIED:
                    temp = "此学生卡未挂失！";
                    break;
                case Error.PWD_LOAD_ERROR:
                    temp = "写卡密码验证失败！";
                    break;
                case Error.READ_PHYID_ERROR:
                    temp = "读取物理卡号失败！";
                    break;
                case Error.SEND_CARD_ERROR:
                    temp = "发卡失败！！无法补卡";
                    break;
                case Error.SEND_CARD_SUCCESS:
                    temp = "发卡成功！请取走卡";
                    break;
                case Error.UPDATE_INFO_ERROR:
                    temp = "更新学生信息失败！";
                    break;
                case Error.WRITE_CARD_ERROR:
                    temp = "写卡失败！";
                    break;
                default:
                    temp = "未知错误！";
                    break;
            }
            return temp;
        }
    }
}
