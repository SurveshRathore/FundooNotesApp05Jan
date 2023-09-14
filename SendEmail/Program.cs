using GreenPipes;
using MassTransit;
using SendEmail.Mail;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ configuration for Consumer
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MailService>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.UseHealthCheck(provider);
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest"); //username
            h.Password("guest"); //password
        });
        cfg.ReceiveEndpoint("MailQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<MailService>(provider); // Mess
        });
    }));
});
builder.Services.AddMassTransitHostedService();

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
