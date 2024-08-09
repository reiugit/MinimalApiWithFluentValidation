using FluentValidation;
using MinimalApiWithFluentValidation.Dtos;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.AddFluentValidationEndpointFilter();

var app = builder.Build();

app.MapPost("/api/products-v1", (ProductRequest productRequest, IValidator<ProductRequest> validator) =>
{
    var result = validator.Validate(productRequest);

    if (result.IsValid)
    {
        return Results.Created($"/api/products/1", productRequest.ToResponse());
    }

    return Results.ValidationProblem(result.ToDictionary());
});

app.MapPost("/api/products-v2", (ProductRequest productRequest, IValidator<ProductRequest> validator) =>
{
    return Results.Created($"/api/products/1", productRequest.ToResponse());
})
.AddFluentValidationFilter();


app.MapPost("/api/products-v3", (ProductRequest productRequest, IValidator<ProductRequest> validator) =>
{
    return Results.Created($"/api/products/1", productRequest.ToResponse());
})
.AddFluentValidationAutoValidation();

app.Run();
