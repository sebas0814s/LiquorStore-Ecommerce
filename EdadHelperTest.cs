using System;
using VerificacionEdad.Helpers;
using Xunit;

namespace VerificacionEdad.Tests
{
    public class EdadHelperTests
    {
        //verifica que la función calcula correctamente la edad, cuando se tiene 18 años
        [Fact]
        public void CalcularEdad_Exacto18_Returns18()
        {
            // Arrange
            var today = DateTime.Today;
            var dob = today.AddYears(-18);

            // Act
            var edad = EdadHelper.CalcularEdad(dob, today);

            // Assert
            Assert.Equal(18, edad);
        }

        //verifica que la función calcula correctamente la edad, cuando se tiene 17 años y 364 día
        [Fact]
        public void CalcularEdad_UnDiaAntesCumple_Returns17()
        {
            // Arrange
            var today = new DateTime(2025, 9, 6); // ejemplo fijo
            var dob = new DateTime(2008, 9, 7);   // cumpliría mañana
            // Act
            var edad = EdadHelper.CalcularEdad(dob, today);
            // Assert
            Assert.Equal(16, edad); // 2025-2008 = 17, pero aún no cumple => 16
        }

        //verifica que la función calcula correctamente la edad, cuando se tiene 18 años y 1 día
        [Fact]
        public void EsMayorDeEdad_With19Years_ReturnsTrue()
        {
            var today = DateTime.Today;
            var dob = today.AddYears(-19);
            Assert.True(EdadHelper.EsMayorDeEdad(dob, today));
        }

        //verifica que la función calcula correctamente que es una edad negativa, cuando la fecha de nacimiento es en el futuro
        [Fact]
        public void CalcularEdad_FutureDate_ReturnsNegative()
        {
            var today = DateTime.Today;
            var dob = today.AddYears(1);
            Assert.True(EdadHelper.CalcularEdad(dob, today) < 0);
        }
    }
}
