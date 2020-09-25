using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace GetMac
{
  class Program
  {
    static void Main(string[] args)
    {
      // Get Mac Address From Your Network.
      string MacAddress = "";
      string name = "";
      int length = 30;
      NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
      foreach (NetworkInterface adapter in interfaces)
      {
        string adapterName = adapter.Name.ToString();
        string namex = "";
        if (adapterName.Count() < length)
        {
          int kurangnya = length - adapterName.Count();
          namex = $"{adapterName}{new String(' ', kurangnya)}";
        }
        else
        {
          namex = adapterName.Substring(0, (length - 3)) + "...";
        }
        if (adapterName.ToLower().Contains("wi-fi"))
        {
          name = adapter.Name;
          MacAddress = adapter.GetPhysicalAddress().ToString();
          WriteFullLine($"Name: {namex} | Mac: {String.Join("-", MacAddress.SplitInParts(2))} | VALID! {Environment.NewLine}", ConsoleColor.Green);
        }
        else
        {
          WriteFullLine($"Name: {namex} | Mac: {String.Join("-", adapter.GetPhysicalAddress().ToString().SplitInParts(2))} {Environment.NewLine}");
        }
      }
      if (MacAddress.Length > 1)
      {
        Console.WriteLine();
        WriteFullLine($"Mac Address yang distore ke Database: {String.Join("-", MacAddress.SplitInParts(2))} dari adapter: {name} {Environment.NewLine}", ConsoleColor.Green);
      }
      else
      {
        Console.WriteLine();
        WriteFullLine($"Tidak ada Mac Address yang cocok. {Environment.NewLine}");
      }
      Console.ReadLine();
    }
    private static void WriteFullLine(string value, ConsoleColor color = ConsoleColor.Red)
    {
      //
      // This method writes an entire line to the console with the string.
      //
      Console.ForegroundColor = color;
      Console.Write(value);
      Console.ResetColor();
    }
  }
  static class StringExtensions
  {
    public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
    {
      if (s == null)
        throw new ArgumentNullException(nameof(s));
      if (partLength <= 0)
        throw new ArgumentException("Part length has to be positive.", nameof(partLength));

      for (var i = 0; i < s.Length; i += partLength)
        yield return s.Substring(i, Math.Min(partLength, s.Length - i));
    }

  }
}
