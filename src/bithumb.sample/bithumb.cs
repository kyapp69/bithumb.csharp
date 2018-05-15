using CCXT.NET.Bithumb.Private;
using CCXT.NET.Bithumb.Public;
using CCXT.NET.Bithumb.Trade;
using System;

namespace CCXT.NET.Bithumb.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Bithumb
    {
        /// <summary>
        /// 1. Public API
        /// </summary>
        public static async void XPublicApi(int debug_step = 3)
        {
            var _public_api = new PublicApi();

            if (debug_step == 1)
            {
                var _ticker = await _public_api.FetchTicker("ETH");
                if (_ticker.status == 0)
                    Console.WriteLine(_ticker.data.closing_price);
            }

            if (debug_step == 2)
            {
                var _orderbook = await _public_api.FetchOrderBooks("ETH", 1, 50);
                if (_orderbook.status == 0)
                    Console.WriteLine(_orderbook.data.timestamp);
            }

            if (debug_step == 3)
            {
                var _recent_transactions = await _public_api.FetchTrades("ETH", 0, 100);
                if (_recent_transactions.status == 0)
                    Console.WriteLine(_recent_transactions.status);
            }
        }

        /// <summary>
        /// 2. User API
        /// </summary>
        public static async void XUserApi(int debug_step = 7)
        {
            var __info_api = new PrivateApi("", "");

            if (debug_step == 1)
            {
                var _account = await __info_api.FetchAccount("ETH");
                if (_account.status == 0)
                    Console.WriteLine(_account.data.account_id);
            }

            if (debug_step == 2)
            {
                var _balance = await __info_api.FetchBalances("ETH");
                if (_balance.status == 0)
                    Console.WriteLine(_balance.data.available_btc);
            }

            if (debug_step == 3)
            {
                var _wallet_address = await __info_api.FetchAddress("ETH");
                if (_wallet_address.status == 0)
                    Console.WriteLine(_wallet_address.data.wallet_address);
            }

            if (debug_step == 4)
            {
                var _ticker = await __info_api.FetchTicker(order_currency: "ETH");
                if (_ticker.status == 0)
                    Console.WriteLine(_ticker.data.units_traded);
            }

            if (debug_step == 5)
            {
                var _order_detail = await __info_api.FetchOrderDetails("ETH", "order_id", "ask");
                if (_order_detail.status == 0)
                    Console.WriteLine(_order_detail.data.Count);
            }

            if (debug_step == 6)
            {
                var _user_transactions = await __info_api.FetchTrades("ETH");
                if (_user_transactions.status == 0)
                    Console.WriteLine(_user_transactions.data.Count);
            }

            if (debug_step == 7)
            {
                var _orders = await __info_api.FetchOpenOrders("ETH");
                if (_orders.status == 0)
                    Console.WriteLine(_orders.data.Count);
            }
        }

        /// <summary>
        /// 3. Trade API
        /// </summary>
        public static async void XTradeApi(int debug_step = 1)
        {
            var __trade_api = new TradeApi("", "");

            if (debug_step == 1)
            {
                var _place = await __trade_api.PlaceLimitSell("ETH", 0.1m, 600000);
                if (_place.status == 0)
                    Console.WriteLine(_place.data.Count);
            }

            if (debug_step == 2)
            {
                var _cancel = await __trade_api.CancelOrder("ETH", "order_id", "ask");
                if (_cancel.status == 0)
                    Console.WriteLine(_cancel.status);
            }

            if (debug_step == 3)
            {
                var _market_buy = await __trade_api.PlaceMarketBuy("ETH", 0.1m);
                if (_market_buy.status == 0)
                    Console.WriteLine(_market_buy.order_id);
            }

            if (debug_step == 4)
            {
                var _market_sell = await __trade_api.PlaceMarketSell("ETH", 0.1m);
                if (_market_sell.status == 0)
                    Console.WriteLine(_market_sell.order_id);
            }

            if (debug_step == 5)
            {
                var _btc_withdrawal = await __trade_api.BtcWithdrawal("ETH", 0.1m, "address");
                if (_btc_withdrawal.status == 0)
                    Console.WriteLine(_btc_withdrawal.status);
            }

            if (debug_step == 6)
            {
                var _krw_deposit = await __trade_api.KrwDeposit();
                if (_krw_deposit.status == 0)
                    Console.WriteLine(_krw_deposit.status);
            }

            if (debug_step == 7)
            {
                var _krw_withdrawal = await __trade_api.KrwWithdrawal("003_기업은행", "111-2222-33333", 10000m);
                if (_krw_withdrawal.status == 0)
                    Console.WriteLine(_krw_withdrawal.status);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debug_step"></param>
        public static void Start(int debug_step = 1)
        {
            // 1. Public API
            if (debug_step == 1)
                XPublicApi();

            // 2. Private API
            if (debug_step == 2)
                XUserApi();

            // 3. Trade API
            if (debug_step == 3)
                XTradeApi();
        }
    }
}