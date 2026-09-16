using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Prctice_6.Models.DTO
{
    public class OrderRequest
    {
        public  Order Order { get; set; }
        public byte[] ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}