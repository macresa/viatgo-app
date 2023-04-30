API:

La api es una Minimal API en .NET 7 en la que utilizo como base la [Arquitectura Vertical](https://jimmybogard.com/vertical-slice-architecture/) junto con Entity Framework Core para el acceso a los datos en SQL server, también usa el patrón repositorio el cual se inyecta en la capa de aplicación para separar la lógica de negocio de la capa de persistencia de datos.


En este proyecto utilicé las siguientes libreras:

- [MediatR](https://www.nuget.org/packages/MediatR)
- [AutoMapper](https://www.nuget.org/packages/AutoMapper)
- [Fluent Validation](https://www.nuget.org/packages/FluentValidation)
- [ASP.NET Core Identity](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/8.0.0-preview.3.23177.8)
- [JWT authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)

## Cómo ejecutar la aplicación con Docker cli

- Ejecuta el comando: `docker-compose up --build`

### Ingresa desde el navegador

#### Swagger

- `http://localhost:8000/swagger/index.html`

#### Angular SPA

- `http://localhost:4000/`

### Cómo conectarte a la bbdd para agregar vuelos

Utiliza las siguientes credenciales
- Server: localhost,1433
- Username: sa
- Password: pass_1254a
