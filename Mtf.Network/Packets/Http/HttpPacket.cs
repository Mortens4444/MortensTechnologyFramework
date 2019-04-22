using System.Text;
using Mtf.Utils.EnumExtensions;

namespace Mtf.Network.Packets.Http
{
    public class HttpPacket
    {
        private string method;
        private string uri = "/";
        private string hostOrIp = "127_0_0_1";
        private byte majorProtocolVersion = 1;
        private byte minorProtocolVersion = 1;

        public bool KeepAliveConnection { get; set; } = false;

        public string Accept { get; set; } = "*.*"; // "text/plain,text/html";

        public string AcceptCharset { get; set; } = "*"; // iso-8859-2, unicode-1-1;q=0.8

        public string AcceptEncoding { get; set; } = "*"; // compress, gzip

        public string AcceptLanguage { get; set; } = "en"; // da, en-gb;q=0.8, en;q=0.7

        public string AcceptRanges { get; set; } = "none"; // bytes

        public HttpPacket(HttpMethod httpMethod)
        {
            Initialize(httpMethod);
        }

        public HttpPacket(HttpMethod httpMethod, string hostOrIp, string uri)
        {
            Initialize(httpMethod);
            this.hostOrIp = hostOrIp;
            this.uri = uri;
        }

        public HttpPacket(HttpMethod httpMethod, string url)
        {
            Initialize(httpMethod);
            URL = url;
        }

        public HttpPacket(HttpMethod httpMethod, string hostOrIp, string uri, HttpProtocolVersion protocolVersion)
        {
            Initialize(httpMethod);
            this.hostOrIp = hostOrIp;
            this.uri = uri;
            ProtocolVersion = protocolVersion;
        }

        public HttpPacket(HttpMethod httpMethod, string url, HttpProtocolVersion protocolVersion)
        {
            Initialize(httpMethod);
            URL = url;
            ProtocolVersion = protocolVersion;
        }

        public string URL
        {
            get
            {
                return $"http://{hostOrIp}{uri}";
            }
            set
            {
                var url = value.Replace("http://", "");
                var index = url.IndexOf('/');
                if (index < 0)
                {
                    hostOrIp = url;
                    uri = "/";
                }
                else
                {
                    hostOrIp = url.Substring(0, index);
                    uri = url.Substring(index);
                }
            }
        }

        private void Initialize(HttpMethod httpMethod)
        {
            method = httpMethod.GetDescription();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{method} {uri} HTTP/{majorProtocolVersion}.{minorProtocolVersion}");
            sb.AppendLine($"Host: {hostOrIp}");
            sb.AppendLine($"Accept: {Accept}");
            sb.AppendLine($"Accept-Charset: {AcceptCharset}");
            sb.AppendLine($"Accept-Encoding: {AcceptEncoding}");
            sb.AppendLine($"Accept-Language: {AcceptLanguage}");
            sb.AppendLine($"Accept-Ranges: {AcceptRanges}");

            if (KeepAliveConnection)
            {
                sb.AppendLine("Connection: Keep-Alive");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public HttpProtocolVersion ProtocolVersion
        {
            get
            {
                switch (majorProtocolVersion)
                {
                    case 1:
                        switch (minorProtocolVersion)
                        {
                            case 0:
                                return HttpProtocolVersion.HTTP_1_0;
                            case 1:
                                return HttpProtocolVersion.HTTP_1_1;
                            case 2:
                                return HttpProtocolVersion.HTTP_1_2;
                            default:
                                return HttpProtocolVersion.Unknown;
                        }
                    case 2:
                        switch (minorProtocolVersion)
                        {
                            case 0:
                                return HttpProtocolVersion.HTTP_2_0;
                            default:
                                return HttpProtocolVersion.Unknown;
                        }
                    default:
                        return HttpProtocolVersion.Unknown;
                }
            }
            set
            {
                switch (value)
                {
                    case HttpProtocolVersion.HTTP_1_0:
                        majorProtocolVersion = 1;
                        minorProtocolVersion = 0;
                        break;
                    case HttpProtocolVersion.HTTP_1_1:
                        majorProtocolVersion = 1;
                        minorProtocolVersion = 1;
                        break;
                    case HttpProtocolVersion.HTTP_1_2:
                        majorProtocolVersion = 1;
                        minorProtocolVersion = 2;
                        break;
                    case HttpProtocolVersion.HTTP_2_0:
                        majorProtocolVersion = 2;
                        minorProtocolVersion = 0;
                        break;
                }
            }
        }
    }
}