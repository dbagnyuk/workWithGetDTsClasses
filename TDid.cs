using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workWithGetDTsClasses
{
    public class TDid
    {
        public long terminalDevice;
        bool haveLookedSrv = false;
        List<SRV> srvList = new List<SRV>();
        public void processInputData(long inputTD, string srvWhole)
        {
            this.terminalDevice = inputTD;

            string[] srvSplited = srvWhole.Split('|');
            foreach (var service in srvSplited)
            {
                SRV oneSRV = new SRV();
                oneSRV.parseService(service);

                srvList.Add(oneSRV);
            }
        }
        public void writeTDtoConsole()
        {
            Console.WriteLine("\t" + this.terminalDevice);
            this.srvList.ForEach(i => i.writeSRVtoConsole());
        }
        public long lookupSRV(string lookedSRV)
        {
            foreach(SRV i in srvList)
            {
                if (i.lookupSRV(lookedSRV) && i.serviceValidity())
                    haveLookedSrv = true;
            }

            if (this.haveLookedSrv)
                return this.terminalDevice;
            else return 0;
        }
        public void sortSrvList(string sortSRVsType)
        {
            if (sortSRVsType == "asc")
                srvList = srvList.OrderBy(p => p.srvName).ToList();
            else if (sortSRVsType == "desc")
                srvList = srvList.OrderByDescending(p => p.srvName).ToList();
        }
    }
}
