using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Narciarze_v_2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Narciarze_v_2.Models.Narty_V2Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Narciarze_v_2Context")));
builder.Services.AddScoped<ITrasy, TrasySql>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
