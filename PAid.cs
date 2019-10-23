using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using workWithGetDTsClasses.ISca;

namespace workWithGetDTsClasses
{
    public class PAid
    {
        long personalAccount;
        List<TDid> tdsList = new List<TDid>();
        List<long> tdcWithLookedSrv = new List<long>();
        public void processInputData(long inputPA, GetTDsOutput inputData)
        {
            this.personalAccount = inputPA;

            foreach (var tdcs in inputData.TDs)
            {
                TDid oneTDid = new TDid();
                oneTDid.processInputData(tdcs.TdId, tdcs.Services);

                tdsList.Add(oneTDid);
            }
        }
        public void writePAtoConsole()
        {
            Console.WriteLine($"TDIds with Services for PA {this.personalAccount}:\n");
            //Console.WriteLine(this.personalAccount + "\n");
            this.tdsList.ForEach(i => i.writeTDtoConsole());
        }
        public void writeTDwithSRVtoConsole(string lookedSrv)
        {
            if (!string.IsNullOrEmpty(lookedSrv))
            {
                Console.WriteLine($"\n\nThe TDs which have the valid service {lookedSrv}:");
                tdcWithLookedSrv.ForEach(i => Console.Write("{0}\n", i));
            }
        }
        public void lookupSRV(string lookedSrv)
        {
            if (!string.IsNullOrEmpty(lookedSrv))
            {
                foreach (TDid i in tdsList)
                {
                    if (i.lookupSRV(lookedSrv) != 0)
                        tdcWithLookedSrv.Add(i.lookupSRV(lookedSrv));
                }
            }
        }
        public void sortTDsList(string sortTDsType, string sortSRVsType)
        {
            if (sortTDsType == "asc")
            {
                tdsList = tdsList.OrderBy(p => p.terminalDevice).ToList();
                tdcWithLookedSrv = tdcWithLookedSrv.OrderBy(p => p).ToList();
                this.tdsList.ForEach(i => i.sortSrvList(sortSRVsType));
            }
            else if (sortTDsType == "desc")
            {
                tdsList = tdsList.OrderByDescending(p => p.terminalDevice).ToList();
                tdcWithLookedSrv = tdcWithLookedSrv.OrderByDescending(p => p).ToList();
                this.tdsList.ForEach(i => i.sortSrvList(sortSRVsType));
            }
        }
    }
}
