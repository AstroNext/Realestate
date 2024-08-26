using System;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public EnumsModel.showType showType { get; set; }

    }
}
