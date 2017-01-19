using PNCEngine.Utils;
using System;
using System.IO;

namespace Zippit
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = string.Empty, destination = string.Empty;

            if (args.Length > 1)
            {
                source = args[0];
                destination = args[1];
            }
            else if (args.Length == 1)
            {
                source = args[0];
                destination = Path.GetDirectoryName(source) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(source) + ".bin";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Arguments required to run.\n -arg1: path to sourcefile\n -arg2: path to destinationfile\nIf only one argument is given, the file will have the same name with a *.bin-extension.");
                Console.ReadLine();
                return;
            }

            if (!File.Exists(source))
            {
                Console.Write("File {0} does not exist.", source);
                Console.ReadLine();
                return;
            }

            if (File.Exists(destination))
                File.Delete(destination);

            File.WriteAllBytes(destination, ZipCompressor.Zip(File.ReadAllText(source)));
            Console.WriteLine("{0} zipped as {1}", source, destination);
            Console.ReadLine();
        }
    }
}
