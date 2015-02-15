using chattycrow.enums;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChattyCrow
{
    /// <summary>
    /// ChattyCrow base class.
    /// </summary>
    public class ChattyCrowClient
    {
        /// <summary>
        /// Default host end point.
        /// </summary>
        public const string DEFAULT_HOST = "https://chattycrow.com/api/v1/";

        /// <summary>
        /// Persist default token.
        /// </summary>
        private string DefaultToken;

        /// <summary>
        /// Persist default channel.
        /// </summary>
        private string DefaultChannel;

        /// <summary>
        /// Default host.
        /// </summary>
        private string DefaultHost;

        /// <summary>
        /// Create basic chattycrow class.
        /// </summary>
        /// <param name="DefaultToken">Default token</param>
        /// <param name="DefaultChannel">Default channel</param>
        /// <param name="DefaultHost">Default host (optional)</param>
        public ChattyCrowClient(string DefaultToken, string DefaultChannel, string DefaultHost = DEFAULT_HOST)
        {
            this.DefaultHost = DefaultHost;
            this.DefaultToken = DefaultToken;
            this.DefaultChannel = DefaultChannel;
        }

        /// <summary>
        /// Method sets default values for request to cc api.
        /// </summary>
        /// <param name="type">Request type</param>
        /// <param name="request">Request instance</param>
        /// <param name="channel">Optional channel</param>
        /// <param name="token">Optional token</param>
        public RestRequest createRequest(RequestTypes type, string channel = "", string token = "", string host = "")
        {
            // Assign host if non optional
            string url = host.Length > 0 ? host : this.DefaultHost;

            // Ensure last char will be / for better concatenation
            if (url[url.Length - 1] != '/')
                url += '/';

            // Prepare method
            Method requestMethod = Method.POST;

            switch (type)
            {
                case RequestTypes.SendNormal:
                    url += "notification";
                    break;

                case RequestTypes.SendBatch:
                    url += "batch";
                    break;

                case RequestTypes.GetMessage:
                    url += "message";
                    requestMethod = Method.GET;
                    break;

                case RequestTypes.AddContacts:
                    url += "contacts";
                    break;

                case RequestTypes.RemoveContacts:
                    url += "contacts";
                    requestMethod = Method.DELETE;
                    break;
            }

            // Create request
            RestRequest req = new RestRequest(url, requestMethod);

            // Set channel and token headers
            req.AddHeader("Token", token.Length > 0 ? token : DefaultToken);
            req.AddHeader("Channel", channel.Length > 0 ? channel : DefaultChannel);

            // Return request
            return req;
        }


        /// <summary>
        /// Return current token.
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return this.DefaultToken;
        }

        /// <summary>
        /// Return current channel.
        /// </summary>
        /// <returns></returns>
        public string GetChannel()
        {
            return this.DefaultChannel;
        }

        /// <summary>
        /// Return current host.
        /// </summary>
        /// <returns></returns>
        public string GetHost()
        {
            return this.DefaultHost;
        }
    }
}
