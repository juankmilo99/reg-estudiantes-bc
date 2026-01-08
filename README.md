# API de Registro de Estudiantes

Backend desarrollado en .NET 10 con PostgreSQL para gestionar el registro de estudiantes en un programa de créditos académicos.

## ?? Deployment en Render

### 1. Preparar el Repositorio

```bash
# Agregar archivos
git add .
git commit -m "Initial commit - Backend API de Registro de Estudiantes"

# Conectar con el repositorio remoto
git remote add origin https://github.com/juankmilo99/reg-estudiantes-bc.git
git branch -M main
git push -u origin main
```

### 2. Configurar en Render

1. Ve a [Render Dashboard](https://dashboard.render.com/)
2. Click en **"New +"** ? **"Web Service"**
3. Conecta tu repositorio de GitHub: `https://github.com/juankmilo99/reg-estudiantes-bc`
4. Configura:
   - **Name**: `reg-estudiantes-bc`
   - **Environment**: `Docker`
   - **Branch**: `main`
   - **Region**: Elige el más cercano (US East recomendado por la DB)
   - **Instance Type**: Free

### 3. Variables de Entorno en Render

En la sección **Environment**, agrega:

**Nombre**: `ConnectionStrings__DefaultConnection`  
**Valor**: 
```
Host=ep-divine-smoke-ahu80rl5-pooler.c-3.us-east-1.aws.neon.tech;Port=5432;Username=neondb_owner;Password=npg_82hZgKeTAXdW;Database=neondb;SSL Mode=Require;Trust Server Certificate=true
```

?? **Importante**: Usa doble guión bajo `__` entre `ConnectionStrings` y `DefaultConnection`.

### 4. Deploy

Click en **"Create Web Service"**. Render automáticamente:
- ? Clonará tu repositorio
- ? Construirá la imagen Docker con .NET 10
- ? Desplegará la aplicación
- ? Te dará una URL pública

### 5. URLs de Producción

Una vez desplegado:
- **API Base**: `https://reg-estudiantes-bc.onrender.com/api/`
- **Scalar UI**: `https://reg-estudiantes-bc.onrender.com/scalar/v1`
- **OpenAPI JSON**: `https://reg-estudiantes-bc.onrender.com/openapi/v1.json`

---

## ?? Desarrollo Local

### Requisitos
- .NET 10 SDK
- PostgreSQL (Neon Cloud)

### Ejecutar Localmente

```bash
dotnet restore
dotnet run
```

Accede a:
- HTTPS: `https://localhost:7216`
- HTTP: `http://localhost:5114`
- Scalar UI: `https://localhost:7216/scalar/v1`

---

## ?? Endpoints API

### Estudiantes

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/estudiantes` | Lista todos los estudiantes |
| GET | `/api/estudiantes/{id}` | Obtiene un estudiante |
| POST | `/api/estudiantes` | Crea un estudiante |
| PUT | `/api/estudiantes/{id}` | Actualiza un estudiante |
| DELETE | `/api/estudiantes/{id}` | Elimina un estudiante |
| GET | `/api/estudiantes/{id}/materias-companeros` | Ver compañeros de clase |

### Materias

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/materias` | Lista todas las materias |
| GET | `/api/materias/{id}` | Obtiene una materia |
| GET | `/api/materias/disponibles/{estudianteId}` | Materias disponibles para inscribir |

### Inscripciones

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/inscripciones` | Inscribir materias |
| DELETE | `/api/inscripciones/{estudianteId}/{materiaId}` | Desinscribir materia |
| GET | `/api/inscripciones/estudiante/{estudianteId}` | Ver inscripciones |

### Profesores

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/profesores` | Lista todos los profesores |
| GET | `/api/profesores/{id}` | Obtiene un profesor |

---

## ?? Ejemplos de Uso

### Crear un Estudiante

```bash
curl -X POST https://reg-estudiantes-bc.onrender.com/api/estudiantes \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan Pérez",
    "email": "juan@example.com"
  }'
```

### Inscribir Materias

```bash
curl -X POST https://reg-estudiantes-bc.onrender.com/api/inscripciones \
  -H "Content-Type: application/json" \
  -d '{
    "estudianteId": 1,
    "materiasIds": [1, 3, 5]
  }'
```

### Ver Compañeros de Clase

```bash
curl https://reg-estudiantes-bc.onrender.com/api/estudiantes/1/materias-companeros
```

---

## ??? Características del Sistema

? **CRUD completo** de estudiantes  
? **10 materias** disponibles (3 créditos cada una)  
? **5 profesores** (cada uno dicta 2 materias)  
? **Máximo 3 materias** por estudiante  
? **No repetir profesor** entre materias seleccionadas  
? **Ver compañeros** de clase por materia  
? **Validaciones automáticas** en base de datos y API  

---

## ??? Estructura del Proyecto

```
reg-estudiantes-bc/
??? Controllers/          # Controladores REST API
??? Services/            # Lógica de negocio
??? Models/              # Entidades de base de datos
??? DTOs/                # Data Transfer Objects
??? Data/                # DbContext de Entity Framework
??? Dockerfile           # Configuración Docker para Render
??? .dockerignore        # Archivos ignorados por Docker
??? .gitignore           # Archivos ignorados por Git
```

---

## ?? Seguridad

?? **Archivos sensibles NO versionados:**
- `appsettings.json` (con credenciales)
- `appsettings.*.json` 
- `bin/` y `obj/`

? **Variables de entorno en Render** para credenciales de producción

---

## ?? Docker

### Construir imagen localmente

```bash
docker build -t reg-estudiantes-bc .
```

### Ejecutar contenedor

```bash
docker run -p 80:80 \
  -e ConnectionStrings__DefaultConnection="Host=...;Port=5432;Username=...;Password=...;Database=neondb;SSL Mode=Require;Trust Server Certificate=true" \
  reg-estudiantes-bc
```

---

## ?? Tecnologías

- **.NET 10** - Framework principal
- **Entity Framework Core 10** - ORM
- **PostgreSQL** - Base de datos (Neon Cloud)
- **Npgsql** - Driver PostgreSQL
- **Scalar** - UI para documentación API
- **Docker** - Containerización

---

## ?? Licencia

MIT

---

## ????? Autor

Juan Camilo - [GitHub](https://github.com/juankmilo99)
