using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

namespace xaml_list.final_price
{
    public class Rootobject
    {
        //현재 시간
        public long timestamp { get; set; }

        //체결가
        public string last { get; set; }

        //매도가
        public string bid { get; set; }

        //매수가
        public string ask { get; set; }

        //최저가
        public string low { get; set; }

        //최고가
        public string high { get; set; }

        //거래량
        public string volume { get; set; }

        //변동가
        public string change { get; set; }

        //변동률
        public string changePercent { get; set; }
    }
}


