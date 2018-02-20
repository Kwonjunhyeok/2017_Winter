namespace xaml_list.final_price
{
    public class PublicTicker_Variable
    {

        //코인 이름
        public string timestamp
        {
            get; set;
        }

        //최종 체결 가격
        public int last
        {
            get; set;
        }

        //최우선 매수호가
        public int bid
        {
            get; set;
        }

        //최우선 매도호가
        public int ask
        {
            get; set;
        }

        //최근 24시간 동안의 체결 가격 중의 가장 낮은 가격
        public int low
        {
            get; set;
        }

        //최근 24시간 동안의 체결 가격 중 가장 높은 가격
        public int high
        {
            get; set;
        }

        //거래량
        public long volume
        {
            get; set;
        }

        //변동 가격
        public int change
        {
            get; set;
        }

        //변동율
        public long changePercent
        {
            get; set;
        }

    }
}