using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MergeSort
{
    class CriarArquivo
    {
        public void Criar(string arquivo, Int32 tam)
        {            
            Random rnd = new Random();                        
            int leia_linha = 0;
            string pasta = "C:\\teste";
            Console.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), "Criando Arquivo");            
            try
            {
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);
                if (!File.Exists(arquivo))
                    File.Create(arquivo).Close();
                else                    
                    using(FileStream fs = File.Create(arquivo))                    
                    {
                        StreamWriter sw = new StreamWriter(fs);                        
                        {                    
                            for (int i = 0; i < tam; i++)
                            {
                                // Porcentagem do progresso da divisão
                                if (++leia_linha % 5000 == 0)
                                    Console.Write("{0:f2}%   \r",                                    
                                       100.0 * i / tam);                                
                                sw.WriteLine(rnd.Next(Int32.MaxValue));
                        }
                    }                        
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
