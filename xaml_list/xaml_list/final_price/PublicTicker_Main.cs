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
        public string CoinName { get; set; }

        //현재 시간
        public long timestamp { get; set; }

        //체결가
        public int last { get; set; }

        //매도가
        public int bid { get; set; }

        //매수가
        public int ask { get; set; }

        //최저가
        public int low { get; set; }

        //최고가
        public int high { get; set; }

        //거래량
        public string volume { get; set; }

        //변동가
        public int change { get; set; }

        //변동률
        public float changePercent { get; set; }

        //시간 formating
        public DateTime TimeStampFormatting
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                DateTime dateTime = dateTimeOffset.UtcDateTime.ToLocalTime();

                return dateTime;
            }
        }
    }
}


