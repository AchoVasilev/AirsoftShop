namespace AirsoftShop.Services.Models.Product;

public class GunsQueryServiceModel
{
        public List<string>? Manufacturers { get; init; }

        public List<string>? Dealers { get; init; }

        public List<string>? Colors { get; init; }

        public List<double>? Powers { get; init; }

        public string? CategoryName { get; init; }

        public string? OrderBy { get; init; }

        public int Page { get; init; }

        public int ItemsPerPage { get; init; }
}