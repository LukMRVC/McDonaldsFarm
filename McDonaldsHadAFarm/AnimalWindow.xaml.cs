#nullable enable
using System;
using System.Windows;
using FarmLibrary;
using FarmLibrary.Animals;

namespace McDonaldsHadAFarm
{
  public partial class AnimalWindow : Window
  {

    public Animal? Animal { get; private set; } = null;
    public AnimalWindow()
    {
      InitializeComponent();
    }

    public AnimalWindow(Animal a) : this()
    {
      ChipCodeBox.Text = a.ChipCode;
      ProducedBox.Text = a.TotalProduced.ToString();
      AnimalChoice.SelectedValue = a.GetType().Name;
      BirthPicker.SelectedDate = a.Birth;
      Animal = a;
    }

    private void SaveAnimal_Click(object sender, RoutedEventArgs e)
    {
      string? chip = ChipCodeBox.Text;
      DateTime birth;
      int? totalProduced;
      string? animalType;
      try
      {
        totalProduced = Int32.Parse(ProducedBox.Text);
      }
      catch (Exception)
      {
        totalProduced = null;
      }
      try
      {
        animalType = AnimalChoice.SelectedValue.ToString();
        if (BirthPicker.SelectedDate.HasValue)
        {
          birth = BirthPicker.SelectedDate.Value;
        }
        else
        {
          MessageBox.Show("There were some problems while saving", "Incorrect values",
            MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
          
      }
      catch (Exception)
      {
        MessageBox.Show("There were some problems while saving", "Incorrect values",
          MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      switch (animalType)
      {
        case "Chicken": Animal = new Chicken(birth, chip) { TotalProduced = totalProduced };
          break;
        case "Cow": Animal = new Cow(birth, chip) { TotalProduced = totalProduced };
          break;
        case "Duck": Animal = new Duck(birth, chip) { TotalProduced = totalProduced };
          break;
        case "Sheep": Animal = new Sheep(birth, chip) { TotalProduced = totalProduced };
          break;
        default:
          MessageBox.Show("There were some problems while saving", "Incorrect values",
            MessageBoxButton.OK, MessageBoxImage.Error);
          return;
      }
      Close();
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}