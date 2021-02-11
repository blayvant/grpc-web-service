### NOTES

The project was built using the .NET Core gRPC template and the Grpc.AspNetCore.Web package was added
to enable the handling of browser requests

When compiled, the classes associated with the services and messages defined in the .proto files will be placed in *obj/Debug/net5.0/Protos*

In order for protobuf compilation to work, .proto files must listed in the .csproj file

Service classes containing business logic should be placed in the *Services* directory and inherit from the `[ServiceName].[ServiceName]Base` classes
e.g. `Greeter.GreeterBase` which are automatically generated during compilation
