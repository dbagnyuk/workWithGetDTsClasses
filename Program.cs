#define FIRST
//#define SECOND
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ServiceModel;
using workWithGetDTsClasses.ISca;

namespace workWithGetDTsClasses
{
    class Program
    {
        static void Main(string[] args)
        {
#if FIRST
            if (args.Length == 0 || args.Length > 4)
            {
                exitError();
                System.Environment.Exit(1);
            }

            if (args.Length == 1 && args[0] == "/?")
            {
                writeAppHelp();
                System.Environment.Exit(1);
            }

            long inputedPA = 0;
            string lookedSrv = null;
            string sortTDsType = "asc";
            string sortSRVsType = "asc";
            int sortIdx = 1;

            for(int i = 0; i < args.Length; i++)
            {
                if (i == 0 && args[i].Length != 12)
                {
                    exitError();
                    System.Environment.Exit(1);
                }
                else if (i == 0 && !long.TryParse(args[0], out inputedPA))
                {
                    exitError();
                    System.Environment.Exit(1);
                }

                if (i > 0 && !string.Equals(args[i].ToLower(), "asc") && !string.Equals(args[i].ToLower(), "desc") && Regex.IsMatch(args[i], @"^[a-zA-Z0-9]+$"))
                        lookedSrv = Convert.ToString(args[i]).ToUpper();

                if (i > 0 && (string.Equals(args[i].ToLower(), "asc") || string.Equals(args[i].ToLower(), "desc")) && sortIdx++ == 1)
                        sortTDsType = Convert.ToString(args[i].ToLower());

                if (i > 0 && (string.Equals(args[i].ToLower(), "asc") || string.Equals(args[i].ToLower(), "desc")) && sortIdx == 2)
                    sortSRVsType = Convert.ToString(args[i].ToLower());
            }

            Console.Clear();
#endif
#if SECOND
            long inputedPA = 277300065848;
            string lookedSrv = "TEST666";
            string sortTDsType = "asc";
            string sortSRVsType = "asc";
#endif
            ScaClient scaClient = new ScaClient("ConfigurationService_ISca", new EndpointAddress("http://msk-dev-foris:8106/SCA"));
            var scaOutput = scaClient.GetTDs(new GetTDsInput() { PANumber = inputedPA });

            PAid searchedPA = new PAid();

            searchedPA.processInputData(inputedPA, scaOutput);
            searchedPA.lookupSRV(lookedSrv);
            searchedPA.sortTDsList(sortTDsType,sortSRVsType);
            searchedPA.writePAtoConsole();
            searchedPA.writeTDwithSRVtoConsole(lookedSrv);

            Console.Write("\nPress Enter for exit...");
            Console.Read();
        }
        static void exitError()
        {
            Console.WriteLine("\nWrong input, please use 'workWithGetDTsClasses.exe /?' for help.");
        }
        static void writeAppHelp()
        {
            Console.WriteLine("\nUsage:\n\tworkWithGetDTsClasses.exe PA [Srv] [TDs sort] [Srvs sort]");
            Console.WriteLine("Input:\n\tPA - Personal Account.");
            Console.WriteLine("Optional:\n\tSrv       - Service which you looking for.");
            Console.WriteLine("\tTDs sort  - {Asc|Desc} sorting for Terminal Devices.");
            Console.WriteLine("\tSrvs sort - {Asc|Desc} sorting for Services.");
            Console.WriteLine("\n\t/? - for this help.");
        }
    }
}
