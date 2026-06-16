using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VerificacionEdad.Data;      // ajusta namespace real
using VerificacionEdad.Controllers; // ajusta si tu controller está en otro namespace
using VerificacionEdad.Models;    // para Usuario, VerificacionEdadDTO
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace VerificacionEdad.Tests
{
    public class UsuarioControllerTests
    {
        // Crea un contexto de base de datos en memoria para pruebas
        private VerifEdadContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<VerifEdadContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new VerifEdadContext(options);
        }
        // Prueba que un menor de edad recibe Forbid y no se guarda en DB
        [Fact]
        public void VerifEdadApi_Underage_ReturnsForbid()
        {
            // Arrange
            using var db = CreateContext();
            var logger = NullLogger<UsuarioController>.Instance;
            var controller = new UsuarioController(db, logger);

            var dto = new VerificacionEdadDTO
            {
                FechaCumple = DateTime.Today.AddYears(-17),
                NumDocumento = "123"
            };

            // Act
            var result = controller.VerifEdadApi(dto);

            // Assert
            Assert.IsType<ForbidResult>(result);
            Assert.Empty(db.Usuarios.ToList()); // no se guardó nada
        }

        // Prueba que un adulto se guarda en DB y retorna Ok
        [Fact]
        public void VerifEdadApi_Adult_SavesUserAndReturnsOk()
        {
            // Arrange
            using var db = CreateContext();
            var logger = NullLogger<UsuarioController>.Instance;
            var controller = new UsuarioController(db, logger);

            var dto = new VerificacionEdadDTO
            {
                FechaCumple = DateTime.Today.AddYears(-25),
                NumDocumento = "ABC123"
            };

            // Act
            var result = controller.VerifEdadApi(dto);

            // Assert result is OK
            var ok = Assert.IsType<OkObjectResult>(result);
            // Assert DB updated
            var user = db.Usuarios.FirstOrDefault();
            Assert.NotNull(user);
            Assert.True(user.EdadVerificada);
            Assert.Equal(dto.FechaCumple, user.FechaCumpleaños);
        }

        // Prueba que una fecha inválida retorna BadRequest y no se guarda en DB
        [Fact]
        public void VerifEdadApi_FutureDate_ReturnsBadRequest()
        {
            using var db = CreateContext();
            var logger = NullLogger<UsuarioController>.Instance;
            var controller = new UsuarioController(db, logger);

            var dto = new VerificacionEdadDTO
            {
                FechaCumple = DateTime.Today.AddDays(1), // futuro
                NumDocumento = "X"
            };

            var result = controller.VerifEdadApi(dto);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Empty(db.Usuarios.ToList());
        }
    }
}
