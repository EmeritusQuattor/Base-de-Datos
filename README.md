# Podcast API - Backend

API REST para la plataforma de podcasts desarrollada con **ASP.NET Core** y **SQL Server**.

## Descripción del Proyecto

Esta es una base de datos y API backend para una plataforma de podcasts que permite:
- Crear y gestionar usuarios
- Crear y gestionar podcasts
- Publicar episodios de podcasts
- Registrar reproducciones de episodios
- Gestionar categorías de podcasts

## Tecnologías

- **Framework:** ASP.NET Core 6+
- **ORM:** Entity Framework Core
- **Base de Datos:** SQL Server
- **API:** REST con Swagger
- **Lenguaje:** C#

## Estructura de Carpetas

```
Podcast/
├── Models/              # Entidades de base de datos
│   ├── UserC.cs
│   ├── PodcastModel.cs
│   ├── Episode.cs
│   ├── Reproduction.cs
│   └── Category.cs
├── DTOs/                # Data Transfer Objects
│   ├── UserDTO.cs
│   ├── PodcastDTO.cs
│   ├── EpisodeDTO.cs
│   ├── ReproductionDTO.cs
│   └── CategoryDTO.cs
├── Controllers/         # Controladores API (pendiente)
├── Repositories/        # Repositorios de datos (pendiente)
├── AppDbContext.cs      # Contexto de Entity Framework
├── Program.cs           # Configuración de la aplicación
└── appsettings.json     # Configuración
```

## Entidades

### Usuario (UserC)
| Campo | Tipo | Descripción |
|-------|------|-------------|
| IdUser | int | ID único (PK) |
| Name | string | Nombre del usuario |
| User | string | Nombre de usuario |
| Password | string | Contraseña |
| Register | DateTime | Fecha de registro |

### Podcast
| Campo | Tipo | Descripción |
|-------|------|-------------|
| IdPodcast | int | ID único (PK) |
| IdUser | int | ID del creador (FK) |
| Title | string | Título del podcast |
| Description | string? | Descripción |
| Portrait | string? | URL de imagen de portada |
| CreationTime | DateTime | Fecha de creación |

### Episodio (Episode)
| Campo | Tipo | Descripción |
|-------|------|-------------|
| IdEpisode | int | ID único (PK) |
| IdPodcast | int | ID del podcast (FK) |
| Title | string | Título del episodio |
| Description | string? | Descripción |
| Duration | int | Duración en segundos |
| UrlAudio | string? | URL del archivo de audio |
| PublishTime | DateTime | Fecha de publicación |
| AudioGuid | Guid | GUID del audio |
| AudioData | byte[]? | Datos binarios del audio |

### Reproducción (Reproduction)
| Campo | Tipo | Descripción |
|-------|------|-------------|
| IdReproduction | int | ID único (PK) |
| IdEpisode | int | ID del episodio (FK) |
| IdUser | int | ID del usuario (FK) |
| ReproductionTime | DateTime | Fecha de reproducción |
| TimeHeard | int | Segundos escuchados |

### Categoría (Category)
| Campo | Tipo | Descripción |
|-------|------|-------------|
| IdCategory | int | ID único (PK) |
| Name | string | Nombre de la categoría |
| Description | string? | Descripción |

## Guía de Instalación

### Requisitos
- .NET 6 SDK o superior
- SQL Server 2019 o superior
- Visual Studio 2022 o VS Code

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/EmeritusQuattor/Base-de-Datos.git
   cd Base-de-Datos
   ```

2. **Configurar la base de datos**
   - Editar `appsettings.json` con tu conexión a SQL Server:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=PodcastDB;Trusted_Connection=true;"
     }
   }
   ```

3. **Instalar dependencias**
   ```bash
   cd Podcast
   dotnet restore
   ```

4. **Crear migraciones (si es necesario)**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

La API estará disponible en: `https://localhost:5001`

## DTOs (Data Transfer Objects)

### UserDTO
```csharp
public class UserDTO
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string User { get; set; }
    public DateTime Register { get; set; }
    // Nota: Password NO se incluye en respuestas
}
```

