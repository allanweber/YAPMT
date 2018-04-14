using Microsoft.AspNetCore.TestHost;
using System;

namespace YAPMT.Framework.Test
{
    public static class TestServerExtensions
    {
        public static TInstance GetService<TInstance>(this TestServer testServer)
        {
            if (testServer == null)
                throw new ArgumentNullException(nameof(testServer));

            return (TInstance)testServer.Host.Services.GetService(typeof(TInstance));
        }
    }
}
