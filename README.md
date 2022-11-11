# 3DM a PARTITION

## Autores

- Airam Rafael Luque León (alu0101335148)

- Juan Salvador Magariños Alba (alu0101352145)

- Alejandro García Perdomo (alu0101312101)

- Lucas Hernández Abreu (alu0101317496)

## Descripción

En esta práctica vamos a implementar la traducción polinomial del algoritmo de 3DM a PARTITION.

## Índice de contenidos

- [1. Condificación de la clase 3DM](#1-condificación-de-la-clase-3dm)

- [2. Codificación de la clase Partition](#2-codificación-de-la-clase-partition)

- [3. Clase Translator](#3-clase-translator)

- [4. Clase Main](#4-clase-main)

- [5. Ejecución](#5-ejecución)

- [6. Referencias](#6-Referencias)

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
private uint sizeWXY_;
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
public Partition(ulong[] numberList) {
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

* Obtención de los parámetros $k$, $q$, $p$: en primer lugar, se almacenan en variables los parámetros necesarios para realizar la transformación, que son las cardinalidades de los conjuntos $M$, $W$, $X$, $Y$, y el número de bits necesarios para representar cada elemento de las tripletas, que se calcula con la fórmula $p = Ceiling(log_2{k + 1})$.

```cs
// nº de tripletas de M (k)
uint sizeM = original_problem.GetMSize(); 
// nº bits por elemento (p)
double numberOfBits = Math.Ceiling(Math.Log(sizeM + 1.0f, 2f));
// cardinalidad de los conjuntos W, X e Y (q)
uint sizeWXY = original_problem.GetWXYSize(); 
```

* Cálculo de los $s(a)$ y su sumatorio: seguidamente, se crea una lista de números vacía, que contendrá los elementos del conjunto final $A$ del Partition, y la variable "sum", que almacena la suma de los $s(a)$. Después, para cada elemento de cada tripleta de $M$, se calcula qué posición en el número binario ocupa y obtener un desplazamiento. Así, se obtiene el número binario que representa cada tripleta, se añade al conjunto $$A$$ y se suma al total calculado.

```cs
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
```

* Cálculo de $B$: en este caso, $B$ se calcula creando el número binario que resultaría de sumar los de un conjunto de tripletas que constituyeran un matching, es decir, un número binario que tiene un 1 en todos los grupos que representan un elemento.

```cs
// Cálculo de B
ulong matchingChecker = 0;

for (int currentSet = 2; currentSet >= 0; currentSet--)  {
  for (int currentElement = (int)sizeWXY - 1; currentElement >= 0; currentElement--) 
  {
    int shift = (int)(((currentSet * numberOfBits * 3) + ((2 - currentElement) * numberOfBits)));
    matchingChecker |= one << shift;
  }
}
```

* Cálculo de $b1$ y $b2$: se calculan $b1$ y $b2$ con las fórmulas $b1 = 2 · sum - B$ y $b2 = sum + B$.

```cs
// Cálculo de b1 y b2
ulong b1 = 2 * sum - matchingChecker;
ulong b2 = sum + matchingChecker;
```

* Creación del Partition: se añaden los elementos $b1$ y $b2$ al conjunto $A$ para completar la lista, que se utiliza para crear la instancia del Partition devuelta.

```cs
// Creación del Partition
numbers.Add(b1);
numbers.Add(b2);

return new Partition(numbers.ToArray());
```

## 4. Clase Main

Esta será la clase principal de nuestro programa, que implementa una entrada por línea de comandos para poder elegir el fichero de entrada y el fichero de salida. En caso de no especificarlo tomará uno por defecto. Este instancia una clase 3DM a la cual le pasa el fichero de entrada, luego instanciamos una clase Partition que igualamos a una instancia Translator, a la cual le pasamos la instancia de 3DM. Por último, llamamos al método WriteToFile de la clase Partition, pasándole el fichero de salida.

```csharp
_3DM instance3DM = new _3DM(inputFilePath);
instance3DM.Print();
Partition instancePartition = Translator.Translate3DMToPartition(instance3DM);
instancePartition.WriteToFile(outputFilePath);
```

## 5. Ejecución

El código se puede ejecutar de la siguiente forma:

```bash
dotnet run <input.json> [output.json]
```

## 6. Referencias

* [NP-completeness Proff for 3DM, VC, CLIQUE, HC and PARTITION](https://engineering.purdue.edu/kak/ComputabilityComplexityLanguages/Lecture30.pdf)
