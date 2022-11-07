using System.Text.Json;

namespace src {
  /// <summary>
  /// Clase que representa un 3DM
  /// </summary>
  public class Partition {
    // Números que se tienen que dividir en dos grupos iguales
    private ulong[] numbers_;
    
    /// <summary>
    /// Constructor de la clase 3DM que recibe los números que se tienen que
    /// dividir en dos grupos iguales
    /// </summary>
    public Partition(ulong[] numberList) {
      numbers_ = numberList;
    }

    /// <summary>
    /// Método que escribe la información de la clase en un archivo de salida
    /// outputFileName: Ruta del archivo de salida
    /// </summary>
    public void WriteToFile(string outputFilePath)  {
      string jsonString = JsonSerializer.Serialize(
        outputFilePath, 
        new JsonSerializerOptions() { WriteIndented = true }
      );
      using (StreamWriter outputFile = new StreamWriter(outputFilePath)) {
        outputFile.WriteLine(jsonString);
      }
    }
  }
}
