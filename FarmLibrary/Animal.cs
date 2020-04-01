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

    public string DisplayName
    {
      get { return ToString(); }
      set
      {
        
      }
    }

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
      if (!Adult)
        return null;
      IncreaseProduced();
      return Product;
    }
    
    public override string ToString()
    {
      return String.Format("{4}, Product: {0}, Adult: {1}, Total produced: {2}, Chip: {3}",
        Product, Adult ? "YES" : "NO" , TotalProduced, ChipCode, this.GetType().Name);
    }
  }
}