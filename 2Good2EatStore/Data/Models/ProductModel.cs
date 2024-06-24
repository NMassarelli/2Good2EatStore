using _2Good2EatStore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace _2Good2EatStore.Data.Models
{
    public class ProductModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public string? ProductImageURL { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int Inventory { get; set; }
        public bool IsVisible {  get; set; }
        public bool IsDeleted { get; set; }
        public static Product MapToEntity (ProductModel model)
        {
            return new Product
            {
                Name = model.Name,
                Description = model.Description,
                ProductType = model.ProductType,
                ProductImageURL = model.ProductImageURL,
                Inventory = model.Inventory,
                RetailPrice = model.RetailPrice,
                WholesalePrice = model.WholesalePrice,
                isDeleted = model.IsDeleted,
                isVisible = model.IsVisible,

            };
        }
    }

    public class ProductModelFluentValidator : AbstractValidator<ProductModel>
    { 
        public ProductModelFluentValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(25);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }



}
