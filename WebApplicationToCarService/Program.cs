using WebApplicationToCarService.Structures;
using Microsoft.EntityFrameworkCore;
using WebApplicationToCarService;

var builder = WebApplication.CreateBuilder();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataBaseApplication>(options => options.UseSqlServer(connection));
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/auto", async (DataBaseApplication db) => await db.autoList.ToListAsync());

app.MapGet("/api/auto/{id:int}", async (int id, DataBaseApplication db) =>
{
    var auto = await db.autoList.FirstOrDefaultAsync(u => u.Id == id);

    if (auto == null) return Results.NotFound(new { message = "Auto not found" });

    return Results.Json(auto);
});

app.MapDelete("/api/auto/{id:int}", async (int id, DataBaseApplication db) =>
{
    var auto = await db.autoList.FirstOrDefaultAsync(u => u.Id == id);

    if (auto == null) return Results.NotFound(new { message = "Auto not found" });

    db.autoList.Remove(auto);
    await db.SaveChangesAsync();
    return Results.Json(auto);
});

app.MapPost("/api/auto", async (Auto auto, DataBaseApplication db) =>
{
    await db.autoList.AddAsync(auto);
    await db.SaveChangesAsync();
    return auto;
});

app.MapPut("/api/auto", async (Auto autoData, DataBaseApplication db) =>
{
    var auto = await db.autoList.FirstOrDefaultAsync(u => u.Id == autoData.Id);

    if (auto == null) return Results.NotFound(new { message = "Auto not found" });

    auto.AutoNumber = autoData.AutoNumber;
    auto.Brand = autoData.Brand;
    auto.Color = autoData.Color;
    auto.ReleaseDate = autoData.ReleaseDate;
    await db.SaveChangesAsync();
    return Results.Json(auto);
});

app.Run();