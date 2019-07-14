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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    }
}
