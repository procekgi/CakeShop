use master
go

create database CakeShop
go

use CakeShop
go

create table cliente(
	Id_Cliente integer primary key identity(1,1),
	Nome_Cliente varchar(200),
	Telefone varchar(11),
	Email varchar(250),
	Login_usuario varchar(50),
	Senha varchar(8),
	Endereco varchar(300),
	Numero int,
	CEP int,
	Cidade varchar(50),
	Estado char(3)
);

create table pedido (
	id_pedido int primary key identity(1,1),
	Id_cliente int references cliente(Id_cliente),
	QTD_Item_pedido int,
	DataEntrega datetime
);

create table UnidadeDeMedida (
	Id int primary key identity(1,1),
	Nome varchar(20),
	Sigla char(3)
) 

create table Categoria (
	Id int primary key identity(1,1),
	Nome varchar(10)
);


insert into Categoria(Nome) 
values 
('Doces'),
( 'Salgados'),
( 'Bolos')


create table produto (
	Id_Produto int primary key identity(1,1),
	Nome_Produto varchar(200),
	Preco Decimal (10,2),
	Id_UnidadeDeMedida int references UnidadeDeMedida(Id),
	Id_Categoria int references Categoria(ID),
	Descricao varchar(max)
);

create table Item_pedido (
	Id_pedido int references pedido(Id_pedido),
	Item_produto varchar(100),
	Id_produto int references produto(Id_produto),
	QTD_Item_produto int
);

create table usuario (
	id int primary key,
	Nome varchar(100),
	LoginUsuario varchar(20),
	Senha varchar(10), 
	Email varchar(200)
);


select*from produto

