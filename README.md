🍷 LiquorApp

Aplicación de e-commerce para la venta de licores con verificación de edad obligatoria, diseñada para garantizar compras seguras, legales y eficientes.

🎯 Objetivo

Construir una aplicación web/móvil (PWA) con frontend en HTML, CSS y JavaScript y backend en C# .NET + SQL Server, que permita la compra y venta de licores de forma intuitiva, escalable y cumpliendo con las regulaciones legales.

🚀 Funcionalidades Principales (MVP)

👤 Gestión de Usuarios

Registro e inicio de sesión (JWT Auth).

Recuperación de contraseña.

Verificación de edad mediante documento/fecha.

Gestión de perfil.

🛒 Catálogo de Productos

Visualización de productos con detalles.

Búsqueda por nombre/categoría.

Filtros y ordenamiento (precio, popularidad).

📦 Gestión de Compras

Carrito de compras (agregar, editar, eliminar).

Cálculo de totales y descuentos.

💳 Proceso de Pago

Selección de método de pago.

Validación y procesamiento seguro vía pasarela externa.

Generación de factura y confirmación de compra.

🚚 Seguimiento de Pedidos

Estado del pedido en tiempo real.

Historial de compras.

Notificaciones push/email.

⭐ Extras

Sistema de calificaciones y reseñas.

Soporte multilenguaje (ES/EN).

Integración con servicios de entrega.

🛠️ Stack Tecnológico

Frontend: HTML5, CSS3, JavaScript (PWA instalable en Android/iOS).

Backend: C# .NET Core 8 (API RESTful con arquitectura limpia).

Base de Datos: SQL Server (con Entity Framework Core).

Autenticación: JWT + Refresh Tokens (con opción 2FA).

Control de versiones: Git + GitHub.

Diseño UI/UX: Figma.

Pruebas: xUnit (backend), Jest/Vitest (frontend), Postman (APIs).

CI/CD: GitHub Actions + despliegue en Azure.

📐 Requisitos No Funcionales

Interfaz intuitiva, accesible (WCAG 2.1 AA).

Tiempo de respuesta < 2s.

Seguridad: encriptación de datos sensibles, 2FA, antifraude.

Escalabilidad: soportar 10,000 usuarios concurrentes.

Compatibilidad: navegadores modernos, PWA en Android 8.0+ e iOS 12+.

Código documentado, pruebas automatizadas y monitoreo de errores.

📊 Casos de Uso Principales

Registrar usuario.

Iniciar sesión.

Verificar edad.

Consultar catálogo de productos.

Ver producto y agregarlo al carrito.

Procesar compra.

Ver historial de compras.

Configurar perfil.

Administrar productos (rol interno).

📅 Roadmap (alto nivel)

Sprint 1: Registro, login y verificación de edad.

Sprint 2: Catálogo y carrito.

Sprint 3: Compras y pagos.

Sprint 4: Historial de compras y administración.

👥 Equipo de Desarrollo

Sebastián Gutiérrez – Product Owner / Dev

Daniel – Backend Dev

Estiven Parra – Frontend De

## Quickstart (Registro de Usuarios)

- Requisitos: .NET 8 SDK, SQL Server (o LocalDB).
- Backend:
  - Ejecuta: `cd LiquorApp.Api && dotnet run`
  - Configura la cadena de conexión en `LiquorApp.Api/appsettings.json` (`ConnectionStrings:DefaultConnection`).
  - Por defecto usa `(localdb)\\MSSQLLocalDB`.
- Frontend mínimo:
  - Disponible en `http://localhost:5000/` ó `https://localhost:5001/` (sirve `wwwroot/index.html`).

### Endpoint

- `POST /api/users/register`
- Request JSON:
  ```json
  {
    "firstName": "Ana",
    "lastName": "García",
    "email": "ana@example.com",
    "password": "Abcdef1$",
    "confirmPassword": "Abcdef1$"
  }
  ```
- Respuestas:
  - 201 Created: usuario creado
  - 400 Bad Request: validación
  - 409 Conflict: email en uso

### Validaciones (cliente y servidor)
- Requeridos: nombre, apellido, email, password, confirmación
- Email válido
- Password: mínimo 8 caracteres, mayúscula, minúscula, dígito y símbolo
- Confirmación igual a password

### Pruebas (xUnit)
- Ejecuta: `dotnet test`
- Cobertura de:
  - Validación de registro
  - Hash y verificación de contraseñas
  - Índice único por email en el modelo EF Core

