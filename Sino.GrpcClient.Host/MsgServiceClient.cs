using Grpc.Core;
using Sino.GrpcService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sino.GrpcClient.Host
{
    public static class MsgServiceClient
    {
        private static Channel _channel;
        private static MsgService.MsgServiceClient _client;

        static MsgServiceClient()
        {
            var cacert = File.ReadAllText("server.crt");
            var ssl = new SslCredentials(cacert);
            var channOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.SslTargetNameOverride,"root")
            };
            _channel = new Channel("localhost:9007", ssl, channOptions);
            _client = new MsgService.MsgServiceClient(_channel);
        }

        public static GetMsgListReply GetList(int userId, string title, long startTime, long endTime)
        {
            return _client.GetList(new GetMsgListRequest
            {
                UserId = userId,
                Title = title,
                StartTime = startTime,
                EndTime = endTime
            });
        }
    }
}
