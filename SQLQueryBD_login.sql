create table Usuarios(
idUsuarios varchar (10) not null,
contrase�aUsuarios varchar (45) not null,
tipoUsuarios varchar (45) not null,
primary key (idUsuarios))

Insert into Usuarios(idUsuarios,contrase�aUsuarios,tipoUsuarios)
values ('11111','carlsen','Administrador')

Insert into Usuarios(idUsuarios,contrase�aUsuarios,tipoUsuarios)
values ('22222','niggaMan','Cliente')

Insert into Usuarios(idUsuarios,contrase�aUsuarios,tipoUsuarios)
values ('33333','memintio','Administrador')

Insert into Usuarios(idUsuarios,contrase�aUsuarios,tipoUsuarios)
values ('44444','la_caja_negra','Administrador')

select * from Usuarios