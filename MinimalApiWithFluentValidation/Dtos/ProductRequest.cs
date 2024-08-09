namespace MinimalApiWithFluentValidation.Dtos;

public record ProductRequest(string Name, double Price)
{
    public ProductResponse ToResponse() => new(1, Name, Price);
}
