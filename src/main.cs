/**
 * Universidad de La Laguna
 * Escuela Superior de Ingeniería y Tecnología
 * Grado en Ingeniería Informática
 * Asignatura: Complejidad Computacional
 * Curso: 2022-2023
 * Práctica Módulo 2: 3DM a Partition
 * Autores:
 *  - Airam Rafael Luque León
 *  - Lucas Hernández Abreu
 *  - Juan Salvador Magariños Alba
 *  - Alejandro García Perdomo
 * Descipción: 
 * Clase que controla el problema principal
 */

using System;
using _3dm = src._3dm;
using partition = src.partition;

namespace src {
  class Program {
    // Método principal, recibe los nombres de los archivos de entrada y salida, 
    // los lee y crea las instancias de la clase 3DM y translator, al que le 
    // pasa la instancia 3DM para que retorce una reduccón del 3DM a partition
    static void Main(string[] args) {
      string inputFileName = args[0];
      string outputFileName = args[1];
      _3DM instance3DM = new _3DM(inputFileName);
      partition instancePartition = Translator.Translate3DMToPartition(instance3DM);
      instancePartition.WriteToFile(outputFileName);
    }
  }
}