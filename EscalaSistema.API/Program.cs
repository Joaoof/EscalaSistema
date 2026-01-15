using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;
using EscalaSistema.API.Repository;
using EscalaSistema.API.UseCase;
using EscalaSistema.API.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
