using System;

namespace src {
  public class Translator {
    public static void Translate3DMToPartition(_3DM original_problem) {
      uint sizeM = original_problem.GetMSize(); // nº de tripletas de M (k)
      double numberOfBits = Math.Log(sizeM + 1.0f, 2f); // nº de bits por elemento (p)
      if (numberOfBits % 1 == 0) {
        numberOfBits += 1;
      } else {
        numberOfBits =  Math.Ceiling(numberOfBits);
      }
      uint sizeXYZ = original_problem.GetXYZSize();

    }
  }
}
