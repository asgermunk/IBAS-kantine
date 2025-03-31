using Azure.Data.Tables;
using Azure;
using asmuKantine.Model;

var builder = WebApplication.CreateBuilder(args);
var tablename = "asmuKantine";

// Get connection string with error handling
var connectionString = builder.Configuration.GetConnectionString("AzureTableStorageConnectionString");
var _productionRepo = new List<asmuKantineDTO>();

try 
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        TableClient tableClient = new TableClient(connectionString, tablename);
        Pageable<TableEntity> entities = tableClient.Query<TableEntity>();

        foreach (TableEntity entity in entities)
        {
            var dto = new asmuKantineDTO {
                Weekday = entity.GetString("Weekday"),
                WarmDish = entity.GetString("WarmDish"),
                ColdDish = entity.GetString("ColdDish")
            };
            _productionRepo.Add(dto);
        }
    }
    else
    {
        Console.WriteLine("Connection string is missing. Please check your appsettings.json file.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to Azure Table Storage: {ex.Message}");
}

builder.Services.AddSingleton(_productionRepo);
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
