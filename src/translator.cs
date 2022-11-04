using System;

namespace src {
  /// <summary>
  /// Clase encargada de realizar la transformación entre una instancia del problema 3DM y una de PARTITION
  /// </summary>
  public class Translator {

    /// <summary>
    /// Método estático que lleva a cabo la conversión de un problema a otro. Su argumento es:
    /// original_problem: instancia del 3DM que se va a transformar
    /// 
    /// DEBERÍA DEVOLVER UNA INSTANCIA DEL PARTITION
    /// </summary>
    public static void Translate3DMToPartition(_3DM original_problem) {
      uint sizeM = original_problem.GetMSize(); // nº de tripletas de M (k)
      double numberOfBits = Math.Log(sizeM + 1.0f, 2f); // nº de bits por elemento (p)
      if (numberOfBits % 1 == 0)
      {
        numberOfBits += 1;
      }
      else
      {
        numberOfBits = Math.Ceiling(numberOfBits);
      }
      uint sizeXYZ = original_problem.GetXYZSize(); // cardinalidad de los conjuntos W, X e Y

      // Conversión a binario y cálculo de la suma total de los s(a)
      ulong sum = 0;
      List<ulong> binaries = new List<ulong>();
      for (int triplet = 0; triplet < sizeM; ++triplet)
      {
        ulong newNumber = 0;
        const uint one = 1;

        // se extraen los elementos de la tripleta
        string firstElement = original_problem.GetElement(triplet, 0);
        string secondElement = original_problem.GetElement(triplet, 1);
        string thirdElement = original_problem.GetElement(triplet, 2);

        // se obtiene la posición de cada uno en el conjunto
        int firstPosition = original_problem.GetElementPositionInSet(firstElement, "w");
        int secondPosition = original_problem.GetElementPositionInSet(secondElement, "x");
        int thirdPosition = original_problem.GetElementPositionInSet(thirdElement, "y");

        // se crea el número binario que corresponde a la tripleta, y su valor se añade al total
        int firstShift = (int)(((2 * numberOfBits * 3) + ((2 - firstPosition) * numberOfBits)));
        int secondShift = (int)(((1 * numberOfBits * 3) + ((2 - secondPosition) * numberOfBits)));
        int thirdShift = (int)(((0 * numberOfBits * 3) + ((2 - thirdPosition) * numberOfBits)));
        newNumber |= one << firstShift;
        newNumber |= one << secondShift;
        newNumber |= one << thirdShift;

        binaries.Add(newNumber);
        sum += newNumber;
      }

      // Con esto se calculan las s(a) y el valor C. Lo único que queda por hacer de la traducción es calcular la B
      // y usarla para obtener B1 y B2

      Console.WriteLine("aa");
    }
  }
}
