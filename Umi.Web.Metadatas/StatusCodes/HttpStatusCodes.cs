using System;

namespace Umi.Web.Metadatas.StatusCodes
{
    /// <summary>
    ///  Http 状态码
    /// </summary>
    public sealed class HttpStatusCodes
    {
        /// <summary>
        ///  状态码
        /// </summary>
        /// <value></value>
        public int Code { get; }

        /// <summary>
        /// 消息
        /// </summary>
        /// <value></value>
        public string Message { get; }

        private HttpStatusCodes(int code, string message)
        {
            Code = code;
            Message = message;
        }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 1XX


        /// <summary>
        ///  The server has received the request headers and the client should proceed to send the request body (in the case of a request for which a body needs to be sent; 
        /// for example, a POST request). 
        /// Sending a large request body to a server after a request has been rejected for inappropriate headers would be inefficient. 
        /// To have a server check the request's headers, a client must send Expect: 
        /// 100-continue as a header in its initial request and receive a 100 Continue status code in response before sending the body. 
        /// If the client receives an error code such as 403 (Forbidden) or 405 (Method Not Allowed) then it shouldn't send the request's body. 
        /// The response 417 Expectation Failed indicates that the request should be repeated without the Expect header as it indicates that
        ///  the server doesn't support expectations (this is the case, for example, of HTTP/1.0 servers)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes CONTINUE = new HttpStatusCodes(100, "Continue");

        /// <summary>
        /// The requester has asked the server to switch protocols and the server has agreed to do so.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes SWITCHING_PROTOCOLS = new HttpStatusCodes(101, "Switching Protocols");

        /// <summary>
        /// A WebDAV request may contain many sub-requests involving file operations, 
        /// requiring a long time to complete the request. 
        /// This code indicates that the server has received and is processing the request, 
        /// but no response is available yet.
        ///  This prevents the client from timing out and assuming the request was lost.
        /// (RFC 2518)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes PROCESSING = new HttpStatusCodes(102, "Processing");

        /// <summary>
        /// Used to return some response headers before final HTTP message.
        /// (RFC 8297)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes EARLY_HINTS = new HttpStatusCodes(103, "Early Hints");

        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 2XX

        /// <summary>
        /// Standard response for successful HTTP requests. 
        /// The actual response will depend on the request method used. 
        /// In a GET request, the response will contain an entity corresponding to the requested resource.
        ///  In a POST request, the response will contain an entity describing or containing the result of the action.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes OK = new HttpStatusCodes(200, "OK");

        /// <summary>
        /// The request has been fulfilled, resulting in the creation of a new resource.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes CREATED = new HttpStatusCodes(201, "Created");

        /// <summary>
        /// The request has been accepted for processing, but the processing has not been completed. 
        /// The request might or might not be eventually acted upon, and may be disallowed when processing occurs.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes ACCEPTED = new HttpStatusCodes(202, "Accepted");

        /// <summary>
        /// The server is a transforming proxy (e.g. a Web accelerator) that received a 200 OK from its origin, 
        /// but is returning a modified version of the origin's response.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes NON_AUTHORITATIVE_INFORMATION = new HttpStatusCodes(203, "Non-Authoritative Information");

        /// <summary>
        /// The server successfully processed the request and is not returning any content.[
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes NO_CONTENT = new HttpStatusCodes(204, "No Content");

        /// <summary>
        /// The server successfully processed the request, but is not returning any content. Unlike a 204 response, 
        /// this response requires that the requester reset the document view.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes RESET_CONTENT = new HttpStatusCodes(205, "Reset Content");


        /// <summary>
        /// The server is delivering only part of the resource (byte serving) due to a range header sent by the client. 
        /// The range header is used by HTTP clients to enable resuming of interrupted downloads, 
        /// or split a download into multiple simultaneous streams.        /// 
        /// (RFC 7233)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes PARTIAL_CONTENT = new HttpStatusCodes(206, "Partial Content");

