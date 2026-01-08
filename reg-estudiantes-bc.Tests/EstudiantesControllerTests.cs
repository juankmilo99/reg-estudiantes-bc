using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.Controllers;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Tests.Controllers;

public class EstudiantesControllerTests
{
    private readonly Mock<IEstudianteService> _mockService;
    private readonly EstudiantesController _controller;

    public EstudiantesControllerTests()
    {
        _mockService = new Mock<IEstudianteService>();
        _controller = new EstudiantesController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfEstudiantes()
    {
        // Arrange
        var estudiantes = new List<EstudianteDto>
        {
            new EstudianteDto { Id = 1, Nombre = "Juan Pérez", Email = "juan@example.com" },
            new EstudianteDto { Id = 2, Nombre = "María García", Email = "maria@example.com" }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(estudiantes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<EstudianteDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithEstudiante()
    {
        // Arrange
        var estudiante = new EstudianteDto { Id = 1, Nombre = "Juan Pérez", Email = "juan@example.com" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(estudiante);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<EstudianteDto>(okResult.Value);
        Assert.Equal("Juan Pérez", returnValue.Nombre);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithNewEstudiante()
    {
        // Arrange
        var createDto = new EstudianteCreateDto { Nombre = "Juan Pérez", Email = "juan@example.com" };
        var createdEstudiante = new EstudianteDto { Id = 1, Nombre = "Juan Pérez", Email = "juan@example.com" };
        _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdEstudiante);

        // Act
        var result = await _controller.Create(createDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<EstudianteDto>(createdResult.Value);
        Assert.Equal("Juan Pérez", returnValue.Nombre);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenEstudianteIsDeleted()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
