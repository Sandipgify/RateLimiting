using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var partitionKey = $"{context.Connection.RemoteIpAddress}-{context.Request.Path}";

            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: partitionKey,
                factory: x => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromSeconds(10),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                });
        });
    });

    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
