using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
namespace yitiji_ma.util
{
    class HttpUtil
    {
        public static Dictionary<string, object>[] getStudentInfo(string tel, string tel1) 
        {
            string actionUrl = "HTTP://api.jxqwt.cn/apiuser/stunameno";
            Dictionary<string, object>[] temp = null;
            Stream info = getBackStream(tel,tel1, actionUrl);
            if (info != null)
            {
                info.Position = 0;
                StreamReader reader = new StreamReader(info);
                string result = reader.ReadToEnd();
                Dictionary<string, object> obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                if (obj.ContainsKey("status"))
                {
                    if (obj["status"].ToString() == "0")
                    {
                        temp = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(obj["info"].ToString());
                        //int ret_stutus = stu_info["stuno"], stu_info["name"];

                    }
                }
            }            
            return temp;
        }
        private static Stream getBackStream(string tel,string tel1,string actionUrl)
        {
            
            HttpContent tel1Content = new StringContent(tel);
            HttpContent tel2Content = new StringContent(tel1);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(tel1Content, "tel");
                formData.Add(tel2Content, "tel1");
                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Log.WriteError("网络错误，请检查网络！状态码："+response.IsSuccessStatusCode);
                    return null;
                }

                return response.Content.ReadAsStreamAsync().Result;
            }
        }
        public static Dictionary<string, object>[] getBukaInfo(string tel,string tel1)
        {
            string actionUrl = "HTTP://api.jxqwt.cn/apiuser/stuname";
            Dictionary<string, object>[] temp = null;
            Stream info = getBackStream(tel, tel1, actionUrl);
            if (info != null)
            {
                info.Position = 0;
                StreamReader reader = new StreamReader(info);
                string result = reader.ReadToEnd();
                Dictionary<string, object> obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                if (obj.ContainsKey("status"))
                {
                    if (obj["status"].ToString() == "0")
                    {
                        temp = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(obj["info"].ToString());
                        //int ret_stutus = stu_info["stuno"], stu_info["name"];

                    }
                }
            }            
            return temp;
        }
    }
}
