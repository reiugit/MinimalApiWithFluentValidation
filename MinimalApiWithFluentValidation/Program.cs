using FluentValidation;
using MinimalApiWithFluentValidation.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapPost("/api/products", (ProductRequest productRequest, IValidator<ProductRequest> validator) =>
{
    var result = validator.Validate(productRequest);

    if (result.IsValid)
    {
        var response = productRequest.ToResponse();

        return Results.Created($"/api/products/{response.Id}", productRequest.ToResponse());
    }

    return Results.ValidationProblem(result.ToDictionary());
});

app.Run();
