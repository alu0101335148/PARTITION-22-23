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
///  Clase que controla el problema principal
/// </summary>

namespace src {
  public class Program {
    /// <summary>
    /// Recibe los archivos de entrada y salida, lee el de entrada y crea las 
    /// instancias de la clase 3DM y translator, al que le pasa la instancia 3DM
    /// para que retorne una reduccón del 3DM a partition y la imprima en el 
    /// archivo de salida
    /// </summary>
    static void Main(string[] args) {
      String inputFilePath = "";
      String outputFilePath = "";
      if (args.Length != 2) {
        if (args.Length == 1) {
          if (args[0] == "-h" || args[0] == "--help") {
            ShowHelp();
            return;
          }
          inputFilePath = args[0];
          outputFilePath = "output/out.json";
          Console.WriteLine("No output file specified, using default: " + outputFilePath); 
        } else {
          Console.WriteLine("Error: Missing input and output files");
        }
      }
      else {
        inputFilePath = args[0];
        outputFilePath = args[1];
      }

      try {
        _3DM instance3DM = new _3DM(inputFilePath);
        instance3DM.Print();
        Partition instancePartition = Translator.Translate3DMToPartition(instance3DM);
        instancePartition.WriteToFile(outputFilePath);
      }
      catch (System.Exception) {
        ShowHelp();
        throw;
      }
    }

    /// <summary>
    /// Método que muestra la ayuda del programa
    /// </summary>
    static void ShowHelp() {
      Console.WriteLine("Usage: dotnet run <input_file> [output_file]");
    }
  }
}
