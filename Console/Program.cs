﻿using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {

            string line;
            string line2;
            string line3;
            List<string> dbList = new List<string>();

            Console.WriteLine("What database you wanna open bro?");
            line = Console.ReadLine();

            Console.WriteLine("What's your name mate?");
            line2 = Console.ReadLine();

            Console.WriteLine("And your password dude?");
            line3 = Console.ReadLine();


            if (!line2.Equals("admin") && !line3.Equals("admin"))
            {
                Console.WriteLine("Error: Not sufficient privileges");

            }
            else if (!dbList.Contains(line))
            {
                new CreateDB(line, line2, line3);
                dbList.Add(line);
                //DB db = new DB(line);

                //bool bucle = true;
                //string linea;
                //no se puede cerrar pulsando la X
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
                using (DB db = new DB(line))
                {
                    bool bucle = true;
                    string linea;

                    string fileName = @"..\..\..\Archivos\";
                    string[] nombres = Directory.GetFiles(fileName);
                    string[] columnas = new string[20];
                    string nombre;
                    int i = 0;

                    //----------Codigo para leer linea
                    StreamReader archivo;
                    string row = null;
                    int k = 0;
                    //--------------------------------
                    while (i < nombres.Length)
                    {
                        nombre = nombres[i];
                        //Console.WriteLine(nombres[i]);
                        //Console.WriteLine(nombres[i].Length);
                        int aux = nombres[i].Length - 4;
                        //Console.WriteLine(aux);
                        string nom = nombres[i].Substring(18);
                        nom = nom.Replace(".txt", "");

                        using (archivo = File.OpenText(nombre))
                        {
                            //Console.WriteLine(nom);
                            //File.Delete(fileName);
                            while (!archivo.EndOfStream)
                            {
                                row = archivo.ReadLine();
                                if (k == 0)
                                {
                                    columnas = row.Split(';');
                                    db.createTable(nom, columnas);
                                    k++;
                                }
                                else
                                {

                                    db.insertData(nom, columnas, row.Split(';'));
                                }
                            }
                            //codigo para lectura con pattern
                            //-------------------------------
                            i++;
                        }
                    }
                    while (bucle)
                    {
                        Console.WriteLine("Inserte sentencia o escriba 'exit' para salir");
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
                    Console.WriteLine("Database created");
                }
            }
            else
            {
                Console.WriteLine("Not sufficient privileges");
            }
        }

            
    



    // Necesario para que no se cierre la ventana de comandos
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        //-------------------------------------------------------------------------------
    }
    
}
