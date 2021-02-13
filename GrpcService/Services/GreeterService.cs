using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private static List<string> _names = new();

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _names.Add(request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task StreamNames(StreamNamesRequest request,
            IServerStreamWriter<NameResponse> responseStream, ServerCallContext context)
        {
            var nameResponses = _names.Select(name => new NameResponse {Name = name});

            foreach (var response in nameResponses)
            {
                await responseStream.WriteAsync(response);
            }
        }
    }
}
