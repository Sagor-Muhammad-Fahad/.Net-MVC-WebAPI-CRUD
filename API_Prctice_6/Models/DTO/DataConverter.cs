using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;

namespace API_Prctice_6.Models.DTO
{
    public class DataConverter : MediaTypeFormatter
    {
        public DataConverter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
        }
        public override bool CanReadType(Type type)
        {
           return type == typeof(OrderRequest);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var multiPart = await content.ReadAsMultipartAsync();
            var orderData = new OrderRequest();

            foreach (var item in multiPart.Contents)
            {
                var fieldName = item.Headers.ContentDisposition.Name.Trim('\"');

                if(fieldName =="Order")
                {
                    var order=await item.ReadAsStringAsync();
                    orderData.Order = JsonConvert.DeserializeObject<Order>(order);
                }
                else if(fieldName =="ImageFile")
                {
                    orderData.ImageFile = await item.ReadAsByteArrayAsync();
                    orderData.ImageName = item.Headers.ContentDisposition.FileName;
                }
            }
            return orderData;
        }
    }
}