using MediatR;
using WellFlix.Catalog.Api.Configuration;
using WellFlix.Catalog.Application.AppService.Category.CreateCategory;
using WellFlix.Catalog.Domain.Repository;
using WellFlix.Catalog.Infra.CrossCutting.Interfaces;
using WellFlix.Catalog.Infra.Data;
using WellFlix.Catalog.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(CreateCategory));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddConfigControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();