        /// <summary>
        /// The message body that follows is by default an XML message and can contain a number of separate response codes, 
        /// depending on how many sub-requests were made.
        /// (RFC 4918)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes MULTI_STATUS = new HttpStatusCodes(207, "Multi-Status");

        /// <summary>
        /// The members of a DAV binding have already been 
        /// enumerated in a preceding part of the (multistatus) response, 
        /// and are not being included again.
        /// (RFC 5842)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes ALREADY_REPORTED = new HttpStatusCodes(208, "Already Reported");

        /// <summary>
        /// The server has fulfilled a request for the resource, 
        /// and the response is a representation of the result of 
        /// one or more instance-manipulations applied to the current instance.
        /// (RFC 3229)
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes IM_USED = new HttpStatusCodes(226, "IM Used ");

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 3XX


        /// <summary>
        /// Indicates multiple options for the resource from which the client may choose (via agent-driven content negotiation). 
        /// For example, this code could be used to present multiple video format options, 
        /// to list files with different filename extensions, or to suggest word-sense disambiguation.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes MULTIPLE_CHOICES = new HttpStatusCodes(300, "Multiple Choices");

        /// <summary>
        /// This and all future requests should be directed to the given URI.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes MOVED_PERMANENTLY = new HttpStatusCodes(301, "Moved Permanently");

        /// <summary>
        /// Tells the client to look at (browse to) another URL. 
        /// 302 has been superseded by 303 and 307. 
        /// This is an example of industry practice contradicting the standard. 
        /// The HTTP/1.0 specification (RFC 1945) required the client to perform a temporary redirect (the original describing phrase was "Moved Temporarily"),
        ///  but popular browsers implemented 302 with the functionality of a 303 See Other. 
        /// Therefore, HTTP/1.1 added status codes 303 and 307 to distinguish between the two behaviours.
        ///  However, some Web applications and frameworks use the 302 status code as if it were the 303.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes FOUND = new HttpStatusCodes(302, "Found");

        /// <summary>
        /// The response to the request can be found under another URI using the GET method. 
        /// When received in response to a POST (or PUT/DELETE), 
        /// the client should presume that the server has received the data and should issue a 
        /// new GET request to the given URI.
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes SEE_OTHER = new HttpStatusCodes(303, "See Other");

        /// <summary>
        /// Indicates that the resource has not been modified 
        /// since the version specified by the request headers 
        /// If-Modified-Since or If-None-Match. In such case, 
        /// there is no need to retransmit the resource since the client still has a previously-downloaded copy.
        /// RFC 7232
        /// </summary>
        /// <returns></returns>
        public static readonly HttpStatusCodes NOT_MODIFIED = new HttpStatusCodes(304, "Not Modified");

        /// <summary>The requested resource is available only through a proxy, the address for which is provided in the response. For security reasons, many HTTP clients (such as Mozilla Firefox and Internet Explorer) do not obey this status code.(since HTTP/1.1)</summary>
        public static readonly HttpStatusCodes USE_PROXY = new HttpStatusCodes(305, "Use Proxy ");

        /// <summary>No longer used. Originally meant "Subsequent requests should use the specified proxy."</summary>
        public static readonly HttpStatusCodes SWITCH_PROXY = new HttpStatusCodes(306, "Switch Proxy");
        /// <summary>In this case, the request should be repeated with another URI; however, future requests should still use the original URI. In contrast to how 302 was historically implemented, the request method is not allowed to be changed when reissuing the original request. For example, a POST request should be repeated using another POST request.(since HTTP/1.1)</summary>
        public static readonly HttpStatusCodes TEMPORARY_REDIRECT = new HttpStatusCodes(307, "Temporary Redirect ");
        /// <summary>The request and all future requests should be repeated using another URI. 307 and 308 parallel the behaviors of 302 and 301, but do not allow the HTTP method to change. So, for example, submitting a form to a permanently redirected resource may continue smoothly.(RFC 7538)</summary>
        public static readonly HttpStatusCodes PERMANENT_REDIRECT = new HttpStatusCodes(308, "Permanent Redirect ");

