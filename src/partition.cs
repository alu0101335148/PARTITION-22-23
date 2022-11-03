using System;

namespace src {
  class Partition {
    // Números que se tienen que dividir en dos grupos iguales
    private ulong[] numbers;
    
    public Partition(ulong[] numberList)  {
      numbers = numberList;
    }

    public int WriteToFile(string outputFileName)  {
      string jsonString = JsonSerializer.Serialize(destination, new JsonSerializerOptions() { WriteIndented = true});
      using (StreamWriter outputFile = new StreamWriter(outputFileName))
      {
          outputFile.WriteLine(jsonString);
      }

    }
  }
}