using AngularSocialNetwork.API.Data;
using AngularSocialNetwork.API.Data.DatabaseTest;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Seed>();
builder.Services.AddTransient<IAuthRepo, AuthRepoTest>();
builder.Services.AddTransient<INotificationsRepo, NotificationsRepoTest>();
builder.Services.AddTransient<IPostsRepo, PostsRepoTest>();
builder.Services.AddTransient<ISeedRepo, SeedRepoTest>();
builder.Services.AddTransient<IUsersRepo, UsersRepoTest>();

builder.Services.BuildServiceProvider().GetService<Seed>().SeedAll();


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

app.MapControllers();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.Run();