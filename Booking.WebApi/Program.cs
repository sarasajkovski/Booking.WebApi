using Autofac;
using Autofac.Extensions.DependencyInjection;
using Booking.Repository;
using Booking.Service;
using Booking.WebApi.Mapping;
using BookingRepository.Common;
using BookingService.Common;
using Repository.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<RoomRepository>().As<IRoomRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ReservationRepository>().As<IReservationRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ReservationService>().As<IReservationService>().InstancePerLifetimeScope();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Use the Assembly overload to avoid the CS1503 ambiguity when passing a Type.
//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(cfg =>{}, typeof(MappingProfile));


/*builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();*/

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
