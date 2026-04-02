# TechBridge Donation (Backend)

TechBridge is a device donation platform connecting businesses, refurbishers, and schools. This repo is the backend for the Invoice module. It provides the REST API, JWT authentication, and database layer used by the Angular frontend.

![Backend CI](https://github.com/ting11222001/TechBridge-Invoice/actions/workflows/backend-ci.yml/badge.svg)

[About the TechBridge project](https://tech-bridge-landing-page.vercel.app/) · [Invoice Module Live Demo](https://tech-bridge-invoice-app.vercel.app/) · [Invoice Module (Frontend repo)](https://github.com/ting11222001/TechBridge-Invoice-app) · Donation Module (coming soon)

<!-- --- -->

## Tech Stack
- ASP.NET Core Web API built with .NET 8
- PostgreSQL

<!-- --- -->

## Getting Started
1. Clone the repo
2. Update `appsettings.json` with your connection string
3. Run `dotnet ef database update`
4. Run `dotnet run`

<!-- --- -->

## What's built
- ASP.NET Core Web API (.NET 8)
- EF Core + SQL Server (Code First, migrations)
- Repository pattern with interfaces
- AutoMapper (domain → DTO)
- CRUD endpoints: Organisations, Devices
- JWT authentication (register + login)
- Role-based authorization (Admin / Viewer)
- Swagger UI with Bearer token support

<!-- --- -->

## In progress
- Filtering, sorting, pagination on Devices
- PostgreSQL swap (replacing SQL Server)
- React frontend
- Railway deployment
