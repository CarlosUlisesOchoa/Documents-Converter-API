# Documents Converter API

A secure REST API built with .NET 8 that converts documents. The API is protected with OAuth 2.0 authentication and provides standardized error responses using Problem Details [RFC 9457](https://datatracker.ietf.org/doc/html/rfc9457).

## 🚀 Features

- OAuth 2.0 Authentication (JWT Bearer)
- Open API (Swagger UI) Documentation
- Standardized Error Responses [RFC 9457 Problem Details](https://datatracker.ietf.org/doc/html/rfc9457)
- XML to JSON Conversion

## 📋 Prerequisites

- .NET 8.0 SDK
- A valid JWT token

## 🛠️ Setup

1. Clone the repository:
```powershell
git clone https://github.com/CarlosUlisesOchoa/Documents-Converter-API.git Documents-Converter-API
cd Documents-Converter-API
```

2. Create environment configuration:
   - Copy `.env.example` to `.env` or Rename `.env.example` to `.env`
   - Update the following variables in `.env`:
```plaintext
JWT_ISSUER=your-jwt-issuer
JWT_AUDIENCE=your-jwt-audience
JWKS_URL=your-jwks-url
```

3. Build the project:
```powershell
dotnet build
```

4. Run the application:
```powershell
dotnet run
```

The application will start and be available at:
- https://localhost:7271/swagger/index.html (OpenAPI / Swagger UI)

Note: The port number may vary depending on your configuration.

## 🔒 Authentication

The API uses JWT Bearer authentication. Include your JWT token in the Authorization header:

```plaintext
Authorization: Bearer your-jwt-token
```

## 📡 API Endpoints

### Convert XML to JSON

```http
POST /api/v1/documents/xml-to-json
```

#### Request Body
```json
{
  "xml": "your-base64-encoded-xml-string"
}
```

#### Example OK Response
```json
{
  "Emisor": {},
  "Receptor": {},
  "Conceptos": [],
  "Impuestos": {}
}
```

#### Example Error Responses

- **400 Bad Request**: Invalid input
- **401 Unauthorized**: Access token is expired or invalid
- **500 Internal Server Error**: Unexpected error

All error responses follow the RFC 9457 Problem Details format with a extension (errors array):
```json
{
  "title": "Error Title",
  "status": 400,
  "detail": "Detailed error message",
  "errors": ["Error 1", "Error 2"]
}
```

## 🧪 Development

The project includes:
- Open API (Swagger UI) for API documentation and testing
- Comprehensive error handling
- JWT authentication middleware

## About developer

[Carlos Ochoa](https://carlos8a.com)

## 📄 References

- [RFC 9457 Problem Details](https://datatracker.ietf.org/doc/html/rfc9457)  
- [Azure DevOps OAuth Authentication](https://learn.microsoft.com/en-us/azure/devops/integrate/get-started/authentication/oauth?view=azure-devops)  
- [JSON Handling in C#: A Comprehensive Guide for Developers](https://medium.com/@Has_San/json-handling-in-c-a-comprehensive-guide-for-developers-0ed233365bf2)  
- [JWT Authentication in ASP.NET Core](https://learn.microsoft.com/es-es/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-8.0&tabs=windows)  
- [OpenAPI Support in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-8.0&tabs=visual-studio)  
- [Authentication made easy with ASP.NET Core Identity in .NET 8](https://www.youtube.com/watch?v=S0RSsHKiD6Y)  
- [JWT Debugger](https://jwt.io/)  
- [RFC 9457 Problem Details Specification](https://datatracker.ietf.org/doc/html/rfc9457)  
- [ProblemDetails in ASP.NET Core](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-8.0)  
- [Configuring API Behavior Options](https://learn.microsoft.com/es-es/dotnet/api/microsoft.extensions.dependencyinjection.mvccoremvccorebuilderextensions.configureapibehavioroptions?view=aspnetcore-8.0)  
- [Convert.FromBase64String in .NET](https://learn.microsoft.com/en-us/dotnet/api/system.convert.frombase64string?view=net-8.0)  
- [System.Xml.Linq.XDocument Class](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument?view=net-8.0)  
- [Dependency Injection in ASP.NET Core](https://learn.microsoft.com/es-es/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)  
- [XmlSchemaSet Class](https://learn.microsoft.com/es-es/dotnet/api/system.xml.schema.xmlschemaset?view=net-8.0)  
- [XmlReaderSettings Class](https://learn.microsoft.com/es-es/dotnet/api/system.xml.xmlreadersettings?view=net-8.0)  
- [XmlSerializer Class](https://learn.microsoft.com/es-es/dotnet/api/system.xml.serialization.xmlserializer?view=net-8.0)  
- [AddJsonOptions in ASP.NET Core](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvccoremvcbuilderextensions.addjsonoptions?view=aspnetcore-8.0)
