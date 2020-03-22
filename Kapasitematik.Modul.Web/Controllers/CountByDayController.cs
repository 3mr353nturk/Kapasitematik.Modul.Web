using Kapasitematik.Modul.Web.Data;
using Kapasitematik.Modul.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kapasitematik.Modul.Web.Controllers
{
    public class CountByDayController : Controller
    {
        // GET: Company
        TestDBEntities db = new TestDBEntities();

        #region CountByDayTableMethods

        public ActionResult Index()
        {
            List<IndexViewModel> model = (from s in db.CountDay
                                          orderby s.TodayDate ascending
                                     select new IndexViewModel
                                     {
                                         Id = s.Id,
                                         TodayDate = s.TodayDate.Value,
                                         Name = s.Name,
                                         Count = (int)s.Count,
                                         TotalCount = (int)s.TotalCount
                                     }).ToList();
            return View(model);
        }

        public JsonResult DayList(int id)
        {
            
            var model = (from s in db.Summary
                         join c in db.CountDay on s.Id equals c.SummaryId
                         where c.Id == id
                         orderby c.TodayDate ascending
                         select new
                         {
                             id = c.Id,
                             date = c.TodayDate.Value.ToString(),
                             name = c.Name,
                             count = c.Count,
                             totalCount = c.TotalCount
                         }).ToList();

            return Json(
            new
            {
                Result = (from obj in model
                          select new
                          {
                              Id = obj.id,
                              TodayDate = obj.date,
                              Name = obj.name,
                              Count = obj.count,
                              TotalCount = obj.totalCount
                          })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BarOlustur(int id)
        {
            var model = (from s in db.Summary
                         join c in db.CountDay on s.Id equals c.SummaryId
                         where c.Id == id
                         orderby c.TodayDate ascending
                         select new
                         {
                             id = id,
                             date = c.TodayDate.Value.ToString(),
                             name = c.Name,
                             count = c.Count,
                             totalCount = c.TotalCount
                         }).ToList();

            return Json(
            new
            {
                Result = (from obj in model
                          select new
                          {
                              Id = obj.id,
                              TodayDate = obj.date,
                              Name = obj.name,
                              Count = obj.count,
                              TotalCount = obj.totalCount
                          })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChartOlustur(int id)
        {
            var model = (from s in db.Summary
                         join c in db.CountDay on s.Id equals c.SummaryId
                         orderby c.TodayDate ascending
                         select new
                         {
                             id = c.Id,
                             date = c.TodayDate.Value.ToString(),
                             name = c.Name,
                             count = c.Count,
                             totalCount = c.TotalCount
                         }).ToList();

            return Json(
            new
            {
                Result = (from obj in model
                          select new
                          {
                              Id = obj.id,
                              TodayDate = obj.date,
                              Name = obj.name,
                              Count = obj.count,
                              TotalCount = obj.totalCount
                          })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(string pieceName, int pieceOfCount, int totalCount)
        {
            Summary summary = new Summary();
            summary.Name = pieceName;
            summary.TotalCount = totalCount;
            db.Summary.Add(summary);
            CountDay countBy = new CountDay();
            countBy.SummaryId = summary.Id;
            countBy.Name = pieceName;
            countBy.TodayDate = DateTime.Today;
            countBy.Count = pieceOfCount;
            countBy.TotalCount = totalCount;
            db.CountDay.Add(countBy);
            var deger = db.SaveChanges();
            return Json(deger, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}