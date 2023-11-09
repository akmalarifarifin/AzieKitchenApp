﻿using System;
namespace AzieKitchen.Models
{
	public class FoodItem
	{
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
    }
}

