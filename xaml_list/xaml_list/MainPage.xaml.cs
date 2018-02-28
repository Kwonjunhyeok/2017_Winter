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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace xaml_list
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
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
                    B_OR.Text = "0";
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
                    S_OR.Text = "0";
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
                    Deposit.Text = "0";
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
                    Withdraw.Text = "0";
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
    }
}
