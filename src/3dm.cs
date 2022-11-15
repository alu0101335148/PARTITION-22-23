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
  /// Clase que representa una instancia del problema 3DM
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
      // Lee el archivo JSON y crea un diccionario con sus valores
      string jsonString = File.ReadAllText(inputFileName);
      var jsonMap = 
        JsonSerializer.Deserialize<Dictionary<string, String[]>>(jsonString)
        ?? throw new ArgumentNullException("Error: Invalid input file");

      // Extrae los valores del JSON y comprueba si son válidos
      CheckValues(jsonMap);

      // Asigna los valores a los atributos
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
    /// Método que comprueba si los valores del JSON son correctos, devolviendo true si
    /// es así
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
    /// Getter de la cardinalidad del conjunto M
    /// </summary>
    public uint GetMSize() {
      return sizeM_;
    }

    /// <summary>
    /// Getter de la cardinalidad de los conjuntos W, X e Y
    /// </summary>
    public uint GetWXYSize() {
      return sizeWXY_;
    }

    /// <summary>
    /// Método para obtener el número asociado a un elemento en un conjunto
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
          throw new Exception("The set " + setName + " does not exist.");
      }
    }

    /// <summary>
    /// Método que devuelve el elemento de una posición determinada en una tripleta
    /// </summary>
    public string GetElement(int triplet, int position) {
      if ((triplet < 0) || (triplet >= sizeM_)) {
        throw new Exception("The triplet number " + triplet + " does not exist.");
      }
      if ((position < 0) || (position > 2)) {
        throw new Exception("Triplets can't be indexed with the number " + position);
      }
      return mSet_[triplet,position];
    }
    
    /// <summary>
    /// Método que imprime los conjuntos del 3DM
    /// </summary>
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
