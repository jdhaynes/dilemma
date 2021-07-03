using System;
using System.Runtime.InteropServices;
using Grpc.Core;

namespace IdentitySvc.Grpc
{
    public class Client
    {
        public static void ClientMain()
        {
            Channel channel = new Channel("localhost", 25289, ChannelCredentials.Insecure);
            var client = new Identity.IdentityClient(channel);

            var response = client.AuthenticateUser(new AuthenticateUserRequest()
            {
                Token = "TEST_TOKEN"
            });
            
            Console.WriteLine(response);
        }
    }
}