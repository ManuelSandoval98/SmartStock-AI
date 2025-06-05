-- Tabla ROLES
CREATE TABLE Roles (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

-- Tabla USUARIOS
CREATE TABLE Usuarios (
    dni VARCHAR(20) PRIMARY KEY,
    nombre VARCHAR(100),
    apellido VARCHAR(100),
    direccion TEXT,
    celular VARCHAR(20),
    razon_social VARCHAR(150),
    correo VARCHAR(150),
    contrasena VARCHAR(255),
    fecha_creacion TIMESTAMP,
    id_rol INT,
    FOREIGN KEY (id_rol) REFERENCES Roles(id)
);

-- Tabla CATEGORIAS
CREATE TABLE Categorias (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100)
);

-- Tabla PRODUCTOS
CREATE TABLE Productos (
    id SERIAL PRIMARY KEY,
    cod_producto VARCHAR(50),
    nombre VARCHAR(100),
    descripcion TEXT,
    stock INT,
    umbral INT,
    id_categoria INT REFERENCES Categorias(id),
    precio_venta FLOAT,
    precio_compra FLOAT,
    precio_descuento FLOAT,
    fecha_ingreso TIMESTAMP,
    fecha_egreso TIMESTAMP
);

-- Tabla PROVEEDORES
CREATE TABLE Proveedores (
    id SERIAL PRIMARY KEY,
    nombre_empresa VARCHAR(150),
    ruc VARCHAR(20),
    direccion TEXT,
    telefono VARCHAR(20),
    correo VARCHAR(100)
);

-- Tabla INGRESOS_PRODUCTO
CREATE TABLE Ingresos_Producto (
    id SERIAL PRIMARY KEY,
    id_producto INT REFERENCES Productos(id),
    id_proveedor INT REFERENCES Proveedores(id),
    cantidad INT,
    precio_compra FLOAT,
    fecha_ingreso TIMESTAMP,
    observacion TEXT
);

-- Tabla VENTAS
CREATE TABLE Ventas (
    id SERIAL PRIMARY KEY,
    id_usuario VARCHAR(20) REFERENCES Usuarios(dni),
    fecha_venta TIMESTAMP,
    total_venta FLOAT,
    metodo_pago VARCHAR(50)
);

-- Tabla DETALLE_VENTA
CREATE TABLE Detalle_Venta (
    id SERIAL PRIMARY KEY,
    id_venta INT REFERENCES Ventas(id),
    id_producto INT REFERENCES Productos(id),
    cantidad INT,
    precio_unitario FLOAT,
    descuento_aplicado FLOAT
);

-- Tabla MOVIMIENTOS_INVENTARIO
CREATE TABLE Movimientos_Inventario (
    id SERIAL PRIMARY KEY,
    id_producto INT REFERENCES Productos(id),
    tipo_movimiento VARCHAR(50),
    cantidad INT,
    fecha_movimiento TIMESTAMP,
    observacion TEXT
);

-- Tabla CLIENTES
CREATE TABLE Clientes (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100),
    correo VARCHAR(150),
    telefono VARCHAR(20),
    direccion TEXT
);

-- Tabla LOGS (auditoría)
CREATE TABLE Logs (
    id SERIAL PRIMARY KEY,
    usuario VARCHAR(20) REFERENCES Usuarios(dni),
    accion TEXT,
    tabla_afectada VARCHAR(100),
    fecha TIMESTAMP
);
