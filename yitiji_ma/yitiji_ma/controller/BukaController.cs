using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yitiji_ma.entity;
using yitiji_ma.util;
using System.Data;
namespace yitiji_ma.controller
{
    /// <summary>
    /// 处理补卡的相关逻辑
    /// </summary>
    class BukaController
    {
        public Error Buka(string tel1,string tel2,string info)
        {
            if (!ValidateUtil.ValidateTelNumber(tel1) || !ValidateUtil.ValidateTelNumber(tel2))
            {
                return Error.VALIDATE_ERROR;//用户输入验证错误
            }
            else if(info== "无此人补卡缴费信息")
            {
                return Error.NOT_EXIST_BUKA;
            }
            else
            {
                //Dictionary<string, string> stuinfo = HttpUtil.getStudentInfo(tel1, tel2);
                //if (stuinfo == null)
                //{
                //    Log.WriteError("从服务器获取学生数据失败" + tel1 + "," + tel2);
                //    return Error.HTTP_ERROR;//获取的http数据出现错误了
                //}
                //string stuno = stuinfo["stuno"];
                //string name = stuinfo["name"];
                string[] ss = info.Split('|');
                string stuno = ss[1];
                string name =ss[0];
                string studentInfo = string.Format("学生姓名：{0},学生学号：{1}", name, stuno);
                
                if ((stuno == null || stuno == "") | (name == null || name == ""))
                {
                    Log.WriteError("服务器没有此人数据"+tel1+","+tel2);
                    return Error.HTTP_NONE;//没有学生信息
                }
                string sqlText = "select * from hr_employee where empno='"+stuno+"' and empname='"+name+"'";
                string sqlMaxStuno = "select max(cardnum) from hr_employee;";
                DataTable table = new DataTable();
                table = SQLHelper.GetAllResult(sqlText);
                if (table.Rows.Count<=0)
                {
                    Log.WriteError("本地数据库不存在此人："+studentInfo);
                    return Error.NOT_EXIST_BUKA;
                }
                int cardStatus =Convert.ToInt32(table.Rows[0]["cardstatus"].ToString());
                if (cardStatus != 2)
                {
                    Log.WriteError("此卡未挂失："+studentInfo);
                    return Error.NOTGUASHIED;//未挂失
                }
                object cardno = SQLHelper.GetOneResult(sqlMaxStuno);
                int cardnum = 0;
                if (cardno != null)
                {
                    cardnum = Convert.ToInt32(cardno.ToString())+1;
                }
                Log.WriteData("卡号为："+cardnum);
                string flag = table.Rows[0]["empcard"].ToString().Trim();
                double money = Convert.ToDouble(table.Rows[0]["empje01"].ToString());
                byte lx = GetType(flag);
                Config config = ConfigUtil.getConfig();
                string mm= config.Initpwd;
                string com = config.Ledport;
                CardOperater operater = new CardOperater();
                Error error=operater.WriteCard(com,cardnum,name,mm,money,lx);
                if (!error.Equals(Error.SEND_CARD_SUCCESS))
                {
                    return error;
                }
                Log.WriteJsonData(name, stuno, operater.phyid, money);
                //更新本地库信息
                string updateOperaterSql = string.Format("insert into dlc_operation (empno,empname,carduid,created,operation,jine)values('{0}','{1}','{2}','{3}','{4}','{5}');",stuno,name,cardnum,DateTime.Now.ToLocalTime().ToString(),"buka",money);
                string updatePhyidSql = string.Format("update hr_employee set phyid='{0}',cardstatus='0',cardendrq='{1}',cardnum='{3}' where empno='{2}';",operater.phyid,DateTime.Now.AddYears(10).ToString("yyyy-MM-dd"),stuno,cardnum);
                //string delete = "delete from hr_blackname where empno='"+stuno+"';";
                int updateRow = SQLHelper.Update(updateOperaterSql+updatePhyidSql);
                if (updateRow <= 0)
                {
                    Log.WriteError("学生补卡吐卡成功，更新数据失败："+studentInfo);
                    return Error.UPDATE_INFO_ERROR;//返回信息更新失败消息
                }
                return Error.BUKA_SUCCESS;
            }
           
        }
        //获取类型数据
        private byte GetType(string flag)
        {
            byte temp = 0x41;
            switch (flag)
            {
                case "A":
                    temp = 0x41;
                    break;
                case "B":
                    temp = 0x42;
                    break;
                case "C":
                    temp = 0x43;
                    break;
                case "D":
                    temp = 0x44;
                    break;
                case "E":
                    temp = 0x45;
                    break;
                case "F":
                    temp = 0x46;
                    break;
                case "G":
                    temp = 0x47;
                    break;
                case "H":
                    temp = 0x48;
                    break;
                case "I":
                    temp = 0x49;
                    break;
                case "J":
                    temp = 0x4A;
                    break;
                default:
                    break;
            }
            return temp;
        }
    }
}
