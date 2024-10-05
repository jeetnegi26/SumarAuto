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
}
