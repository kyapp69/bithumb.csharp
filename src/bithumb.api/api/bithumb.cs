﻿using OdinSdk.BaseLib.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CCXT.NET.Bithumb
{
    /// <summary>
    /// 
    /// </summary>
    public class BithumbClient : XApiClient
    {
        private const string __api_url = "https://api.bithumb.com";

        /// <summary>
        /// 
        /// </summary>
        public BithumbClient(string connect_key, string secret_key)
            : this(__api_url, connect_key, secret_key)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public BithumbClient(string api_url, string connect_key, string secret_key)
            : base(api_url, connect_key, secret_key)
        {
        }

        /// <summary>
        /// 결과 메시지
        /// </summary>
        public string getErrorMessage(int status)
        {
            var _result = "";

            switch (status)
            {
                case 0:
                    _result = "success";
                    break;
                case 5100:
                    _result = "bad request";
                    break;
                case 5200:
                    _result = "not member";
                    break;
                case 5300:
                    _result = "invalid apikey";
                    break;
                case 5302:
                    _result = "method not allowed";
                    break;
                case 5400:
                    _result = "database fail";
                    break;
                case 5500:
                    _result = "invalid parameter";
                    break;
                case 5600:
                    _result = "custom notice(output error messages in context)";
                    break;
                case 5900:
                    _result = "unknown error";
                    break;
                default:
                    _result = "unknown error";
                    break;
            }

            return _result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="rgData"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetHttpHeaders(string endpoint, Dictionary<string, object> rgData, string apiKey, string apiSecret)
        {
            var _nonce = CUnixTime.NowMilli.ToString();
            var _data = EncodeURIComponent(rgData);
            var _message = String.Format("{0};{1};{2}", endpoint, _data, _nonce);

            var _secretKey = Encoding.UTF8.GetBytes(apiSecret);
            var _hmac = new HMACSHA512(_secretKey);
            _hmac.Initialize();

            var _bytes = Encoding.UTF8.GetBytes(_message);
            var _rawHmac = _hmac.ComputeHash(_bytes);

            var _encoded = EncodeHex(_rawHmac);
            var _signature = Convert.ToBase64String(_encoded);

            var _headers = new Dictionary<string, object>();
            {
                _headers.Add("Api-Key", apiKey);
                _headers.Add("Api-Sign", _signature);
                _headers.Add("Api-Nonce", _nonce);
            }

            return _headers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task<string> CallApiPostAsync(string endpoint, Dictionary<string, object> args = null)
        {
            var _request = CreateJsonRequest(endpoint, Method.POST);
            {
                var _params = new Dictionary<string, object>();
                {
                    _params.Add("endpoint", endpoint);
                    if (args != null)
                    {
                        foreach (var a in args)
                            _params.Add(a.Key, a.Value);
                    }
                }

                _request.AddHeader("api-client-type", "2");

                var _headers = GetHttpHeaders(endpoint, _params, __connect_key, __secret_key);
                foreach (var h in _headers)
                    _request.AddHeader(h.Key, h.Value.ToString());

                foreach (var a in _params)
                    _request.AddParameter(a.Key, a.Value);
            }

            var _client = CreateJsonClient(__api_url);
            {
                var _tcs = new TaskCompletionSource<IRestResponse>();
                var _handle = _client.ExecuteAsync(_request, response =>
                {
                    _tcs.SetResult(response);
                });

                var _response = await _tcs.Task;
                return _response.Content;
            }
        }
    }
}