### CreateUserDTO
```csharp
public class CreateUserDTO
{
    public string Name { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
```

### PodcastDTO
```csharp
public class PodcastDTO
{
    public int IdPodcast { get; set; }
    public int IdUser { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Portrait { get; set; }
    public DateTime CreationTime { get; set; }
}
```

### CreatePodcastDTO
```csharp
public class CreatePodcastDTO
{
    public int IdUser { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Portrait { get; set; }
}
```

### EpisodeDTO
```csharp
public class EpisodeDTO
{
    public int IdEpisode { get; set; }
    public int IdPodcast { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string? UrlAudio { get; set; }
    public DateTime PublishTime { get; set; }
    public Guid AudioGuid { get; set; }
}
```

### CreateEpisodeDTO
```csharp
public class CreateEpisodeDTO
{
    public int IdPodcast { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string? UrlAudio { get; set; }
    public byte[]? AudioData { get; set; }
}
```

### ReproductionDTO
```csharp
public class ReproductionDTO
{
    public int IdReproduction { get; set; }
    public int IdEpisode { get; set; }
    public int IdUser { get; set; }
    public DateTime ReproductionTime { get; set; }
    public int TimeHeard { get; set; }
}
```

### CategoryDTO
```csharp
public class CategoryDTO
{
    public int IdCategory { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
```

## Endpoints API

| Método | Endpoint | Descripción | DTOs |
|--------|----------|-------------|---------|
| POST | `/api/users` | Crear usuario | CreateUserDTO |
| GET | `/api/users/{id}` | Obtener usuario | UserDTO |
| PUT | `/api/users/{id}` | Actualizar usuario | UpdateUserDTO |
| DELETE | `/api/users/{id}` | Eliminar usuario | - |
| POST | `/api/podcasts` | Crear podcast | CreatePodcastDTO |
| GET | `/api/podcasts/{id}` | Obtener podcast | PodcastDTO |
| GET | `/api/podcasts/user/{userId}` | Podcasts del usuario | PodcastDTO[] |
| PUT | `/api/podcasts/{id}` | Actualizar podcast | UpdatePodcastDTO |
| DELETE | `/api/podcasts/{id}` | Eliminar podcast | - |
| POST | `/api/episodes` | Crear episodio | CreateEpisodeDTO |
| GET | `/api/episodes/{id}` | Obtener episodio | EpisodeDTO |
| GET | `/api/episodes/podcast/{podcastId}` | Episodios del podcast | EpisodeDTO[] |
| PUT | `/api/episodes/{id}` | Actualizar episodio | UpdateEpisodeDTO |
| DELETE | `/api/episodes/{id}` | Eliminar episodio | - |
| POST | `/api/reproductions` | Registrar reproducción | CreateReproductionDTO |
| GET | `/api/reproductions/user/{userId}` | Reproducciones del usuario | ReproductionDTO[] |
| GET | `/api/categories` | Obtener todas las categorías | CategoryDTO[] |
| POST | `/api/categories` | Crear categoría | CreateCategoryDTO |
| PUT | `/api/categories/{id}` | Actualizar categoría | UpdateCategoryDTO |
| DELETE | `/api/categories/{id}` | Eliminar categoría | - |

## 📝 Ejemplos de Requests/Responses

### Crear Usuario
**POST** `/api/users`

**Request:**
```json
{
  "name": "Juan Pérez",
  "user": "juanperez",
  "password": "securepassword123"
}
```

**Response (201 Created):**
```json
{
  "idUser": 1,
  "name": "Juan Pérez",
  "user": "juanperez",
  "register": "2026-05-19T13:57:14Z"
}
```

### Crear Podcast
**POST** `/api/podcasts`

**Request:**
```json
{
  "idUser": 1,
  "title": "Mi Primer Podcast",
  "description": "Un podcast sobre tecnología",
  "portrait": "https://example.com/image.jpg"
}
```

