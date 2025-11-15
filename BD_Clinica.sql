-- ======================================================
--   CREACIÓN DE BASE DE DATOS PARA SISTEMA MÉDICO
-- ======================================================

-- TABLA: USUARIOS
CREATE TABLE usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    correo VARCHAR(150) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    telefono VARCHAR(20),
    rol VARCHAR(20) NOT NULL CHECK (rol IN ('paciente', 'medico', 'admin')),
    esta_activo BOOLEAN DEFAULT TRUE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABLA: PACIENTES
CREATE TABLE pacientes (
    id_paciente SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL UNIQUE REFERENCES usuarios(id_usuario) ON DELETE CASCADE,
    fecha_nacimiento DATE,
    genero VARCHAR(10),
    direccion TEXT
);

-- TABLA: MÉDICOS
CREATE TABLE medicos (
    id_medico SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL UNIQUE REFERENCES usuarios(id_usuario) ON DELETE CASCADE,
    especialidad VARCHAR(100) NOT NULL,
    numero_cedula VARCHAR(50) UNIQUE NOT NULL,
    hora_inicio TIME NOT NULL DEFAULT '09:00',
    hora_fin TIME NOT NULL DEFAULT '17:00',
    duracion_turno_minutos INT NOT NULL DEFAULT 30
);

-- TABLA: CITAS (fecha y hora separadas)
CREATE TABLE citas (
    id_cita SERIAL PRIMARY KEY,
    id_paciente INT NOT NULL REFERENCES pacientes(id_paciente) ON DELETE CASCADE,
    id_medico INT NOT NULL REFERENCES medicos(id_medico) ON DELETE CASCADE,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    estado VARCHAR(20) DEFAULT 'pendiente'
        CHECK (estado IN ('pendiente', 'confirmada', 'cancelada', 'realizada')),
    motivo TEXT,
    observaciones TEXT,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABLA: HISTORIAL CLÍNICO
CREATE TABLE historial_clinico (
    id_historial SERIAL PRIMARY KEY,
    id_paciente INT NOT NULL REFERENCES pacientes(id_paciente) ON DELETE CASCADE,
    id_medico INT REFERENCES medicos(id_medico) ON DELETE SET NULL,
    id_cita INT REFERENCES citas(id_cita) ON DELETE SET NULL,
    diagnostico TEXT NOT NULL,
    tratamiento TEXT,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABLA: TOKENS DE SESIÓN
CREATE TABLE tokens_sesion (
    id_token SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL REFERENCES usuarios(id_usuario) ON DELETE CASCADE,
    token VARCHAR(500) NOT NULL,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_expiracion TIMESTAMP
);

-- TABLA: HORARIOS DE MÉDICOS
CREATE TABLE horarios_medicos (
    id_horario SERIAL PRIMARY KEY,
    id_medico INT NOT NULL REFERENCES medicos(id_medico) ON DELETE CASCADE,
    dia_semana INT NOT NULL CHECK (dia_semana BETWEEN 1 AND 7),
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL
);


-- ======================================================
--                  DATOS DE PRUEBA
-- ======================================================

INSERT INTO usuarios (nombre, apellido, correo, contrasena, rol)
VALUES 
('Luis', 'Martínez', 'luis@hospital.com', 'HASHED_PASS', 'medico'),
('Ana', 'López', 'ana@gmail.com', 'HASHED_PASS', 'paciente');

INSERT INTO medicos (id_usuario, especialidad, numero_cedula)
VALUES (1, 'Cardiología', 'MED123456');

INSERT INTO pacientes (id_usuario, fecha_nacimiento, genero, direccion)
VALUES (2, '1995-04-20', 'Femenino', 'Av. Siempre Viva #123');

INSERT INTO citas (id_paciente, id_medico, fecha, hora, motivo)
VALUES (1, 1, '2025-10-15', '10:30', 'Chequeo general');

INSERT INTO horarios_medicos (id_medico, dia_semana, hora_inicio, hora_fin)
VALUES
(1, 1, '09:00', '13:00'),
(1, 1, '14:00', '18:00'),
(1, 2, '10:00', '16:00');

-- ======================================================
--               CONSULTAS DE VERIFICACIÓN
-- ======================================================

SELECT * FROM usuarios;
SELECT * FROM medicos;
SELECT * FROM pacientes;
SELECT * FROM citas;
