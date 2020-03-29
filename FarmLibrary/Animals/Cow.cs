using System;
using System.Net;
using Microsoft.VisualBasic;

namespace FarmLibrary.Animals
{
  public class Cow : Animal
  {
    public Cow(DateTime birth, string? chip = null) : base(product: Product.ProductType.Milk, birth: birth, chipCode: chip)
    {
      Adult = false;
      if ((DateTime.Now - birth).TotalDays / 7 >= 53 * 4)
        Adult = true;
    }

    public override string WhatAmI()
    {
      return String.Format("I am a {0}", Adult ? "Cow" : "Calve");
    }
  }
}