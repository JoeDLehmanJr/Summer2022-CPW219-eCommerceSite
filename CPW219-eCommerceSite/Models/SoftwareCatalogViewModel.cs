namespace CPW219_eCommerceSite.Models
{
    public class SoftwareCatalogViewModel
    {
        public SoftwareCatalogViewModel(List<Product> product, int lastPage, int currPage)
        {
            Products = product;
            LastPage = lastPage;
            CurrentPage = currPage;
        }
        
        public List<Product> Products { get; private set; }

        /// <summary>
        /// The last page of the catalog. Calculated by having a total number 
        /// of products divided by products per page
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
