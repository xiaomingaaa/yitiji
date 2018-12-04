using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yitiji_ma.entity
{
    public class Config
    {
        private string host;//主机ip
        private string database;//数据库
        private string user;//数据库连接用户名
        private string pwd;//用户密码
        private int school_id;//学校号
        private string ledport;//写卡吐卡端口
        private int startSectoin;//加密扇区
        public string Host
        {
            get { return host; }
            set { host = value; }
        }        
        public string Database
        {
            get { return database; }
            set { database = value; }
        }       
        public string User
        {
            get { return user; }
            set { user = value; }
        }        
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }        
        public int School_id
        {
            get { return school_id; }
            set { school_id = value; }
        }        
        public string Ledport
        {
            get { return ledport; }
            set { ledport = value; }
        }        
        public int StartSectoin
        {
            get { return startSectoin; }
            set { startSectoin = value; }
        }
        public override string ToString()
        {
            string sqlconn = string.Format("Data Source={0};Database={1};User id={2};PWD={3}",this.Host,this.Database,this.User,this.Pwd);
            return sqlconn;
        }
    }
}
