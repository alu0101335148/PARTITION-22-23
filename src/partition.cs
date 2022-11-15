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
///  Clase que se encarga de representar una instancia del problema Partition
/// </summary>


using System.Text.Json;

namespace src {
  /// <summary>
  /// Clase que representa una instancia del problema Partition
  /// </summary>
  public class Partition {
    // Números que se tienen que dividir en dos grupos iguales
    private ulong[] numbers_;
    
    /// <summary>
    /// Constructor de la clase Partition que recibe los números que se tienen que
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
      Console.WriteLine("Lenght: " + numbers_.Length);
      string jsonString = JsonSerializer.Serialize(numbers_);
      File.WriteAllText(outputFilePath, jsonString);
    }
  }
}
