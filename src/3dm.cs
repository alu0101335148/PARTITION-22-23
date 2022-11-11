/// <summary>
///  Universidad de La Laguna
///  Escuela Superior de Ingeniería y Tecnología
///  Grado en Ingeniería Informática
///  Asignatura: Complejidad Computacional
///  Curso: 2022-2023
///  Práctica Módulo 2: 3DM a Partition
///  Autores:
///   - Airam Rafael Luque León
///   - Lucas Hernández Abreu
///   - Juan Salvador Magariños Alba
///   - Alejandro García Perdomo
///  Descipción: 
///  Clase encargada de representar los conjuntos del problema 3DM
/// <summary>

using System.Text.Json;

namespace src {
  /// <summary>
  /// Clase que representa un 3DM
  /// </summary>
  public class _3DM {
    private uint sizeM_;
    private uint sizeWXY_;
    private string[] wSet_; 
    private string[] xSet_; 
    private string[] ySet_; 
    private string[,] mSet_; 

    /// <summary>
    /// Constructor de la clase 3DM, lee el archivo de entrada y asigna los
    /// valores a los atributos de la clase
    /// inputFilePath: Ruta del archivo de entrada
    /// </summary>
    public _3DM(string inputFileName) {
      // Read the json file an create a dictionary with the values of the file
      string jsonString = File.ReadAllText(inputFileName);
      var jsonMap = 
        JsonSerializer.Deserialize<Dictionary<string, String[]>>(jsonString)
        ?? throw new ArgumentNullException("Error: Invalid input file");

      // Extract the values from the json and check if they are valid
      CheckValues(jsonMap);

      // Assign the values to the attributes
      sizeM_ = (uint)jsonMap["mSet"].Length;
      wSet_ = jsonMap["wSet"];
      xSet_ = jsonMap["xSet"];
      ySet_ = jsonMap["ySet"];
      mSet_ = new string[sizeM_, 3];
      for (int i = 0; i < sizeM_; i++) {
        for (int j = 0; j < 3; j++) {
          mSet_[i, j] = jsonMap["mSet"][i][j].ToString();
        }
      }
      sizeWXY_ = (uint) xSet_.Length;
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
          wSet.Length != ySet.Length) {
        throw new ArgumentException(
          errorMessage + "(The w, x and y sets must have the same length)"
        );
      }
    }

    /// <summary>
    /// Method to get the number of triplets in the M set
    /// </summary>
    public uint GetMSize() {
      return sizeM_;
    }

    /// <summary>
    /// Method to get the cardinality of the W, X and Y sets
    /// </summary>
    public uint GetWXYSize() {
      return sizeWXY_;
    }

    /// <summary>
    /// Method to get the number that is associated to a certain element of a set
    /// </summary>
    public int GetElementPositionInSet(string element, string setName) {
      switch (setName) {
        case "w":
          return Array.IndexOf(wSet_, element);
        case "x":
          return Array.IndexOf(xSet_, element);
        case "y":
          return Array.IndexOf(ySet_, element);
        default:
          throw new Exception("El conjunto " + setName + " no existe.");
      }
    }

    /// <summary>
    /// Method to get an element, given a position and a triplet
    /// </summary>
    public string GetElement(int triplet, int position) {
      if ((triplet < 0) || (triplet >= sizeM_)) {
        throw new Exception("No existe la tripleta nº " + triplet);
      }
      if ((position < 0) || (position > 2)) {
        throw new Exception("No existe la posición nº " + position + 
          " en las tripletas.");
      }
      return mSet_[triplet,position];
    }
    
    public void Print() {
      Console.WriteLine("sizeM: " + sizeM_);
      Console.WriteLine("sizeWXY: " + sizeWXY_);
      Console.WriteLine("wSet: " + string.Join(", ", wSet_));
      Console.WriteLine("xSet: " + string.Join(", ", xSet_));
      Console.WriteLine("ySet: " + string.Join(", ", ySet_));
      Console.WriteLine("mSet: ");
      for (int i = 0; i < sizeM_; i++) {
        for (int j = 0; j < 3; j++) {
          Console.Write(mSet_[i, j] + " ");
        }
        Console.WriteLine();
      }
    }
  }
}
