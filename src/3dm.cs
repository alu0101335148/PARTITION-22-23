using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
// https://zetcode.com/csharp/json/

/*
  class _3DM {
    private:
      uint sizeM;             ## Tamaño del conjunto de tripletas
      uint sizeXYZ;           ## Tamaño de los conjuntos
      string[] wSet;          ## Conjunto "W"
      string[] xSet;          ## Conjunto "X"
      string[] ySet;          ## Conjunto "Y"
      string[,] mSet;         ## Conjunto de tripletas "M"
    public:
      _3DM(string)            ## Constructor de la clase, recibe un nombre de archivo como entrada
 }
*/


namespace src {

  public struct Triplet {
    public string x;
    public string y;
    public string z;
      public Triplet(string nx, string ny, string nz) { x = nx; y = ny; z = nz; }
      public override string ToString() => $"({x}, {y}, {z})";
  }
  class _3DM {
    private uint sizeM_;
    private uint sizeXYZ_;
    private string[] wSet_; 
    private string[] xSet_; 
    private string[] ySet_; 
    private string[,] mSet_; 
    
    public _3DM(string inputFileName) {

      using (StreamReader r = new StreamReader(inputFileName)) {
          string json = r.ReadToEnd();
          source = JsonSerializer.Deserialize<List<Input>>(json);
      }
    }

    public uint GetMSize() {
      return sizeXYZ_;
    }


    public uint GetXYZSize() {
      return sizeM_;
    }
    
  }
}
