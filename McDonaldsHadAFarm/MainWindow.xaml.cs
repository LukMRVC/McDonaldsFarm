#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using FarmLibrary;
using FarmLibrary.Animals;
using Microsoft.Win32;


namespace McDonaldsHadAFarm
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private Farm _farm;
    
    public MainWindow()
    {
      List<Animal> animals = new List<Animal>();
      animals.Add(new Chicken(DateTime.Now.AddMonths(-6)));
      animals.Add(new Cow(DateTime.Now.AddYears(-8)));
      animals.Add(new Sheep(DateTime.Now.AddYears(-2)));
      _farm = new Farm(animals);
      InitializeComponent();
    }

    private void Load_OnClick(object sender, RoutedEventArgs e)
    {
      var ofd = new OpenFileDialog();
      ofd.FileName = "Farm";
      ofd.DefaultExt = ".xml";
      ofd.Filter = "XML Documents (.xml)|*.xml";
      bool? result = ofd.ShowDialog();
      if (result.HasValue && result.Value)
      {
        string filename = ofd.FileName;
        LoadFromXml(filename);
      }

    }

    private void LoadFromXml(string filename)
    {
      XmlTextReader reader = new XmlTextReader(filename);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "XML Documents (.xml)|*.xml";
      saveFileDialog.FileName = "Farm";
      saveFileDialog.DefaultExt = ".xml";
      var result = saveFileDialog.ShowDialog();
      if (result.HasValue && result.Value)
      {
        try
        {
          SaveToXML(saveFileDialog.OpenFile());
        }
        catch (FileLoadException exception)
        {
          Console.WriteLine(exception);
          MessageBox.Show("There were some problems while opening a file. Please try again", "File could not be loaded",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }

    private void SaveToXML(Stream stream)
    {
      XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
      writer.WriteStartDocument();
      writer.WriteStartElement("Farm");
      writer.WriteStartElement("Name");
      writer.WriteString(_farm.Name);
      writer.WriteEndElement();
      writer.WriteStartElement("Animals");
      writer.WriteStartAttribute("count");
      writer.WriteString(_farm.Animals.Count.ToString());
      writer.WriteEndAttribute();
      foreach (var animal in _farm.Animals)
      {
        writer.WriteStartElement(animal.GetType().Name);
        foreach (PropertyInfo propertyInfo in animal.GetType().GetProperties())
        {
          writer.WriteStartElement(propertyInfo.Name);
          var value = propertyInfo.GetValue(animal);
          if (value != null)
            writer.WriteString(value.ToString());      
          writer.WriteEndElement();
        }
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
      writer.WriteFullEndElement();
      writer.Close();
    }

    private void New_OnClick(object sender, RoutedEventArgs e)
    {
      
    }

    private void Edit_OnClick(object sender, RoutedEventArgs e)
    {
      
      
    }
  }
}