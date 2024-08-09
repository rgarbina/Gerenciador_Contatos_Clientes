
# Gerenciador_Contatos_Clientes

Desenvolver uma aplicação web full stack para gerenciar clientes e seus contatos, 
utilizando C# no backend, Blazor no frontend, e Docker para conteinerização.

## Stack utilizada

**Back-end:** .Net 8

**Front-end:** BlazorApp

## Instalação

### Pré-requisitos

- [SDK .NET 6 & 8](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [Docker](https://www.docker.com/products/docker-desktop/)

### Passos para Execucao

#### Visual Studio
1. Executar via Debugger na propria ferramenta

### Ou

#### Manual via prompt (nao funcional -! Necessario resolver problema de certificado com o front !-)

1. Na raiz do projeto, executar o comando:
    ```bash
    dotnet restore Gerenciador_Contatos_Clientes.sln

2. Na raiz do projeto, executar o comando:
    ```bash
    docker-compose up --build
    
