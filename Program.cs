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
            long inputedPA = 277300065848;
            string lookedSrv = "TEST666";
            string sortTDsType = "asc";
            string sortSRVsType = "asc";

            ScaClient scaClient = new ScaClient("ConfigurationService_ISca", new EndpointAddress("http://msk-dev-foris:8106/SCA"));
            var scaOutput = scaClient.GetTDs(new GetTDsInput() { PANumber = inputedPA });

            PAid searchedPA = new PAid(inputedPA, scaOutput);

            searchedPA.lookupSRV(lookedSrv);
            searchedPA.sortTDsList(sortTDsType,sortSRVsType);
            searchedPA.writePAtoConsole();
            searchedPA.writeTDwithSRVtoConsole(lookedSrv);

            Console.Write("\nPress Enter for exit...");
            Console.Read();
        }
    }
}
