using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCT.BaseLib.API.Bithumb.Trading
{
    /// <summary>
    /// https://api.bithumb.com/
    /// </summary>
    public class BTradeApi
    {
        private string __connect_key;
        private string __secret_key;

        /// <summary>
        /// 
        /// </summary>
        public BTradeApi(string connect_key, string secret_key)
        {
            __connect_key = connect_key;
            __secret_key = secret_key;
        }

        private BithumbClient __trade_client = null;

        private BithumbClient TradeClient
        {
            get
            {
                if (__trade_client == null)
                    __trade_client = new BithumbClient(__connect_key, __secret_key);
                return __trade_client;
            }
        }

        /// <summary>
        /// bithumb 회원 판/구매 거래 주문 등록 및 체결 (미수 주문등록 및 체결은 현 API에서 지원 안 함)
        /// </summary>
        /// <param name="order_currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="units">주문 수량, 1회 최소 수량 (BTC: 0.0001 | ETH: 0.001 | DASH: 0.001 | LTC: 0.01 | ETC: 0.01 | XRP: 1) - 1회 최대 수량 (BTC: 300 | ETH: 2,500 | DASH: 4,000 | LTC: 15,000 | ETC: 30,000 | XRP: 2,500,000)</param>
        /// <param name="price">1Currency당 거래금액 (BTC, ETH, DASH, LTC, ETC, XRP)</param>
        /// <param name="payment_currency">KRW (기본값)</param>
        /// <param name="misu">신용거래(Y : 사용, N : 일반) – 추후 제공</param>
        /// <returns></returns>
        public async Task<TradePlace> PlaceLimitBuy(string order_currency, decimal units, decimal price, string payment_currency = "KRW", string misu = "N")
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("units", units);
                _params.Add("price", price);
                _params.Add("type", "bid");
                _params.Add("order_currency", order_currency.ToUpper());
                _params.Add("payment_currency", payment_currency.ToUpper());
                _params.Add("misu", misu);
            }

            return await TradeClient.CallApiPostAsync<TradePlace>("/trade/place", _params);
        }

        /// <summary>
        /// bithumb 회원 판매 거래 주문 등록 및 체결 (미수 주문등록 및 체결은 현 API에서 지원 안 함)
        /// </summary>
        /// <param name="order_currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="units">주문 수량, 1회 최소 수량 (BTC: 0.0001 | ETH: 0.001 | DASH: 0.001 | LTC: 0.01 | ETC: 0.01 | XRP: 1) - 1회 최대 수량 (BTC: 300 | ETH: 2,500 | DASH: 4,000 | LTC: 15,000 | ETC: 30,000 | XRP: 2,500,000)</param>
        /// <param name="price">1Currency당 거래금액 (BTC, ETH, DASH, LTC, ETC, XRP)</param>
        /// <param name="payment_currency">KRW (기본값)</param>
        /// <param name="misu">신용거래(Y : 사용, N : 일반) – 추후 제공</param>
        /// <returns></returns>
        public async Task<TradePlace> PlaceLimitSell(string order_currency, decimal units, decimal price, string payment_currency = "KRW", string misu = "N")
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("units", units);
                _params.Add("price", price);
                _params.Add("type", "ask");
                _params.Add("order_currency", order_currency.ToUpper());
                _params.Add("payment_currency", payment_currency.ToUpper());
                _params.Add("misu", misu);
            }

            return await TradeClient.CallApiPostAsync<TradePlace>("/trade/place", _params);
        }

        /// <summary>
        /// 시장가 구매
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="units">주문 수량, 1회 최소 수량 (BTC: 0.0001 | ETH: 0.001 | DASH: 0.001 | LTC: 0.01 | ETC: 0.01 | XRP: 1) - 1회 거래 한도 : 1억원</param>
        /// <returns></returns>
        public async Task<TradePlaceMarket> PlaceMarketBuy(string currency, decimal units)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("units", units);
            }

            return await TradeClient.CallApiPostAsync<TradePlaceMarket>("/trade/market_buy", _params);
        }

        /// <summary>
        /// 시장가 판매
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="units">주문 수량, 1회 최소 수량 (BTC: 0.0001 | ETH: 0.001 | DASH: 0.001 | LTC: 0.01 | ETC: 0.01 | XRP: 1) - 1회 거래 한도 : 1억원</param>
        /// <returns></returns>
        public async Task<TradePlaceMarket> PlaceMarketSell(string currency, decimal units)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("units", units);
            }

            return await TradeClient.CallApiPostAsync<TradePlaceMarket>("/trade/market_sell", _params);
        }

        /// <summary>
        /// bithumb 회원 판/구매 거래 취소
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="type">거래유형 (bid : 구매, ask : 판매)</param>
        /// <returns></returns>
        public async Task<TradeCancel> CancelOrder(string currency, string order_id, string type)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("order_id", order_id);
                _params.Add("type", type);
            }

            return await TradeClient.CallApiPostAsync<TradeCancel>("/trade/cancel", _params);
        }

        /// <summary>
        /// 판/구매 거래 주문 등록 또는 진행 중인 거래
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="type">거래유형(bid : 구매, ask : 판매)</param>
        /// <param name="count">Value : 1 ~1000 (default : 100)</param>
        /// <param name="after">YYYY-MM-DD hh:mm:ss 의 UNIX Timestamp (2014-11-28 16:40:01 = 1417160401000)</param>
        /// <returns></returns>
        public async Task<TradeOpenOrders> OpenOrders(string currency, string order_id = "", string type = "", int count = 100, long after = 0)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("order_id", order_id);
                _params.Add("type", type);
                _params.Add("count", count);
                _params.Add("after", after);
            }

            return await TradeClient.CallApiPostAsync<TradeOpenOrders>("/info/orders", _params);
        }

        /// <summary>
        /// 회원 거래 내역
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP (기본값: BTC)</param>
        /// <param name="offset">Value : 0 ~ (default : 0)</param>
        /// <param name="count">Value : 1 ~ 50 (default : 20)</param>
        /// <param name="searchGb">	0 : 전체, 1 : 구매완료, 2 : 판매완료, 3 : 출금중, 4 : 입금, 5 : 출금, 9 : KRW입금중</param>
        /// <returns></returns>
        public async Task<TradeCompleteOrders> CompleteOrders(string currency, int offset = 0, int count = 20, int searchGb = 0)
        {
            var _params = new Dictionary<string, object>();
            {
                _params.Add("currency", currency.ToUpper());
                _params.Add("offset", offset);
                _params.Add("count", count);
                _params.Add("searchGb", searchGb);
            }

            return await TradeClient.CallApiPostAsync<TradeCompleteOrders>("/info/user_transactions", _params);
        }
    }
}