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


select*from cliente


create table Contato(
	Id int primary key identity(1,1),
	NomeCliente varchar (100),
	emailCliente varchar(200),
	Mensagem varchar(max))


create table pedido (
	id_pedido int primary key identity(1,1),
	Id_cliente int references cliente(Id_cliente),
	DataPedido datetime not null default getdate(),
	DataEntrega datetime,
	QTD_Item_pedido int
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
('Unitário', 'un')

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

select * from pedido;

--insert into pedido (Id_cliente, DataEntrega) values (1, getdate());
--insert into pedido (Id_cliente, DataEntrega) values (1, getdate());

update pedido set datapedido = getdate();

select * from Item_pedido;

select * from UnidadeDeMedida

select * from categoria;

select * from produto;


--insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
--values ('Bolo 1', 10, 3, 3, 'balblalalblballbalbalblba albballba balbalba');

--insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
--values ('Salgado 1', 5, 2, 2, 'balblalalblballbalbalblba albballba balbalba');

--insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
--values ('Doce 1', 3, 1, 1, 'balblalalblballbalbalblba albballba balbalba');