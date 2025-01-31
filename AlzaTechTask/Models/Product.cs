using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AlzaTechTask.Data;

/// <summary>
/// Represents a product in the system
/// </summary>
 
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the product
    /// </summary>
    /// <remarks>
    /// This is a required field and serves as the primary key
    /// </remarks>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product
    /// </summary>
    /// <remarks>
    /// This is a required field with maximum length constraints
    /// </remarks>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI for the product's image
    /// </summary>
    /// <remarks>
    /// This should be a valid URI pointing to the product image
    /// </remarks>
    public string ImgUri { get; set; }

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    /// <remarks>
    /// The price is represented as a decimal value for precision
    /// </remarks>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the optional description of the product
    /// </summary>
    /// <remarks>
    /// This field can contain additional details about the product
    /// </remarks>
    public string? Description { get; set; }
}
