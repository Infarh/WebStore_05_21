using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class AjaxTestDataViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime ServerTime { get; set; } = DateTime.Now;
    }
}
