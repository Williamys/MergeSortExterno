using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MergeSort
{
    class UnindoPartes
    {
        public void MergePartes()
        {
            Stopwatch time = new Stopwatch();
            
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Unindo");            
            string[] caminho = Directory.GetFiles("C:\\teste\\", "parte*.dat");
            int partes = caminho.Length; // Número de partes
            int tamRegistro = 100; // tamanho de registro
            int registros = 10000000; // Número total registros 
            //int registros = 20000900; // Número total registros
            int usomaximo = 500000000; // uso de memoria maxima
            int tambuffer = usomaximo / partes; // tamanho em bytes de cada buffer
            double sobrecargaRegistros = 7.5; // A sobrecarga de usar filas <>
            int bufferlen = (int)(tambuffer / tamRegistro / sobrecargaRegistros); // Número de registros em cada buffer

            // abrir os arquivos
            StreamReader[] readers = new StreamReader[partes];
            for (int i = 0; i < partes; i++)
                readers[i] = new StreamReader(caminho[i]);

            // cria as filas           
            Queue<int>[] queues = new Queue<int>[partes];
            for (int i = 0; i < partes; i++)
                queues[i] = new Queue<int>(bufferlen);
            //carrega as filas
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Carregando as filas");
            for (int i = 0; i < partes; i++)
                LoadQueue(queues[i], readers[i], bufferlen);
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Filas carregadas completas");

            // unindo os arquivos ordenados
            String arquivoOrdenado = "C:\\teste\\ArquivoOrdenado.dat";
            if (!System.IO.File.Exists(arquivoOrdenado))
                System.IO.File.Create(arquivoOrdenado).Close();            
            StreamWriter sw = new StreamWriter(arquivoOrdenado);
            bool fim = false;
            int menorIndice, j, progresso = 0;            
            int menorValor = 0;
            while (!fim)
            {
                // Relatório de progresso
                if (++progresso % 5000 == 0)
                    Console.Write("{0:f2}%   \r",
                      100.0 * progresso / registros);

                // Encontre a parte com o valor mais baixo
                menorIndice = -1;
                menorValor = 0;                
                for (j = 0; j < partes; j++)
                {                    
                    if (queues[j] != null)
                    {
                        // comparo qual menor valor das filas
                        if (menorIndice < 0 || queues[j].Peek() < menorValor) 
                        {
                            menorIndice = j;
                            menorValor = queues[j].Peek();                  
                        }
                    }
                }
                // Não foi encontrado nada em qualquer fila? break.
                if (menorIndice == -1) { fim = true; break; }
                // escrevo menor valor
                sw.WriteLine(menorValor);
                // Remover da fila
                queues[menorIndice].Dequeue();
                // carrega novamente a fila depois de vazia.
                if (queues[menorIndice].Count == 0)
                {
                    LoadQueue(queues[menorIndice], readers[menorIndice], bufferlen);                    
                    if (queues[menorIndice].Count == 0)
                    {
                        queues[menorIndice] = null;
                    }
                }
            }
            sw.Close();
            // Fechar e excluir os arquivos
            for (int i = 0; i < partes; i++)
            {
                readers[i].Close();
                File.Delete(caminho[i]);
            }            
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Completo");
        }
        /// Carregar de um até número de registos em uma fila
        static void LoadQueue(Queue<int> queue, StreamReader file, int registros)        
        {
            for (int i = 0; i < registros; i++)
            {
                if (file.Peek() < 0) break;
                queue.Enqueue(Convert.ToInt32(file.ReadLine()));                
            }
        }
    }
}
