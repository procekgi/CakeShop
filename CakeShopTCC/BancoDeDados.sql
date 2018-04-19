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
	Login_Cliente varchar(50),
	Senha varchar(8),
	Endereco varchar(300),
	Numero int,
	Complemento varchar(50),
	CEP int,
	Cidade varchar(50),
	Estado char(3)
);

alter table cliente 
add Login_Cliente Varchar(50);

alter table cliente
drop column login_usuario;

alter table cliente
add Complemento varchar(50);

select*from cliente


create table Contato(
	Id int primary key identity(1,1),
	NomeCliente varchar (100),
	emailCliente varchar(200),
	Mensagem varchar(max))

select*from Contato

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

insert into UnidadeDeMedida (Nome, Sigla)
values
('Quilograma', 'kg'),
('Grama', 'g'),
('Unit�rio', 'un')

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

insert into usuario(id, Nome, LoginUsuario, Senha, Email)
values
(1, 'Giovana Machado', 'ConfeiteiraLog', 'cs_giovana', 'procekgi@gmail.com')
	
select*from usuario




select * from usuario
select * from cliente