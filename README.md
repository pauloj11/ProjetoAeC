Projeto contem uma API (RestFull) que consome dados da Brasil API, retornando um determinado clima para uma cidade ou aeroporto informada na rota da API. API retorna no console os dados a cada requisição realizada.

Script em SQL para criação das tabelas presentes no projeto:

CREATE TABLE WeatherCity ( Id INT IDENTITY(1,1) PRIMARY KEY, Cidade NVARCHAR(255) NOT NULL, Estado NVARCHAR(2) NOT NULL, Atualizado_em DATETIME NOT NULL, Clima NVARCHAR(MAX) NOT NULL );

CREATE TABLE WeatherAirport ( Id INT IDENTITY(1,1) PRIMARY KEY, Umidade INT, Visibilidade NVARCHAR(255), CodigoICAO NVARCHAR(10), PressaoAtmosferica INT, Vento INT, DirecaoVento INT, Condicao NVARCHAR(2), CondicaoDescricao NVARCHAR(255), Temperatura INT, AtualizadoEm DATETIME );

CREATE TABLE ErrorLog ( Id INT IDENTITY(1,1) PRIMARY KEY, Timestamp DATETIME NOT NULL, ErrorMessage NVARCHAR(MAX) NOT NULL, );