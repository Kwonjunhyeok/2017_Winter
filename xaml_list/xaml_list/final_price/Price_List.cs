using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xaml_list.final_price
{
    class Price_List
    {
        //코인 이름
        public string Coin_Name { get; set; }

        //체결가
        public string Last { get; set; }

        //매도가
        public string Bid { get; set; }

        //매수가
        public string Ask { get; set; }

        //최저가
        public string Low { get; set; }

        //최고가
        public string High { get; set; }

        //거래량
        public string Volume { get; set; }

        //변동가
        public string Change { get; set; }

        //변동률
        public string ChangePercent { get; set; }
    }
}
