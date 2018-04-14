# YAPMT

# Para rodar

 - Para rodar a API acessar YAPMT.Api e rodar o camando ***dotnet run*** a api ficará acessível no endereço **http://localhost:5000**
 - Para a interface todar os comandos ***npm install*** e ***ng serve*** a aplicação que ficará acessível no endereço **http://localhost:4200**

**Interface**

Optei por utilizar angular-cli para ter um start mais rápido do projeto é começar logo a codificar.
Foi utilizado angular 5 e nenhuma pacote adicional foi necessário, apenas o bootstrap afim de dar um melhor acabamento na interface.
A navbar apenas para navegabilidade da aplicação, usando rotas simples, sem child routes, nem lazyloading de modules.

**API**

Foi usado DDD onde 
YAPMT.Domain, como o nome diz possuí o domínio da aplicação, divididos em:
 - CommandHandlers: Usando o princípio de CQRS (Command-Query Responsibility), que separa a responsabilidade de quem alterada dados. Aqui ainda existem os comandos que abstraem as informações que serão enviadas pelas APIs e posteriormente mapeadas via AutoMapper para as entidades afim de não expor nossas entidades. Ainda 
  - Dtos: Objetos de Data transfer para serem respondidos pelas APIS, sem expor as entidades.
 - Entities: As entidades que mantêm os dados da aplicação.
 - Repositories: Assinaturas dos repositórios de manipulação de dados.
 
YAPMT.Infrastructure, além de implementar as classes concretas do domínio ainda:
 - Mappers: Configurações de AutoMapper entre Entidades e Dtos e vice-versa.
 - Repositories: Repositórios dos dados.
 - Repositories.Mappers: Mapeamentos das entidades com banco relacional.

YAPMT.Api, Api propriamente, responde as informações do domínio e infraestrutura.

YAPMT.Framework, a ideia aqui é um frame básico para qualquer microserviço, com funcionalidades básicas, idealmente ficaria em algum gerenciador de pacotes, como por exemplo um servidor nuget dentro da empresa.
 
 YAPMT.Tests, os testes da aplicação, usando o ambiente IntegrationTests que será em memória. Foi usado Xunit para os testes

# Tecnologias e Pacotes Usados
 1. Xunit para os teste
 2. EF Core para acesso a dados
 3. AutoMapper para mapeamento de objetos
 4. MediatR para o CQRS 
 5. MySql como banco de dados
 6. Swagger para documentar as APIS
 7. .NET Core 2.0 para os projetos backend
 8. Angular e angular-cli para o projeto de front-end.

# O que faltou e melhorias
 - Faltou Logger assincrono 
 - UnitOfWork.
 - Abstrair o serviço de CQRS para no futuro poder enfileirar algumas chamadas talvez com rabbitMQ.
 - Um serviço de notification handler ligado os commands do CQRS para ligar eventos quando os dados da aplicação fossem alterados.
 - Fazer uso dos cancellationToken dos CommandHandler para cancelamento entre threads.
 - etc.

