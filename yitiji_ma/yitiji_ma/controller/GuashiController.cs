using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yitiji_ma.entity;
using yitiji_ma.util;
using System.Data;
using System.Data.SqlClient;
namespace yitiji_ma.controller
{
    /// <summary>
    /// 处理用户的挂失处理逻辑
    /// </summary>
    class GuashiController
    {
        
        public Error GuaShi(string tel1,string tel2,string info) 
        {
            if (!ValidateUtil.ValidateTelNumber(tel1) && !ValidateUtil.ValidateTelNumber(tel2))
            {
                return Error.VALIDATE_ERROR;//返回验证错误
            }
            else 
            {
                
                
                if (info == "(无学生信息)")
                {
                    return Error.NOTEXIST_ERROR;//此学生信息不存在
                }
                string[] ss = info.Split('|');
                string stuno=ss[1];
                string name=ss[0];
                string studentInfo = string.Format("学生姓名：{0},学生学号：{1}",name,stuno);
                if ((stuno == null || stuno == "")|(name==null||name==""))
                {
                    return Error.HTTP_NONE;//没有学生信息
                }
                Log.WriteLog(string.Format("学生挂失卡信息:姓名：{0}",studentInfo));
                //查询本地数据库学生信息
                string sql = "select cardnum,phyid,deptname,cardstatus from hr_employee where empno = '" + stuno + "' and empname = '" + name + "'";
                DataTable table = SQLHelper.GetAllResult(sql);
                if (table.Rows.Count<=0)
                {
                    return Error.NOTEXIST_ERROR;
                }
                string cardnum = table.Rows[0]["cardnum"].ToString();
                string phyid = table.Rows[0]["phyid"].ToString();
                string deptname = table.Rows[0]["deptname"].ToString();
                int cardstatus =Convert.ToInt32(table.Rows[0]["cardstatus"].ToString());
                if (cardstatus == 2)
                {
                    Log.WriteError("此学生已经挂失："+studentInfo);
                    return Error.GUASHIED;//返回已经挂失的标识
                }
                string updateStatus = "update hr_employee set cardstatus='2' where empno='" + stuno + "'";
                int flag = SQLHelper.Update(updateStatus);
                if (flag <= 0)
                {
                    Log.WriteError("变更学生卡状态失效："+studentInfo);
                   return Error.UPDATECARD_ERROR;//返回更新失败的错误信息
                }
                string[] param = new string[] { stuno,name,deptname,phyid,cardnum,"无",DateTime.Now.ToLocalTime().ToString()};
                string insertBlackName =string.Format("insert into hr_blackname(empno,empname,deptname,phyid,cardnum,logname,logtime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');",param);
                int flagInsert = SQLHelper.Update(insertBlackName);
                if (flagInsert <= 0)
                {
                    Log.WriteLog("插入黑名单失败：学生姓名->"+name+",学号->"+stuno);
                    return Error.INSERT_BLACKNAME_ERROR;//返回黑名单更新失败
                }
                
                  
                Log.WriteLog("卡挂失成功："+studentInfo);
                return Error.GUASHI_SUCCESS;//挂失成功，成功将挂失数据添加到黑名单中
            }
        }
    }
}
