var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/users", async (IUserRepository repo) =>
{
    return await repo.GetAll();
})
.WithName("GetUsers")
.WithOpenApi();

app.MapPost("/users", async (User user, IUserRepository repo) =>
{
    return await repo.Add(user);
})
.WithName("Add user")
.WithOpenApi();

app.Run();