using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WeChat
{
    /// <summary>
    /// createMenu 的摘要说明
    /// </summary>
    public class createMenu : IHttpHandler
    {
        HttpContext context = null;
        public string access_token { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            FileStream fs1 = new FileStream(context.Server.MapPath(".") + "\\menu.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("UTF-8"));
            string menu = sr.ReadToEnd();
            sr.Close();
            fs1.Close();
            var str = GetPage("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxc24f5f004a27cf95&secret=cb67202cddf570afe0c6b2bc5bb12004", "");
            JObject jo = JObject.Parse(str);
            access_token = jo["access_token"].ToString();
            var content = GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token + "", menu);
            context.Response.Write(content);
        }
        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}