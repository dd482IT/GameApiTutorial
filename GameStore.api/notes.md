# Module 4
https://www.youtube.com/watch?v=bKCzoR01lpE&t=4304s&ab_channel=JulioCasal
### Design Patterns and Best Practices
- User repository pattern for better data maniupulation
- Use the dependency injection pattern for more maintable code
- Understand services lifetimes
- Use data transfer ojects for clear contracts between client server


Access Data from APP.

- App logic communitcaes with SQL.
- If the app becomes very popular, you may move to NoSQL database
- You would not have to rewrite the logic.
- Repository pattern
(Repository is an abstraction that enscapsulates the logic for accessing and manipulating data.
Main purpose: abstract details from the underlying access technology from the application )
- Include repo between App and database. The repoistory is the only component that interfaces with the database.
- The repo is the only change you would need to make if changing databases.

UnitOfWork (Mostly related with transactions across tables/entities (not in this course)
We will use a simple version of repo pattern

Dependency Injection
- If for example, MyService uses a dependency class called MyLogger, any changes to
MyLogger would require changes to my service
- MyService needs to know how to construct/configure MyLogger dependency
- It's hard to test MyService since the MyLogger Dependency cannot be mocked or stubbed.
Use IServiceProvider
- Application can register dependencies using IServiceProvider usally within Program.cs
- Upon calls, IServiceProvieder will injection dependcies
    This allows
    - MyService won't be affected
    - MyService doesn't need to know how to construct or configure dependcies
    - Dependencies can also be injected as params to min APIS
    - Opening the door to using dependency inversion

Dependency Inversion Principle
- Moving to the cloud, we could use a cloud logger class
- MyService depends on Ilogger interface (MyLogger, Cloudlogger depends on Ilogger allowing for decoupling) )
Service Life Times
    When should instances be created?
    What happens if a new request comes in when a new request arrives (should it use new or original instance)\
    What happens if another service also uses the instance of Illogger?

    There are three lifetimes
    1. Transient - very lightweight and statless, ok to create a new instance AddTransient method.
        a new request would generate a new instance
    2. Scoped - class that keeps track of some sort of state shared accross classes, AddCode Method. Request arrives will
        get an instance. If thee is any other service in the same request, the same instance.
        A new http request will generate a new instance
    3. Singleton - not cheap to instanation and only once instance should be used. When HTTP request arrives, an instance is created but if a new request comes in, the same instance
        is used until application is closed.


## Understanding Data Transfer Objects

    DTO - encapsulates data in a format and can be transmitted between applications or layers of applicatiosn
        Contract between client and a server
        Why use DTOs? - consider what could happen if requirements change and asked to rename properties and storing secret code for internal use only.
                      - We are sending these new attributes to client which may break or reveal data to the client.
                      - Client should only recieve what was asked for
         
       
## Database Connection String



## Secrets Management
1. Init Secret Manager
2. User Secret ID should be listed in project file automatically 

# Entity Framework Core - Modify data using only C#
1. Use database migrations 
2. Implement an entity framework
3. Use database migrations 
4. Implement an Entity Framework repository 
5. Use the async programming model.

The need for Object Relation Mapping
    Web api is in C#, Sql only understands SQL. 
    C# needs to translate query so that SQL (T-SQL) can understand it.
    This must run the SQL query and return the rows so that C# can use it 
    This muse be optimized for good performance. 
    Need to manually keep C# model models in sync with DB tables

What is O/RM?
    Objects to represent (games, users etc). O/RM allows for the translation between objects in C# and the databases that store the data of those objects. 
    This is Entity Framework Core - a lightweight extensible open source and cross platform object relation mapper for .NET. 

    Benefits - 
        No need to become a master of SQL, minimal data access code (LINQ) 
        Keep track of changes to objects 
        Tooling to keep C# models in sync with DB tables. 
        Multiple database solution support. 

    
    to create a Dbcontext class, make sure it inhereits from DbContext class
    DBcontext can act is a repository, so why do we need one defined (igames, inmems)
    We don't want to tie/inject the Dbcontext directly with apis instead of using the repository? 
       --> This is because the repository acts as an interface between the API and the database. 
       --> This means our endpoints would be coupled, if we want to move to NoSQL or we need to scale, we would need to make a lot of changes to the endpoints. 
