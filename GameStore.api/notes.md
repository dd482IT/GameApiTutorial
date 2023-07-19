# Module 4

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