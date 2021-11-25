<!-- <p align="right">
  <a href="https://github.com/DannyGB/CodeExampleCompilation/actions/workflows/build.yml">
      <img src="https://github.com/DannyGB/CodeExampleCompilation/actions/workflows/build.yml/badge.svg">
  </a>    
</p> -->

# 🦊 Pingen

An Azure function to generate random pin numbers

## 🚂 Motivation

To experiment with vscode dev containers in a (semi-) useful way.

## 👉 Features

* An angular front end that uses the back end to generate pins (this is not necessary though, you can just look at the Backend)
* An Azure function that generates random 4 digit pin numbers within a given range
* Reads config from an Azurite docker container
* Publishes an NServiceBus event on Pin generation using RabbitMQ transport and SQL persistence (in a Rabbitmq and MSSQL docker container respectively)
* Handle the NServiceBus event and store some info in a REDIS docker container

## 🚀 Installation and Build

### Pre-requisites

1. vscode
2. Docker
3. Docker-compose

### Build

1. Clone this git repository
2. Open vscode
3. Open the `backend` folder in remote container
    * This will open the backend folder in a docker container and kick off the installation of the dependant docker containers
    * Once completed you can use it as if it were your local environment

### Running

1. From the vscode terminal (within the container) executing `func start` from the `pin-number-gen.functions` folder will run the function which can then be accessed using [http://localhost:7071/api/pingen]() from the host machine

### Configuration

The container requires the below config to be added to the Azurite docker container in a table called configuration with the structure:
> Servernames, passwords and user id's are what are configured in the [dockerfile](https://github.com/DannyGB/Pingen/blob/main/Backend/.devcontainer/Dockerfile) and [docker-compose.yml](https://github.com/DannyGB/Pingen/blob/main/Backend/.devcontainer/docker-compose.yml) files and should only be amended in-line with those files.

```
Partition Key: LOCAL
RowKey: Pingen_1.0
Data: {    
    "SqlConnection": "Server=\"localhost\";Initial Catalog=\"Pingen\";User Id=\"sa\";Password=\"P@ssw0rd\"",
    "NServiceBusConnection": "host=rabbitmq;username=rabbitmq;password=rabbitmq",
    "RedisConnectionString": "redis",
    "Service": {
        "Range": [0, 10000],
        "Exclusions":  [
        "0000",
        "1111",
        "2222",
         "3333",
         "4444",
         "5555",
         "6666",
         "7777",
         "8888",
         "9999",
         "0123",
         "1234",
         "2345",
         "3456",
         "4567",
         "5678",
          "6789",
          "7890"
        ]
    }
}
```

The local.settings.json in the functions folder should look like the following
> Servernames are configured in the docker-compose.yml files and should only be amended in-line with that file.
> You should replace the `<your_key>` text with the Primary Key from your Azurite docker installation

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "AccountName=devstoreaccount1;AccountKey=<your_key>;DefaultEndpointsProtocol=http;BlobEndpoint=http://azurite:10000/devstoreaccount1;QueueEndpoint=http://azurite:10001/devstoreaccount1;TableEndpoint=http://azurite:10002/devstoreaccount1;",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ConfigNames": "Pingen",
    "ConfigurationStorageConnectionString": "AccountName=devstoreaccount1;AccountKey=<your_key>;DefaultEndpointsProtocol=http;TableEndpoint=http://azurite:10002/devstoreaccount1;",
    "EnvironmentName": "LOCAL"
  },
  "Host": {
    "CORS": "*"
  }
}
```

Each docker container can be accessed from the host as well as the backend docker container, this allows you to connect your SSMS, Azure Storage Explorer etc ... tooling to those containers. The RabbitMq management website is also available via your browser at [http://localhost:15672]() from your host machine. User Id's and passwords can be seen in the above config or the [docker-compose.yml](https://github.com/DannyGB/Pingen/blob/main/Backend/.devcontainer/docker-compose.yml) file.

## 📖 License

© dannygb

This software is provided as-is under the [MIT license](https://github.com/DannyGB/Pingen/LICENSE).
