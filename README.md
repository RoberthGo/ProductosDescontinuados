# Sistema de Gestión de Productos Descontinuados

Sistema completo (Frontend + Backend) para gestionar productos descontinuados mediante escaneo de códigos de barras.

## Características

- Búsqueda de productos por código de barras
- Soporte para lectores de código de barras USB
- Descontinuación de productos con transacciones
- Validación de duplicados y estados
- API REST con documentación Swagger
- Verificación de conexión automática

## Requisitos

- Windows 10 o superior
- .NET 8.0 SDK
- MySQL Server 8.0+
- Lector de código de barras USB (opcional)

## Estructura del Proyecto

```
ProductosDescontinuados/
??? Frontend (Windows Forms)
?   ??? Models/
?   ??? Services/
?   ??? Form1.cs
?
??? Backend (ASP.NET Core API)
    ??? Controllers/
    ??? Models/
    ??? Services/
```

## Instalación

### 1. Base de Datos MySQL

Ejecuta el script del repositorio

### 2. Backend API

```bash
cd descontinuarProductosBackend
dotnet restore
dotnet run
```

La API estará disponible en:
- HTTP: `http://localhost:5053`
- Swagger: `http://localhost:5053/swagger`

### 3. Frontend

```bash
cd ProductosDescontinuados
dotnet restore
dotnet run
```

## Configuración

### Backend - Connection String

Edita `descontinuarProductosBackend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=tienda_db;User=root;Password=1234;Port=3309;"
  }
}
```

### Frontend - URL de la API

La URL se configura automáticamente en `Form1.cs`:
```csharp
_apiService = new ApiService("http://localhost:5053");
```

## Uso

### Agregar Productos a la Lista

1. Coloca el cursor en el campo "Código de Barras"
2. Escanea el producto con el lector USB o ingresa el código manualmente
3. Presiona `Enter` o haz clic en "Buscar"
4. El producto se agregará a la lista si existe y no está descontinuado

### Descontinuar Productos

1. Selecciona un producto de la lista (clic en la fila)
2. Haz clic en el botón "DESCONTINUAR"
3. Confirma la acción en el diálogo
4. El producto se marcará como descontinuado en la base de datos

## API Endpoints

### Consultar Producto
```http
GET /api/productos/consultar/{codigoBarras}

Response 200:
{
  "success": true,
  "message": "Producto encontrado exitosamente",
  "data": {
    "idProducto": 1,
    "codigoBarras": "7501234567890",
    "nombre": "Coca Cola 600ml",
    "descripcion": "Refresco de cola",
    "precioVenta": 15.50,
    "existencia": 100
  }
}
```

### Descontinuar Producto
```http
POST /api/productos/descontinuar
Content-Type: application/json

{
  "codigoBarras": "7501234567890"
}

Response 200:
{
  "success": true,
  "message": "Producto descontinuado exitosamente",
  "data": {
    "codigoBarras": "7501234567890"
  }
}
```

### Health Check
```http
GET /api/productos/health

Response 200:
{
  "status": "Healthy",
  "message": "API de Productos funcionando correctamente",
  "timestamp": "2025-01-27T10:30:00"
}
```

## Validaciones

El sistema valida automáticamente:
- Código de barras no vacío
- Producto existe en la base de datos
- Producto no está descontinuado
- Producto no está duplicado en la lista actual
- Conexión con el servidor activa

## Tecnologías

### Frontend
- .NET 8.0
- Windows Forms
- System.Text.Json
- HttpClient

### Backend
- ASP.NET Core 8.0
- MySQL
- Swagger/OpenAPI
- MySql.Data

