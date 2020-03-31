using System.Collections.Generic;
using System.Collections.ObjectModel;
using FarmLibrary;

namespace McDonaldsHadAFarm
{
  public class FarmView
  {
    public ObservableCollection<Animal> ListOfAnimals { get; set; }

    public FarmView(Farm farm)
    {
      ListOfAnimals = new ObservableCollection<Animal>(farm.Animals);
    }
  }
}