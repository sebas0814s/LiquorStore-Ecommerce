# VerificacionEdad

Proyecto funcionalidad para la **verificación de edad** (ASP.NET Core MVC + EF Core + Bootstrap).  
Permite validar la fecha de nacimiento del usuario (>= 18 años), guardar el resultado y listar usuarios verificados.
Incluye pruebas unitarias con xUnit.

---

## 🚀 Tecnologías usadas
- **ASP.NET Core MVC** — estructura de controladores, vistas y modelos.  
- **Entity Framework Core (InMemory)** — persistencia de datos en memoria.  
- **Bootstrap 5** — estilos y componentes para la interfaz.  
- **xUnit** — framework de pruebas unitarias.  

### Requisitos
- .NET SDK (6 o 7)  
- Visual Studio 2022/2023 o VS Code  

## 🖼️ Frontend (vistas)
- VerifyAge.cshtml: formulario con campos de fecha de nacimiento y número de documento.
- Validaciones en cliente con JavaScript:
- Que la fecha no esté vacía.
- Que no sea futura.
- Que la edad sea >= 18.
- Llamada fetch al endpoint /api/users/verify-age.
- Listar.cshtml: tabla con los usuarios ya verificados.

## ⚙️ Backend (controladores, helpers)
# UsuarioController:
- VerifyAge() → carga la vista de verificación.
- VerifEdadApi(dto) → recibe datos en JSON, valida:
  - Fecha vacía o inválida → 400 BadRequest.
  - Fecha futura → 400 BadRequest.
  - Menor de 18 → 403 Forbid.
  - Adulto → guarda en la BD y responde 200 OK.
# EdadHelper:
- CalcularEdad(dob, now) → calcula edad exacta considerando meses y días.
- EsMayorDeEdad(dob, now) → devuelve true si >= 18.

## 🧪 Pruebas unitarias
- Ejecutar pruebas:
  - dotnet test
	 
# Incluyen:
- EdadHelperTests
- Calcula edad exacta a los 18 años.
- Caso de día antes del cumpleaños.
- Caso fecha futura.
- UsuarioControllerTests
- Usuario menor → retorna Forbid, no guarda en DB.
- Usuario adulto → guarda en DB y retorna Ok.
- Fecha futura → retorna BadRequest.

## 🔗 Endpoints principales
POST /api/users/verify-age
Entrada JSON:
{
  "fechaCumple": "2000-05-01",
  "numDocumento": "123456"
}

# Respuestas:
- 200 OK → verificación exitosa.
- 403 Forbid → menor de edad.
- 400 BadRequest → fecha inválida o futura.
# GET /Usuario/Listar
- Devuelve la vista con la lista de usuarios verificados.

## 📦 Cómo integrarlo en otro proyecto
# Copiar:
- Models/Usuario.cs y VerificacionEdadDTO.cs.
- Helpers/EdadHelper.cs.
- Controllers/UsuarioController.cs.
- Vistas de Usuario/VerifyAge.cshtml y Usuario/Listar.cshtml.
# Registrar el DbContext en Program.cs:
- builder.Services.AddDbContext<VerifEdadContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Adaptar para trabajar con usuarios reales (ej. Identity).

Ejecutar migraciones de EF Core para persistencia real.

## 📁 Estructura del proyecto
/VerificacionEdad
  /Controllers
  /Data
  /Helpers
  /Models
  /Views
  /wwwroot
  /Tests
  README.md

