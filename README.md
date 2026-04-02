# TechBridge Donation (Backend)

TechBridge is a device donation platform connecting businesses, refurbishers, and schools. This repo is the backend for the Donation module. It provides the REST API, JWT authentication, and database layer used by the React frontend.

[About the TechBridge project](https://tech-bridge-landing-page.vercel.app/) · Donation Module (Frontend Repo) (coming soon) · [Invoice Module Live Demo](https://tech-bridge-invoice-app.vercel.app/) · [Invoice Module (Frontend repo)](https://github.com/ting11222001/TechBridge-Invoice-app) · [Invoice Module (Backend repo)](https://github.com/ting11222001/TechBridge-Invoice)



## Table of Contents

- [What This Platform Does](#what-this-platform-does)
- [Roles and Users](#roles-and-users)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [What's Built](#whats-built)
- [In Progress](#in-progress)



## What This Platform Does

**Device Donation Management**

Manages the full journey of a donated device: from submission and 
approval, through certified data wiping and refurbishment, to final 
allocation via a partner school or NGO. Schools submit requests on 
behalf of students. The platform matches, tracks, and records every 
device at each step.

- Device lifecycle with enforced status transitions (no skipping, 
  no going backwards)
- Role-based access for all four partner types
- Audit trail for every status change
- Admin-controlled matching and allocation

**Who uses it:**
Business Donor · Refurb Partner · School / NGO · Admin



## Roles and Users

There are four user roles in the system:

**Admin**: Program operators who manage the full workflow. They approve donations, assign devices to refurbishment partners, match devices to requests, and have access to audit logs and reports.

**Business Donor**: Companies donating end-of-life devices. They can register their organisation, create a donation, and add devices before submission. After approval, they can only view status.

**Refurb Partner**: IT recyclers or repair shops. They see only devices assigned to them, update wipe and refurbishment status, and add technical notes.

**Request Partner**: Schools or NGOs submitting requests on behalf of students. They submit requests with student count and specs needed, view request status, and confirm receipt of devices.

> Admins can see everything. Everyone else sees only what they own or are assigned to.



## Tech Stack

- ASP.NET Core Web API built with .NET 8
- PostgreSQL



## Getting Started

1. Clone the repo
2. Update `appsettings.json` with your connection string
3. Run `dotnet ef database update`
4. Run `dotnet run`



## What's Built

- ASP.NET Core Web API (.NET 8)
- EF Core + PostgreSQL (with data seeding and migrations)
- Domain models: Device, Donation, Organisation
- DbContext configured with PostgreSQL connection via User Secrets
- Database tables created via EF Core migrations
- Seed data for Organisations, Devices, and Donations
- OrganisationsController with GET all and GET by ID endpoints



## In Progress

- Repository pattern with interfaces
- AutoMapper (domain → DTO)
- CRUD endpoints: Organisations, Devices
- JWT authentication (register + login)
- Role-based authorization (Admin / Viewer)
- Swagger UI with Bearer token support
- Filtering, sorting, pagination on Devices
- React frontend
- Railway deployment