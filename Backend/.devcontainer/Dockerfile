# [Choice] .NET version: 6.0, 5.0, 3.1, 6.0-bullseye, 5.0-bullseye, 3.1-bullseye, 6.0-focal, 5.0-focal, 3.1-focal
ARG VARIANT=5.0-bullseye
FROM mcr.microsoft.com/vscode/devcontainers/dotnet:0-${VARIANT}

# [Choice] Node.js version: none, lts/*, 16, 14, 12, 10
ARG NODE_VERSION="none"
RUN if [ "${NODE_VERSION}" != "none" ]; then su vscode -c "umask 0002 && . /usr/local/share/nvm/nvm.sh && nvm install ${NODE_VERSION} 2>&1"; fi

# Install SQL Tools: SQLPackage and sqlcmd
COPY mssql/installSQLtools.sh installSQLtools.sh
RUN bash ./installSQLtools.sh \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

RUN wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb \
     && dpkg -i packages-microsoft-prod.deb

# There is a current limitation where v3 Azure functions targetting .Net5.0 also requires the .Net3.1 SDK
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
     && apt-get install -y --no-install-recommends azure-functions-core-tools-3 \               
     && apt-get install -y apt-transport-https \
     && apt-get update \
     && apt-get install -y dotnet-sdk-3.1

# [Optional] Uncomment this line to install global node packages.
#RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your_package_name> 2>&1

RUN dotnet dev-certs https --trust