# ECommerceApi
# Projeto de Sistema de Autenticação JWT

Este é um projeto de exemplo que implementa um sistema de autenticação baseado em JWT (JSON Web Tokens) usando ASP.NET Core.

## Funcionalidades

- Registro de usuários com armazenamento seguro de senhas.
- Geração de tokens JWT com base nas credenciais do usuário.
- Autenticação e autorização com base em roles (funções).
- Exemplo de uso de claims com tokens JWT.

## Tecnologias Utilizadas

- .NET Core
- ASP.NET Core Identity
- JWT
- Entity Framework Core

## Configuração do Projeto

1. Configure a string de conexão com o banco de dados no `appsettings.json` ou utilize variáveis de ambiente.
2. Execute as migrações do banco de dados (caso esteja usando EF Core):
   ```bash
   dotnet ef database update
