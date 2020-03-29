using System.ComponentModel;

namespace FarmLibrary
{
  public class Product 
  {
    public enum ProductType
    {
      Milk,
      Egg,
      Feather,
      Wool
    }

    public ProductType Type { get; set; }

    public override string ToString()
    {
      if (Type == ProductType.Egg)
        return "Egg";
      if (Type == ProductType.Milk)
        return "Milk";
      if (Type == ProductType.Feather)
        return "Feather";
      if (Type == ProductType.Wool)
        return "Wool";
      return "Unknown product";
    }
  }
}