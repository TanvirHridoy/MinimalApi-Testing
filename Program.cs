using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CertificateMSContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("CertificateDb")));
var app = builder.Build();

app.MapGet("/api/Dept", async (CertificateMSContext _Db ) =>   await _Db.Departments.ToListAsync());

app.Run();
