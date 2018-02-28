using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using Windows.UI.Xaml.Media;

namespace xaml_list.final_price
{
    public class Rootobject
    {
        private static Dictionary<Coins, string> CoinnameDictionary = new Dictionary<Coins, string>()
        {
            [Coins.btc_krw] = "BITCOIN",
            [Coins.bch_krw] = "BITCOIN_CASH",
            [Coins.btg_krw] = "BITCOIN_GOLD",
            [Coins.eth_krw] = "ETHEREUM",
            [Coins.etc_krw] = "ETHEREUM_CLASS",
            [Coins.xrp_krw] = "REPLE",
        };

        public Coins Coin { get; set; } 
        
        public string CoinName
        {
            get
            {
                return CoinnameDictionary[Coin];
            }
        }
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
                
        public DateTime TimeStampFormatting
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                DateTime dateTime = dateTimeOffset.UtcDateTime.ToLocalTime();

                return dateTime;
            }
        }

        //public SolidColorBrush Color

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Coin == (obj as Rootobject).Coin;
        }

        public override int GetHashCode()
        {
            return Coin.GetHashCode();
        }
    }
}


