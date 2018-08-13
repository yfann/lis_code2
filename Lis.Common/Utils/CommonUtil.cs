using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Lis.Common.Utils
{
    public static class CommonUtil
    {
        /// <summary>
        /// Returns IP4Address of request client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIp(HttpRequestMessage request)
        {
            string userHostAddress = null;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                userHostAddress = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (HttpContext.Current != null)
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            // for an IPV6 enabled machine, it will return the IPv6 address, try get the IPv4 address
            var clientIp = GetIP4Address(userHostAddress);
            return clientIp;
        }


        public static string GetComputerName(HttpRequestMessage request)
        {
            try
            {
                var clientIP = GetClientIp(request);
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetMac(HttpRequestMessage request)
        {
            try
            {
                var clientIP = GetClientIp(request);
                return MacResolver.GetRemoteMAC(clientIP);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetMac(string Ip)
        {
            try
            {
                return MacResolver.GetRemoteMAC(Ip);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private static string GetIP4Address(string userHostAddress)
        {
            var IP4Address = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(userHostAddress))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }


        public static string SearializeObject(object o)
        {
            var xmlString = string.Empty;
            var serializer = new XmlSerializer(o.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, o);
                xmlString = writer.ToString();
            }
            return xmlString;
        }

        public static object DesearializeObject(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }

    public static class MacResolver
    {
        [DllImport("Ws2_32.dll")]
        private static extern int inet_addr(string ip);
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, int host, ref long mac, ref int len);
        public static string GetRemoteMAC(string remoteIP)
        {
            try
            {
                int ldest = inet_addr(remoteIP);
                long macinfo = 0;
                int len = 6;

                int res = SendARP(ldest, 0, ref macinfo, ref len);

                return FormatMac(macinfo);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private static string FormatMac(long mac)
        {
            var separator = '-';
            if (mac <= 0)
                return "00-00-00-00-00-00";

            char[] oldmac = Convert.ToString(mac, 16).PadLeft(12, '0').ToCharArray();

            System.Text.StringBuilder newMac = new System.Text.StringBuilder(17);

            if (oldmac.Length < 12)
                return "00-00-00-00-00-00";

            newMac.Append(oldmac[10]);
            newMac.Append(oldmac[11]);
            newMac.Append(separator);
            newMac.Append(oldmac[8]);
            newMac.Append(oldmac[9]);
            newMac.Append(separator);
            newMac.Append(oldmac[6]);
            newMac.Append(oldmac[7]);
            newMac.Append(separator);
            newMac.Append(oldmac[4]);
            newMac.Append(oldmac[5]);
            newMac.Append(separator);
            newMac.Append(oldmac[2]);
            newMac.Append(oldmac[3]);
            newMac.Append(separator);
            newMac.Append(oldmac[0]);
            newMac.Append(oldmac[1]);

            return newMac.ToString().ToUpper();
        }
    }
}
