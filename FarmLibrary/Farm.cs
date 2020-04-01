using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FarmLibrary
{
  public class Farm : IEnumerable
  {
    List<Animal> _animals = new List<Animal>();

    public List<Animal> Animals
    {
      get { return _animals; }
      set { _animals = value;  }
    }

    public string Name { get; private set; }
    public Farm(string name = "McDonalds")
    {
      Name = name;
    }

    public Farm(List<Animal> animals, string name = "McDonalds")
    {
      _animals = animals;
      Name = name;
    }

    public Animal this[int i]
    {
      get => _animals[i];
      set { _animals[i] = value; }
    }

    public Animal this[string chipCode]
    {
      get => _animals.FirstOrDefault(x => x.ChipCode == chipCode);
      set
      {
        var idx = _animals.FindIndex(x => x.ChipCode == chipCode);
        if (idx != -1)
          _animals[idx] = value;
        else
          throw new Exception("This chipCode was not found");
      }
    }
  
    public IEnumerator<Animal> GetEnumerator()
    {
      foreach (Animal animal in _animals)
      {
        yield return animal;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}