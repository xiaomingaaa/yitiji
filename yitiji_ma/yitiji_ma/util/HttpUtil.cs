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
        public static Dictionary<string, string> getStudentInfo(string tel, string tel1) 
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            Stream info = getBackStream(tel,tel1);            
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
                        temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj["info"].ToString());
                        //int ret_stutus = stu_info["stuno"], stu_info["name"];
                       
                    }                    
                }               
            }
            return temp;
        }
        private static Stream getBackStream(string tel,string tel1)
        {
            string actionUrl = "HTTP://api.jxqwt.cn/apiuser/stunameno";
            HttpContent tel1Content = new StringContent(tel);
            HttpContent tel2Content = new StringContent(tel1);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(tel1Content, "tel");
                formData.Add(tel2Content, "tel2");
                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return response.Content.ReadAsStreamAsync().Result;
            }
        }

    }
}
