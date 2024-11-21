// Create a new WebApplication instance using the provided command-line arguments.
var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages services to the container. Razor Pages is a lightweight web framework.
builder.Services.AddRazorPages();

// Build the WebApplication instance.
var app = builder.Build();

// Configure the HTTP request pipeline.

// If the application is not running in development mode,
// use the exception handler to redirect to the "/Error" page.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Enable serving static files (e.g., HTML, CSS, JavaScript) from the wwwroot folder.
app.UseStaticFiles();

// Enable routing for handling HTTP requests.
app.UseRouting();

// Enable authorization. This is typically used for securing access to different parts of the application.
app.UseAuthorization();

// Map Razor Pages routes.
app.MapRazorPages();

// Run the application.
app.Run();
