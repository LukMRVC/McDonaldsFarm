using System;

namespace FarmLibrary.Animals
{
  public class Duck : Animal
  {
    public Duck(DateTime birth, string? chip = null) : base(product: Product.ProductType.Feather, birth: birth, chipCode: chip)
    {
      Adult = false;
      if ((DateTime.Now - birth).TotalDays / 7 >= 10)
        Adult = true;
    }

    public override string WhatAmI()
    {
      return String.Format("I am a {0}", Adult ? "Duck" : "Duckling");
    }
  }
}