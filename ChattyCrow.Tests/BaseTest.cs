using System;
using ChattyCrow;
using NUnit.Framework;
using RestSharp;
using chattycrow.enums;
using System.Collections.Generic;

namespace chattycrowtest
{
    /// <summary>
    /// Base class for cc.
    /// </summary>
    [TestFixture]
    public class BaseTest
    {
        /// <summary>
        /// Token name.
        /// </summary>
        public const string TOKEN_NAME = "Token1";

        /// <summary>
        /// Channel name.
        /// </summary>
        public const string CHANNEL_NAME = "Channel1";

        /// <summary>
        /// New test instance.
        /// </summary>
        ChattyCrowClient client = new ChattyCrowClient(TOKEN_NAME, CHANNEL_NAME);

        [Test]
        public void CorrectAssign()
        {
            Assert.AreEqual(client.GetHost(), ChattyCrowClient.DEFAULT_HOST);
            Assert.AreEqual(client.GetToken(), TOKEN_NAME);
            Assert.AreEqual(client.GetChannel(), CHANNEL_NAME);   
        }

        public struct ResultContainer
        {
            public string url;
            public Method method;
        }

        [Test]
        public void CreateNormalRequest()
        {
            // Create test dictionary
            var dictionary = new Dictionary<RequestTypes, ResultContainer>();
            dictionary.Add(RequestTypes.SendNormal, new ResultContainer() { url = "notification", method = Method.POST });
            dictionary.Add(RequestTypes.SendBatch, new ResultContainer() { url = "batch", method = Method.POST });
            dictionary.Add(RequestTypes.AddContacts, new ResultContainer() { url = "contacts", method = Method.POST });
            dictionary.Add(RequestTypes.RemoveContacts, new ResultContainer() { url = "contacts", method = Method.DELETE });
            dictionary.Add(RequestTypes.GetMessage, new ResultContainer() { url = "message", method = Method.GET });

            // Get client
            var RestCl = client.GetClient();

            // Assert + iterate
            foreach (KeyValuePair<RequestTypes, ResultContainer> entry in dictionary)
            {
                var reqest = client.createRequest(entry.Key);
                var expectedUrl = new Uri(ChattyCrowClient.DEFAULT_HOST + entry.Value.url);
                var output = RestCl.BuildUri(reqest);
                Assert.AreEqual(expectedUrl, output);
            }
        }
    }
}
