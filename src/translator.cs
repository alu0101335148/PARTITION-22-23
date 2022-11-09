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
/// Descipción:
/// Clase encargada de hacer una reducción del problema 3DM a PARTITION.
/// </summary>

namespace src {
  /// <summary>
  /// Clase encargada de realizar la transformación entre una instancia del 
  /// problema 3DM y una de PARTITION
  /// </summary>
  public class Translator {

    /// <summary>
    /// Método estático que lleva a cabo la conversión de un problema a otro. 
    /// original_problem: instancia del 3DM que se va a transformar
    /// </summary>
    public static Partition Translate3DMToPartition(_3DM original_problem) {
      // nº de tripletas de M (k)
      uint sizeM = original_problem.GetMSize(); 
      // nº bits por elemento (p)
      double numberOfBits = Math.Log(sizeM + 1.0f, 2f);
      if (numberOfBits % 1 == 0) { numberOfBits += 1; }
      else { numberOfBits = Math.Ceiling(numberOfBits); }
      // cardinalidad de los conjuntos W, X e Y
      uint sizeXYZ = original_problem.GetXYZSize(); 

      // Conversión a binario y cálculo de la suma total de los s(a)
      ulong sum = 0;
      List<ulong> numbers = new List<ulong>();
      const uint one = 1;

      for (int triplet = 0; triplet < sizeM; ++triplet) {
        ulong newNumber = 0;

        // se extraen los elementos de la tripleta
        string firstElement = original_problem.GetElement(triplet, 0);
        string secondElement = original_problem.GetElement(triplet, 1);
        string thirdElement = original_problem.GetElement(triplet, 2);
        
        // se obtiene la posición de cada uno en el conjunto
        int firstPosition = 
          original_problem.GetElementPositionInSet(firstElement, "w");
        int secondPosition = 
          original_problem.GetElementPositionInSet(secondElement, "x");
        int thirdPosition = 
          original_problem.GetElementPositionInSet(thirdElement, "y");

        // se crea el número binario que corresponde a la tripleta, y su valor
        // se añade al total
        int firstShift = (int)(((2 * numberOfBits * 3) + 
            ((2 - firstPosition) * numberOfBits)));
        int secondShift = (int)(((1 * numberOfBits * 3) + 
            ((2 - secondPosition) * numberOfBits)));
        int thirdShift = (int)((((2 - thirdPosition) * numberOfBits)));

        newNumber |= (one << firstShift);
        newNumber |= (one << secondShift);
        newNumber |= (one << thirdShift);
        numbers.Add(newNumber);
        sum += newNumber;
      }

      // Cálculo de B
      ulong matchingChecker = 0;

      for (int currentSet = 2; currentSet >= 0; currentSet--)  {
        for (int currentElement = (int)sizeXYZ - 1; 
             currentElement >= 0; 
             currentElement--) 
        {
          int shift = (int)(((currentSet * numberOfBits * 3) + 
            ((2 - currentElement) * numberOfBits)));
          matchingChecker |= one << shift;
        }
      }

      // Cálculo de b1 y b2
      ulong b1 = 2 * sum - matchingChecker;
      ulong b2 = sum + matchingChecker;

      // Creación del Partition
      numbers.Add(b1);
      numbers.Add(b2);

      return new Partition(numbers.ToArray());
    }
  }
}