        /// <summary>The server cannot or will not process the request due to an apparent client error (e.g., malformed request syntax, size too large, invalid request message framing, or deceptive request routing).</summary>
        public static readonly HttpStatusCodes BAD_REQUEST = new HttpStatusCodes(400, "Bad Request");

        /// <summary>
        /// Similar to 403 Forbidden, but specifically for use when authentication is required and has failed or has not yet been provided. The response must include a WWW-Authenticate header field containing a challenge applicable to the requested resource. See Basic access authentication and Digest access authentication. 401 semantically means "unauthorised", the user does not have valid authentication credentials for the target resource.
        /// Note: Some sites incorrectly issue HTTP 401 when an IP address is banned from the website (usually the website domain) and that specific address is refused permission to access a website.(RFC 7235)
        /// </summary>
        public static readonly HttpStatusCodes UNAUTHORIZED = new HttpStatusCodes(401, "Unauthorized ");
        /// <summary>Reserved for future use. The original intention was that this code might be used as part of some form of digital cash or micropayment scheme, as proposed, for example, by GNU Taler, but that has not yet happened, and this code is not usually used. Google Developers API uses this status if a particular developer has exceeded the daily limit on requests. Sipgate uses this code if an account does not have sufficient funds to start a call. Shopify uses this code when the store has not paid their fees and is temporarily disabled.</summary>
        public static readonly HttpStatusCodes PAYMENT_REQUIRED = new HttpStatusCodes(402, "Payment Required");
        /// <summary>The request was valid, but the server is refusing action. The user might not have the necessary permissions for a resource, or may need an account of some sort. This code is also typically used if the request provided authentication via the WWW-Authenticate header field, but the server did not accept that authentication.</summary>
        public static readonly HttpStatusCodes FORBIDDEN = new HttpStatusCodes(403, "Forbidden");
        /// <summary>The requested resource could not be found but may be available in the future. Subsequent requests by the client are permissible.</summary>
        public static readonly HttpStatusCodes NOT_FOUND = new HttpStatusCodes(404, "Not Found");
        /// <summary>A request method is not supported for the requested resource; for example, a GET request on a form that requires data to be presented via POST, or a PUT request on a read-only resource.</summary>
        public static readonly HttpStatusCodes METHOD_NOT_ALLOWED = new HttpStatusCodes(405, "Method Not Allowed");
        /// <summary>The requested resource is capable of generating only content not acceptable according to the Accept headers sent in the request. See Content negotiation.</summary>
        public static readonly HttpStatusCodes NOT_ACCEPTABLE = new HttpStatusCodes(406, "Not Acceptable");
        /// <summary>The client must first authenticate itself with the proxy.(RFC 7235)</summary>`
        public static readonly HttpStatusCodes PROXY_AUTHENTICATION_REQUIRED = new HttpStatusCodes(407, "Proxy Authentication Required ");
        /// <summary>The server timed out waiting for the request. According to HTTP specifications: "The client did not produce a request within the time that the server was prepared to wait. The client MAY repeat the request without modifications at any later time."</summary>
        public static readonly HttpStatusCodes REQUEST_TIMEOUT = new HttpStatusCodes(408, "Request Timeout");
        /// <summary>Indicates that the request could not be processed because of conflict in the current state of the resource, such as an edit conflict between multiple simultaneous updates.</summary>
        public static readonly HttpStatusCodes CONFLICT = new HttpStatusCodes(409, "Conflict");
        /// <summary>Indicates that the resource requested is no longer available and will not be available again. This should be used when a resource has been intentionally removed and the resource should be purged. Upon receiving a 410 status code, the client should not request the resource in the future. Clients such as search engines should remove the resource from their indices. Most use cases do not require clients and search engines to purge the resource, and a "404 Not Found" may be used instead.</summary>
        public static readonly HttpStatusCodes GONE = new HttpStatusCodes(410, "Gone");
        /// <summary>The request did not specify the length of its content, which is required by the requested resource.</summary>
        public static readonly HttpStatusCodes LENGTH_REQUIRED = new HttpStatusCodes(411, "Length Required");
        /// <summary>The server does not meet one of the preconditions that the requester put on the request header fields. (RFC 7232)</summary>
        public static readonly HttpStatusCodes PRECONDITION_FAILED = new HttpStatusCodes(412, "Precondition Failed ");
        /// <summary>The request is larger than the server is willing or able to process. Previously called "Request Entity Too Large".(RFC 7231)</summary>
        public static readonly HttpStatusCodes PAYLOAD_TOO_LARGE = new HttpStatusCodes(413, "Payload Too Large ");
        /// <summary>The URI provided was too long for the server to process. Often the result of too much data being encoded as a query-string of a GET request, in which case it should be converted to a POST request. Called "Request-URI Too Long" previously.(RFC 7231)</summary>
        public static readonly HttpStatusCodes URI_TOO_LONG = new HttpStatusCodes(414, "URI Too Long ");
        /// <summary>The request entity has a media type which the server or resource does not support. For example, the client uploads an image as image/svg+xml, but the server requires that images use a different format.(RFC 7231)</summary>
        public static readonly HttpStatusCodes UNSUPPORTED_MEDIA_TYPE = new HttpStatusCodes(415, "Unsupported Media Type ");
        /// <summary>The client has asked for a portion of the file (byte serving), but the server cannot supply that portion. For example, if the client asked for a part of the file that lies beyond the end of the file. Called "Requested Range Not Satisfiable" previously.(RFC 7233)</summary>
        public static readonly HttpStatusCodes RANGE_NOT_SATISFIABLE = new HttpStatusCodes(416, "Range Not Satisfiable ");
        /// <summary>The server cannot meet the requirements of the Expect request-header field.</summary>
        public static readonly HttpStatusCodes EXPECTATION_FAILED = new HttpStatusCodes(417, "Expectation Failed");
        /// <summary>This code was defined in 1998 as one of the traditional IETF April Fools' jokes, in RFC 2324, Hyper Text Coffee Pot Control Protocol, and is not expected to be implemented by actual HTTP servers. The RFC specifies this code should be returned by teapots requested to brew coffee. This HTTP status is used as an Easter egg in some websites, including Google.com.(RFC 2324, RFC 7168)</summary>
        public static readonly HttpStatusCodes IM_A_TEAPOT = new HttpStatusCodes(418, "I'm a teapot");
        /// <summary>The request was directed at a server that is not able to produce a response (for example because of connection reuse).(RFC 7540)</summary>
        public static readonly HttpStatusCodes MISDIRECTED_REQUEST = new HttpStatusCodes(421, "Misdirected Request ");
        /// <summary>The request was well-formed but was unable to be followed due to semantic errors.(WebDAV; RFC 4918)</summary>
        public static readonly HttpStatusCodes UNPROCESSABLE_ENTITY = new HttpStatusCodes(422, "Unprocessable Entity ");
        /// <summary>The resource that is being accessed is locked.(WebDAV; RFC 4918)</summary>
        public static readonly HttpStatusCodes LOCKED = new HttpStatusCodes(423, "Locked ");
        /// <summary>The request failed because it depended on another request and that request failed (e.g., a PROPPATCH).(WebDAV; RFC 4918)</summary>
        public static readonly HttpStatusCodes FAILED_DEPENDENCY = new HttpStatusCodes(424, "Failed Dependency ");
        /// <summary>Indicates that the server is unwilling to risk processing a request that might be replayed.(RFC 8470)</summary>
        public static readonly HttpStatusCodes TOO_EARLY = new HttpStatusCodes(425, "Too Early ");
        /// <summary>The client should switch to a different protocol such as TLS/1.0, given in the Upgrade header field.</summary>
        public static readonly HttpStatusCodes UPGRADE_REQUIRED = new HttpStatusCodes(426, "Upgrade Required");
        /// <summary>The origin server requires the request to be conditional. Intended to prevent the 'lost update' problem, where a client GETs a resource's state, modifies it, and PUTs it back to the server, when meanwhile a third party has modified the state on the server, leading to a conflict.(RFC 6585)</summary>
        public static readonly HttpStatusCodes PRECONDITION_REQUIRED = new HttpStatusCodes(428, "Precondition Required ");
        /// <summary>The user has sent too many requests in a given amount of time. Intended for use with rate-limiting schemes.(RFC 6585)</summary>
        public static readonly HttpStatusCodes TOO_MANY_REQUESTS = new HttpStatusCodes(429, "Too Many Requests ");
        /// <summary>The server is unwilling to process the request because either an individual header field, or all the header fields collectively, are too large.(RFC 6585)</summary>
        public static readonly HttpStatusCodes REQUEST_HEADER_FIELDS_TOO_LARGE = new HttpStatusCodes(431, "Request Header Fields Too Large ");
        /// <summary>A server operator has received a legal demand to deny access to a resource or to a set of resources that includes the requested resource. The code 451 was chosen as a reference to the novel Fahrenheit 451 (see the Acknowledgements in the RFC).(RFC 7725)</summary>
        public static readonly HttpStatusCodes UNAVAILABLE_FOR_LEGAL_REASONS = new HttpStatusCodes(451, "Unavailable For Legal Reasons ");

