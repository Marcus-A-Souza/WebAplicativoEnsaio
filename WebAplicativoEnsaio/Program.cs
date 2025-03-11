using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAplicativoEnsaio.Data;
using Microsoft.AspNetCore.Identity;
using WebAplicativoEnsaio.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços do WhatsApp
builder.Services.AddTransient<WhatsAppService>();

// Configuração do banco de dados
builder.Services.AddDbContext<MusicsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MusicsDbContext>()
    .AddDefaultTokenProviders();

// Configuração de injeção de dependências
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEnsaioService, EnsaioService>();
builder.Services.AddScoped<IMusicoService, MusicoService>();
builder.Services.AddScoped<IMusicsService, MusicsService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();

// Configuração de política de senha
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

// Configuração de Controllers com Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed Data para criar usuário administrador
await SeedAdminUser(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configuração de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "musicos",
    pattern: "AcessoMusico/{action=MusicosView}/{id?}",
    defaults: new { controller = "AcessoMusico" });

app.Run();

/// <summary>
/// Método para garantir que o primeiro usuário cadastrado seja o Administrador.
/// </summary>
async Task SeedAdminUser(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Criar a role "Administrador" se ainda não existir
    if (!await roleManager.RoleExistsAsync("Administrador"))
    {
        await roleManager.CreateAsync(new IdentityRole("Administrador"));
    }

    // Criar usuário administrador se não existir
    var adminUser = await userManager.FindByNameAsync("Administrador");

    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = "Administrador", PhoneNumber = "999999" };
        var result = await userManager.CreateAsync(adminUser, "Admin123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Administrador");
        }
    }
    else
    {
        // Se o usuário já existe, garante que ele tenha a role "Administrador"
        if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
        {
            await userManager.AddToRoleAsync(adminUser, "Administrador");
        }
    }
}
