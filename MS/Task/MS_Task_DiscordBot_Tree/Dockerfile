FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MS/Task/MS_Task_DiscordBot_Tree/MS_Task_DiscordBot_Tree.csproj", "MS/Task/MS_Task_DiscordBot_Tree/"]
COPY ["API/V1/CS/API_V1_RPC_Server_CS/API_V1_RPC_Server_CS.csproj", "API/V1/CS/API_V1_RPC_Server_CS/"]
COPY ["Tools/Environment/RabbitMQ/Tools_Environment_RabbitMQ/Tools_Environment_RabbitMQ.csproj", "Tools/Environment/RabbitMQ/Tools_Environment_RabbitMQ/"]
COPY ["Tools/Environment/Tools_EnvironmentExt/Tools_EnvironmentExt.csproj", "Tools/Environment/Tools_EnvironmentExt/"]
COPY ["API/V1/CS/API_V1_RPC_Client_CS/API_V1_RPC_Client_CS.csproj", "API/V1/CS/API_V1_RPC_Client_CS/"]
RUN dotnet restore "MS/Task/MS_Task_DiscordBot_Tree/MS_Task_DiscordBot_Tree.csproj"
COPY . .
WORKDIR "/src/MS/Task/MS_Task_DiscordBot_Tree"
RUN dotnet build "MS_Task_DiscordBot_Tree.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MS_Task_DiscordBot_Tree.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MS_Task_DiscordBot_Tree.dll"]