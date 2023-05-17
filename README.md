# Project Architecture

## Presentation Layer 
Handles incoming HTTP request and returns response, validations, configurations of project

- Controllers : All the controllers and end points of API, Uses Mapper for object mapping from ViewModels to DTOs and vice versa
- Filters : Different Filter to handle events like Authorization, Exceptions
	- ActionFilter 
	- ExceptionFilter
- ViewModel : Direct connected models with Front-end
	- ReqViewModel
	- ResViewModel
- ServiceExtention : Dependency Injections
- Program.cs : Starting point of Project with configuration of Filters, Controllers, Authentication, Authorization etc.
- appsettings.json : Project settings and global variables

## DTO 
Data Transfers around the layers of architecture

- ReqDTO : Transfers request data from Presentation Layer to Business Layer
- ResDTO : Transfers response data from Business Layer to Presentation Layers

## Service Layer 
For Encapsulation, Separation of Concerns, Reusability, Scalability.

- Interface : All the interfaces with "I" prefix with specific controller name.
	- ICustom 
- Implementation : All the Implementation classes with "Impl" postfix with specific controller name.
	- CustomImpl

## Business Layer 
Main Business logic, connection between Presentation and Data Layer

- CustomBLL : Contains functions as per api need, All the classes with "BLL" postfix with specific controller name.

## Data Layer 
Storage and Retrieval of data from a database

- Entities : All the database table in form of c# classes to perform database operations
- DbScripts : Database scripts for database actions.

## Helper 
A Common library to use as Utility in all the different layers

- CommonHelpers : Common helping functions and contants.
	- CommonConstant
	- CommonHelper
- CommonModels : Common helping models for global use.
	- CommonResponse
	- FilePaths
