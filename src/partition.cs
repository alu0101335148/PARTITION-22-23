using System;
using System.Text.Json;

namespace src {
  public class Partition {
    // Números que se tienen que dividir en dos grupos iguales
    private ulong[] numbers;
    private string destination;
    
    public Partition(ulong[] numberList, string output) {
      numbers = numberList;
      destination = output;
    }

     public void WriteToFile(string outputFileName)  {
       string jsonString = JsonSerializer.Serialize(
         destination, 
         new JsonSerializerOptions() { WriteIndented = true }
       );
       using (StreamWriter outputFile = new StreamWriter(outputFileName))
       {
         outputFile.WriteLine(jsonString);
       }
     }
  }
}
