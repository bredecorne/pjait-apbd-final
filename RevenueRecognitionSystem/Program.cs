using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RrsDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DockerDb"))
);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();