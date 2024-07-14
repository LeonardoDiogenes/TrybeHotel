# Trybe Hotel

Bem-vindo ao repositório do projeto Trybe Hotel.

Este projeto simula uma API para a gestão de uma rede de hotéis, utilizando ASP.NET e SQL Server. Ele segue a arquitetura de software MVC e emprega o Entity Framework para a interação com o banco de dados, que funciona dentro de um container Docker.

## Models da Aplicação
- City
- Hotel
- Room
- User
- Booking

## Rotas

### Rotas de Cidade (City)
- **GET /city** 
  Lista todas as cidades.
- **POST /city (admin)**  
  Cadastro de cidades.
- **PUT /city (admin)**  
  Atualiza os dados de uma cidade existente.
- **DELETE /city (admin)**
  Remove uma cidade.

### Rotas de Hotel (Hotel)
- **GET /hotel**  
  Lista todos os hotéis.
- **POST /hotel (admin)**  
  Cadastro de hotéis.
- **PUT /hotel (admin)**  
  Atualiza hotel existente.
- **DELETE /hotel (admin)**  
  Remove um hotel.

### Rotas de Quarto (Room)
- **GET /room/:hotelId**  
  Lista todos os quartos de um determinado hotel.
- **POST /room (admin)**  
  Cadastro de quarto em um determinado hotel.
- **DELETE /room/:roomId (admin)**  
  Remoção de determinado quarto em determinado hotel.
- **PUT /room/:roomId (admin)**
  Atualização de um quarto de um determinado hotel.


### Rotas de Usuário (User)
- **POST /user**  
  Cadastro de nova pessoa usuária.
- **GET /user (admin)**  
  Lista todas as pessoas usuárias.
- **DELETE /user (admin)**  
  Remove uma pessoa usuária.
- **PUT /user (admin)**  
  Atualiza todos os campos de uma pessoa usuária.

### Rotas de Autenticação (Auth)
- **POST /login**  
  Realiza login de pessoa usuária e gera um token de autorização conforme o tipo de usuário.

### Rotas de Reserva (Booking)
- **POST /booking (client)**  
  Cadastro de reserva de um quarto em um hotel.
- **GET /booking/:id (client)**  
  Lista uma reserva de um usuário cliente.
- **PUT /booking/:id (client)**
  Atualiza uma reserva de um usuário cliente.
- **DELETE /booking/:id (client)**
  Remove uma reserva de um usuário cliente.

### Rotas de Geolocalização (Geo)
- **GET /geo/status**  
  Confere o status da API externa responsável por geolocalização.
- **GET /geo/address**  
  Lista hotéis ordenados por distância de um endereço (ordem crescente).

**Observação:**  
*(client)*: Rota que requer um token de usuário do tipo client ou admin para funcionar.  
*(admin)*: Rota que requer um token de usuário do tipo admin para funcionar.  
O token é gerado ao realizar o login e deve ser inserido no cabeçalho `Authorization` da requisição.
