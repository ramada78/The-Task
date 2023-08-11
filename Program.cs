using ElkoodTask;
using ElkoodTask.Servies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var connectiosString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddTransient<DataSeeder>();

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectiosString)
// );
builder.Services.AddDbContext<ApplicationDbContext>
(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Interfaces
builder.Services.AddTransient<IBranchTypesService, BranchTypesService>();
builder.Services.AddTransient<IProductTypesService, ProductTypesService>();
builder.Services.AddTransient<ICompaniesService, CompaniesService>();

builder.Services.AddCors();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP .NET - Backend Task",
        Description = "Software system With API",
        //TermsOfService = new Uri("https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "Rama Dadeekhi",
            Email = "Rama.dadeekhi.78@gmail.com",
            //Url = new Uri("https://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "My license",
            //Url = new Uri("https://www.google.com")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}
//await app.MigrationAsync<ApplicationDbContext>(DataSeed.Seed);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())      
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

//TaskInitializer.Seed(app);
app.Run();
