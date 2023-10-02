using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Try_Photo_Upload.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PhotoUploadDBContext>(
	  options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

//JSON
builder.Services
	.AddControllersWithViews()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null;    //��property���n�ܤp�g
		options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);  //�s�X����
		options.JsonSerializerOptions.WriteIndented = true;  //�۰ʱƪ�JSON��X
	});

// CORS�s�u
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
	pattern: "{controller=Photo}/{action=Index}/{photoName?}");


app.Run();
