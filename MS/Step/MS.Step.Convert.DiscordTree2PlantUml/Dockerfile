#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1809 AS build
WORKDIR /src
COPY ["MS/Step/MS.Step.Convert.DiscordTree2PlantUml/MS.Step.Convert.DiscordTree2PlantUml.csproj", "MS/Step/MS.Step.Convert.DiscordTree2PlantUml/"]
COPY ["Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/Tools.Environment.RabbitMQ.csproj", "Tools/Environment/RabbitMQ/Tools.Environment.RabbitMQ/"]
COPY ["Tools/Environment/Tools.EnvironmentExt/Tools.EnvironmentExt.csproj", "Tools/Environment/Tools.EnvironmentExt/"]
COPY ["API/V1/CS/API.V1.RPC.Client.CS/API.V1.RPC.Client.CS.csproj", "API/V1/CS/API.V1.RPC.Client.CS/"]
COPY ["API/V1/CS/API.V1.RPC.Server.CS/API.V1.RPC.Server.CS.csproj", "API/V1/CS/API.V1.RPC.Server.CS/"]
COPY ["Tools/Data/TreeStructure/MLP.Tools.Data.TreeStructure/Tools.Data.TreeStructure.csproj", "Tools/Data/TreeStructure/MLP.Tools.Data.TreeStructure/"]
RUN dotnet restore "MS/Step/MS.Step.Convert.DiscordTree2PlantUml/MS.Step.Convert.DiscordTree2PlantUml.csproj"
COPY . .
WORKDIR "/src/MS/Step/MS.Step.Convert.DiscordTree2PlantUml"
RUN dotnet build "MS.Step.Convert.DiscordTree2PlantUml.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MS.Step.Convert.DiscordTree2PlantUml.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MS.Step.Convert.DiscordTree2PlantUml.dll"]