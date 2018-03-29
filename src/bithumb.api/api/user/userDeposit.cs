namespace XCT.BaseLib.API.Bithumb.User
{
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