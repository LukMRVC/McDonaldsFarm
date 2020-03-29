using System;

namespace FarmLibrary.Animals
{
  public class Sheep : Animal
  {
    public Sheep(DateTime birth, string? chip = null) : base(product: Product.ProductType.Wool, birth: birth, chipCode: chip)
    {
      Adult = false;
      if ((DateTime.Now - birth).TotalDays / 7 >= 53)
        Adult = true;
    }

    public override string WhatAmI()
    {
      return String.Format("I am a {0}", Adult ? "Sheep" : "Lamb");
    }
  }
}