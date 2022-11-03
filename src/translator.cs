using System;

namespace src {
  class Translator {
    public static void Translate3DMToPartition(_3DM original_problem) {
      uint sizeM = original_problem.GetMSize(); // nº de tripletas de M (k)
      float numberOfBits = Math.Log(sizeM + 1, 2); // nº de bits por elemento (p)
      if (numberOfBits % 1 == 0.0) {
        numberOfBits += 1;
      } else {
        numberOfBits =  Math.Ceiling(numberOfBits);
      }
      uint sizeXYZ = original_problem.GetXYZSize();

    }
  }
}
