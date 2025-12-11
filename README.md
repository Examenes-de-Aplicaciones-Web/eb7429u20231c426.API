# EXAMEN FINAL OXXOSMART


## Summary

Este examen es un API aplication hecho con Microsoft C#, ASP.NET Core, Entity Framework Core y MySQl. Tambien ilustra la configuracion de documentacion de open api y la integracion de Swagger UI.

## Features

- RESTful API
- Swagger UI
- OpenAPI Documentation
- ASP.NET Framework
- Entity Framework Core
- Custom Route Naming Conventions
- Custom Object-Relational Mapping Naming Conventions.
- MySQL Database
- Domain-Driven Design


## Bounde Context 

Esta version esta dividido en 2 bounded context: Assets y Operation

### Assets

El Assets' context es el responsable de manejar los lockers que son casilleros inteligentes instalados en cada tienda. Esto incluye las siguientes
funcionalidades.

  - CreateLocker
  - GetLockerById

Este Contexto tambien incluye la actualizacion de automatica de los lockers como disponibles o ocupados, según los pedidos (orders) activos que tenga. 

### Operations

En Operations' Context es el responsable del manejo de los pedidos (orders). Esto incluye las siguientes funcionalidades en sus dos controladores tanto para OrdersController y UserController referente a la entidad User que tiene una participacion media a diferencia de orders.

  - GetOrderById
  - CreateLocker
  - PickUpOrder (para recoger un pedido y actualizar la fecha de recogo PickedUpAt)
  - GetUserById
  - CreateUser

# Diagrama de clases DDD

<div align ="center">
	<img src="docs/diagrama de clases.svg" alt="Logo-UPC">
</div>

(abre la imagen en una nueva ventana para mejor visualizacion)
