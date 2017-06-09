using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Console;

namespace Traingle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filePath = @"D:\triangle.txt";
            using (CalculateDiagonal calc = new CalculateDiagonal(filePath))
            {
                WriteLine(calc.Calculate());
                Read();
            }
                           
        }
    }

    public class CalculateDiagonal : IDisposable
    {
        public List<string> allLines { get; set; }
        public List<string> lstOfnumbers {get;set;}

        public int Index { get; set; }

        public string FilePath { get; set; }

        public int TotalSum { get; set; }

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public CalculateDiagonal(string filePath)
        {
            allLines = new List<string>();
            lstOfnumbers = new List<string>();
            Index = 0;
            TotalSum = 0;
            FilePath = filePath;
        }

        public string Calculate()
        {
            allLines = File.ReadAllLines(FilePath).ToList();

            allLines.ForEach(

                (line) =>
                {
                    lstOfnumbers = line.Split(' ').ToList();
                    if (allLines.IndexOf(line) == Index && lstOfnumbers.Count > 2)
                    {
                        TotalSum += Convert.ToInt32(lstOfnumbers[Index - 1]);
                    }
                    else
                    {
                        TotalSum += Convert.ToInt32(lstOfnumbers[0]);
                    }
                    Index++;
                }

                );
            return TotalSum.ToString();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                allLines = null;
                lstOfnumbers = null;
                FilePath = null;
                handle.Dispose();
            }
            disposed = true;
        }
    }
}
