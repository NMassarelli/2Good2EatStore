using _2Good2EatStore.Components;
using _2Good2EatStore.Components.Account;
using _2Good2EatStore.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Azure.Identity;
using CloudinaryDotNet;
using Microsoft.Extensions.DependencyInjection.Extensions;
using _2Good2EatStore.Data.Interfaces;
using _2Good2EatStore.Data.Services;
using _2Good2EatStore.Data.Entities;
using _2Good2EatStore.Data.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICartService,CartService>();

builder.Services.AddScoped<ProductModelFluentValidator>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));

var connectionString = builder.Configuration.GetConnectionString("DBConnectionString") ?? throw new InvalidOperationException("Connection string 'DBConnectionString' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
var cloudinaryURL = builder.Configuration.GetConnectionString("CLOUDINARY_URL") ?? throw new InvalidOperationException("Connection string 'CLOUDINARY_URL' not found.");
Cloudinary cloudinary = new(cloudinaryURL);
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.Run();
