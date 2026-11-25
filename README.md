# HANDS-ON #11 | OWASP Top 10 — SQL Injection

**Ponto de Conhecimento Tecnológico | Tecnologia na prática**

Série #03 — Segurança Aplicacional

## 1. Enquadramento

Este HANDS-ON apresenta uma abordagem prática à identificação, demonstração e prevenção de vulnerabilidades de SQL Injection em APIs REST desenvolvidas com ASP.NET Core e .NET 10.

Através de um laboratório controlado, são comparadas duas abordagens de acesso a dados: consultas SQL vulneráveis e consultas parametrizadas.

## 2. Objetivos

- Compreender o risco de SQL Injection no contexto OWASP Top 10.
- Identificar práticas inseguras no acesso a bases de dados.
- Implementar consultas SQL parametrizadas.
- Validar mecanismos de prevenção através de testes automatizados e manuais.
- Identificar e corrigir vulnerabilidades em dependências NuGet.
- Garantir uma configuração segura por defeito.

## 3. Tecnologias utilizadas

| Tecnologia | Finalidade |
|---|---|
| Visual Studio 2026 | Ambiente de desenvolvimento |
| .NET 10 | Plataforma de desenvolvimento |
| C# | Linguagem de programação |
| ASP.NET Core | Desenvolvimento da API REST |
| Entity Framework Core | Acesso a dados |
| SQLite | Base de dados do laboratório |
| OpenAPI | Documentação da API |
| xUnit | Testes automatizados |
| PowerShell | Testes manuais |
| Git e GitHub | Controlo de versões |

## 4. Arquitetura da solução

```text
HandsOn11-OwaspTop10/
├── HandsOn11.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
├── HandsOn11.Tests/
├── docs/
│   └── HANDS-ON_11_Manual_Tecnico_SQL_Injection.pdf
├── HandsOn11.OwaspTop10.sln
├── .gitignore
└── README.md
```

## 5. Endpoints da API

| Método | Endpoint | Finalidade |
|---|---|---|
| GET | `/api/health` | Verificação da disponibilidade |
| GET | `/api/produtos/seguro` | Consulta parametrizada |
| GET | `/api/produtos/lab/vulneravel` | Demonstração em laboratório controlado |

## 6. Execução do projeto

### Pré-requisitos

- .NET 10 SDK
- Visual Studio 2026 ou ferramenta equivalente
- Git

### Compilação

```powershell
dotnet restore
dotnet build
```

### Execução da API

```powershell
dotnet run --project HandsOn11.Api
```

### Execução dos testes automatizados

```powershell
dotnet test
```

## 7. Resultados dos testes automatizados

| Indicador | Resultado |
|---|---:|
| Total de testes | 6 |
| Testes aprovados | 6 |
| Testes falhados | 0 |
| Testes ignorados | 0 |
| Taxa de sucesso | 100% |

## 8. Resultados dos testes manuais

| Cenário | Resultado | Estado |
|---|---|---|
| Health Check | OK | Aprovado |
| Consulta segura legítima | 2 produtos | Aprovado |
| Endpoint vulnerável desativado | HTTP 404 | Aprovado |
| Consulta vulnerável legítima | 2 produtos | Aprovado |
| Demonstração de SQL Injection | 4 produtos | Vulnerabilidade demonstrada |
| Prevenção de SQL Injection | 0 produtos | Aprovado |
| Reposição da configuração segura | HTTP 404 | Aprovado |

Os resultados demonstram que a parametrização preserva a lógica da consulta perante a entrada maliciosa testada.

## 9. Gestão de dependências e vulnerabilidades

Durante a validação do projeto, foi identificada uma vulnerabilidade de severidade elevada numa dependência transitiva do pacote Microsoft.OpenApi.

| Indicador | Resultado |
|---|---|
| Dependência | Microsoft.OpenApi |
| Versão inicialmente identificada | 2.0.0 |
| Versão aplicada na correção | 2.7.5 |
| Aviso NuGet | NU1903 |
| Advisory | GHSA-v5pm-xwqc-g5wc |

Após a atualização da dependência, foram novamente executados os testes automatizados e a auditoria dos pacotes NuGet.

```powershell
dotnet list package --vulnerable --include-transitive
```

Resultados obtidos:

- Zero vulnerabilidades reportadas pelas fontes consultadas.
- Zero avisos de compilação.
- Zero erros de compilação.
- Seis testes automatizados aprovados.

## 10. Segurança por defeito

O endpoint vulnerável destina-se exclusivamente a fins pedagógicos, num ambiente local e controlado.

A configuração final mantém o laboratório desativado:

```json
{
  "Lab": {
    "EnableVulnerableEndpoint": false
  }
}
```

A indisponibilidade do endpoint foi confirmada através da resposta HTTP 404.

Este projeto não deve ser utilizado em ambientes de produção sem uma avaliação de segurança adequada.

## 11. Documentação técnica

O manual técnico apresenta o desenvolvimento passo a passo, os fundamentos técnicos, a implementação e as evidências reais dos testes realizados.

[Consultar o Manual Técnico — HANDS-ON #11 (PDF)](docs/HANDS-ON_11_Manual_Tecnico_SQL_Injection.pdf)

## 12. Competências exploradas

- Segurança aplicacional
- OWASP Top 10
- SQL Injection
- Consultas SQL parametrizadas
- Segurança de APIs REST
- Testes de integração
- Gestão de dependências
- Identificação e correção de vulnerabilidades
- Secure by Default

## 13. Referências técnicas

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [OWASP SQL Injection Prevention Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html)
- [Microsoft — ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
- [Microsoft — Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Microsoft — NuGet Audit](https://learn.microsoft.com/nuget/concepts/auditing-packages)
- [xUnit](https://xunit.net/)

## 14. Sobre a iniciativa

**Ponto de Conhecimento Tecnológico**

Uma iniciativa de partilha de conhecimento orientada para a exploração prática de tecnologias, metodologias e boas práticas na área dos Sistemas de Informação.

**Tecnologia na prática. Conhecimento através da partilha.**

---

**Autora:** Flórida Araújo

**Ano:** 2026

**Série #03 — Segurança Aplicacional | HANDS-ON #11**