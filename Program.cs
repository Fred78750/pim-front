using PimFront.Services;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// PIM API Client
builder.Services.AddHttpClient<PimApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PimApi:BaseUrl"]
        ?? throw new InvalidOperationException("PimApi:BaseUrl manquant dans la configuration"));

    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
            builder.Configuration["PimApi:Keys:Public"]
            ?? throw new InvalidOperationException("PimApi:Keys:Public manquant"));
});

// Client séparé scope admin (read:full)
builder.Services.AddHttpClient<PimAdminApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PimApi:BaseUrl"]
        ?? throw new InvalidOperationException("PimApi:BaseUrl manquant"));

    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
            builder.Configuration["PimApi:Keys:Admin"]
            ?? throw new InvalidOperationException("PimApi:Keys:Admin manquant"));
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(o =>
{
    o.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
