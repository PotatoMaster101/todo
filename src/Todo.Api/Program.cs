using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo;
using Todo.Api.Constants;
using Todo.Api.Extensions;
using Todo.Common.Authentication;
using Todo.Common.Behaviours;
using Todo.Features.Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var dbConnection = builder.Configuration.GetConnectionString("DataConnection");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ITodoDbContext, TodoDbContext>(o => o.UseNpgsql(dbConnection));
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<Todo.Api.ExceptionHandler>();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey,
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Bearer",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                },
                Scheme = "oauth2",
            }, new List<string>()
        }
    });
});

// setup mediatr
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();

// setup authentication
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(ApplicationPolicyNames.Admin, p => p.RequireClaim(ApplicationClaimTypes.Admin, "true"));
builder.Services.AddDbContext<IAuthenticationDbContext, AuthenticationDbContext>(o => o.UseNpgsql(dbConnection));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AuthenticationDbContext>();

var app = builder.Build();
await app.ApplyMigrations<TodoDbContext>();
await app.ApplyMigrations<AuthenticationDbContext>();
await app.AddAdminUser();
await app.AddAdminClaim();

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGroup("/api/identity/").MapIdentityApi<IdentityUser>().WithTags("Identity");
app.Run();
