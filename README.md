# Introduction

This repo hosts the **User Contact**  API .

## Getting Started

This solution uses `.NET 6` and Visual Studio 2022.

## Software architecture
Clean architecture

### API versioning
`URI versioning`. The versions are numbered as v1, v2 etc.

### API Documentation
Run the API project to view the Swagger UI at <https://localhost:44308/swagger/index.html>

### Dependency injection
Inversion of Control between clasess and their dependencies in the API is achieved using the built-in support for dependency injection in `.NET 6`

### Logging
ILogger Interface (Microsoft.Extensions.Logging)

## Generate models using EF Core
Create or update the EF model based on Database frist approach using Scafflod-DbContext command.

## Auomated Test

### Unit tests

Used xUnit, Moq and FluentAssertion for readable assert statements. Followed the naming convention *{NameOfTestMethodTBeingTested}Test* for the class and descriptive name for the test method which explains behaviour and state undr test.

### Unit test for Repository

Used EF Core InMemory Database.

## Database
Used SQLite in this REPO for faster development.

## Deployment

### Build docker image and start container
Go to the dockerfile location in solution and follow the below steps

To create docker image use the following command
```sh
docker build -t contactapp .
```
Type the follwing cmd to see docker images
```sh
docker images
```
Create and run conatiner
```sh
docker run -d -p 8080:80 --name customercontat contactapp
```

Check container is up and running
```sh
docker ps
```
Access the API : https://localhost:8080/api/customer/conatct

