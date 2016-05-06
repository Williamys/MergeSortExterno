using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MergeSort
{           
    class Dividir
    {
        public void DividirArquivo(string file, int quantidade)
        {            
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Dividindo");         
            using (var sourceFile = new StreamReader(file))
            {
                var fileCounter = 0;
                var destinationFile = new StreamWriter(string.Format("C:\\teste\\parte{0:d5}.dat", fileCounter + 1));
                try
                {
                    var lineCounter = 0;
                    long leia_linha = 0;

                    string line;
                    while ((line = sourceFile.ReadLine()) != null)
                    {
                        // Porcentagem do progresso da divisão
                        if (++leia_linha % 5000 == 0)
                            Console.Write("{0:f2}%   \r",
                              100.0 * sourceFile.BaseStream.Position / sourceFile.BaseStream.Length);
                        //if (lineCounter >= 20000900) // aproximadamento preenche arquivo 238 mb
                        // condicao de parada para escrever em outro arquivo
                        if (lineCounter >= quantidade)
                        {                            
                            lineCounter = 0;
                            fileCounter++;
                            destinationFile.Dispose(); // ;libero memoria
                            destinationFile = new StreamWriter(string.Format("C:\\teste\\parte{0:d5}.dat", fileCounter + 1));
                        }
                        destinationFile.WriteLine(line);
                        lineCounter++;
                    }
                }
                finally
                {                    
                    destinationFile.Dispose();                                        
                }
            }
        }
    }
}
