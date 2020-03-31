#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
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

    public Farm Farm = new Farm();
    
    public MainWindow()
    {
      InitializeComponent();
      AnimalsListView.ItemsSource = Farm;
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

      ICollectionView view = CollectionViewSource.GetDefaultView(AnimalsListView.ItemsSource);
      view.Refresh();
    }

    private void LoadFromXml(string filename)
    {
      XmlTextReader? reader = null;
      try
      {
        reader = new XmlTextReader(filename);
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            switch (reader.Name)
            {
              case "Chicken": 
              case "Cow": 
              case "Sheep": 
              case "Duck":
                Animal? a = createFromXml(reader.ReadSubtree(), reader.Name);
                if (a == null)
                  throw new Exception("Data could not be loaded");
                Farm.Animals.Add(a);
                break;
            }
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }

    private Animal? createFromXml(XmlReader reader, string className)
    {
      DateTime birth = DateTime.Now;
      int? totalProduced = null;
      string? chipCode = null;
      reader.Skip();
      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element)
        {
          switch (reader.Name)
          {
            case "Birth":
              string dateString = reader.ReadElementContentAsString();
              birth = DateTime.Parse(dateString);
              break;
            case "TotalProduced":
              try
              {
                totalProduced = reader.ReadElementContentAsInt();
              }
              catch (FormatException)
              {
                totalProduced = null;
              }
              break;
            case "ChipCode":
              chipCode = reader.ReadElementContentAsString();
              break;
          }
        }
      }

      switch (className)
      {
        case "Chicken": return new Chicken(birth, chipCode) {Product = {Type = Product.ProductType.Egg }, TotalProduced = totalProduced };
        case "Cow": return new Cow(birth, chipCode) {Product = {Type = Product.ProductType.Milk }, TotalProduced = totalProduced };
        case "Sheep": return new Sheep(birth, chipCode) {Product = {Type = Product.ProductType.Wool }, TotalProduced = totalProduced };
        case "Duck": return new Duck(birth, chipCode) {Product = {Type = Product.ProductType.Feather }, TotalProduced = totalProduced };
      }
      return null;
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
      writer.WriteString(Farm.Name);
      writer.WriteEndElement();
      writer.WriteStartElement("Animals");
      writer.WriteStartAttribute("count");
      writer.WriteString(Farm.Animals.Count.ToString());
      writer.WriteEndAttribute();
      foreach (var animal in Farm.Animals)
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