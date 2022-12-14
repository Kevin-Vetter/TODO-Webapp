using TODO_Webapp.Service.Interface;
using TODO_Webapp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().Services.AddSingleton<IRepo, Repository>().AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Login","");
}).Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
