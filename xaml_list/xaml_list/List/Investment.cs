namespace xaml_list
{
    public class Investment
    {
        //내가 산 코인 매도 금액
        public int Hstock { set; get; }
        //내가 산 코인 이름
        public string Sname { set; get; }
        //지금 코인 돈 현황
        public int Smoney { set; get; }
        //몇 퍼센트 올랏나 내려갓나 표시
        public int Sper{set; get;}

        public int Goal { set; get; }
    }
}