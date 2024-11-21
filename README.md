# 📦 Dotlanches Pedidos

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=98Lanches_dotlanche-pedido&metric=coverage)](https://sonarcloud.io/summary/new_code?id=98Lanches_dotlanche-pedido)

Microsserviço de Pedidos Dotlanche. Responsável pela criação do pedido.

# Funcionalidades
- Criar um pedido

# Stack
- .NET 8.0
- MongoDb
- NUnit
- Moq
- Reqnroll
- Docker
- Docker Compose
- Kubernetes
- GitHub Actions

# Arquitetura do Sistema
O serviço foi construído utilizando clean architecture para organização interna. O banco de dados selecionado foi o MongoDB pela escalabilidade e performance de escrita.

# Como executar o projeto

## Pré-requisitos
- Docker

1. Na raiz do projeto, execute o comando
```
docker compose up
```
2. Acesse o navegador o endereço http://localhost:8080/swagger/index.html

# Testes
Tanto os testes de unidade quanto os testes de BDD encontram-se no diretório `test`.

Para executar os testes da aplicação, basta rodar o comando abaixo na raiz do projeto:
```
dotnet test
```