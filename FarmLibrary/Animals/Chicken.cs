#nullable enable
using System;

namespace FarmLibrary.Animals
{
  public class Chicken : Animal
  {
    public Chicken(DateTime birth, string? chip = null) : base(product: Product.ProductType.Egg, birth: birth, chipCode: chip)
    {
      Adult = false;
      if ((DateTime.Now - birth).TotalDays / 7 >= 16)
        Adult = true;
    }

    public override string WhatAmI()
    {
      return String.Format("I am a {0}", Adult ? "Hen" : "Chicken");
    }

    
  }
}