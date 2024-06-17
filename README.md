# Trybe Hotel - Fase D

Boas-vindas ao repositório do projeto Trybe Hotel - Fase D

Para realizar o projeto, atente-se a cada passo descrito a seguir, e se tiver **qualquer dúvida**, nos envie no _Slack_ da turma! #vqv 🚀

Aqui, você vai encontrar os detalhes de como estruturar o desenvolvimento do seu projeto a partir desse repositório, utilizando uma branch específica e um _Pull Request_ para colocar seus códigos.

## Termos e acordos

Ao iniciar este projeto, você concorda com as diretrizes do [Código de Conduta e do Manual da Pessoa Estudante da Trybe](https://app.betrybe.com/learn/student-manual/codigo-de-conduta-da-pessoa-estudante).

## Entregáveis
---

<details>
<summary><strong>🤷🏽‍♀️ Como entregar</strong></summary>

Para entregar o seu projeto você deverá criar um _Pull Request_ neste repositório.

Lembre-se que você pode consultar nosso conteúdo sobre [Git & GitHub](https://app.betrybe.com/learn/course/5e938f69-6e32-43b3-9685-c936530fd326/module/fc998c60-386e-46bc-83ca-4269beb17e17/section/fe827a71-3222-4b4d-a66f-ed98e09961af/day/1a530297-e176-4c79-8ed9-291ae2950540/lesson/2b2edce7-9c49-4907-92a2-aa571f823b79) e nosso [Blog - Git & GitHub](https://blog.betrybe.com/tecnologia/git-e-github/) sempre que precisar!

</details>
  
<details>
<summary><strong>🧑‍💻 O que deverá ser desenvolvido</strong></summary>

Sua empresa do coração começou a desenvolver um software de booking de várias redes de hotéis.
Sua missão é continuar o desenvolvimento dessa API. Será necessário criar uma rota padrão para ver o status da aplicação e construir um Dockerfile capaz de preparar sua aplicação para Deploy. Nessa fase, sua missão será refatorar o projeto para comportar essa funcionalidade e desenvolvê-la.

</details>
  
<details>
  <summary><strong>📝 Habilidades a serem trabalhadas </strong></summary>

Neste projeto, verificamos se você é capaz de:

- Entender o processo de criar containers para a aplicação.
- Preparar um sistema para deploy.


</details>


## Orientações
---

<details>
  <summary><strong>‼️ Antes de começar a desenvolver</strong></summary><br />

  1. Clone o repositório

  - Use o comando: `git clone git@github.com:tryber/csharp-033-csharp-projeto-trybe-hotel-fase-d.git`.
  - Entre na pasta do repositório que você acabou de clonar:
    - `cd csharp-033-csharp-projeto-trybe-hotel-fase-d`

  2. Instale as dependências
  
  - Entre na pasta `src/`.
  - Execute o comando: `dotnet restore`.
  
  3. Crie uma branch a partir da branch `master`

  - Verifique se você está na branch `master`
    - Exemplo: `git branch`
  - Se não estiver, mude para a branch `master`
    - Exemplo: `git checkout master`
  - Agora crie uma branch à qual você vai submeter os `commits` do seu projeto
    - Você deve criar uma branch no seguinte formato: `nome-de-usuario-nome-do-projeto`
    - Exemplo: `git checkout -b joaozinho-csharp-033-csharp-projeto-trybe-hotel-fase-d`

  4. Adicione as mudanças ao _stage_ do Git e faça um `commit`

  - Verifique que as mudanças ainda não estão no _stage_
    - Exemplo: `git status` (deve aparecer listada a pasta _joaozinho_ em vermelho)
  - Adicione o novo arquivo ao _stage_ do Git
    - Exemplo:
      - `git add .` (adicionando todas as mudanças - _que estavam em vermelho_ - ao stage do Git)
      - `git status` (deve aparecer listado o arquivo _joaozinho/README.md_ em verde)
  - Faça o `commit` inicial
    - Exemplo:
      - `git commit -m 'iniciando o projeto x'` (fazendo o primeiro commit)
      - `git status` (deve aparecer uma mensagem tipo essa: _nothing to commit_ )

  5. Adicione a sua branch com o novo `commit` ao repositório remoto

  - Usando o exemplo anterior: `git push -u origin joaozinho-csharp-033-csharp-projeto-trybe-hotel-fase-d`

  6. Crie um novo `Pull Request` _(PR)_

  - Vá até a página de _Pull Requests_ do [repositório no GitHub](https://github.com/tryber/csharp-033-csharp-projeto-trybe-hotel-fase-d/pulls)
  - Clique no botão verde _"New pull request"_
  - Clique na caixa de seleção _"Compare"_ e escolha a sua branch **com atenção**
  - Coloque um título para a sua _Pull Request_
    - Exemplo: _"Cria tela de busca"_
  - Clique no botão verde _"Create pull request"_
  - Adicione uma descrição para o _Pull Request_ e clique no botão verde _"Create pull request"_
  - **Não se preocupe em preencher mais nada por enquanto!**
  - Volte até a [página de _Pull Requests_ do repositório](https://github.com/tryber/csharp-0x-projeto-trybe-hotel/pulls) e confira que o seu _Pull Request_ está criado

</details>

<details>
  <summary><strong>⌨️ Durante o desenvolvimento</strong></summary><br/>

  - Faça `commits` das alterações que você fizer no código regularmente

  - Lembre-se sempre de, após um (ou alguns) `commits`, atualizar o repositório remoto

  - Os comandos que você utilizará com mais frequência são:
    1. `git status` _(para verificar o que está em vermelho - fora do stage - e o que está em verde - no stage)_
    2. `git add` _(para adicionar arquivos ao stage do Git)_
    3. `git commit` _(para criar um commit com os arquivos que estão no stage do Git)_
    4. `git push -u origin nome-da-branch` _(para enviar o commit para o repositório remoto na primeira vez que fizer o `push` de uma nova branch)_
    5. `git push` _(para enviar o commit para o repositório remoto após o passo anterior)_

</details>

<details>
  <summary><strong>🤝 Depois de terminar o desenvolvimento (opcional)</strong></summary><br/>

  Para sinalizar que o seu projeto está pronto para o _"Code Review"_, faça o seguinte:

  - Vá até a página **DO SEU** _Pull Request_, adicione a label de _"code-review"_ e marque seus colegas:

    - No menu à direita, clique no _link_ **"Labels"** e escolha a _label_ **code-review**;

    - No menu à direita, clique no _link_ **"Assignees"** e escolha **o seu usuário**;

    - No menu à direita, clique no _link_ **"Reviewers"** e digite `students`, selecione o time `tryber/students-sd-033-csharp`.

  Caso tenha alguma dúvida, [aqui tem um vídeo explicativo](https://vimeo.com/362189205).

</details>

<details>
  <summary><strong>🕵🏿 Revisando um pull request</strong></summary><br />

  Use o conteúdo sobre [Code Review](https://app.betrybe.com/course/real-life-engineer/code-review) para te ajudar a revisar os _Pull Requests_.

</details>

<details>
  <summary><strong>🎛 Linter</strong></summary><br />

  Usaremos o [NetAnalyzer](https://docs.microsoft.com/pt-br/dotnet/fundamentals/code-analysis/overview) para fazer a análise estática do seu código.

  Este projeto já vem com as dependências relacionadas ao _linter_ configuradas no arquivo `.csproj`.

  O analisador já é instalado pelo plugin da `Microsoft C#` no `VSCode`. Para isso, basta fazer o download do [plugin](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) e instalá-lo.
</details>

<details>
  <summary><strong>🛠 Testes</strong></summary><br />

  O .NET já possui sua própria plataforma de testes.
  
  Este projeto já vem configurado e com suas dependências.

  ### Executando todos os testes

  Para executar os testes com o .NET, execute o comando dentro do diretório do seu projeto `src`!

  ```
  dotnet test
  ```

  ### Executando um teste específico

  Para executar um teste específico, basta executar o comando `dotnet test --filter Name~TestReq01`.

  :warning: **Importante:** o comando irá executar testes cujo nome contém `TestReq01`.

  :warning: **O avaliador automático não necessariamente avalia seu projeto na ordem em que os requisitos aparecem no readme. Isso acontece para deixar o processo de avaliação mais rápido. Então, não se assuste se isso acontecer, ok?**

  ### Outras opções para testes
  - Algumas opções que podem lhe ajudar são:
    -  `-?|-h|--help`: exibe a descrição completa de como utilizar o comando.
    -  `-t|--list-tests`: lista todos os testes, ao invés de executá-los.
    -  `-v|--verbosity <LEVEL>`: define o nível de detalhe na resposta dos testes.
      - `q | quiet`
      - `m | minimal`
      - `n | normal`
      - `d | detailed`
      - `diag | diagnostic`
      - Exemplo de uso: 
         ```
           dotnet test -v diag
         ```
         ou
         ```            
           dotnet test --verbosity=diagnostic
         ``` 
</details>

## Requisitos do projeto

Você está desenvolvendo uma API que será utilizada em uma aplicação de booking de várias redes de hotéis.

Na primeira fase deste projeto, você desenvolveu algumas rotas de entidades acerca de cidades, hotéis e quartos. Na segunda fase, você construiu rotas para o cadastro e login de pessoas clientes e o cadastro de reservas. Na terceira fase, você adicionou novas funcionalidades em rotas e adicionou serviços externos. **Agora, você irá desenvolver uma funcionalidade preparar a sua aplicação para deploy.**

No intuito de auxiliar o desenvolvimento, o time de produto já disponibilizou o diagrama de entidade-relacionamento atualizado e o time de DevOps disponibilizou um container na qual você poderá utilizar um banco de dados.

O sistema está dividido em diretórios específicos para auxiliar na organização e desenvolvimento do projeto.

- `Controllers/`: Este diretório armazena os arquivos com as lógicas dos controllers da aplicação. Os métodos a serem desenvolvidos estão prontos mas sem implementação alguma, o que você desenvolverá ao longo do projeto.
<br />

- `Models/`: Este diretório armazena os arquivos com as models do banco de dados. Você já desenvolveu as models `City`, `Hotel`, `Room`, `User` e `Bokking` que serão os modelos para as tabelas `Cities`, `Hotels`, `Rooms`, `Users` e `Bookings`.
<br />

- `DTO/`: Este diretório armazena as classes de DTO. Algumas rotas esperam as `responses` baseadas nestes DTOs. Você pode conferir isso pelo requisito do projeto e pelo retorno dos métodos dos `repositories`.
<br />

- `Repository/`: Este diretório armazena as lógicas que farão a interação com o banco de dados. Os métodos de cada requisito já estão criados e você deverá incluir a implementação de cada um desses métodos respeitando o retorno do DTO. Além disso, você terá o arquivo `TrybeHotelContext` com o contexto para a conexão com o banco de dados. Todos os `repository` e o `context` possuem interfaces que estão nesse diretório e fornecem o contrato para essas classes. Caso você precise criar um novo método para interação com o banco de dados que não esteja mapeado, você pode livremente criar esse novo método na `repository` mas sem se esquecer de escrever o contrato deste método na interface referente.
<br />

- `Services`: Este diretório armazena os serviços responsáveis pela geração de token e pelo serviço geográfico.

<details id='der'>
  <summary><strong>🎲 Banco de Dados</strong></summary>
  <br/>

  Para o desenvolvimento, o time de produto disponibilizou um *Diagrama de Entidade-Relacionamento (DER)* para construir a modelagem do banco de dados. Com essa imagem você já consegue saber:
  - Como nomear suas tabelas e colunas;
  - Quais são os tipos de suas colunas;
  - Relações entre tabelas.

    ![banco de dados](img/der.png)

  O diagrama infere 05 tabelas:
  - ***Cities***: tabela que armazenará um conjunto de cidades nas quais os hotéis estão localizados (já desenvolvida).
  - ***Hotels***: tabela que armazenará os hotéis da nossa aplicação. Note que informamos o `CityId`, atributo que armazenará o id da cidade (já desenvolvida).
  - ***Rooms***: tabela que armazenará os quartos de cada hotel da nossa aplicação. Note que informamos o `HotelId`, atributo que armazenará o id do hotel (já desenvolvida).
  - ***Users***: tabela que armazenará as pessoas usuárias do sistema.
  - ***Bookings***: tabela que armazenará as reservas de quartos de hotéis. Note que informamos os atributos `UserId`, que armazenará o id da pessoa usuária e `RoomId`, que armazenará o id do quarto reservado.

  Acerca dos relacionamentos, pelo diagrama de entidade-relacionamento temos:
  - Uma cidade pode ter vários hotéis.
  - Um hotel pode ter vários quartos.
  - Uma pessoa usuária pode ter várias reservas.
  - Um quarto pode ter várias reservas.

  ⚠️ **Você poderá criar migrations para visualizar o banco de dados**

</details>

<details>
<summary><strong>🐳 Docker</strong></summary><br />

Para auxiliar no desenvolvimento, este projeto possui um arquivo do docker compose para subir um serviço do banco de dados `Azure Data Studio`. Este banco de dados possui a mesma arquitetura do `SQL Server`.

Para subir o serviço, utilize o comando:

```shell
docker-compose up -d --build
```

Para conectar ao seu sistema de gerenciamento de banco de dados, utilize as seguintes credenciais:

- `Server`: localhost
- `User`: sa
- `Password`: TrybeHotel12!
- `Database`: TrybeHotel
- `Trust server certificate`: true

Para criar o contexto do banco de dados na sua aplicação, utilize como connection string:

```csharp
var connectionString = "Server=localhost;Database=TrybeHotel;User=SA;Password=TrybeHotel12!;TrustServerCertificate=True";
```

⚠️ ** Essa connection string poderá ser utilizada no requisito 1 **

</details>

<details id='refatorando'>
  <summary>Continuando o projeto Trybe Hotel</summary>

Você já iniciou o projeto da nossa aplicação e portanto, todas as funcionalidades podem ser trazidas para não duplicar o funcionamento. Isso será muito importante, especialmente no que diz respeito ao banco de dados. Algumas models do seu banco de dados anterior serão referenciadas nas models agora, portanto, vamos trazer as funcionalidades anteriores.

Mas como fazemos isso:

Após clonar o repositório deste projeto, apenas copie e cole as funcionalidades que você construiu anteriormente:

- `Controllers`: copie todos os arquivos do diretório `Controllers` do projeto anterior e cole no diretório `Controllers` deste projeto.
- `Dto`: copie todos os arquivos do diretório `Dto` do projeto anterior e cole no diretório `Dto` deste projeto.
- `Models`: copie os arquivos referentes às models `City`, `Hotel`, `Room`, `User` e `Booking` do projeto anterior e cole no diretório `Models` deste projeto.
- `Repository`: copie os arquivos `RoomRepository`, `HotelRepository`, `CityRepository`, `UserRepository` e `BookingRepository` do projeto anterior e cole no diretório `Repository` deste projeto. Não copie as interfaces. Para o arquivo `TrybeHotelContext`, não o substitua. Apenas adicione os `DBSets` e implemente os métodos `OnConfiguring()` e `OnModelCreating()`.
- `Migrations`: Se você possui um diretório de migrations, significa que você criou migrations no projeto anterior. Não copie este diretório e crie migrations novas porque a instância do banco de dados no container não será o mesmo.
- `Services`: copie todos os arquivos do diretório `Services` do projeto anterior e cole no diretório `Services` deste projeto.
- `Testes`: No projeto de testes, você pode copiar a funcionalidade do arquivo `src/TrybeHotel.Test/IntegrationTest.cs`.

</details>



### 1. Desenvolva o endpoint GET /

<details>
  <summary><strong>Mais informações:</strong></summary>

  - Desenvolva o endpoint `GET /` de modo que a response da API seja um status de sucesso com o seguinte corpo de resposta:

  ```json
  {
	  "message": "online"
  }
  ```
 - Implemente o desenvolvimento no método `GetStatus()` do arquivo `src/TrybeHotel/Controllers/StatusController.cs`.

**O que será testado:**

- Será testado que a response da API segue o padrão solicitado.

</details>


### 2. Desenvolva o Dockerfile

<details>
  <summary><strong>Mais informações:</strong></summary>

  - Desenvolva o Dockerfile da aplicação capaz de criar um container da sua API
  - Implemente no arquivo `src/TrybeHotel/Dockerfile`.

**O que será testado:**

- Será testado que a response da API segue o padrão solicitado.

</details>


### 3. Faça o deploy da aplicação para o Railway - Bônus não avaliativo

<details>
  <summary><strong>Mais informações:</strong></summary>

  - Utilize os conhecimentos adquiridos nesta seção para publicar a sua API Containerizada no Railway.
  - **Este requisito é não avaliativo, portanto, não possui avaliação automatizada.**


</details>

<details>
  <summary><strong>🗣 Nos dê feedbacks sobre o projeto!</strong></summary><br />

Ao finalizar e submeter o projeto, não se esqueça de avaliar sua experiência preenchendo o formulário. 
**Leva menos de 3 minutos!**

[FORMULÁRIO DE AVALIAÇÃO DE PROJETO](https://be-trybe.typeform.com/to/ZTeR4IbH#cohort_hidden=CH33-CSHARP&template=betrybe/csharp-0x-projeto-trybe-hotel-fase-d)

</details>

<details>
  <summary><strong>🗂 Compartilhe seu portfólio!</strong></summary><br />

  Você sabia que o LinkedIn é a principal rede social profissional e que compartilhar aprendizados lá é muito importante para quem deseja construir uma carreira de sucesso? Compartilhe este projeto no seu LinkedIn, marque o perfil da Trybe (@trybe) e mostre para a sua rede toda a sua evolução.

</details>
