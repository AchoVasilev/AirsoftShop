namespace AirsoftShop.Common.Models;

public class PagingModel
{
    public bool HasPreviousPage => this.PageNumber > 1;

    public bool HasNextPage => this.PageNumber < this.PagesCount;

    public int PreviousPageNumber => this.PageNumber - 1;

    public int NextPageNumber => this.PageNumber + 1;

    public int PageNumber { get; set; }

    public int PagesCount => (int)Math.Ceiling((double)this.ItemCount / this.ItemsPerPage);

    public int ItemCount { get; set; }

    public int ItemsPerPage { get; set; }
}