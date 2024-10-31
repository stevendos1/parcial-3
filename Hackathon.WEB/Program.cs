// Configura el HttpClient con la base de la URI y el puerto correcto
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5012/") });

// Configura la inyección de dependencias para IRepository
builder.Services.AddScoped<IRepository, Repository>();

await builder.Build().RunAsync();

