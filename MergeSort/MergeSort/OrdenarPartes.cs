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
                //Array.Sort(numeros); // metodo que ordena os numeros.
                SortMerg
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

        static public void MainMerge(int[] numbers, int left, int mid, int right)
        {

            int[] temp = new int[25];

            int i, eol, num, pos;



            eol = (mid - 1);

            pos = left;

            num = (right - left + 1);



            while ((left <= eol) && (mid <= right))
            {

                if (numbers[left] <= numbers[mid])

                    temp[pos++] = numbers[left++];

                else

                    temp[pos++] = numbers[mid++];

            }



            while (left <= eol)

                temp[pos++] = numbers[left++];



            while (mid <= right)

                temp[pos++] = numbers[mid++];



            for (i = 0; i < num; i++)
            {

                numbers[right] = temp[right];

                right--;

            }

        }



        static public void SortMerge(int[] numbers, int left, int right)
        {

            int mid;



            if (right > left)
            {

                mid = (right + left) / 2;

                SortMerge(numbers, left, mid);

                SortMerge(numbers, (mid + 1), right);



                MainMerge(numbers, left, (mid + 1), right);

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
