using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.Repositories;
using TicketPlatform.API.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500",
                                             "http://127.0.0.1:5500/?#");
                      });
});

// Add services to the container.

builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.Configure<Configurations>(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();