using Azure.Storage.Blobs;
using CloudSecurity;
using CloudSecurity.Areas.Identity;
using CloudSecurity.Data;
using CloudSecurity.Data.Repositories;
using CloudSecurity.Services;
using CloudSecurity.Services.Interfaces;
using CloudSecurity.Validators;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudSecuritySettings>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var azureBlobConnection = builder.Configuration["AZURE_BLOB_CONNECTION"];
builder.Services.AddSingleton(_ => new BlobServiceClient(azureBlobConnection));
builder.Services.AddScoped<IBlobRepository, BlobRepository>();
builder.Services.AddScoped<IBlobService, BlobService>();

var maxFileSize = builder.Configuration.GetValue<int>("BlobConfig:MaxFileSize");
builder.Services.AddTransient(_ => new BlobFileValidator(maxFileSize));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
