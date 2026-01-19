using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;
using EscalaSistema.API.Models;
using EscalaSistema.API.Repository;
using EscalaSistema.API.Repository.Login;
using EscalaSistema.API.Service;
using EscalaSistema.API.UseCase;
using EscalaSistema.API.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EscalaSistemaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssemblyContaining<PublishScaleValidation>();
builder.Services.AddScoped<IValidator<User>, UserValidation>();

builder.Services.AddScoped<ICultRepository, CultRepository>();
builder.Services.AddScoped<ICultUseCase, CultUseCase>();

builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IMusicUseCase, MusicUseCase>();

builder.Services.AddScoped<IPublishScaleRepository, PublishScaleRepository>();  
builder.Services.AddScoped<IPublishScaleUseCase, PublishScaleUseCase>();

builder.Services.AddScoped<ICreateScaleUseCase, ScaleUseCase>();
builder.Services.AddScoped<ICreateScaleRepository, ScaleRepository>();

builder.Services.AddScoped<IAssignMusicianUseCase, AssignMusicianUseCase>();
builder.Services.AddScoped<IAssignMusicianRepository, AssignMusicianRepository>();

builder.Services.AddScoped<ICreateMusicianUseCase, CreateMusicianUseCase>();
builder.Services.AddScoped<IMusicianRepository, MusicianRepository>();

builder.Services.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
builder.Services.AddScoped<IUserRegisterRepository, UserRegisterRepository>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository,LoginRepository>();

builder.Services.AddScoped<ITokenService ,TokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    success = false,
                    message = "Token ausente ou inválido"
                });

                return context.Response.WriteAsync(result);
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<CustomExceptionMiddleware>();

app.Run();
