# Arquitectura Hexagonal en .NET (Ports and Adapters)

Este repositorio contiene una implementación práctica de **Arquitectura Hexagonal (Puertos y Adaptadores)** en .NET, diseñada para mantener desacoplada la lógica del negocio de los mecanismos de entrada (CLI y API Web) y de los mecanismos de almacenamiento y persistencia (JSON y XML).

---

## 🏛️ Estructura y Componentes de la Arquitectura

El núcleo del sistema está desacoplado gracias al uso de interfaces (puertos) e implementaciones intercambiables (adaptadores):

```text
Arquitectura-Software/
│
├── Arquitectura-Hexagonal/             # Proyecto Core (Dominio, Puertos y Adaptadores)
│   ├── Entities/                       # Entidades del Dominio
│   │   └── Products.cs                 # Entidad central con reglas de validación
│   ├── Ports/                          # Contratos e Interfaces (Puertos)
│   │   ├── Primary/                    # Puertos de Entrada (Casos de Uso)
│   │   │   └── IService.cs
│   │   └── Secundary/                  # Puertos de Salida (Persistencia)
│   │       └── IRepository.cs
│   ├── Services/                       # Implementación de Casos de Uso
│   │   └── ProductService.cs
│   ├── Repositories/                   # Adaptadores de Salida (Persistencia)
│   │   ├── JsonRepository/
│   │   │   └── ProductRepository.cs    # Persistencia en archivos JSON
│   │   └── XMLRepository/
│   │       └── XmlProductRepository.cs # Persistencia en archivos XML (LINQ to XML)
│   └── Presentation/                   # Adaptadores de Entrada
│       └── CLI/
│           └── Cli.cs                  # Interfaz de Consola por Línea de Comandos
│
├── API-integration/                    # Proyecto Web API (Adaptador de Entrada HTTP)
│   ├── Program.cs                      # Minimal APIs, Swagger e Inyección de Dependencias
│   ├── appsettings.json                # Configuración de rutas y almacenamiento
│   └── Properties/launchSettings.json
│
├── .gitignore
└── README.md
```

---

## 🧩 Principios Aplicados

1. **Desacoplamiento Total:** La entidad `Products` y el servicio `ProductService` no dependen de bibliotecas de serialización JSON, XML ni frameworks web.
2. **Inversión de Dependencias (DIP):** Los adaptadores de persistencia implementan `IRepository<T>`, lo que permite cambiar el motor de persistencia (JSON por XML o una base de datos SQL) modificando únicamente el registro en el contenedor de Inyección de Dependencias:
   ```csharp
   // Para usar JSON:
   builder.Services.AddSingleton<IRepository<Products>, ProductRepository>(...);

   // Para usar XML:
   builder.Services.AddSingleton<IRepository<Products>, XmlProductRepository>(...);
   ```
3. **Múltiples Clientes:** La misma lógica de negocio es consumida simultáneamente por:
   - Una aplicación interactiva por consola (**CLI**).
   - Una **API REST** moderna con **Swagger UI** y Minimal APIs.

---

## 🚀 Requisitos Previos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior.
- Git.

---

## 🛠️ Cómo Ejecutar

### 1. Clonar el repositorio
```bash
git clone https://github.com/Brhayxn/Software-Architecture.git
cd Software-Architecture
```

### 2. Ejecutar la Aplicación CLI (Consola)
```bash
dotnet run --project Arquitectura-Hexagonal
```
Sigue el menú interactivo para registrar productos y listar los existentes.

### 3. Ejecutar la Web API
```bash
dotnet run --project API-integration
```
- Una vez iniciada, abre tu navegador en:
  - **Swagger UI:** `http://localhost:5187/swagger`
- Endpoints disponibles:
  - `GET /products`: Obtiene el listado completo de productos.
  - `POST /products`: Registra un nuevo producto.

---

## 📄 Licencia

Proyecto de libre uso con fines educativos y de demostración de patrones de arquitectura de software.
