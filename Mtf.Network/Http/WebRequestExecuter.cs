using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Mtf.Network.Http
{
    public class WebRequestExecuter
    {
        private const int NotFound = -1;

        public string GetHttpWebResponse(string uri, string method = "GET",
            string contentType = "text/xml; charset=\"utf-8\"")
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = method;
            webRequest.ContentType = contentType;
            webRequest.ServicePoint.Expect100Continue = false;

            var asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            // TODO Async read
            string result;
            using (var webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        public List<string> GetUriResponse(string uri)
        {
            var result = new List<string>();
            var request = WebRequest.Create(uri);
            using (var response = request.GetResponse())
            {
                using (var receiveStream = response.GetResponseStream())
                {
                    if (receiveStream != null)
                    {
                        using (var sr = new StreamReader(receiveStream))
                        {
                            while (sr.Peek() != NotFound)
                            {
                                result.Add(sr.ReadLine());
                            }
                            sr.Close();
                        }
                        receiveStream.Close();
                    }
                }
                response.Close();
            }

            return result;
        }

        public void DownloadFile(string link, string filename)
        {
            var client = new WebClient();
            client.DownloadFile(new Uri(link), filename);
        }

        public void DownloadFileAsync(string link, string filename, AsyncCompletedEventHandler completed)
        {
            DownloadFileAsync(link, filename, null, completed);
        }

        public void DownloadFileAsync(string link, string filename, DownloadProgressChangedEventHandler progressChanged, AsyncCompletedEventHandler completed)
        {
            var client = new WebClient();
            client.DownloadFileCompleted += completed;
            client.DownloadProgressChanged += progressChanged;
            client.DownloadFileAsync(new Uri(link), filename);
        }
    }
}