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
insert into cliente
(Nome_Cliente, Telefone, Email)
values
('Giovana', '92000-2313', 'procekgi@gmail')



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
	QTD_Item_pedido int,
	Horario_Entrega varchar(4)

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
	Descricao varchar(max),
	foto varchar(2000)
);

create table ITEM_PEDIDO (
	ID_ITEM_PEDIDO  int primary key identity(1,1),
	ID_PEDIDO int references PEDIDO(Id_pedido),
	ID_PRODUTO int references PRODUTO(Id_produto),
	PRECO decimal(15,2),
	QTD_ITEM_PRODUTO int
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

--update pedido set datapedido = getdate();

select * from Item_pedido;

select * from UnidadeDeMedida

select * from categoria;

select * from produto;


insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
values ('Bolo 1', 10, 3, 6, 'balblalalblballbalbalblba albballba balbalba');

insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
values ('Salgado 1', 5, 2, 5, 'balblalalblballbalbalblba albballba balbalba');

insert into produto (Nome_Produto, Preco, Id_UnidadeDeMedida, Id_Categoria, Descricao)
values ('Doce 1', 3, 1, 4, 'balblalalblballbalbalblba albballba balbalba');

insert into pedido
(Id_cliente,DataPedido,DataEntrega,QTD_Item_pedido)
values
(1, '30-05-2018 20:03', '05-06-2018 15:30', 06)


insert into Item_pedido
(Id_pedido,Id_produto, preco, QTD_Item_produto)
values
(1,1, 10, 01),
(1,2, 11, 200),
(1,3, 12, 300);



alter table pedido drop column situacao;
alter table cliente drop column login_usuario

alter table cliente add LOGIN_CLIENTE VARCHAR(50)

select * from cliente

update cliente set LOGIN_CLIENTE = 'giovana', senha = '123', Endereco = 'Endereco', Numero = '4321', cep = '81123-123', Cidade = 'CWB', Estado = 'PR', Complemento = 'ASDSADaaa'

alter table produto add foto varchar(2000);

select * from produto
select * from categoria



--update produto set foto = 'download.jpg'