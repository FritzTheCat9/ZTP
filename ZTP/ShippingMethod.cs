﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP
{
    public class ShippingMethod
    {
        /* POLA */
        [Key]
        [Display(Name = "DostawaId")]
        public int ShippingMethodID { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        /* POLA - ENTITY FRAMEWORK */
        public ICollection<Order> Orders { get; set; }
    }
}
