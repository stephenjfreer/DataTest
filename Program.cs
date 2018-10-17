using System;
using FileData;

namespace FileData
{
    public static class Program
    {
        private static bool _getFileVersion = false;
        private static bool _getFileSize = false;
        private static string _filename = "";

        
        public static void Main(string[] args)
        {
            _getFileVersion = false;
            _getFileSize = false;
            _filename = "";
            bool suceessful = true;

            try
            {
                if (ParseArgs(args))
                {
                    FileDetails fd = new FileDetails();
                    if (_getFileSize)
                    {
                        int filesize = fd.Size(_filename);
                        Console.WriteLine(String.Format("The file {0} has a size of {1}",
                            _filename, filesize.ToString()));
                    }
                    else if (_getFileVersion)
                    {
                        string version = fd.Version(_filename);
                        Console.WriteLine(String.Format("The file {0} has the version {1}",
                            _filename, version.ToString()));

                    }
                }
                else
                {
                    Console.WriteLine("Error parsing parameters");
                    suceessful = false;
                }
            }
            catch (ArgumentException exarg)
            {
                Console.WriteLine(String.Format("Error passing parametsr due to {0}", exarg.Message));
                suceessful = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Unhandled exeption due to {0}", ex.Message));
                suceessful = false;
            }

            if (suceessful)
            {
                Console.WriteLine("Program completed successfully");                
            }
            Console.WriteLine("Press any ley to exit the program");
            Console.Read();

        }


        /// <summary>
        /// Parse the two arguments to the program, if there are 2
        /// expected p\r\meters are:
        /// First parameter
        ///     –v, --v, /v, --version 
        ///  or
        ///      –s, --s, /s, --size
        /// second paraeter:
        ///     The filename of the file
        /// </summary>
        /// <remarks>
        /// Updates the members Filename, 
        /// </remarks>
        /// <param name="args">Parameters as am array of strings</param>
        /// <returns>the status of decoding the parameters</returns>
        private static bool ParseArgs(string[] args)
        {
            bool status = true;

            // mmke sure we have what we expected as parameters
            if (args != null && args.GetLength(0) == 2)
            {
                // Store the filename
                _filename = args[1];

                // test for either --version or --size
                if (args[0].StartsWith("--"))
                {
                    if (String.Compare(args[0].Substring(2), "version", true) == 0)
                    {
                        _getFileVersion = true;
                    }
                    else if (String.Compare(args[0].Substring(2), "size", true) == 0)
                    {
                        _getFileSize = true;
                    }
                }
                // test for -v -and -s
                else if (args[0].Substring(0).StartsWith("-"))
                {
                    if (String.Compare(args[0].Substring(1), "s", true) == 0)
                    {
                        _getFileSize = true;
                    }
                    else if (String.Compare(args[0].Substring(1), "v", true) == 0)
                    {
                        _getFileVersion = true;
                    }
                }
                // test for /s or /v
                else if (args[0].StartsWith("//"))
                {

                    if (String.Compare(args[0].Substring(2), "v", true) == 0)
                    {
                        _getFileVersion = true;
                    }


                    if (String.Compare(args[0].Substring(2), "s", true) == 0)
                    {
                        _getFileSize = true;
                    }

                }
                else
                {
                    // no matches
                    ArgumentException argEx = new ArgumentException("The arguments are invalid");
                    throw argEx;
                }
            }
            else
            {
                status = false;
            }
            

            return status;
        }
    }
}
