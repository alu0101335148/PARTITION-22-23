namespace src {
  public class Program {
    /// <summary>
    // Recibe los archivos de entrada y salida, lee el de entrada y crea las 
    // instancias de la clase 3DM y translator, al que le pasa la instancia 3DM
    // para que retorne una reduccón del 3DM a partition y la imprima en el 
    // archivo de salida
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
        instance3DM.print();
        // Partition instancePartition = new Partition(new ulong[]{1,2,3,4});
        Partition instancePartition = Translator.Translate3DMToPartition(instance3DM);
        Console.WriteLine("ª");
        // instancePartition.WriteToFile(outputFileName);
      }
      catch (System.Exception) {
        ShowHelp();
        throw;
      }
    }
    static void ShowHelp() {
      Console.WriteLine("Usage: dotnet run <input_file> [output_file]");
    }
  }
}
