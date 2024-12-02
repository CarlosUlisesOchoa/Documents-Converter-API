# Documents Converter API

A secure REST API built with .NET 8 that converts documents. The API is protected with OAuth 2.0 authentication and provides standardized error responses using Problem Details (RFC 9457)[https://datatracker.ietf.org/doc/html/rfc9457].

## 🚀 Features

- OAuth 2.0 Authentication (JWT Bearer)
- Open API (Swagger UI) Documentation
- Standardized Error Responses (RFC 9457 Problem Details)[https://datatracker.ietf.org/doc/html/rfc9457]
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
  "emisor": {},
  "receptor": {},
  "conceptos": [],
  "impuestos": {}
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