**Response (201 Created):**
```json
{
  "idPodcast": 1,
  "idUser": 1,
  "title": "Mi Primer Podcast",
  "description": "Un podcast sobre tecnología",
  "portrait": "https://example.com/image.jpg",
  "creationTime": "2026-05-19T13:57:14Z"
}
```

### Crear Episodio
**POST** `/api/episodes`

**Request:**
```json
{
  "idPodcast": 1,
  "title": "Episodio 1",
  "description": "El primer episodio del podcast",
  "duration": 3600,
  "urlAudio": "https://example.com/episode1.mp3",
  "audioData": null
}
```

**Response (201 Created):**
```json
{
  "idEpisode": 1,
  "idPodcast": 1,
  "title": "Episodio 1",
  "description": "El primer episodio del podcast",
  "duration": 3600,
  "urlAudio": "https://example.com/episode1.mp3",
  "publishTime": "2026-05-19T13:57:14Z",
  "audioGuid": "550e8400-e29b-41d4-a716-446655440000"
}
```

### Registrar Reproducción
**POST** `/api/reproductions`

**Request:**
```json
{
  "idEpisode": 1,
  "idUser": 1,
  "timeHeard": 1800
}
```

**Response (201 Created):**
```json
{
  "idReproduction": 1,
  "idEpisode": 1,
  "idUser": 1,
  "reproductionTime": "2026-05-19T14:00:00Z",
  "timeHeard": 1800
}
```

## 📱 Guía para Frontend TypeScript

### Instalación de Dependencias

```bash
npm install axios
```

### Interfaces TypeScript

```typescript
// User
export interface UserDTO {
  idUser: number;
  name: string;
  user: string;
  register: Date;
}

export interface CreateUserDTO {
  name: string;
  user: string;
  password: string;
}

// Podcast
export interface PodcastDTO {
  idPodcast: number;
  idUser: number;
  title: string;
  description?: string;
  portrait?: string;
  creationTime: Date;
}

export interface CreatePodcastDTO {
  idUser: number;
  title: string;
  description?: string;
  portrait?: string;
}

// Episode
export interface EpisodeDTO {
  idEpisode: number;
  idPodcast: number;
  title: string;
  description?: string;
  duration: number;
  urlAudio?: string;
  publishTime: Date;
  audioGuid: string;
}

export interface CreateEpisodeDTO {
  idPodcast: number;
  title: string;
  description?: string;
  duration: number;
  urlAudio?: string;
  audioData?: ArrayBuffer;
}

// Reproduction
export interface ReproductionDTO {
  idReproduction: number;
  idEpisode: number;
  idUser: number;
  reproductionTime: Date;
  timeHeard: number;
}

export interface CreateReproductionDTO {
  idEpisode: number;
  idUser: number;
  timeHeard: number;
}

// Category
export interface CategoryDTO {
  idCategory: number;
  name: string;
  description?: string;
}

export interface CreateCategoryDTO {
  name: string;
  description?: string;
}
```

### Servicio API con Axios

