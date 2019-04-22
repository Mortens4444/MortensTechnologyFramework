using System.ComponentModel;

namespace Mtf.Network.Packets.Http
{
	public enum HttpMethod : byte
	{
		/// <summary>Converts the request connection to a transparent TCP/IP tunnel, usually to facilitate SSL-encrypted communication (HTTPS) through an unencrypted HTTP proxy.</summary>
		[Description("CONNECT")]
		HTTP_CONNECT,

		/// <summary>HTTP Debug method.</summary>
		[Description("DEBUG")]
		HTTP_DEBUG,

		/// <summary>Deletes the specified resource.</summary>
		[Description("DELETE")]
		HTTP_DELETE,

		/// <summary>Requests a representation of the specified resource. Requests using GET (and a few other HTTP methods) "SHOULD NOT have the significance of taking an action other than retrieval". The W3C has published guidance principles on this distinction, saying, "Web application design should be informed by the above principles, but also by the relevant limitations." Safe method.</summary>
		[Description("GET")]
		HTTP_GET,

		/// <summary>Asks for the response identical to the one that would correspond to a GET request, but without the response body. This is useful for retrieving meta-information written in response headers, without having to transport the entire content. Safe method.</summary>
		[Description("HEAD")]
		HTTP_HEAD,

		/// <summary>Returns the HTTP methods that the server supports for specified URL. This can be used to check the functionality of a web server by requesting '*' instead of a specific resource. Safe method.</summary>
		[Description("OPTIONS")]
		HTTP_OPTIONS,

		/// <summary>Is used to apply partial modifications to a resource.</summary>
		[Description("PATCH")]
		HTTP_PATCH,

		/// <summary>Submits data to be processed (e.g., from an HTML form) to the identified resource. The data is included in the body of the request. This may result in the creation of a new resource or the updates of existing resources or both.</summary>
		[Description("POST")]
		HTTP_POST,

		/// <summary>Uploads a representation of the specified resource.</summary>
		[Description("PUT")]
		HTTP_PUT,

		/// <summary>Echoes back the received request, so that a client can see what (if any) changes or additions have been made by intermediate servers. Safe method.</summary>
		[Description("TRACE")]
		HTTP_TRACE,

		/// <summary>HTTP Track method.</summary>
		[Description("TRACK")]
		HTTP_TRACK
	}
}
