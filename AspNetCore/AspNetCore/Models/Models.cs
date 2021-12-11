using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Models
{
    public class TestViewModel
    {
        [Required]
        [Display(Name ="구매할 아이템 ID")]
        public int Id { get; set; }

        [Range(1,10,ErrorMessage = "아이템 개수는 1-10")]
        [Display(Name ="수량")]
        public int Count { get; set; }
    }
}
