using FluentAssertions.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
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

app.UseRouting();

app.UseAuthorization();
//builder.Services.AddCors();
//app.UseCors(options =>
//{
//    options.AllowAnyOrigin();
//    options.AllowAnyMethod();
//    options.AllowAnyHeader();
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmployeeDetail}/{action=Index}/{id?}");

app.Run();
