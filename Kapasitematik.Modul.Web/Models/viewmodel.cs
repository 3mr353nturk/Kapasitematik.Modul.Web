
using Kapasitematik.Modul.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kapasitematik.Modul.Web.Models
{
    public class viewmodel
    {
        public int Id { get; set; }
        public int? SummaryId { get; set; }
        public DateTime? TodayDate { get; set; }
        public string Name { get; set; }
        public int? Count { get; set; }
        public int? TotalCount { get; set; }
    }
}