RiskFirst-WebAPI
Web API built with C#

This project uses a Model, Controller approach with an in memory database setup using EntityFramework.

An alternate route that could be taken in creating this web api would be to separate the logic currently present 
in the Controller and create a Service class that uses that logic. Then the controller would call the service class.
