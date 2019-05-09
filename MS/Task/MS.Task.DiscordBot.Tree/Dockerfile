#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1809 AS build
WORKDIR /src
COPY ["MS/Task/MS.Task.DiscordBot.Tree/MS.Task.DiscordBot.Tree.csproj", "MS/Task/MS.Task.DiscordBot.Tree/"]
COPY ["API/V1/CS/API.V1.RPC.Client.CS/API.V1.RPC.Client.CS.csproj", "API/V1/CS/API.V1.RPC.Client.CS/"]
COPY ["Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/Tools.Environment.RabbitMQ.csproj", "Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/"]
COPY ["Tools/Environment/Tools.EnvironmentExt/Tools.EnvironmentExt.csproj", "Tools/Environment/Tools.EnvironmentExt/"]
COPY ["API/V1/CS/API.V1.RPC.Server.CS/API.V1.RPC.Server.CS.csproj", "API/V1/CS/API.V1.RPC.Server.CS/"]
RUN dotnet restore "MS/Task/MS.Task.DiscordBot.Tree/MS.Task.DiscordBot.Tree.csproj"
COPY . .
WORKDIR "/src/MS/Task/MS.Task.DiscordBot.Tree"
RUN dotnet build "MS.Task.DiscordBot.Tree.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MS.Task.DiscordBot.Tree.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MS.Task.DiscordBot.Tree.dll"]