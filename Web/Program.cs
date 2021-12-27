using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bussiness.DependencyInjection.Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataAccess.Utils.IConfiguration>(new DataAccess.Utils.Configuration() { ConnectionString = builder.Configuration.GetConnectionString("DefeaultConnection") });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBussinessModule()));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
