# ECommerce_Furniture
# Furniture E-Commerce Platform

A modern **Microservices-based Furniture E-Commerce Platform** built with **ASP.NET Core 8**, **Angular 22**, **SQL Server**, **RabbitMQ**, **JWT Authentication**, and **Ocelot API Gateway**.

---

# Overview

The Furniture E-Commerce Platform is designed using a **Microservices Architecture** to provide scalability, maintainability, security, and independent deployment of business capabilities.

Each microservice owns its own database and communicates using **REST APIs** and **RabbitMQ** for asynchronous messaging.

---

# Features

- Customer Registration & Login
- JWT Authentication
- Refresh Token Authentication
- Role-Based Authorization (Admin/User)
- Product Management
- Shopping Cart
- Order Management
- Payment Processing
- Email & SMS Notifications
- RabbitMQ Event-Driven Communication
- API Gateway (Ocelot/YARP)
- SQL Server Database per Service

---

# Technology Stack

## Frontend

- Angular 22
- Standalone Components
- Angular Material
- TypeScript
- RxJS

## Backend

- ASP.NET Core 8 Web API
- C#
- Entity Framework Core
- Repository Pattern
- Dependency Injection

## Database

- Microsoft SQL Server

## Security

- JWT Authentication
- Refresh Token
- Password Hashing
- Role-Based Authorization

## Messaging

- RabbitMQ
- Topic Exchange
- Routing Keys
- Producer/Consumer Pattern

## API Gateway

- Ocelot
- YARP Reverse Proxy

## DevOps

- Docker Desktop
- WSL2
- Git
- GitHub
- Swagger

---

# Project Architecture

```
                    Angular 22 Frontend
                             |
                      JWT Authentication
                             |
                  API Gateway (YARP)
                             |
 ----------------------------------------------------------------
 |             |             |             |                     |
Customer     Product       Order       Payment           Notification
Service      Service       Service      Service             Service
 |             |             |             |                    |
CustomerDB   ProductDB     OrderDB     PaymentDB       NotificationDB
                             |
                         RabbitMQ
                      Topic Exchange
                             |
                    Event Communication
```

---

# Microservices

## Customer Service

Responsibilities

- Registration
- Login
- JWT Authentication
- Refresh Token
- Customer Profile
- Role Management

Database

CustomerDB

---

## Product Service

Responsibilities

- Product Catalog
- Categories
- Inventory
- Product Images
- Product Ratings

Database

ProductDB

---

## Order Service

Responsibilities

- Shopping Cart
- Orders
- Order Items
- Checkout

Database

OrderDB

---

## Payment Service

Responsibilities

- Payment Processing
- Payment Status
- Transaction History

Database

PaymentDB

---

## Notification Service

Responsibilities

- Email Notification
- SMS Notification  - not yet implemented
- Notification History --- not yet implemented

Database

NotificationDB

---

# Authentication Flow

```
Customer Login
      |
Validate Credentials
      |
Generate JWT Token
      |
Generate Refresh Token
      |
Return Tokens
      |
Access Protected APIs
      |
JWT Expired?
      |
Refresh Token
      |
Generate New JWT
```

---

# RabbitMQ Communication

```
Payment Service
       |
Publish Event
       |
Topic Exchange
       |
-------------------------------
|              |              |
Inventory   Notification   Analytics
Consumer      Consumer      Consumer
```

Example Routing Keys

```
payment.success
order.created
inventory.updated
notification.email
notification.sms
```

---

# Order Processing Flow

```
Customer Login
      |
Browse Products
      |
Add Product To Cart
      |
Checkout
      |
Create Order
      |
Process Payment
      |
RabbitMQ Event
      |
Update Inventory
      |
Send Email
      |
Send SMS
      |
Order Completed
```

---

# Repository Structure

```
FurnitureECommerce.sln

src/

CustomerService/

ProductService/

OrderService/

PaymentService/

NotificationService/

ApiGateway/

AngularUI/

Docker/

README.md
```

---

# API Endpoints

## Customer

| Method | Endpoint |
|----------|---------------------------|
| POST | /api/customers/register |
| POST | /api/customers/login |
| GET | /api/customers/profile |
| PUT | /api/customers/profile |

---

## Product

| Method | Endpoint |
|----------|----------------------|
| GET | /api/products |
| GET | /api/products/{id} |
| POST | /api/products |
| PUT | /api/products/{id} |
| DELETE | /api/products/{id} |

---

## Order

| Method | Endpoint |
|----------|--------------------------|
| GET | /api/cart |
| POST | /api/cart/items |
| POST | /api/orders |
| GET | /api/orders |

---

## Payment

| Method | Endpoint |
|----------|--------------------|
| POST | /api/payments |
| GET | /api/payments/{id} |

---

## Notification

| Method | Endpoint |
|----------|------------------------------|
| POST | /api/notifications/email |
| POST | /api/notifications/sms |
| GET | /api/notifications/{id} |

---

# Design Patterns

- Repository Pattern
- Dependency Injection
- SOLID Principles
- REST API Design
- Database per Microservice
- Event-Driven Architecture

---

# Implemented Features

- ✅ 5 Independent Microservices
- ✅ JWT Authentication
- ✅ Refresh Token Authentication
- ✅ Password Hashing
- ✅ Entity Framework Core
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ RabbitMQ Topic Exchange
- ✅ Routing Keys
- ✅ Producer/Consumer Messaging
- ✅ YARP Reverse Proxy
- ✅ Docker Support
- ✅ Swagger API Documentation
- ✅ GitHub Repository
- ✅ SQL Server Database per Service

---

# Future Enhancements

- Redis Caching
- Kubernetes Deployment
- Azure Service Bus
- Elasticsearch & Kibana
- Serilog Logging
- Prometheus Monitoring
- Grafana Dashboard
- CI/CD with GitHub Actions
- Azure DevOps Pipeline

---

# Author

**Pravinna Rajagopal**

Microservices | ASP.NET Core 8 | Angular 22 | RabbitMQ | SQL Server | Docker | Ocelot | YARP | Entity Framework Core
