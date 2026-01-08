# API de Registro de Estudiantes

Backend desarrollado en .NET 10 con  para gestionar el registro de estudiantes en un programa de créditos académicos.

##  Descripción

Sistema de inscripción de materias que permite a los estudiantes registrarse en línea y adherirse a un programa de créditos con las siguientes características:

- **10 materias** disponibles (cada una equivale a 3 créditos)
- **5 profesores** (cada uno dicta 2 materias)
- Cada estudiante puede seleccionar **máximo 3 materias**
- **Restricción**: No puede tener clases con el mismo profesor en diferentes materias
- Los estudiantes pueden ver en línea los registros de otros estudiantes
- Pueden ver el nombre de los alumnos con quienes compartirán cada clase

##  Tecnologías

- **.NET 10** - Framework principal
- **Entity Framework Core** - ORM
- **Npgsql** - Driver PostgreSQL
- **Scalar** - Documentación API interactiva

##  Endpoints Principales

### Estudiantes
- `GET /api/estudiantes` - Listar todos los estudiantes
- `GET /api/estudiantes/{id}` - Obtener un estudiante
- `POST /api/estudiantes` - Crear estudiante
- `PUT /api/estudiantes/{id}` - Actualizar estudiante
- `DELETE /api/estudiantes/{id}` - Eliminar estudiante
- `GET /api/estudiantes/{id}/materias-companeros` - Ver compañeros de clase

### Materias
- `GET /api/materias` - Listar todas las materias
- `GET /api/materias/{id}` - Obtener una materia
- `GET /api/materias/disponibles/{estudianteId}` - Materias disponibles para inscribir

### Inscripciones
- `POST /api/inscripciones` - Inscribir materias
- `DELETE /api/inscripciones/{estudianteId}/{materiaId}` - Desinscribir materia
- `GET /api/inscripciones/estudiante/{estudianteId}` - Ver inscripciones de un estudiante

### Profesores
- `GET /api/profesores` - Listar todos los profesores
- `GET /api/profesores/{id}` - Obtener un profesor

## Ejecución

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar la aplicación
dotnet run
```

Acceder a:
- **Scalar UI**: `https://localhost:7216/scalar/v1`
- **API Base**: `https://localhost:7216/api/`

##  Estructura

```
reg-estudiantes-bc/
??? Controllers/      # Endpoints REST API
??? Services/         # Lógica de negocio
??? Models/           # Entidades de base de datos
??? DTOs/             # Data Transfer Objects
??? Data/             # Contexto Entity Framework
```

##  Autor

Juan Camilo - [GitHub](https://github.com/juankmilo99)
