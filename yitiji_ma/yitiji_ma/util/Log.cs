﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using yitiji_ma.entity;
using Newtonsoft.Json;
namespace yitiji_ma.util
{
    /// <summary>
    /// 使用此类记录用户操作数据以及错误日志
    /// </summary>
    class Log
    {
        public static void WriteError(string str)
        {
            string log_path = AppDomain.CurrentDomain.BaseDirectory + "Logs" + "/error_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                str = DateTime.Now + "\r\n" + str;
                byte[] bytes = Encoding.Default.GetBytes(str + "\r\n");
                FileStream fileStream = File.OpenWrite(log_path);
                fileStream.Position = fileStream.Length;
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
                fileStream.Close();
            }
            catch
            {

            }
        }
        public static void WriteLog(string str)
        {
            string log_path = AppDomain.CurrentDomain.BaseDirectory + "Logs" + "/log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                str = DateTime.Now + "\r\n" + str;
                byte[] bytes = Encoding.Default.GetBytes(str + "\r\n");
                FileStream fileStream = File.OpenWrite(log_path);
                fileStream.Position = fileStream.Length;
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
                fileStream.Close();
            }
            catch
            {

            }
        }
        public static string WriteJsonData(string name,string stuno,long phyid,double money)
        {
            Student student = new Student(name,stuno,phyid,money);
            string path = "./Logs/student_log"+DateTime.Now.ToString()+".json";
           
            try
            {

                string jsonStr = JsonConvert.SerializeObject(student.ToString());
                if (!File.Exists(path)) 
                {
                    File.CreateText(path);
                }
                StreamWriter write = new StreamWriter(path,true);
                write.WriteLine(jsonStr);
                write.Close();
                return "";
            }
            catch (Exception e) 
            {
                return e.Message;
                
            }
        }
    }
}
