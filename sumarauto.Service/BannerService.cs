using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Model;
using sumarauto.database;

namespace Service
{
    public class BannerService
    {
        #region singleton
        public static BannerService Instance
        {
            get
            {
                if (instance == null) instance = new BannerService();
                return instance;
            }
        }
        private static BannerService instance { get; set; }
        public BannerService()
        {
        }
        #endregion
        public IEnumerable<Banner> GetBannerList()
        {
            using (var context = new AppDbContext())
            {
                return context.Banners.ToList();
            }
        }
        public IEnumerable<Banner> GetActiveBannerList()
        {
            using (var context = new AppDbContext())
            {
                return context.Banners.Where(x => x.Status == true).ToList();
            }
        }
        public ResultModel SaveBanner(Banner model)
        {
            var result = new ResultModel();
            result.Result = false;
            result.Messsage = Constant.Message.Error;
            using (var context = new AppDbContext())
            {
                model.CreatedOn = HelperService.Instance.getCurrentDateTime();
                model.EditedOn = HelperService.Instance.getCurrentDateTime();
                model.CreatedBy = "Admin";
                context.Banners.Add(model);
                context.SaveChanges();
                result.Result = true;
                result.Messsage = string.Format(Constant.Message.Add, "Banner Detail");
            }
            return result;
        }
        public ResultModel EditBanner(Banner model)
        {
            var result = new ResultModel();
            result.Result = false;
            result.Messsage = Constant.Message.Error;
            result.Value1 = model.Image;
            using (var context = new AppDbContext())
            {
                model.EditedOn = HelperService.Instance.getCurrentDateTime();
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                result.Result = true;
                result.Messsage = string.Format(Constant.Message.Edit, "Banner Detail");
            }
            return result;
        }
        public Banner GetBannerById(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Banners.FirstOrDefault(x => x.Id == id);
            }
        }
        public ResultModel DeleteBanners(int id)
        {
            var result = new ResultModel();
            result.Result = false;
            result.Messsage = Constant.Message.Error;
            using (var context = new AppDbContext())
            {
                var data = context.Banners.FirstOrDefault(x => x.Id == id);
                result.Value1 = data.Image;
                context.Banners.Remove(data);
                context.SaveChanges();
                result.Result = true;
                result.Messsage = string.Format(Constant.Message.Delete, "Banners Image");
            }
            return result;
        }

        public List<Banner> SelectBanners(string Value)
        {
            using (var context = new AppDbContext())
            {
                var data = context.Banners.Where(x => x.Type == Value && x.Status == true).ToList();
                return data;
            }
        }
    }
}
