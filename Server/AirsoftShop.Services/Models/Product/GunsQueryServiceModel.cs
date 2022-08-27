namespace AirsoftShop.Services.Models.Product;

public class GunsQueryServiceModel
{
        public List<string>? Manufacturers { get; set; }

        public List<string>? Dealers { get; set; }

        public List<string>? Colors { get; set; }

        public List<double>? Powers { get; set; }

        public string? CategoryName { get; set; }

        public string OrderBy { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
}