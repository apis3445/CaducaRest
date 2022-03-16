FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["CaducaRest/CaducaRest.csproj", "CaducaRest/"]
COPY CaducaRest/Setup.sh CaducaRest/Setup.sh

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "CaducaRest/CaducaRest.csproj"
COPY . .
WORKDIR "/src/CaducaRest"

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations

RUN chmod +x /src/CaducaRest/Setup.sh
CMD /bin/bash /src/CaducaRest/Setup.sh