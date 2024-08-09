using FluentValidation;
using MinimalApiWithFluentValidation.Dtos;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.AddFluentValidationEndpointFilter();

var app = builder.Build();

// 1.) FluentValidation
app.MapPost("/api/products-v1", (ProductRequest productRequest, IValidator<ProductRequest> validator) =>
{
    var result = validator.Validate(productRequest);

    if (result.IsValid)
    {
        return Results.Created($"/api/products/1", productRequest.ToResponse());
    }

    return Results.ValidationProblem(result.ToDictionary());
});

// 2.) ForEvolve.FluentValidation
app.MapPost("/api/products-v2", (ProductRequest productRequest) =>
{
    return Results.Created($"/api/products/1", productRequest.ToResponse());
})
.AddFluentValidationFilter();

// 3.) SharpGrip.FluentValidation
app.MapPost("/api/products-v3", (ProductRequest productRequest) =>
{
    return Results.Created($"/api/products/1", productRequest.ToResponse());
})
.AddFluentValidationAutoValidation();

app.Run();
