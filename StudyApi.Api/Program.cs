using MediatR;
using StudyApi.Application.Products.Commands;
using StudyApi.Application.Users.Commands;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Infrastructure;
using StudyApi.Infrastructure.Repositories;
using StudyApi.Infrastructure.Data;
using StudyApi.Application.OrderNumbers.Commands;


var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderNumberCommand).Assembly); 
});

// Infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

// Registrar IDbConnectionFactory e UserRepository
builder.Services.AddScoped<IDbConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

var app = builder.Build();

// Swagger dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();