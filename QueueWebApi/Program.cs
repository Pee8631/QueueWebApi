using Microsoft.EntityFrameworkCore;
using QueueWebApi.Data;
using QueueWebApi.Repositories.queueRepository;
using QueueWebApi.Services.queueService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
//Add Services
builder.Services.AddScoped<IqueueService, queueService>();
//Add Repositories
builder.Services.AddScoped<IqueueRepository, queueRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
