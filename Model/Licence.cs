using System.ComponentModel.DataAnnotations;
using System;

namespace Model
{
    public class Licence
    {
        [Key]
        public int id { get; set; }
        public string LicenceCode { get; set; }
        public int DaysLeft { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Expire { get; set; }
        public string Validation { get; set; }
        public string Type { get; set; }
        public EnumsModel.LicenceValidation licencevalidation { get; set; }
        public EnumsModel.LicenceType licenceType { get; set; }
        public EnumsModel.LicenceUsable licenceUsable { get; set; }
    }
}
