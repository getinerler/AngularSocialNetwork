using System.Text;
using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Data.DatabaseTest;
using AngularSocialNetwork.API.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Seed>();
builder.Services.AddTransient<IAuthRepo, AuthRepoTest>();
builder.Services.AddTransient<ICommentsRepo, CommentsRepoTest>();
builder.Services.AddTransient<INotificationsRepo, NotificationsRepoTest>();
builder.Services.AddTransient<IPostsRepo, PostsRepoTest>();
builder.Services.AddTransient<ISeedRepo, SeedRepoTest>();
builder.Services.AddTransient<IUsersRepo, UsersRepoTest>();

builder.Services.BuildServiceProvider().GetService<Seed>().SeedAll();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot/fileStorage/profilePhotos")),
    RequestPath = new PathString("/profilePhotos")
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
    .WithOrigins("http://localhost:4200/")
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true)
    .AllowAnyMethod()
    .AllowCredentials());

app.MapControllers();

app.MapHub<AngularHub>("/hub");

app.Run();