#nullable enable
using System;

namespace FarmLibrary
{
  public abstract class Animal
  {
    
    public bool Adult { get; set; }

    public string? ChipCode { get; set; }
    
    public Product Product { get; private set; }
    
    public int? TotalProduced { get; set; }

    public DateTime Birth { get; private set; }
    protected Animal(Product.ProductType product, DateTime birth, string? chipCode = null)
    {
      Birth = birth;
      ChipCode = chipCode;
      Product = new Product { Type = product };
    }
    public abstract string WhatAmI();

    public void IncreaseProduced()
    {
      if (TotalProduced.HasValue)
      {
        TotalProduced++;
      }
      else
      {
        TotalProduced = 1;
      }
    }
    public virtual Product? Produce()
    {
      IncreaseProduced();
      if (!Adult)
        return null;
      return Product;
    }
  }
}