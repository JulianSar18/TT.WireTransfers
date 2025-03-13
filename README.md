# 🚀 API de Gestión de Billeteras - Payphone.app

**API REST para gestionar transferencias de saldo**  
Permite operaciones CRUD sobre billeteras y CR sobre el historial de movimientos. Desarrollada con **.NET 8**, **Entity Framework Core**, **SQL Server** y arquitectura limpia.

---

## 📋 Tabla de Contenidos
- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Instalación](#-instalación)
- [Endpoints](#-endpoints)
- [Validaciones](#-validaciones)
- [Pruebas](#-pruebas)

---

## 🌟 Características
- **CRUD de Billeteras**: Crear, leer, actualizar y eliminar billeteras.
- **Transferencias**: Débitos y créditos entre billeteras.
- **Documentación Swagger**: Interfaz interactiva para probar endpoints.
- **Middleware de Errores**: Respuestas estandarizadas (404, 400, 500).

---

## 🛠 Tecnologías
- **Backend**: .NET 8
- **Base de Datos**: SQL Server + Entity Framework Core
- **Arquitectura**: Clean Architecture (Core, Application, Infrastructure, Presentation)
- **Pruebas**: xUnit, Moq
- **Documentación**: Swagger/OpenAPI

---

## 🔧 Instalación

### Requisitos Previos
- .NET SDK 8.0
- SQL Server
- Git

### Pasos
1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/tu-usuario/payphone-wallet-api.git
   cd payphone-wallet-api