------- Insertar Datos en tablas-------

INSERT INTO Catalogos.Especialidad(especialidad)
VALUES ('Medicina Interna'),
	   ('Pediatría'),
	   ('Cardiología'),
	   ('Neurología'),
	   ('Neumonología')

INSERT INTO Catalogos.Paises(id, pais)
VALUES  ('PA', 'Panamá'),
		('CO', 'Colombia'),
		('VE', 'Venezuela'),
		('ES', 'España'),
		('DO', 'Republica Dominicana'),
		('CA', 'Canada')

INSERT INTO Transaccion.Medico(nombre, apellido, numeroDocumento)
VALUES ('Pedro','Perez','1111111'),
	   ('Juan', 'Gil', '2222222'),
	   ('Maria', 'Jimenez','3333333'),
	   ('Carla', 'Contreras', '4444444'),
	   ('Monica', 'Alvarez', '5555555')

INSERT INTO Transaccion.MedicoEspecialidad(idMedico, idEspecialidad, descripcion)
VALUES  (1, 1, null),
		(2, 2, null),
		(3, 3, null),
		(4, 4, null),
		(5, 5, null),
		(5, 2, 'Especialista en')
	
INSERT INTO Transaccion.Paciente(nombre, apellido, numeroDocumento, fechaNacimiento, direccion, idPais, telefono, email, observacion)
VALUES ('Manuel','Mendez','6666666', '2000-01-10', 'Direccion de Prueba 1','PA', null, 'm.mendez@emailficticio.com',''),
	   ('Carlos', 'Crespo', '7777777', '2001-02-11', 'Direccion de Prueba 2', 'VE', '8274192', 'c.crespo@emailficticio.com',''),
	   ('Mario', 'Monagas','8888888', '2002-03-12', 'Direccion de Prueba 3', 'CO', null, 'm.monagas@emailficticio.com',''),
	   ('Mario', 'Montilla', '9999999', '2003-04-14', 'Direccion de Prueba 4', 'DO', null, 'm.montilla@emailficticio.com',''),
	   ('Pablo', 'Pinto', '101010101', '2004-04-15', 'Direccion de Prueba 5', 'ES', '3147822', 'p.pinto@emailficticio.com','')

INSERT INTO Transaccion.PacienteMedico(idPaciente, idMedico)
VALUES  (1, 5),
		(2, 4),
		(2, 3),
		(3, 1),
		(4, 3),
		(5, 2)

------- Consulta usando inner joins-------
select m.id, m.nombre, m.apellido, m.numeroDocumento, e.especialidad from Transaccion.Medico m
inner join Transaccion.MedicoEspecialidad me on me.idMedico = m.id
inner join Catalogos.Especialidad e on e.id = me.idEspecialidad

------- Consulta usando left joins-------

select p.pais, m.id, m.nombre, m.apellido, m.numeroDocumento  from Catalogos.Paises p
left join Transaccion.Paciente m on m.idPais = p.id

------- Consulta usando union-------
select nombre, apellido, numeroDocumento from Transaccion.Paciente
union
select nombre, apellido, numeroDocumento from Transaccion.Medico

------- Consulta usando case-------
SELECT nombre, apellido,
CASE
    WHEN telefono IS NULL THEN 'Campo Telefono Nulo'
    ELSE telefono
END AS telefono
FROM Transaccion.Paciente;


/****** SP para el funcionamiento del Backend ******/

-------StoredProcedure [Transaccion].[sp_get_listamedicos]-------
CREATE PROCEDURE [Transaccion].[sp_get_listamedicos]
as
begin

select m.nombre, m.apellido, m.numeroDocumento, e.especialidad, me.descripcion from Transaccion.Medico m
inner join Transaccion.MedicoEspecialidad me on me.idMedico = m.id
inner join Catalogos.Especialidad e on e.id = me.idEspecialidad

end
GO

-------StoredProcedure [Transaccion].[sp_get_medico]-------

CREATE PROCEDURE [Transaccion].[sp_get_medico]
(
  @id int
)
as
begin

select top 1 m.nombre, m.apellido, m.numeroDocumento, e.especialidad, me.descripcion from Transaccion.Medico m
inner join Transaccion.MedicoEspecialidad me on me.idMedico = m.id
inner join Catalogos.Especialidad e on e.id = me.idEspecialidad
where m.id = @id order by m.id

end
GO

-------StoredProcedure [Transaccion].[sp_upd_medico]-------
CREATE PROC [Transaccion].[sp_upd_medico](
				@idmedico int,
				@nombre varchar(50),
				@apellido varchar(50),
				@numeroDocumento varchar(50),
				@especialidad int,
				@descripcion varchar(50))

AS
begin

	set nocount on

	if exists(SELECT * from Transaccion.Medico WHERE id = @idmedico)
		BEGIN
			UPDATE Transaccion.Medico SET nombre = @nombre,	apellido = @apellido, numeroDocumento = @numeroDocumento
			WHERE id = @idmedico
				if exists(SELECT * from MedicoEspecialidad WHERE idMedico = @idmedico)
				UPDATE Transaccion.MedicoEspecialidad SET idEspecialidad = @especialidad, descripcion = @descripcion
				WHERE idMedico = @idmedico
		END
	ELSE
		SELECT 0 as resultado
end
GO

-------StoredProcedure [Transaccion].[sp_del_medico]-------
CREATE PROCEDURE [Transaccion].[sp_del_medico]
(
  @idmedico int
)
as
begin

	set nocount on

	if exists(SELECT * from Transaccion.Medico WHERE id = @idmedico)
		BEGIN
			if exists(SELECT * from MedicoEspecialidad WHERE idMedico = @idmedico)
				BEGIN
					DELETE FROM Transaccion.MedicoEspecialidad WHERE idMedico = @idmedico
					DELETE FROM Transaccion.Medico WHERE id = @idmedico
				END
			else
				DELETE FROM Transaccion.Medico WHERE id = @idmedico
		END
	ELSE
		SELECT 0 as resultado
end
GO