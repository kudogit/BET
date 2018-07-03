#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> SystemHelper.cs </Name>
//         <Created> 21/04/2018 5:34:40 PM </Created>
//         <Key> 3bd06eed-ba10-4098-8ed5-aa26ce08dae1 </Key>
//     </File>
//     <Summary>
//         SystemHelper.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using BETSTS.Core.Models;
using BETSTS.Core.Models.Constants;
using Elect.Core.DateTimeUtils;
using Elect.Web.Middlewares.HttpContextMiddleware;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;

namespace BETSTS.Core.Utils
{
    public static class SystemHelper
    {
        public static SystemSettingModel Setting => SystemSettingModel.Instance;

        public static Microsoft.AspNetCore.Http.HttpContext CurrentHttpContext => HttpContext.Current;

        #region Date Time Utils

        public static TimeZoneInfo SystemTimeZoneInfo => DateTimeHelper.GetTimeZoneInfo(Setting.TimeZone);

        public static DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow.UtcToSystemTime();

        public static DateTime UtcToSystemTime(this DateTimeOffset dateTimeOffsetUtc)
        {
            return dateTimeOffsetUtc.UtcDateTime.UtcToSystemTime();
        }

        public static DateTime UtcToSystemTime(this DateTime dateTimeUtc)
        {
            var dateTimeWithTimeZone = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, SystemTimeZoneInfo);

            return dateTimeWithTimeZone;
        }

        public static DateTimeOffset? ToSystemDateTime(this string dateTimeString)
        {
            DateTimeOffset result;

            if (DateTime.TryParseExact(dateTimeString, Setting.DateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateTime))
            {
                result = dateTime;
            }
            else if (DateTime.TryParseExact(dateTimeString, Setting.DateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date))
            {
                result = date;
            }
            else
            {
                return null;
            }

            result = result.Date.WithTimeZone(Setting.TimeZone);

            return result;
        }

        public static TimeSpan? ToSystemTimeSpan(this string timeSpanString)
        {
            TimeSpan result;

            if (DateTime.TryParseExact(timeSpanString, Setting.TimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateTime))
            {
                result = dateTime.TimeOfDay;
            }
            else if (TimeSpan.TryParse(timeSpanString, CultureInfo.InvariantCulture, out var timeSpan))
            {
                result = timeSpan;
            }
            else
            {
                return null;
            }

            return result;
        }

        public static string ToSystemString(this TimeSpan timeSpan)
        {
            var result = DateTime.Today.Add(timeSpan).ToString(Setting.TimeFormat);

            return result;
        }

        #endregion

        public static ClaimsIdentity GetClaimsIdentity<T>(T data)
        {
            var properties = typeof(T).GetProperties();

            // Object to Dictionary<string, object>
            var dictionaryObj = new Dictionary<string, object>();

            foreach (var propertyInfo in properties)
            {
                var key = propertyInfo.Name;

                var value = propertyInfo.GetValue(data)?.ToString();

                dictionaryObj.Add(key, value);
            }

            // Dictionary<string, object> to Json
            var json = JsonConvert.SerializeObject(dictionaryObj, Models.Constants.Formatting.JsonSerializerSettings);

            // Json to Dictionary<string, string>
            var dictionaryStr =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(json,
                    Models.Constants.Formatting.JsonSerializerSettings);

            // Build Claims
            List<Claim> claims = new List<Claim>();

            foreach (var key in dictionaryStr.Keys)
            {
                var value = dictionaryStr[key];

                if (value == null)
                {
                    continue;
                }

                claims.Add(new Claim(key, value));
            }

            var claimIdentity = new ClaimsIdentity(claims, TokenType.AuthTokenType);

            return claimIdentity;
        }
        public static DateTime GetNetworkTime()
        {
            //default Windows time server
            const string ntpServer = "time.windows.com";

            // NTP message size - 16 bytes of the digest (RFC 2030)
            var ntpData = new byte[48];

            //Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //The UDP port number assigned to NTP is 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP uses UDP

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //**UTC** time
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                          ((x & 0x0000ff00) << 8) +
                          ((x & 0x00ff0000) >> 8) +
                          ((x & 0xff000000) >> 24));
        }
    }
}