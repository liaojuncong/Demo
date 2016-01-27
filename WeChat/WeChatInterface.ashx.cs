using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChat
{
    /// <summary>
    /// WeChatInterface 的摘要说明
    /// </summary>
    public class WeChatInterface : IHttpHandler
    {
        HttpContext context = null;
        string postStr = "";

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            //WriteLog("before valid \n");
            //Valid();//用于验证
            //WriteLog("after valid, before post \n");

            if (context.Request.HttpMethod.ToLower() == "post")
            {
                System.IO.Stream s = context.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = System.Text.Encoding.UTF8.GetString(b);
                if (!string.IsNullOrWhiteSpace(postStr))
                {
                    ResponseMsg(postStr);
                }
            }
        }

        public void ResponseMsg(string postStr)
        {

            System.Xml.XmlDocument postObj = new System.Xml.XmlDocument();
            postObj.LoadXml(postStr);
            WriteLog("responseMsg:-------" + postStr);
            var FromUserNameList = postObj.GetElementsByTagName("FromUserName");
            string FromUserName = string.Empty;
            for (int i = 0; i < FromUserNameList.Count; i++)
            {
                if (FromUserNameList[i].ChildNodes[0].NodeType == System.Xml.XmlNodeType.CDATA)
                {
                    FromUserName = FromUserNameList[i].ChildNodes[0].Value;
                }
            }
            var toUsernameList = postObj.GetElementsByTagName("ToUserName");
            string ToUserName = string.Empty;
            for (int i = 0; i < toUsernameList.Count; i++)
            {
                if (toUsernameList[i].ChildNodes[0].NodeType == System.Xml.XmlNodeType.CDATA)
                {
                    ToUserName = toUsernameList[i].ChildNodes[0].Value;
                }
            }
            var keywordList = postObj.GetElementsByTagName("Content");
            string Content = string.Empty;
            for (int i = 0; i < keywordList.Count; i++)
            {
                if (keywordList[i].ChildNodes[0].NodeType == System.Xml.XmlNodeType.CDATA)
                {
                    Content = keywordList[i].ChildNodes[0].Value;
                }
            }
            var time = DateTime.Now;
            var textpl = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" +
                "<CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType>" +
                "<Content><![CDATA[欢迎来到微信世界---" + Content + "<a href='www.baidu.com'>baidu</a>]]></Content><FuncFlag>0</FuncFlag></xml> ";
            context.Response.Write(textpl);
            context.Response.End();
        }

        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        private void WriteLog(string strMemo)
        {
            string filename = "D:/WEBHOME/logs/log.txt";
            if (!System.IO.Directory.Exists("D:/WEBHOME/logs/"))
                System.IO.Directory.CreateDirectory("D:/WEBHOME/logs/");
            System.IO.StreamWriter sr = null;
            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    sr = System.IO.File.CreateText(filename);
                }
                else
                {
                    sr = System.IO.File.AppendText(filename);
                }
                sr.WriteLine(strMemo);
            }
            catch
            {
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        public void Valid()
        {
            var echostr = context.Request["echostr"].ToString();
            if (CheckSignature() && !string.IsNullOrEmpty(echostr))
            {
                context.Response.Write(echostr);
                context.Response.End();//推送...不然微信平台无法验证token
            }
        }

        public bool CheckSignature()
        {
            var signature = context.Request["signature"].ToString();
            var timestamp = context.Request["timestamp"].ToString();
            var nonce = context.Request["nonce"].ToString();
            var token = "congtoken";
            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
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