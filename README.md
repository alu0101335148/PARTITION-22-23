# 3DM a PARTITION

## Descripción

En esta práctica vamos a implementar la traducción polinomial del algoritmo de 3DM a PARTITION.

## Índice de contenidos

- [1. Condificación de la clase 3DM](#1-condificación-de-la-clase-3dm)

- [2. Codificación de la clase Partition](#2-codificación-de-la-clase-partition)

- [3. Clase Translator](#3-clase-translator)

- [4. Clase Main](#4-clase-main)

- [5. Ejecución](#5-ejecución)

- [6. Referencias](#6-Referencias)

## Autores

- Airam Rafael Luque León (alu0101335148)

- Juan Salvador Magariños Alba (alu0101352145)

- Alejandro García Perdomo (alu0101312101)

- Lucas Hernández Abreu (alu0101317496)

## 1. Codificación de la clase 3dm

Nuestra clase 3DM va a recibir el fichero de entrada en formato json, con la siguiente estructura:

```json
{
  "wSet": [ "a", "b", "c" ],
  "xSet": [ "1", "2", "3" ],
  "ySet": [ "α", "β", "γ" ],
  "mSet": [ "a2β", "b1γ", "c3α", "a1α" ]
}
```

Este revisará los parámetros especificados y los almacenará en sus atributos

```cs
private uint sizeM_;
private uint sizeXYZ_;
private string[] wSet_; 
private string[] xSet_; 
private string[] ySet_; 
private string[,] mSet_; 
```

De igual forma, contiene dos métodos `getElement` y `getElementPositionInSet`, que serán métodos auxiliares que empleará la clase Translator para la traducción. `getElement` retorna un elemento de una tripleta determinada, dada una posición y `getElementPositionInSet` retorna la posición de un elemento en un conjunto determinado.


## 2. Codificación de la clase Partition
La clase PARTITION se encarga de representar una instancia del problema PARTITION.
Su función principal es almacenar los números generados después de procesar las tripletas del 3DM. Esta clase solo posee el constructor y el método **WriteToFile**.
El método WriteToFile permite escribir los números generados en un archivo con el formato de salida escogido.

```cs
//CONSTRUCTOR
public Partition(ulong[] numberList) 
{
  numbers_ = numberList;
}
//METODO
public void WriteToFile(string outputFilePath)  
{
  Console.WriteLine("Lenght: " + numbers_.Length);
  string jsonString = JsonSerializer.Serialize(numbers_);
  File.WriteAllText(outputFilePath, jsonString);
}
```


## 3. Clase Translator

Esta clase es la encargada de traducir la instancia del 3DM al Partition. Solo posee un método, **Translate3DMToPartition**, que es "static" y permite realizar la traducción del problema 3DM de entrada y devolver una instancia del Partition. La función se puede separar en varias partes:

* Obtención de los parámetros $k$, $q$, $p$: 

* Cálculo de los $s(a)$ y su sumatorio: 

* Cálculo de $B$: 

* Cálculo de $b1$ y $b2$: 

## 4. Clase Main

Esta será la clase principal de nuestro programa, que implementa una entrada por línea de comandos para poder elegir el fichero de entrada y el fichero de salida. En caso de no especificarlo tomará uno por defecto. Este instancia una clase 3DM a la cual le pasa el fichero de entrada, luego instanciamos una clase Partition que igualamos a una instancia Translator, a la cual le pasamos la instancia de 3DM. Por último, llamamos al método WriteToFile de la clase Partition, pasándole el fichero de salida.

```cs
_3DM instance3DM = new _3DM(inputFilePath);
instance3DM.Print();
Partition instancePartition = Translator.Translate3DMToPartition(instance3DM);
instancePartition.WriteToFile(outputFilePath);
```

## 5. Ejecución

## 6. Referencias
