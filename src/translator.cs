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
    /// Devuelve una instancia de la clase Partition
    /// </summary>
    public static Partition Translate3DMToPartition(_3DM original_problem) {
      // nº de tripletas de M (k)
      uint sizeM = original_problem.GetMSize(); 
      // nº bits por elemento (p)
      double numberOfBits = Math.Ceiling(Math.Log(sizeM + 1.0f, 2f));
      // cardinalidad de los conjuntos W, X e Y (q)
      uint sizeWXY = original_problem.GetWXYSize(); 

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
        ulong first = (ulong)(Math.Pow(2, (numberOfBits * (3 * sizeWXY - firstPosition - 1))));
        ulong second = (ulong)(Math.Pow(2, (numberOfBits * (2 * sizeWXY - secondPosition - 1))));
        ulong third = (ulong)(Math.Pow(2, (numberOfBits * (sizeWXY - thirdPosition - 1))));

        newNumber = first + second + third;
        numbers.Add(newNumber);
        sum += newNumber;
      }

      // Cálculo de B
      ulong matchingChecker = 0;

      for (int currentSet = 2; currentSet >= 0; currentSet--)  {
        for (int currentElement = (int)sizeWXY - 1; 
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
