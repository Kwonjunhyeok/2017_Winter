using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using xaml_list.final_price;
using xaml_list.List;
using System.Diagnostics;

//using WebService.PublicTicker_Variable;

namespace xaml_list
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        long Time;
        static string[] Coins = new string[6] { "btc_krw", "bch_krw", "btg_krw", "eth_krw", "etc_krw", "xrp_krw", };
        static string[] Coin_List = new string[6] { "BITCOIN", "BITCOIN_CASH", "BITCOIN_COLD",
                                                "ETHEREUM","ETHEREUM_CLASS", "REPLE",};

        private List<Investment> rate = new List<Investment>();
        private List<Price_List> Price = new List<Price_List>();

        public MainPage()
        {
            this.InitializeComponent();

            rate.Add(new Investment { Sname = "모네로 ->", Hstock = 530000, Smoney = 70000, Sper = 32, Goal = 800000 });
            rate.Add(new Investment { Sname = "리플 ->", Hstock = 4300, Smoney = 3800, Sper = -13, Goal = 4500 });
            rate.Add(new Investment { Sname = "스팀 ->", Hstock = 2200, Smoney = 5000, Sper = 25, Goal = 15000 });

            ListInvest_Money.ItemsSource = rate;
            Storage.ItemsSource = rate;

            init_Sell_Buy();
            getListData();

        }


        private void PtrBox_RefreshInvoked(DependencyObject sender, object args)
        {

        }



        // json 처리
        async void getListData()
        {
            while (true)
            {
                
                for (int i = 0; i < 6; i++)
                {
                    string url = "https://api.korbit.co.kr/v1/ticker/detailed?currency_pair=" + Coins[i];


                    HttpClient client = new HttpClient();

                    string response = await client.GetStringAsync(url);
                    // json 데이터 가져오기
                    var data = JsonConvert.DeserializeObject<Rootobject>(response);

                    Debug.WriteLine("Log : " + data.last);
                    Time = long.Parse(data.timestamp.ToString());

                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Time);
                    DateTime dateTime = dateTimeOffset.UtcDateTime.ToLocalTime();

                    Time_Block.Text = dateTime.ToString();

                    
                    Price.Add(new Price_List
                    {
                        Coin_Name = Coin_List[i],
                        Last = data.last,
                        Bid = data.bid,
                        Ask = data.ask,
                        High = data.high,
                        Low = data.low,
                        Change = data.change,
                        ChangePercent = data.changePercent,
                        Volume = data.volume
                    });
                }
                //ListInvest_Chart 에  Pirce 값을 넣음
                ListInvest_Chart.ItemsSource = Price;
                Storage.ItemsSource = Price;


                await Task.Delay(5000);
                
            }
        }

        async Task<string> json_last(int i)
        {
            string url = "https://api.korbit.co.kr/v1/ticker/detailed?currency_pair=" + Coins[i];
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Rootobject>(response);

            return data.last;
        }

        async void init_Sell_Buy()
        {
            string data = await json_last(0);

            B_O.Text = data.ToString();
            S_O.Text = data.ToString();
        }



        string B_ORinput = "";
        double B_ORnumber;

        //매수 텍스트 예외처리
        private void B_OR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(B_OR.Text == "")
                {
                    B_ORnumber = 0;
                }
                else
                {
                    B_ORnumber = Convert.ToDouble(B_OR.Text); //TEXTBOX
                }
            }
            catch
            {
                B_OR.Text = B_ORinput;
                B_OR.Select(B_OR.Text.Length, 0);
            }
        }

        string S_ORinput = "";
        double S_ORnumber;

        //매도 텍스트 예외처리
        private void S_OR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (S_OR.Text == "")
                {
                    S_ORnumber = 0;
                }
                else
                {
                    S_ORnumber = Convert.ToDouble(S_OR.Text);
                }
            }
            catch
            {
                S_OR.Text = S_ORinput;
                S_OR.Select(B_OR.Text.Length, 0);
            }
        }

        string Deposit_Input = "";
        int Deposit_Number;

        //입금 텍스트 예외처리
        private void Deposit_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(Deposit.Text == "")
                {
                    Deposit_Number = 0;
                }
                else
                {
                    Deposit_Number = Convert.ToInt32(Deposit.Text);
                }
            }
            catch
            {
                Deposit.Text = Deposit_Input;
                Deposit.Select(Deposit.Text.Length, 0);
            }
        }


        string Withdraw_Input = "";
        int Withdraw_Number;

        //출금 텍스트 예외처리
        private void Withdraw_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Withdraw.Text == "")
                {
                    Withdraw_Number = 0;
                }
                else
                {
                    Withdraw_Number = Convert.ToInt32(Withdraw.Text);
                }
            }
                catch
            {
                Withdraw.Text = Withdraw_Input;
                Withdraw.Select(Withdraw.Text.Length, 0);
            }
        }


        int Total; // 총 자산
        int Won; // 원화
        int Dp;
        

        // 입금
        private void Bdp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Total = int.Parse(Total_Text.Text.ToString());
            Won = int.Parse(K_Money.Text.ToString());
            Dp = int.Parse(Deposit.Text.ToString());

            Total += Dp;
            Won += Dp;

            Total_Text.Text = Total.ToString();
            K_Money.Text = Won.ToString();
            Deposit.Text = "";
        }

        int Wdrwa;

        //출금
        private void Wd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Total = int.Parse(Total_Text.Text.ToString());
            Won = int.Parse(K_Money.Text.ToString());
            Wdrwa = int.Parse(Withdraw.Text.ToString());

            Total -= Wdrwa;
            Won -= Wdrwa;

            Total_Text.Text = Total.ToString();
            K_Money.Text = Won.ToString();
            Withdraw.Text = "";
        }


        private async void B_Btc_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(0);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "BITCOIN - BTC";
        }

        private async void B_Btg_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(1);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "BITCOINGOLD-BTG";
        }

        private async void B_Bth_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(2);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "BITCOINCASH-BTH";
        }

        private async void B_Eth_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(3);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "ETHEREUM-ETH";
        }

        private async void B_Etc_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(4);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "ETHEREUM_CLASS-ETC";
        }

        private async void B_Xrp_Click(object sender, RoutedEventArgs e)
        {
            string data = await json_last(5);
            B_O.Text = data.ToString();
            Buy_Btc.Content = "REPLE-XRP";
        }

       

    }
}
