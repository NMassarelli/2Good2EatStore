using _2Good2EatStore.Data.Enums;

namespace _2Good2EatStore.Data
{
	public class Product
	{
		public required int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required ProductTypeEnum ProductType { get; set; }

		public required string ProductImageURL { get; set; }

	}
}
