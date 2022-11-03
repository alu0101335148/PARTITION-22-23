using System.Reflection.Emit;
using System;
namespace src {
  class Program {
    // Recibe los archivos de entrada y salida, lee el de entrada y crea las 
    // instancias de la clase 3DM y translator, al que le pasa la instancia 3DM
    // para que retorce una reduccón del 3DM a partition y la imprime en el 
    // archivo de salida
    static void Main(string[] args) {
      string inputFileName = args[0];
      string outputFileName = args[1];
      // _3DM instance3DM = new _3DM(inputFileName);
      Partition instancePartition = new Partition(new ulong[]{1,2,3,4}); 
      //Translator.Translate3DMToPartition(instance3DM);
      instancePartition.WriteToFile(outputFileName);
    }
  }
}