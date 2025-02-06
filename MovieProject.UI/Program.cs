using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieProject.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

//JWT Authentication ekleme
builder.Services.AddHttpClient(); // HttpClient ekle
builder.Services.AddSession(); // Session yönetimi
builder.Services.AddHttpContextAccessor(); // HttpContext erişimi için
builder.Services.AddScoped<ApiService>();  // ApiService'i ekliyoruz
builder.Services.AddScoped<ITokenService, TokenService>();

//  Authentication Yapısı (SADECE COOKIE AUTHENTICATION)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";     // Giriş yolu
        options.LogoutPath = "/Login/LogOut";     // Çıkış yolu
        options.AccessDeniedPath = "/Default/PageDenied"; // Yetkisiz erişim sayfası
        options.Cookie.Name = "MovieProjectCookie";       // Cookie Adı
        options.Cookie.HttpOnly = true;           // XSS saldırılarından korunmak için
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS varsa Secure yap
        options.Cookie.SameSite = SameSiteMode.Strict; // CSRF saldırılarından korunmak için
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Cookie ömrü
        options.SlidingExpiration = true; // Kullanıcı aktifse süre yenilenir
    });





builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

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
app.UseSession();         // Session yönetimi burada olmalı
app.UseAuthentication();  // Kullanıcı kimlik doğrulama (JWT)
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");
app.Run();
