using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using xaml_list.final_price;

namespace xaml_list
{
    public enum Coins
    {
        btc_krw,
        bch_krw,
        btg_krw,
        eth_krw,
        etc_krw,
        xrp_krw
    }

    class MainPageViewModel : ViewModelBase
    {
        public string LiveTime => DateTime.Now.ToString();

        private string BaseUrl => "https://api.korbit.co.kr/v1/ticker/detailed?currency_pair=";

        private ObservableCollection<Rootobject> price = null;
        public ObservableCollection<Rootobject> Price => price = price == null ? new ObservableCollection<Rootobject>() : price;

        private ObservableCollection<Investment> rate = null;
        public ObservableCollection<Investment> Rate => rate = rate == null ? new ObservableCollection<Investment>() : rate;

        private Dictionary<Coins, string> CoinNameDictionary = new Dictionary<Coins, string>()
        {
            [Coins.btc_krw] = "BITCOIN-BTC",
            [Coins.bch_krw] = "BITCOINCASH-BTH",
            [Coins.btg_krw] = "BITCOINGOLD-BTG",
            [Coins.eth_krw] = "ETHEREUM-ETH",
            [Coins.etc_krw] = "ETHEREUM_CLASS-ETC",
            [Coins.xrp_krw] = "REPLE-XRP",
        };

        // Loaded
        public ICommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Rate.Add(new Investment { Sname = "모네로 ->", Hstock = 530000, Smoney = 70000, Sper = 32, Goal = 800000 });
                    Rate.Add(new Investment { Sname = "리플 ->", Hstock = 4300, Smoney = 3800, Sper = -13, Goal = 4500 });
                    Rate.Add(new Investment { Sname = "스팀 ->", Hstock = 2200, Smoney = 5000, Sper = 25, Goal = 15000 });

                    LiveTimer();
                    GetListData();
                });
            }
        }

        // Buying
        public ICommand SelectCoinCommand
        {
            get
            {
                return new RelayCommand<Coins>((coin) => 
                {
                    BuyingLast = Price.First(coinitem => coinitem.Coin == coin).last;
                    SelectedCoin = CoinNameDictionary[coin];
                });
            }
        }

        private string selectedCoin;
        public string SelectedCoin
        {
            set
            {
                if (selectedCoin != value)
                {
                    selectedCoin = value;
                    RaisePropertyChanged();
                }
            }
            get
            {
                return selectedCoin;
            }
        }

        private int buyingLast;
        public int BuyingLast
        {
            set
            {
                if (buyingLast != value)
                {
                    buyingLast = value;
                    RaisePropertyChanged();
                }
            }
            get
            {
                return buyingLast;
            }
        }

        private int sellLast;
        public int SellLast
        {
            set
            {
                sellLast = value;
                if (sellLast != value)
                {
                    RaisePropertyChanged();
                }
            }
            get
            {
                return sellLast;
            }
        }

        private string GetCoinURL(Coins coin)
        {
            return BaseUrl + coin;
        }

        private void LiveTimer()
        {
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) => RaisePropertyChanged(nameof(LiveTime));
            timer.Start();
        }

        // json 처리
        private bool initFirst;
        private void GetListData()
        {
            HttpClient client = new HttpClient();
            Array coins = Enum.GetValues(typeof(Coins));

            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0) };
            timer.Tick += async (sender, e) =>
            {
                // Timer instantly execute handler and then next handler call to every 5 seconds
                if (timer.Interval.Seconds == 0)
                {
                    timer.Stop();
                    timer.Interval = TimeSpan.FromSeconds(5);
                    timer.Start();
                }
                
                foreach (Coins coin in coins)
                {
                    string response = await client.GetStringAsync(GetCoinURL(coin));
                    // json 데이터 가져오기
                    var data = JsonConvert.DeserializeObject<Rootobject>(response);
                    data.Coin = coin;

                    if (Price.Contains(data))
                    {
                        Price[Price.IndexOf(data)] = data;
                    }
                    else
                    {
                        Price.Add(data);
                    }

                    if (!initFirst && coin == Coins.btc_krw)
                    {
                        SelectedCoin = CoinNameDictionary[coin];
                        BuyingLast = data.last;
                        SellLast = data.last;
                        initFirst = true;
                    }
                }
            };
            timer.Start();
        }
    }
}
