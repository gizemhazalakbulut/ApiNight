﻿namespace ApiNight.WebUI.Dtos.ProductDtos
{
    public class CreateProductDto
    {

        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
