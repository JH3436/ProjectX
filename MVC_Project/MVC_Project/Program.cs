using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllersWithViews();

//連接
builder.Services.AddDbContext<ProjectXContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectXconnection_James")));

// 連接到第二個資料庫
builder.Services.AddDbContext<ProjectXContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectXconnection_Lun")));

//重載(目前好像沒用到，但之後應該會需要就先放著了)
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//JSON
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;    //讓property不要變小寫
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);  //編碼全放
        options.JsonSerializerOptions.WriteIndented = true;  //自動排版JSON輸出
    });

// CORS連線
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Homepage}/{id?}");

app.Run();
