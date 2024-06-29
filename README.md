# TODO
A simple TODO item app.

## Setup & Run
Create DB migrations:
```shell
dotnet ef migrations add InitialCreate --project src/Todo --startup-project src/Todo.Api --context AuthenticationDbContext --output-dir Migrations/Authentication
dotnet ef migrations add InitialCreate --project src/Todo --startup-project src/Todo.Api --context TodoDbContext --output-dir Migrations/Todo
```

Run docker compose:
```shell
docker compose up -d --build
```

To visit the API, go to `https://localhost/swagger`.

## TODO
- [ ] Add admin routes
- [ ] Add frontend UI
- [ ] Add proper authorization
