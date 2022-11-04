using System;
using System.Collections.Generic;
using System.Text.Json;

namespace src {
  /// <summary>
  /// Clase que representa un 3DM
  /// </summary>
  public class _3DM {
    private uint sizeM_;
    private uint sizeXYZ_;
    private string[] wSet_; 
    private string[] xSet_; 
    private string[] ySet_; 
    private string[] mSet_; 

    /// <summary>
    /// Constructor de la clase 3DM, lee el archivo de entrada y asigna los
    /// valores a los atributos de la clase
    /// inputFilePath: Ruta del archivo de entrada
    /// </summary>
    public _3DM(string inputFileName) {
      // Read the json file an create a dictionary with the values of the file
      string jsonString = File.ReadAllText(inputFileName);
      Dictionary<string, String[]> jsonMap = 
        JsonSerializer.Deserialize<Dictionary<string, String[]>>(jsonString)
        ?? throw new ArgumentNullException("Error: Invalid input file");

      // Extract the values from the json and check if they are valid
      CheckValues(jsonMap);

      // Assign the values to the attributes
      wSet_ = jsonMap["wSet"];
      xSet_ = jsonMap["xSet"];
      ySet_ = jsonMap["ySet"];
      mSet_ = jsonMap["mSet"];
      sizeM_ = (uint) mSet_.Length;
      sizeXYZ_ = (uint) xSet_.Length;
    }

    /// <summary>
    /// Check the values of the json file and return true if they are valid
    /// </summary>
    private void CheckValues(Dictionary<string, String[]> jsonMap) {
      string errorMessage = "Error: Invalid input file";
      string[] wSet = jsonMap["wSet"] ?? 
             throw new ArgumentNullException(errorMessage +  "(wSet)");
      string[] xSet = jsonMap["xSet"] ?? 
             throw new ArgumentNullException(errorMessage +  "(xSet)");
      string[] ySet = jsonMap["ySet"] ?? 
             throw new ArgumentNullException(errorMessage +  "(ySet)");
      string[] mSet = jsonMap["mSet"] ?? 
             throw new ArgumentNullException(errorMessage +  "(mSet)");

      if (wSet.Length != xSet.Length || 
          wSet.Length != ySet.Length || 
          wSet.Length != mSet.Length) {
        throw new ArgumentException(
          errorMessage + "(The w, x and y sets must have the same length)"
        );
      }
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
