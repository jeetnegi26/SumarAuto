using Model;
using sumarauto.database;
using sumarauto.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WebInfoService
    {
        #region singleton
        public static WebInfoService Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebInfoService();
                return instance;
            }
        }
        private static WebInfoService instance
        {
            get; set;
        }
        #endregion

        public List<Key> GetInfoKeys()
        {
            using (var db = new AppDbContext())
            {
                return db.Keys.ToList();
            }
        }
        public List<Key> GetBrandLogoAndName()
        {
            using (var db = new AppDbContext())
            {
                return db.Keys.Where(x => x.Type == "BrandLogo" || x.Type == "BrandName").ToList();
            }
        }
        public string GetBrandName()
        {
            using (var db = new AppDbContext())
            {
                return db.Keys.FirstOrDefault(x => x.Type == "BrandName").Name;
            }
        }
        public string GetContactMessage()
        {
            using (var db = new AppDbContext())
            {
                return db.Keys.FirstOrDefault(x => x.Type == "ContactMail").Description;
            }
        }
        public List<Key> GetBrandSocialMedia()
        {
            using (var db = new AppDbContext())
            {
                var data = db.Keys.Where(x => x.Type == "SocialMedia").ToList();
                data = data.Where(x => x.Description != null && x.Description != "").ToList();
                return data;
            }
        }
        public Key GetInfoKey(int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Keys.FirstOrDefault(x => x.Id == id);
            }
        }
        public bool SaveWebInfo(Key Key)
        {
            using (var db = new AppDbContext())
            {
                db.Keys.Add(Key);
                return db.SaveChanges() > 0;
            }
        }
        public bool EditWebInfo(Key key)
        {
            using (var db = new AppDbContext())
            {
                db.Entry(key).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        public async Task<CompanyDetailsViewModel> GetInfoforEmailAsync()
        {
            using (var db = new AppDbContext())
            {
                var data = await db.Keys.ToListAsync();
                var details = new CompanyDetailsViewModel();
                details.CompanyAddress = data.FirstOrDefault(x => x.Name == "Address").Description;
                details.CompanyName = data.FirstOrDefault(x => x.Type == "BrandName").Name;
                details.CompanyContact = data.FirstOrDefault(x => x.Name == "Phone").Description;
                details.CompanyEmail = data.FirstOrDefault(x => x.Name == "Email").Description;
                return details;
            }
        }

        public bool RemoveWebInfo(int id)
        {
            using (var db = new AppDbContext())
            {
                var delete = db.Keys.FirstOrDefault(x => x.Id == id);
                db.Keys.Remove(delete);
                return db.SaveChanges() > 0;
            }
        }
    }
}
