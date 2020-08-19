﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Application.Models
{
    public class MenuItemListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public decimal Price { get; set; }

    }
}
