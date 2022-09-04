--Scripts de defini��o de banco (DDLs)

create database ConsulVet;
go

use ConsulVet;
go

-- Criar tabela Cliente com todas as colunas definidas
-- O Id � auto increment
create table Cliente(
	Id int primary key identity,
	Nome nvarchar(max),
	Email nvarchar(max),
	Senha nvarchar(max),
	NomePet nvarchar(max),
	Imagem nvarchar(max)
);
go

-- Criar tabela Veterin�rio com todas as colunas definidas
-- O Id � auto increment
create table Veterinario(
	Id int primary key identity,
	Nome nvarchar(max),
	Email nvarchar(max),
	Senha nvarchar(max),
	Imagem nvarchar(max)
);
go

-- Criar tabela Consulta para a rela��o NxN
-- O Id � auto increment
create table Consulta(
	Id int primary key identity,
	DataHora datetime,

	--FK
	ClienteId int
	foreign key (ClienteId) references Cliente(Id),

	--FK
	VeterinarioId int
	foreign key (VeterinarioId) references Veterinario(Id)
);
go

-- Criar tabela Resultado para a rela��o NxN
-- O Id � auto increment
create table Resultado(
	Id int primary key identity,
	Diagnostico nvarchar(max),
	--FKs
	ClienteId int
	foreign key (ClienteId) references Cliente(Id),

	--FKs
	VeterinarioId int
	foreign key (VeterinarioId) references Veterinario(Id)
);
go

