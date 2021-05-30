using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.RequestModels
{
    public class ProvinceGet
    {
        [Required]
        public string CountryIsoCode { get; set; }
    }
}
