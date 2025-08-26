# LiquorApp

Aplicación de e-commerce para venta de licores con verificación de edad.

## Objetivo

Construir una aplicación móvil multiplataforma (Android/iOS) en React Native con backend en Firebase, que permita la compra/venta de licores de forma segura, legal y eficiente.

## Funcionalidades principales (MVP)

### Gestión de Usuarios
- Registro e inicio de sesión (Firebase Auth).
- Recuperación de contraseña.
- Verificación de edad con documento.
- Gestión de perfil.

### Catálogo de Productos
- Visualización de productos con detalles.
- Búsqueda por nombre/categoría.
- Filtros y ordenamiento (precio, popularidad).

### Gestión de Compras
- Carrito de compras (agregar, editar, eliminar).
- Cálculo de totales y descuentos.

### Proceso de Pago
- Selección de método de pago.
- Validación y procesamiento seguro.
- Generación de factura y confirmación.

### Seguimiento de Pedidos
- Estado del pedido en tiempo real.
- Historial de compras.
- Notificaciones push (Firebase Cloud Messaging).

### Extras
- Sistema de calificaciones y reseñas.
- Soporte multilenguaje.
- Integración con servicios de entrega.

## Stack Tecnológico
- **Frontend:** React Native
- **Backend & Servicios:** Firebase (Auth, Firestore/Realtime DB, Storage, Cloud Functions, Push Notifications)
- **IDE/Dev Tools:** Android Studio, Xcode, Node.js/npm, GitHub
- **Diseño:** Figma
- **Pruebas:** Postman

## Requisitos no funcionales
- Interfaz intuitiva y accesible (WCAG 2.1).
- Respuesta < 2s.
- Seguridad: encriptación, 2FA, protección antifraude.
- Escalabilidad: soportar 10,000 usuarios concurrentes.
- Compatibilidad: Android 8.0+, iOS 12+.
- Código documentado, pruebas automatizadas, monitoreo de errores.

## Casos de Uso principales
1. Registrar usuario.
2. Iniciar sesión.
3. Verificar edad.
4. Consultar catálogo.
5. Ver producto y agregar al carrito.
6. Procesar compra.
7. Ver historial de compras.
8. Configurar perfil.
