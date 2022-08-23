using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApi;
using MinimalApi.Models;
using System.Text;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
     builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
        );
});
//Configure Services starts 
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = true;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.ClientWhitelist = new List<string>
    {
      "Id-1",
      "Id-2"
    };
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "GET:/api/Test",
                Period = "10s",
                Limit = 2,
            },
            new RateLimitRule
            {
                Endpoint = "*:/api/Test2",
                Period = "10s",
                Limit = 2,
            }
        };
});
#region IP wise Limit Was not configued
//builder.Services.Configure<IpRateLimitPolicies>(options =>
//{

//    options.IpRules = new List<IpRateLimitPolicy>
//    {
//        new IpRateLimitPolicy
//        {
//             Ip="192.168.14.57",
//             Rules=new List<RateLimitRule> 
//             { 
//                 new RateLimitRule
//                 {
//                      Endpoint="GET:/api/Test",
//                      Period="1m",
//                      Limit=1,
//                      QuotaExceededResponse= new QuotaExceededResponse
//                      {
//                            Content="Wrong IP",
//                            ContentType="application/json",
//                            StatusCode=404
//                      }  
//                 }

//             }
//        }
//    };


//}
//);
#endregion IP wise Limit Was not configued
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddMemoryCache();
//builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
//builder.Services.AddInMemoryRateLimiting();

//builder.Services.AddDbContext<CertificateMSV2Context>(o =>
//    o.UseSqlServer(builder.Configuration.GetConnectionString("CertificateDb")));
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
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//Configure services ends

var app = builder.Build();

//Configure start 
app.UseIpRateLimiting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//Configure ends

//apies

app.MapGet("/api/Test", () => "Hello Test!");

app.MapPost("/api/Jwt", (JwtToken model) => ApiConst.CreateToken(model)).AllowAnonymous();

//Api To send Mail
app.MapGet("/api/Mail/{MailAddress}/{Name}", async (string MailAddress, string Name) =>
{
    MailAccountInfo accountInfo = new MailAccountInfo();
    builder.Configuration.GetSection("MailConfig").Bind(accountInfo);
    Random rand = new Random();
    string Otp = rand.Next(111111, 999999).ToString();

    MailHelper helper = new MailHelper();
    var result = await Task.Run(() => helper.SendEmail(Otp, accountInfo, Name, MailAddress));
    if (result)
    {
        return Results.Ok(Otp);
    }
    else
    {
        return Results.BadRequest();
    }
}).AllowAnonymous();

////Authoriztion using attributes
//app.MapGet("/api/Dept", [Authorize] async (CertificateMSV2Context _Db) => await _Db.Departments.ToListAsync());

////Using fluid Authorization assignment
//app.MapGet("/api/Campus", async (CertificateMSV2Context _Db) => await _Db.Campuses.ToListAsync()).RequireAuthorization();
//apies end 
app.Run();
