using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yitiji_ma.entity
{
    class Student
    {
        private string name;//学生姓名
        private double money;//学生学号
        private long newPhyid;//学生新的物理卡号
        private string stuno;//学生
        public string Name
        {
            get { return name; }
            set { name = value; }
        }        
        public string Stuno
        {
            get { return stuno; }
            set { stuno = value; }
        }        
        public long NewPhyid
        {
            get { return newPhyid; }
            set { newPhyid = value; }
        }        
        public double Money
        {
            get { return money; }
            set { money = value; }
        }
        public Student(string name,string stuno,long phyid,double money) 
        {
            this.Name = name;
            this.NewPhyid = phyid;
            this.Stuno = stuno;
            this.Money = money;
        }
        public override string ToString()
        {
            string temp = "{}";
            return temp;
        }
    }
}
