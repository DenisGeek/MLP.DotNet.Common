FROM mcr.microsoft.com/dotnet/core/runtime:3.0-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["MS/Watcher/MS.Watcher.DiscordBot/MS.Watcher.DiscordBot.csproj", "MS/Watcher/MS.Watcher.DiscordBot/"]
COPY ["API/V1/CS/API.V1.RPC.Client.CS/API.V1.RPC.Client.CS.csproj", "API/V1/CS/API.V1.RPC.Client.CS/"]
COPY ["Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/Tools.Environment.RabbitMQ.csproj", "Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/"]
COPY ["Tools/Environment/Tools.EnvironmentExt/Tools.EnvironmentExt.csproj", "Tools/Environment/Tools.EnvironmentExt/"]
COPY ["Tools/Data/TreeStructure/MLP.Tools.Data.TreeStructure/Tools.Data.TreeStructure.csproj", "Tools/Data/TreeStructure/MLP.Tools.Data.TreeStructure/"]
COPY ["Tools/Environment/Discord/Tools.Environment.Discord/Tools.Environment.Discord.csproj", "Tools/Environment/Discord/Tools.Environment.Discord/"]
RUN dotnet restore "MS/Watcher/MS.Watcher.DiscordBot/MS.Watcher.DiscordBot.csproj"
COPY . .
WORKDIR "/src/MS/Watcher/MS.Watcher.DiscordBot"
RUN dotnet build "MS.Watcher.DiscordBot.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MS.Watcher.DiscordBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MS.Watcher.DiscordBot.dll"]