CREATE DATABASE IF NOT EXISTS tienda_db;
USE tienda_db;

CREATE TABLE IF NOT EXISTS productos (
    id_producto INT AUTO_INCREMENT PRIMARY KEY,
    codigo_barras VARCHAR(50) NOT NULL UNIQUE, 
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio_venta DECIMAL(10, 2) NOT NULL,
    precio_compra DECIMAL(10, 2),
    existencia INT NOT NULL DEFAULT 0,
    descontinuado BOOLEAN NOT NULL DEFAULT FALSE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


DELIMITER $$
CREATE PROCEDURE sp_consultar_productos ( IN p_codigo_barras varchar(50))
	BEGIN
		SELECT 
		id_producto, 
		codigo_barras, 
		nombre, 
		descripcion, 
		precio_venta, 
		existencia
	FROM 
		productos 
	WHERE 
		codigo_barras = p_codigo_barras AND descontinuado = FALSE;
END$$
DELIMITER ;



INSERT INTO productos (codigo_barras, nombre, descripcion, precio_venta, precio_compra, existencia, descontinuado) VALUES
('7501234567890', 'Coca Cola 600ml', 'Refresco de cola', 15.50, 10.00, 100, FALSE),
('7501234567891', 'Sabritas Original 45g', 'Papas fritas sabor original', 12.00, 8.00, 150, FALSE),
('7501234567892', 'Agua Bonafont 1L', 'Agua purificada', 10.00, 6.50, 200, FALSE),
('7501234567893', 'Galletas Marías', 'Galletas tradicionales', 18.00, 12.00, 80, FALSE),
('7501234567894', 'Yogurt Danone Natural', 'Yogurt natural 1L', 25.00, 18.00, 60, FALSE),
('7501234567895', 'Pan Blanco Bimbo', 'Pan de caja blanco', 35.00, 25.00, 40, FALSE),
('7501234567896', 'Leche Lala 1L', 'Leche entera pasteurizada', 22.00, 16.00, 75, FALSE),
('7501234567897', 'Cereal Zucaritas 500g', 'Cereal de maíz azucarado', 65.00, 48.00, 30, FALSE),
('7501234567898', 'Jabón Zote 200g', 'Jabón para lavar ropa', 14.00, 9.00, 120, FALSE),
('7501234567899', 'Shampoo Sedal 350ml', 'Shampoo para cabello normal', 45.00, 32.00, 50, FALSE);
