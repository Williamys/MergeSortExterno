using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MergeSort
{
    class OrdenarPartes
    {        
        public void OrdenandoPartes()
        {                        
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Ordenando as partes");
            
            foreach (string caminho in Directory.GetFiles("C:\\teste\\", "parte*.dat"))
            {
                //Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), caminho);         
                string[] matriz = File.ReadAllLines(caminho); // copio todo conteudo do txt pra dentro da matriz
                int[] numeros = matriz.Select(x => int.Parse(x)).ToArray(); // converto para inteiros a matriz de strings        
                matriz = null;
                GC.Collect(); //libero da memoria.
                Array.Sort(numeros); // metodo que ordena os numeros.                
                using (StreamWriter outputFile = new StreamWriter(caminho))
                {
                    foreach (int line in numeros)
                        outputFile.WriteLine(line); //escrevo os números ordenados.
                }
                Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), caminho + "  Ordenada.");                
                numeros = null;                
                GC.Collect(); //libero da memoria.
            }            
        }
        /*public void OrdenandoPartes()
        {
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Ordenando as partes");
            foreach (string caminho in Directory.GetFiles("C:\\teste\\", "parte*.dat"))
            {                
                //W(caminho); //Console.WriteLine("{0}     \r", caminho);
                Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), caminho);
                //Leia todas as linhas em uma matriz
                string[] matriz = File.ReadAllLines(caminho);
                //List <string> lista = ( File.ReadAllLines(caminho));                        
                // Classificar a matriz na memória
                Array.Sort(matriz);                
                // Criar o nome do arquivo 'ordenado'
                string newcaminho = caminho.Replace("parte", "ordenado");
                // escrever no novo arquivo ordenado
                File.WriteAllLines(newcaminho, matriz);
                // Excluir as partes
                File.Delete(caminho);
                // Libera matriz da memoria
                matriz = null;
                GC.Collect();
            }
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Ordenação das partes completa!!");
        }*/        
    }
}
