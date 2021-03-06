CREATE DATABASE CakeShop
GO

USE CakeShop
GO

CREATE TABLE USUARIO (
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOME VARCHAR(100),
	LOGINUSUARIO VARCHAR(20),
	SENHA VARCHAR(10), 
	EMAIL VARCHAR(200)
);

INSERT INTO USUARIO (NOME, LOGINUSUARIO, SENHA, EMAIL) VALUES ('ADMINISTRADOR', 'admin', '123', 'admin@cakeshop.com.br');

CREATE TABLE CLIENTE (
	ID_CLIENTE INTEGER PRIMARY KEY IDENTITY(1,1),
	NOME_CLIENTE VARCHAR(200),
	TELEFONE VARCHAR(50),
	EMAIL VARCHAR(250),
	LOGIN_CLIENTE VARCHAR(50),
	SENHA VARCHAR(8),
	ENDERECO VARCHAR(300),
	NUMERO VARCHAR(50),
	COMPLEMENTO VARCHAR(100),
	CEP VARCHAR(50),
	CIDADE VARCHAR(50),
	ESTADO CHAR(3)
);

INSERT INTO CLIENTE (NOME_CLIENTE, TELEFONE, EMAIL, LOGIN_CLIENTE, SENHA, ENDERECO, NUMERO, COMPLEMENTO, CEP, CIDADE, ESTADO)
VALUES ('Giovana Proceki', '(41) 3271-8450', 'procekgi@gmail.com', 'giovana', '123', 'Rua Padre Leonardo Nunes', '180', 'Port�o', '80330-320', 'Curitiba', 'PR');

CREATE TABLE UNIDADE_MEDIDA (
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOME VARCHAR(200),
	SIGLA CHAR(20)
);
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Unit�rio', 'Un');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro', 'm');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Cent�metro', 'cm');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Mil�metro', 'mm');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Quilograma', 'kg');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Grama', 'g');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Miligrama', 'mg');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Tonelada', 't');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Libra', 'lb');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('On�a', 'oz');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Segundo', 's');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Minuto', 'min');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Hora', 'h');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Amp�re', 'A');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Miliamp�re', 'mA');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Kelvin', 'K');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Grau Celsius', '�C');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Fahrenheit', '�F');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Candela', 'cd');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Mol', 'mol');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro Quadrado', 'm�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Cent�metro Quadrado', 'cm�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro C�bico', 'm�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Litro', 'l');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Cent�metro C�bico', 'cm�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Mililitro', 'ml');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro por Segundo', 'm/s');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Quil�metro por Hora', 'km/h');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro por Segundo ao Quadrado', 'm/s�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Quilograma por Metro C�bico', 'kg/m�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Grama por Cent�metro C�bico', 'g/cm�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Candela por Metro Quadrado', 'cd/m�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Radiano', 'rad');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Grau', '�');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Hert', 'Hz');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Newton', 'N');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Quilograma-For�a', 'kgf');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Pascal', 'Pa');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Atmosfera', 'atm');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Bar', 'bar');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Joule', 'J');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Eletronvolt', 'eV');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Watt', 'W');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Quilowatt', 'kW');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Coulomb', 'C');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Volt', 'V');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Farad', 'F');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Ohm', '#(#937#)#');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Siemens', 'S');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('L�men', 'lm');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Lux', 'lx');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Metro C�bico por Segundo', 'm�/s');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Caixa', 'cx');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('Unidade', 'un');
INSERT INTO UNIDADE_MEDIDA (NOME, SIGLA) VALUES ('M�o de obra', 'mo');

CREATE TABLE CATEGORIA (
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOME VARCHAR(10)
);

INSERT INTO CATEGORIA (NOME) VALUES ('DOCES'), ( 'SALGADOS'), ( 'BOLOS');

CREATE TABLE PRODUTO (
	ID_PRODUTO INT PRIMARY KEY IDENTITY(1,1),
	NOME_PRODUTO VARCHAR(200),
	PRECO DECIMAL(10,2),
	ID_UNIDADE_MEDIDA INT REFERENCES UNIDADE_MEDIDA (ID),
	ID_CATEGORIA INT REFERENCES CATEGORIA (ID),
	DESCRICAO VARCHAR(MAX),
	FOTO VARCHAR(2000)
);

CREATE TABLE PEDIDO (
	ID_PEDIDO INT PRIMARY KEY IDENTITY(1,1),
	ID_CLIENTE INT REFERENCES CLIENTE (ID_CLIENTE),
	DATAPEDIDO DATETIME NOT NULL DEFAULT GETDATE(),
	DATAENTREGA DATETIME,
	[STATUS] INTEGER
);

CREATE TABLE ITEM_PEDIDO (
	ID_ITEM_PEDIDO INT PRIMARY KEY IDENTITY(1,1),
	ID_PEDIDO INT REFERENCES PEDIDO (ID_PEDIDO),
	ID_PRODUTO INT REFERENCES PRODUTO (ID_PRODUTO),
	PRECO DECIMAL(10,2),
	QTD_ITEM_PRODUTO INT
);

select*from CLIENTE