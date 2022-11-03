using System;
using System.Collections.Generic;
using System.Text.Json;

namespace src {
  public class _3DM {
    private uint sizeM_;
    private uint sizeXYZ_;
    private string[] wSet_; 
    private string[] xSet_; 
    private string[] ySet_; 
    private string[] mSet_; 

    public _3DM(string inputFileName) {
      string jsonString = File.ReadAllText(inputFileName);
      // Create a hashmap from the json string
      Dictionary<string, String[]> jsonMap = 
        JsonSerializer.Deserialize<Dictionary<string, String[]>>(jsonString)
        ?? throw new ArgumentNullException("Error in the json file format");

      // Extract the values from the json and we assign them to the attributes
      sizeM_ = (uint)jsonMap["mSet"].Length;
      sizeXYZ_ = (uint)jsonMap["xSet"].Length;
      wSet_ = jsonMap["wSet"];
      xSet_ = jsonMap["xSet"];
      ySet_ = jsonMap["ySet"];
      mSet_ = jsonMap["mSet"];
    }

    public uint GetMSize() {
      return sizeM_;
    }

    public uint GetXYZSize() {
      return sizeXYZ_;
    }
    
    public void print() {
      Console.WriteLine("sizeM: " + sizeM_);
      Console.WriteLine("sizeXYZ: " + sizeXYZ_);
      Console.WriteLine("wSet: " + string.Join(", ", wSet_));
      Console.WriteLine("xSet: " + string.Join(", ", xSet_));
      Console.WriteLine("ySet: " + string.Join(", ", ySet_));
      Console.WriteLine("mSet: " + string.Join(", ", mSet_));
    }
  }
}
