using Microsoft.EntityFrameworkCore;
using UserRoleManagementApi.Models.Data;
using UserRoleManagementApi.Services.Implementations;
using UserRoleManagementApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers()
    //.AddJsonOptions(options =>
    //{
       // options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    //});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));



// these are the created services 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers (essential for your API endpoints)
app.MapControllers();

app.Run();
