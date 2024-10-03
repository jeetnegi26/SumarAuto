using Service;
using sumarauto.database;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ContactService
    {
        #region singleton
        public static ContactService Instance
        {
            get
            {
                if (instance == null) instance = new ContactService();
                return instance;
            }
        }
        private static ContactService instance { get; set; }
        #endregion

        public async Task<List<ContactForm>> GetContactList()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var data = await db.ContactForm.OrderByDescending(x => x.Id).ToListAsync();
                    return data;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool RemoveContactForm(int Id)
        {
            using (var db = new AppDbContext())
            {
                var delete = db.ContactForm.FirstOrDefault(x => x.Id == Id);
                db.ContactForm.Remove(delete);
                return db.SaveChanges() > 0;
            }
        }

        public async Task<bool> SaveContactAsync(ContactForm model)
        {
            using (var db = new AppDbContext())
            {
                model.CreatedOn = HelperService.Instance.getCurrentDateTime();
                db.ContactForm.Add(model);
                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
