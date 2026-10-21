using RepositoryPatternDemo.Repositories;
using RepositoryPatternDemo.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepo,CustomerRepository>();
builder.Services.AddScoped<ICustomerService,CustomerService>();

builder.Services.AddScoped<IInvoiceDetailRepo, InvoiceDetailRepository>();
builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();

builder.Services.AddScoped<IInventoryRepo,InventoryRepository>();
builder.Services.AddScoped<IInventoryService,InventoryService>();

builder.Services.AddScoped<IProductRepo, ProductRepository>();
builder.Services.AddScoped <IProductService, ProductService>();

builder.Services.AddScoped<IInvoiceMasterRepo, InvoiceMasterRepository>();
builder.Services.AddScoped<IInvoiceMasterService,InvoiceMasterService>();

builder.Services.AddScoped<IFileStorageService , FileStorageService>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();

builder.Services.AddScoped<IFileUploadService, IFileUploadService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Configure the HTTP request pipeline.
//builder.Services.AddScoped<ICRepository,CRepositary>();
//builder.Services.AddScoped<ICService,CService>();
// repo...
//services...
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddDbContext<AppDbContext>(options =>
//  options.UseSqlServer(
//      builder.Configuration.GetConnectionString("DefaultConnection")));