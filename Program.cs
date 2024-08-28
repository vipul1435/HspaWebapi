using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webApi.Data;
using webApi.Helper;
using webApi.Interfaces;
using webApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Default") ;

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(connectionString);});


builder.Services.AddControllers().AddNewtonsoftJson();

//including the the autoMapper service
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

//inlcuding the CORS policy service
builder.Services.AddCors();


//add the interface to the application
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var signKey = builder.Configuration.GetSection("Appsettings:Key").Value;

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
//Allowing the JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters { 
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
    );

 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //setting up the global error handler
    //app.ExceptionHandlerConfiguration();



    // adding the custom global error hanlder
    app.UseMiddleware<ExceptionMiddleware>();

    //app.UseExceptionHandler(
    //    options =>
    //    {
    //        options.Run(
    //            async context =>
    //            {
    //                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //                var exception = context.Features.Get<IExceptionHandlerFeature>();
    //                if (exception != null)
    //                {
    //                    await context.Response.WriteAsync(exception.Error.Message);
    //                }
    //            }
    //        );
    //    }
    //);
}

app.UseCors(req =>
{
    req.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
