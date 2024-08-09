
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

### Acessar aplicação 

1. Front End: [https://localhost:7000/](https://localhost:7000) [http://localhost:5000/](http://localhost:5000)
2. Back End: [https://localhost:7001/](https://localhost:7001/swagger/index.html) [http://localhost:5001/](http://localhost:5001/swagger/index.html)

## Apêndice
1.Necessario analisar e remover implementações não utilizados.
2.Realizar Test de cobertura.
3.Corrigir integracao dos Testes, para Repository e context.
4.Unificar utilizacao e regras das classes Model entre Front e Back.
5.Analisar e melhorar trechos de codigo, que necessitam de comentario.
