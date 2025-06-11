using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ เพิ่มการกำหนดค่าให้ API ฟังที่พอร์ต 80
builder.WebHost.UseUrls("http://+:80");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();

        // ✅ ตรวจสอบว่าตารางมีอยู่หรือไม่ ถ้ายังไม่มีให้สร้าง
        dbContext.Database.Migrate();
        Console.WriteLine("✅ Database Migrations Applied Successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error Applying Migrations: {ex.Message}");
    }
}

app.MapControllers();
app.Run();