        /// <summary>A generic error message, given when an unexpected condition was encountered and no more specific message is suitable.</summary>
        public static readonly HttpStatusCodes INTERNAL_SERVER_ERROR = new HttpStatusCodes(500, "Internal Server Error");
        /// <summary>The server either does not recognize the request method, or it lacks the ability to fulfil the request. Usually this implies future availability (e.g., a new feature of a web-service API).</summary>
        public static readonly HttpStatusCodes NOT_IMPLEMENTED = new HttpStatusCodes(501, "Not Implemented");
        /// <summary>The server was acting as a gateway or proxy and received an invalid response from the upstream server.</summary>
        public static readonly HttpStatusCodes BAD_GATEWAY = new HttpStatusCodes(502, "Bad Gateway");
        /// <summary>The server cannot handle the request (because it is overloaded or down for maintenance). Generally, this is a temporary state.</summary>
        public static readonly HttpStatusCodes SERVICE_UNAVAILABLE = new HttpStatusCodes(503, "Service Unavailable");
        /// <summary>The server was acting as a gateway or proxy and did not receive a timely response from the upstream server.</summary>
        public static readonly HttpStatusCodes GATEWAY_TIMEOUT = new HttpStatusCodes(504, "Gateway Timeout");
        /// <summary>The server does not support the HTTP protocol version used in the request.</summary>
        public static readonly HttpStatusCodes HTTP_VERSION_NOT_SUPPORTED = new HttpStatusCodes(505, "HTTP Version Not Supported");
        /// <summary>Transparent content negotiation for the request results in a circular reference. (RFC 2295)</summary>
        public static readonly HttpStatusCodes VARIANT_ALSO_NEGOTIATES = new HttpStatusCodes(506, "Variant Also Negotiates ");
        /// <summary>The server is unable to store the representation needed to complete the request.(WebDAV; RFC 4918)</summary>
        public static readonly HttpStatusCodes INSUFFICIENT_STORAGE = new HttpStatusCodes(507, "Insufficient Storage ");
        /// <summary>The server detected an infinite loop while processing the request (sent instead of 208 Already Reported).(WebDAV; RFC 5842)</summary>
        public static readonly HttpStatusCodes LOOP_DETECTED = new HttpStatusCodes(508, "Loop Detected ");
        /// <summary>Further extensions to the request are required for the server to fulfil it. (RFC 2774)</summary>
        public static readonly HttpStatusCodes NOT_EXTENDED = new HttpStatusCodes(510, "Not Extended ");
        /// <summary>The client needs to authenticate to gain network access. Intended for use by intercepting proxies used to control access to the network (e.g., "captive portals" used to require agreement to Terms of Service before granting full Internet access via a Wi-Fi hotspot).(RFC 6585)</summary>
        public static readonly HttpStatusCodes NETWORK_AUTHENTICATION_REQUIRED = new HttpStatusCodes(511, "Network Authentication Required ");
    }
}