```typescript
import axios, { AxiosInstance } from 'axios';

const API_BASE_URL = 'https://localhost:5001/api';

class PodcastAPI {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: API_BASE_URL,
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  // USUARIOS
  createUser(data: CreateUserDTO): Promise<UserDTO> {
    return this.api.post('/users', data);
  }

  getUser(id: number): Promise<UserDTO> {
    return this.api.get(`/users/${id}`);
  }

  updateUser(id: number, data: UpdateUserDTO): Promise<UserDTO> {
    return this.api.put(`/users/${id}`, data);
  }

  deleteUser(id: number): Promise<void> {
    return this.api.delete(`/users/${id}`);
  }

  // PODCASTS
  createPodcast(data: CreatePodcastDTO): Promise<PodcastDTO> {
    return this.api.post('/podcasts', data);
  }

  getPodcast(id: number): Promise<PodcastDTO> {
    return this.api.get(`/podcasts/${id}`);
  }

  getPodcastsByUser(userId: number): Promise<PodcastDTO[]> {
    return this.api.get(`/podcasts/user/${userId}`);
  }

  updatePodcast(id: number, data: UpdatePodcastDTO): Promise<PodcastDTO> {
    return this.api.put(`/podcasts/${id}`, data);
  }

  deletePodcast(id: number): Promise<void> {
    return this.api.delete(`/podcasts/${id}`);
  }

  // EPISODIOS
  createEpisode(data: CreateEpisodeDTO): Promise<EpisodeDTO> {
    return this.api.post('/episodes', data);
  }

  getEpisode(id: number): Promise<EpisodeDTO> {
    return this.api.get(`/episodes/${id}`);
  }

  getEpisodesByPodcast(podcastId: number): Promise<EpisodeDTO[]> {
    return this.api.get(`/episodes/podcast/${podcastId}`);
  }

  updateEpisode(id: number, data: UpdateEpisodeDTO): Promise<EpisodeDTO> {
    return this.api.put(`/episodes/${id}`, data);
  }

  deleteEpisode(id: number): Promise<void> {
    return this.api.delete(`/episodes/${id}`);
  }

  // REPRODUCCIONES
  createReproduction(data: CreateReproductionDTO): Promise<ReproductionDTO> {
    return this.api.post('/reproductions', data);
  }

  getReproductionsByUser(userId: number): Promise<ReproductionDTO[]> {
    return this.api.get(`/reproductions/user/${userId}`);
  }

  // CATEGORÍAS
  getCategories(): Promise<CategoryDTO[]> {
    return this.api.get('/categories');
  }

  createCategory(data: CreateCategoryDTO): Promise<CategoryDTO> {
    return this.api.post('/categories', data);
  }

  updateCategory(id: number, data: UpdateCategoryDTO): Promise<CategoryDTO> {
    return this.api.put(`/categories/${id}`, data);
  }

  deleteCategory(id: number): Promise<void> {
    return this.api.delete(`/categories/${id}`);
  }
}

export default new PodcastAPI();
```

### Ejemplo de Uso en React

```typescript
import { useState, useEffect } from 'react';
import PodcastAPI from './services/PodcastAPI';
import { PodcastDTO, CreatePodcastDTO } from './types/api';

function PodcastList() {
  const [podcasts, setPodcasts] = useState<PodcastDTO[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchPodcasts = async () => {
      try {
        setLoading(true);
        const data = await PodcastAPI.getPodcastsByUser(1); // userId = 1
        setPodcasts(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchPodcasts();
  }, []);

  const handleCreatePodcast = async (newPodcast: CreatePodcastDTO) => {
    try {
      const created = await PodcastAPI.createPodcast(newPodcast);
      setPodcasts([...podcasts, created]);
    } catch (err: any) {
      setError(err.message);
    }
  };

  if (loading) return <div>Cargando...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div>
      <h1>Mis Podcasts</h1>
      <ul>
        {podcasts.map((podcast) => (
          <li key={podcast.idPodcast}>
            <h2>{podcast.title}</h2>
            <p>{podcast.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default PodcastList;
```

## Documentación Swagger

Una vez que la API está corriendo, accede a Swagger UI:

```
https://localhost:5001/swagger/index.html
```

## Configuración

Editar `appsettings.json` para configurar:
- Conexión a base de datos
- Logging
- CORS
- Autenticación (cuando esté implementada)

## Notas Importantes

- **Seguridad:** Las contraseñas nunca se devuelven en las respuestas de la API
- **DTOs:** Usamos DTOs para separar los datos expuestos de la API de las entidades internas
- **Fechas:** Todas las fechas se devuelven en formato ISO 8601 (UTC)
- **IDs:** Todos los IDs son números enteros (int)

## Equipo Frontend

Para el equipo de TypeScript:
1. Usar las interfaces TypeScript definidas arriba
2. Usar el servicio `PodcastAPI` para todas las llamadas
3. Consultar la tabla de endpoints para saber qué esperar
4. Los ejemplos de React están listos para copiar/pegar

## Contacto

Para dudas sobre la API, contáctenme a mi.
-- EMERITUS IV

---

**Última actualización:** Mayo 19, 2026
