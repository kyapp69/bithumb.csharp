using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCT.BaseLib.API.Bithumb.User
{
    /// <summary>
    /// https://api.bithumb.com/
    /// </summary>
    public class BUserApi
    {
        private string __connect_key;
        private string __secret_key;

        /// <summary>
        /// 
        /// </summary>
        public BUserApi(string connect_key, string secret_key)
        {
            __connect_key = connect_key;
            __secret_key = secret_key;
        }

        private BithumbClient __user_client = null;

        private BithumbClient UserClient
        {
            get
            {
                if (__user_client == null)
                    __user_client = new BithumbClient(__connect_key, __secret_key);
                return __user_client;
            }
        }

        /// <summary>
        /// bithumb 거래소 회원 정보
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <returns></returns>
        public async Task<UserAccount> Account(string currency)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
            }

            return await UserClient.CallApiPostAsync<UserAccount>("/info/account", _params);
        }

        /// <summary>
        /// bithumb 거래소 회원 지갑 정보
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <returns></returns>
        public async Task<UserBalance> Balance(string currency = "ALL")
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
            }

            return await UserClient.CallApiPostAsync<UserBalance>("/info/balance", _params);
        }

        /// <summary>
        /// bithumb 거래소 회원 입금 주소
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <returns></returns>
        public async Task<UserDeposit> WalletAddress(string currency)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
            }

            return await UserClient.CallApiPostAsync<UserDeposit>("/info/wallet_address", _params);
        }

        /// <summary>
        /// 회원 마지막 거래 정보
        /// </summary>
        /// <param name="order_currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="payment_currency">KRW (현재 bithumb에서 제공하는 통화 KRW)</param>
        /// <returns></returns>
        public async Task<UserTicker> Ticker(string order_currency, string payment_currency = "KRW")
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("order_currency", order_currency);
                _params.Add("payment_currency", payment_currency);
            }

            return await UserClient.CallApiPostAsync<UserTicker>("/info/ticker", _params);
        }

        /// <summary>
        /// bithumb 회원 판/구매 체결 내역
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="type">거래유형 (bid : 구매, ask : 판매)</param>
        /// <returns></returns>
        public async Task<UserOrderDetail> OrderDetail(string currency, string order_id, string type)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("order_id", order_id);
                _params.Add("type", type);
            }

            return await UserClient.CallApiPostAsync<UserOrderDetail>("/info/order_detail", _params);
        }

        /// <summary>
        /// bithumb 회원 btc 출금(회원등급에 따른 BTC, ETH, DASH, LTC, ETC, XRP 출금)
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="units">Currency 출금 하고자 하는 수량, 1회 최소 수량 (BTC: 0.001 | ETH: 0.01 | DASH: 0.01 | LTC: 0.01 | ETC: 0.01 | XRP: 21) - 1회 최대 수량 : 회원등급수량</param>
        /// <param name="address">Currency 출금 주소 (BTC, ETH, DASH, LTC, ETC, XRP)</param>
        /// <param name="destination">Currency 출금 Destination Tag (XRP 출금시)</param>
        /// <returns></returns>
        public async Task<UserWithdrawal> Withdrawal(string currency, decimal units, string address, string destination = null)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("units", units);
                _params.Add("address", address);
                if (destination != null)
                    _params.Add("destination", destination);
            }

            return await UserClient.CallApiPostAsync<UserWithdrawal>("/trade/btc_withdrawal", _params);
        }

        /// <summary>
        /// bithumb 회원 krw 출금 신청
        /// </summary>
        /// <param name="bank">은행코드_은행명</param>
        /// <param name="account">출금계좌번호</param>
        /// <param name="price">출금 금액</param>
        /// <returns></returns>
        public async Task<UserWithdrawal> KrwWithdrawal(string bank, string account, decimal price)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("bank", bank);
                _params.Add("account", account);
                _params.Add("price", price);
            }

            return await UserClient.CallApiPostAsync<UserWithdrawal>("/trade/krw_withdrawal", _params);
        }

        /// <summary>
        /// bithumb 회원 krw 입금 가상계좌 정보 요청
        /// </summary>
        /// <returns></returns>
        public async Task<UserKrwDeposit> KrwDeposit()
        {
            return await UserClient.CallApiPostAsync<UserKrwDeposit>("/trade/krw_deposit");
        }
    }
}