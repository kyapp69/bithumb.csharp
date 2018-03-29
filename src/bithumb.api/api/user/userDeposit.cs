namespace XCT.BaseLib.API.Bithumb.User
{
    /// <summary>
    /// bithumb 회원 btc 출금(회원등급에 따른 BTC 출금)
    /// </summary>
    public class UserKrwDeposit : BApiResult
    {
        /// <summary>
        /// 가상계좌번호
        /// </summary>
        public string account
        {
            get;
            set;
        }

        /// <summary>
        /// 신한은행(은행명)
        /// </summary>
        public string bank
        {
            get;
            set;
        }

        /// <summary>
        /// 비티씨코리아닷컴(입금자명)
        /// </summary>
        public string bankuser
        {
            get;
            set;
        }
    }

    /// <summary>
    /// bithumb 거래소 회원 지갑 정보
    /// </summary>
    public class UserDepositAddress
    {
        /// <summary>
        /// 전자지갑 Address
        /// </summary>
        public string wallet_address
        {
            get;
            set;
        }

        /// <summary>
        /// BTC, ETH, DASH, LTC, ETC, XRP
        /// </summary>
        public string currency
        {
            get;
            set;
        }
    }

    /// <summary>
    /// bithumb 거래소 회원 지갑 정보
    /// </summary>
    public class UserDeposit : ApiResult<UserDepositAddress>
    {
        public UserDeposit()
        {
            this.data = new UserDepositAddress();
        }
    }
}