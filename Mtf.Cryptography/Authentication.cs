using System.Text;
using Mtf.Core.Cryptography;

namespace Mtf.Cryptography
{
    public class Authentication : IAuthentication
    {
        private readonly IBase64 base64;

        public Authentication(IBase64 base64)
        {
            this.base64 = base64;
        }

        /// <summary>
        /// PLAIN authentication (RFC 2595)
        /// </summary>
        /// <param name="username">Username (e.g.: user@mail.net)</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string PLAIN(string username, string password)
        {
            var plain = new StringBuilder();
            plain.Append((char)0);
            plain.Append(username);
            plain.Append((char)0);
            plain.Append(password);
            return base64.Encode(plain.ToString());
        }

        /// <summary>
        /// LOGIN authentication
        /// </summary>
        /// <param name="username">Username (e.g.: user@mail.net)</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string LOGIN(string username, string password)
        {
            var login = new StringBuilder(base64.Encode(username, true));
            login.Append("\r\n");
            login.Append(base64.Encode(password, true));
            return login.ToString();
        }

        /// <summary>
        /// HTTP-BASIC authentication (RFC 1945, RFC 2626, RFC 2617)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string HTTP_BASIC(string username, string password)
        {
            var http_basic = new StringBuilder(username);
            http_basic.Append(':');
            http_basic.Append(password);
            return base64.Encode(http_basic.ToString());
        }

        /// <summary>
        /// Digest Access Authentication (RFC 2069)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string Digest_Access(string username, string password, string realm, string nonce, string http_method, string uri)
        {
            var A1 = new StringBuilder();
            A1.Append(username);
            A1.Append(':');
            A1.Append(realm);
            A1.Append(':');
            A1.Append(password);
            var HA1 = Hash.MD5_Hash(A1.ToString());

            var A2 = new StringBuilder();
            A2.Append(http_method);
            A1.Append(':');
            A2.Append(uri);
            var HA2 = Hash.MD5_Hash(A2.ToString());

            var pre_rp = new StringBuilder();
            pre_rp.Append(HA1);
            pre_rp.Append(':');
            pre_rp.Append(nonce);
            pre_rp.Append(':');
            pre_rp.Append(HA2);
            return Hash.MD5_Hash(pre_rp.ToString());
        }

        /// <summary>
        /// Basic and Digest Access Authentication (RFC 2617)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string Basic_And_Digest_Access(string username, string password, string realm, string nonce, string qop, string http_method, string uri, string entity_body, string nonce_count, string client_nonce)
        {
            var A1 = new StringBuilder();
            A1.Append(username);
            A1.Append(':');
            A1.Append(realm);
            A1.Append(':');
            A1.Append(password);
            var HA1 = Hash.MD5_Hash(A1.ToString());

            var A2 = new StringBuilder();
            A2.Append(http_method);
            A1.Append(':');
            A2.Append(uri);

            if (qop.IndexOf("auth_int") > 0) // auth-int
                A2.Append(Hash.MD5_Hash(entity_body));

            var HA2 = Hash.MD5_Hash(A2.ToString());

            var pre_rp = new StringBuilder();
            pre_rp.Append(HA1);
            pre_rp.Append(':');
            pre_rp.Append(nonce);
            pre_rp.Append(':');

            if (qop.IndexOf("auth") > 0) // auth or auth-int
            {
                pre_rp.Append(nonce_count);
                pre_rp.Append(':');
                pre_rp.Append(client_nonce);
                pre_rp.Append(':');
                pre_rp.Append(qop);
                pre_rp.Append(':');
            }
            pre_rp.Append(HA2);
            return Hash.MD5_Hash(pre_rp.ToString());
        }
    }
}