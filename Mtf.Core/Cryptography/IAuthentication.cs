namespace Mtf.Core.Cryptography
{
    public interface IAuthentication
    {
        /// <summary>
        /// PLAIN authentication (RFC 2595)
        /// </summary>
        /// <param name="username">Username (e.g.: user@mail.net)</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string PLAIN(string username, string password);

        /// <summary>
        /// LOGIN authentication
        /// </summary>
        /// <param name="username">Username (e.g.: user@mail.net)</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string LOGIN(string username, string password);

        /// <summary>
        /// HTTP-BASIC authentication (RFC 1945, RFC 2626, RFC 2617)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string HTTP_BASIC(string username, string password);

        /// <summary>
        /// Digest Access Authentication (RFC 2069)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string Digest_Access(string username, string password, string realm, string nonce, string http_method, string uri);

        /// <summary>
        /// Basic and Digest Access Authentication (RFC 2617)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string Basic_And_Digest_Access(string username, string password, string realm, string nonce, string qop, string http_method, string uri, string entity_body, string nonce_count, string client_nonce);
    }
}