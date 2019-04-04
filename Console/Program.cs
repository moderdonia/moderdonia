﻿using MiniSQLEngine;
using System;
using System.Diagnostics;
using System.IO;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {
            DB db = new DB("db1");
            bool bucle = true;
            string linea;

            string fileName = @"..\..\..\Archivos\";
            string[] nombres = Directory.GetFiles(fileName);
            int i = 0;
            while (i < nombres.Length)
            {
                Console.WriteLine(nombres[i]);
                Console.WriteLine(nombres[i].Length);
                int aux = nombres[i].Length-4;
                Console.WriteLine(aux);
                string nom = nombres[i].Substring(18);
                nom = nom.Replace(".txt","");
                Console.WriteLine(nom);
                
                i++;
                
            }
            

            while (bucle)
            {
                Console.WriteLine("Inserte sentencia");
                linea = Console.ReadLine();
                if (linea == "exit")
                {
                    bucle = false;
                }
                else
                {
                    //El tiempo que tarda la sentencia
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string output = db.runQuery(linea) + "(";
                    output += sw.Elapsed.TotalMilliseconds + ")";
                    Console.WriteLine(output);
                    sw.Stop();
                    
                }
            }

            /*
                Console.WriteLine(abc[0]);
                val = Console.ReadLine();
            
          
                System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\..\Archivos\TesterInput-example.txt");
            
                while (linea != null )
                {
                    linea = file.ReadLine();
                    if (linea != "" && linea != null)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        string output = db.runQuery(linea) + "(";
                        output += sw.Elapsed.TotalMilliseconds + ")";
                        Console.WriteLine(output);
                        sw.Stop();
                    }
                }
                Console.WriteLine("Querys Finished");
            */    
    }
        
    }
}