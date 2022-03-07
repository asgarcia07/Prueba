--Script Para Generar la BD--

USE [master]
GO

/******Crear BD******/
CREATE DATABASE [Prueba_TBTB]
GO

USE [Prueba_TBTB]
GO

/****** Crear Esquemas ******/
CREATE SCHEMA [Catalogos]
GO
CREATE SCHEMA [Transaccion]
GO

/****** Crear Tablas ******/

-------Tabla Especialiadad-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogos].[Especialidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[especialidad] [varchar](50) NOT NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idEspecialidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------Tabla Paises-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogos].[Paises](
	[id] [char](3) NOT NULL,
	[pais] [varchar](30) NOT NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idPais] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------Tabla Medico-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transaccion].[Medico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[numeroDocumento] [varchar](50) NOT NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idMedico] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------Tabla MedicoEspecialidad-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transaccion].[MedicoEspecialidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idMedico] [int] NOT NULL,
	[idEspecialidad] [int] NOT NULL,
	[descripcion] [varchar](50) NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idMedicoEspecialidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------Tabla Paciente-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transaccion].[Paciente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[numeroDocumento] [varchar](50) NOT NULL,
	[fechaNacimiento] [date] NULL,
	[direccion] [varchar](100) NULL,
	[idPais] [char](3) NOT NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](30) NULL,
	[observacion] [varchar](1000) NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idPaciente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------Tabla PacienteMedico-------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transaccion].[PacienteMedico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[idMedico] [int] NOT NULL,
	[creacion] [datetime] NULL,
	[actualizacion] [datetime] NULL,
 CONSTRAINT [PK_idPacienteMedico] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

------- FOREIGN KEY y Valores por defecto-------
ALTER TABLE [Catalogos].[Especialidad] ADD  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Catalogos].[Especialidad] ADD  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Catalogos].[Paises] ADD  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Catalogos].[Paises] ADD  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Transaccion].[Medico] ADD  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Transaccion].[Medico] ADD  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad] ADD  CONSTRAINT [DF__MedicoEsp__creac__7C4F7684]  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad] ADD  CONSTRAINT [DF__MedicoEsp__actua__7D439ABD]  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Transaccion].[Paciente] ADD  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Transaccion].[Paciente] ADD  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Transaccion].[PacienteMedico] ADD  DEFAULT (getdate()) FOR [creacion]
GO
ALTER TABLE [Transaccion].[PacienteMedico] ADD  DEFAULT (getdate()) FOR [actualizacion]
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad]  WITH CHECK ADD  CONSTRAINT [FK_MedicoEspecialidad_Especialidad] FOREIGN KEY([idEspecialidad])
REFERENCES [Catalogos].[Especialidad] ([id])
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad] CHECK CONSTRAINT [FK_MedicoEspecialidad_Especialidad]
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad]  WITH CHECK ADD  CONSTRAINT [FK_MedicoEspecialidad_Medico] FOREIGN KEY([idMedico])
REFERENCES [Transaccion].[Medico] ([id])
GO
ALTER TABLE [Transaccion].[MedicoEspecialidad] CHECK CONSTRAINT [FK_MedicoEspecialidad_Medico]
GO
ALTER TABLE [Transaccion].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Paises] FOREIGN KEY([idPais])
REFERENCES [Catalogos].[Paises] ([id])
GO
ALTER TABLE [Transaccion].[Paciente] CHECK CONSTRAINT [FK_Paciente_Paises]
GO
ALTER TABLE [Transaccion].[PacienteMedico]  WITH CHECK ADD  CONSTRAINT [FK_PacienteMedico_Medico] FOREIGN KEY([idMedico])
REFERENCES [Transaccion].[Medico] ([id])
GO
ALTER TABLE [Transaccion].[PacienteMedico] CHECK CONSTRAINT [FK_PacienteMedico_Medico]
GO
ALTER TABLE [Transaccion].[PacienteMedico]  WITH CHECK ADD  CONSTRAINT [FK_PacienteMedico_Paciente] FOREIGN KEY([idPaciente])
REFERENCES [Transaccion].[Paciente] ([id])
GO
ALTER TABLE [Transaccion].[PacienteMedico] CHECK CONSTRAINT [FK_PacienteMedico_Paciente]
GO