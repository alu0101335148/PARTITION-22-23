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

    // First we read a json file with the format:
    //  {
    //   "prop1: [
    //     "val1", 
    //     "val2",
    //     ...
    //   ], 
    //   "prop2": [
    //     "val3", 
    //     "val4",
    //     ...
    //   ]
    //   }
    public _3DM(string inputFileName) {
      // Read the file
      string json = System.IO.File.ReadAllText(inputFileName);
      // Deserialize the json
      dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
      // Get the properties
      string[] wSet = jsonObj["wSet"].ToObject<string[]>();
      string[] xSet = jsonObj["xSet"].ToObject<string[]>();
      string[] ySet = jsonObj["ySet"].ToObject<string[]>();
      string[,] mSet = jsonObj["mSet"].ToObject<string[,]>();
      // Set the properties
      sizeM_ = (uint) mSet.GetLength(0);
      sizeXYZ_ = (uint) xSet.Length;
      wSet_ = wSet;
      xSet_ = xSet;
      ySet_ = ySet;
      mSet_ = mSet;

      // Alternativa:
      // string json = System.IO.File.ReadAllText(inputFileName);
      // // Then we deserialize it into a dictionary
      // Dictionary<string, List<string>> dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
      // // Now we can access the values
      // sizeM_ = (uint)dict["sizeM"][0];
      // sizeXYZ_ = (uint)dict["sizeXYZ"][0];
      // wSet_ = dict["wSet"].ToArray();
      // xSet_ = dict["xSet"].ToArray();
      // ySet_ = dict["ySet"].ToArray();
      // mSet_ = new string[sizeM_, 3];
      // for (int i = 0; i < sizeM_; i++) {
      //   mSet_[i, 0] = dict["mSet"][i * 3];
      //   mSet_[i, 1] = dict["mSet"][i * 3 + 1];
      //   mSet_[i, 2] = dict["mSet"][i * 3 + 2];
      // }

    }

    public uint GetMSize() {
      return sizeXYZ_;
    }


    public uint GetXYZSize() {
      return sizeM_;
    }
    
  }
}
