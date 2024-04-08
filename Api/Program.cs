using Application.Payload.Converter;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Application.Service.Services;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Infrastructure.ImplementRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddControllers();
*/// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddTransient<IEmailServices, EmailServices>();
builder.Services.AddTransient<IBaseRepositories<User>, BaseRepositories<User>>();
builder.Services.AddTransient<IBaseRepositories<RefreshToken>, BaseRepositories<RefreshToken>>();
builder.Services.AddTransient<IBaseRepositories<ConfirmEmail>, BaseRepositories<ConfirmEmail>>();
builder.Services.AddTransient<IBaseRepositories<Role>, BaseRepositories<Role>>();
builder.Services.AddScoped<IDbContext, AppDbContext>();
builder.Services.AddScoped<IUserRepositories, UserRepositories>();
builder.Services.AddScoped<ResponseObject<Response_Resgister>>();
builder.Services.AddScoped<Converter_User>();

//Cinema
builder.Services.AddScoped<ICenimaServices, CinemaServices>();
builder.Services.AddTransient<IBaseRepositories<Cinema>, BaseRepositories<Cinema>>();
builder.Services.AddScoped<ResponseObject<Response_Cinema>>();
builder.Services.AddScoped<Converter_Cinema>();

//Seat
builder.Services.AddScoped<ISeatServices, SeatServices>();
builder.Services.AddTransient<IBaseRepositories<Seat>, BaseRepositories<Seat>>();
builder.Services.AddScoped<ResponseObject<Response_Seat>>();
builder.Services.AddScoped<Converter_Seat>();
//Room
builder.Services.AddScoped<IRoomServices, RoomServices>();
builder.Services.AddTransient<IBaseRepositories<Room>, BaseRepositories<Room>>();
builder.Services.AddScoped<ResponseObject<Response_Room>>();
builder.Services.AddScoped<Converter_Room>();
//Food
builder.Services.AddScoped<IFoodServices, FoodServices>();
builder.Services.AddTransient<IBaseRepositories<Food>, BaseRepositories<Food>>();
builder.Services.AddScoped<ResponseObject<Response_Food>>();
builder.Services.AddScoped<Converter_Food>();
//Movie
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddTransient<IBaseRepositories<Movie>, BaseRepositories<Movie>>();
builder.Services.AddScoped<ResponseObject<Response_Movie>>();
builder.Services.AddScoped<Converter_Movie>();
builder.Services.AddTransient<IBaseRepositories<RankCustomer>, BaseRepositories<RankCustomer>>();
builder.Services.AddTransient<IBaseRepositories<SeatStatus>, BaseRepositories<SeatStatus>>();
builder.Services.AddTransient<IBaseRepositories<SeatType>, BaseRepositories<SeatType>>();
builder.Services.AddTransient<IBaseRepositories<UserStatus>, BaseRepositories<UserStatus>>();
//GetMovie
builder.Services.AddScoped<IGetMovieRepositories, GetMovieRepositories>();
builder.Services.AddScoped<IGetMovieServices, GetMovieServices>();
services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
    options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.Preserve,
    }));
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value!))
        };

    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Thay ??i ??a ch? này thành ??a ch? c?a ?ng d?ng React c?a b?n
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
