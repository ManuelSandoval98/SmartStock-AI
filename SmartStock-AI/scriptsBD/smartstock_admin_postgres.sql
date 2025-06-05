-- Tabla de negocios registrados en el sistema
CREATE TABLE Negocios (
    id SERIAL PRIMARY KEY,
    nombre_comercial VARCHAR(150) NOT NULL,
    correo_admin VARCHAR(150) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    db_asociada VARCHAR(100) NOT NULL,
    estado BOOLEAN DEFAULT TRUE, -- Activo o inactivo
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabla para logs de acceso y control administrativo
CREATE TABLE AdminLogs (
    id SERIAL PRIMARY KEY,
    correo_admin VARCHAR(150),
    accion TEXT,
    fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (correo_admin) REFERENCES Negocios(correo_admin)
);

-- Tabla opcional para sesiones activas o control de tokens
CREATE TABLE Sesiones (
    id SERIAL PRIMARY KEY,
    correo_admin VARCHAR(150),
    token TEXT NOT NULL,
    fecha_inicio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_expiracion TIMESTAMP,
    FOREIGN KEY (correo_admin) REFERENCES Negocios(correo_admin)
);
