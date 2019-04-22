using System.IO;
using System.Net;
using System.Xml;

namespace Mtf.Network.Soap
{
    public class SoapClient
    {
        public string ExecuteFunction(XmlDocument soapEnvelop, string url, string action = null, int timeout = 30000)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Timeout = timeout;
            if (action != null)
            {
                webRequest.Headers.Add("SOAPACTION", action);
            }
            webRequest.ContentType = "text/xml; charset=\"utf-8\"";
            webRequest.Method = "POST";
            webRequest.ServicePoint.Expect100Continue = false;

            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelop.Save(stream);
            }

            var asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            string soapResult;
            using (var webResponse = webRequest.EndGetResponse(asyncResult))
            {
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                {
                    return null;
                }
                using (var readed = new StreamReader(responseStream))
                {
                    soapResult = readed.ReadToEnd();
                }
            }
            return soapResult;
        }
    }
}