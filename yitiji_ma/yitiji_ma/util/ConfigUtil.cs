using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using yitiji_ma.entity;
using yitiji_ma.util;
namespace yitiji_ma.util
{
    /// <summary>
    /// 使用此工具类访问json配置文件
    /// </summary>
    class ConfigUtil
    {  
        /// <summary>
        /// 获取参数，测试通过
        /// </summary>
        /// <returns></returns>
        public static Config getConfig()
        {
            Config temp = new Config();
            try 
            {
                StreamReader file = File.OpenText("config.json");
                string configStr = "";
                string tempStr = "";
                while ((tempStr = file.ReadLine()) != null)
                {
                    configStr += tempStr;
                }
                temp = JsonConvert.DeserializeObject<Config>(configStr);
            }
            catch (Exception e) 
            {
                Log.WriteError("读取配置文件的信息出现错误："+e.Message);
            }
            return temp;
        }
     }
}
