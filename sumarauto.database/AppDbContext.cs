using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;
using System.Data;
using System.Reflection;

namespace sumarauto.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("sumarautoDb")
        {
            
        }
        public DbSet<AutoPart> AutoPart { get; set; }
        public DbSet<AutoPartMake> AutoPartMake { get; set; }
        public DbSet<AutoPartImages> AutoPartImages { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Make> Make { get; set; }
        //-------------------------------------------
        public DbSet<Chassis> Chassis { get; set; }
        public DbSet<Engine> Engine { get; set; }
        public DbSet<Liter> Liter { get; set; }
        public DbSet<MModel> MModel { get; set; }
        public DbSet<Year> Year { get; set; }
        //-------------------------------------------
        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAssign> RoleAssign { get; set; }

        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<ContactForm> ContactForm { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<FileUploadModel> FileUploadModels { get; set; }

    }
}
