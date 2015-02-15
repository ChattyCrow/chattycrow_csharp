using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChattyCrow;

namespace chattycrowtest
{
    /// <summary>
    /// Base class for cc.
    /// </summary>
    [TestClass]
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

        [TestMethod]
        public void CorrectAssign()
        {
            Assert.AreEqual(client.GetHost(), ChattyCrowClient.DEFAULT_HOST);
            Assert.AreEqual(client.GetToken(), TOKEN_NAME);
            Assert.AreEqual(client.GetChannel(), CHANNEL_NAME);   
        }
    }
}
