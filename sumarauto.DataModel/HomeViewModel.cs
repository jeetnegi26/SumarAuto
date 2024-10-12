using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataModel
{
    public class HomeViewModel
    {
        public List<Banner> Banners { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Makes { get; set; }
    }
    public class IsFeaturedMakeAndCat
    {
        public List<Category> Categories { get; set; }
        public List<Make> Makes { get; set; }
    }
    public class BlogsRecentAndFeatured
    {
        public List<Blogs> Recent { get; set; }
        public List<Blogs> Featured { get; set; }
    }
    public class ClientAndGallery
    {
        public List<ClientAndGalleryItem> Clients { get; set; }
        public List<ClientAndGalleryItem> Gallery { get; set; }
    }
    public class ClientAndGalleryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
    public class KeysDataModel
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
    public class DropCatMake
    {
        public List<SelectListItem> SelectCategories { get; set; }
        public List<SelectListItem> SelectMakes { get; set; }
    }
    public class DropModel
    {
        public string Value  { get; set; }
    }
    public class DropYELC
    {
        public List<string> YearsSelect  { get; set; }
        public List<string> Engines  { get; set; }
        public List<string> Liters  { get; set; }
        public List<string> Chassis  { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string RewriteUrl { get; set; }
        public string Package { get; set; }
        public List<ProductMake> ProductMake { get; set; }
    }
    public class ProductMake
    {
        public int ProductId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
    }

    public class FinalProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Package { get; set; }
        public List<FinalProductMake> FinalProductMake { get; set; }
        public List<FinalProductImgs> FinalProductImgs { get; set; }
    }
    public class FinalProductMake
    {
        public int AutoPArtMakeID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Chassis { get; set; }
    }
    public class FinalProductImgs
    {
        public string Image { get; set; }
        public bool Default { get; set; }
    }
}
