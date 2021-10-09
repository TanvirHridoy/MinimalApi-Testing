using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi;
using MinimalApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Configure Services starts 
    builder.Services.AddDbContext<CertificateMSContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("CertificateDb")));
    builder.Services.AddEndpointsApiExplorer();
#region Swagger authorization config     
//builder.Services.AddSwaggerGen(x => 
//{
//    x.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme 
//    {
//        Description ="Jwt Authoentication header using Bearer Scheme",
//        Name ="Authorization",
//        In=ParameterLocation.Header,
//        Type=SecuritySchemeType.ApiKey
//    });
//    x.AddSecurityRequirement(new OpenApiSecurityRequirement 
//    {
//        {new OpenApiSecurityScheme{Reference=new OpenApiReference
//        {
//            Id="JwtBearer",
//            Type=ReferenceType.SecurityScheme
//        }}, new List<string>()}
//    });
//});
#endregion 
builder.Services.AddSwaggerGen();
    //builder.Services
    //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //    .AddJwtBearer();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    }).AddJwtBearer("JwtBearer", jwtoptions =>
    {
        jwtoptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = ApiConst.Issuer,
            ValidAudience = ApiConst.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiConst.key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });
    builder.Services.AddAuthorization();
//Configure services ends

var app = builder.Build();

//Configure start 
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthentication();
    app.UseAuthorization();
//Configure ends

//apies
    app.MapPost("/api/Jwt", (JwtToken model) => ApiConst.CreateToken(model)).AllowAnonymous();

       //Authoriztion using attributes
    app.MapGet("/api/Dept",[Authorize] async (CertificateMSContext _Db ) =>   await _Db.Departments.ToListAsync());

      //Using fluid Authorization assignment
    app.MapGet("/api/Deptt", async (CertificateMSContext _Db) => await _Db.Departments.ToListAsync()).RequireAuthorization();
//apies end 
app.Run();
