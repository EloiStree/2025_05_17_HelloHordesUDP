using System;
using System.Collections.Generic;

using UnityEngine;
namespace Eloi.Hordes
{

    public class HordesMono_SendIntAsUDP : MonoBehaviour
    {
        public static List<string> m_ipv4ToBroadcast = new List<string>();
        public static List<int> m_portToBroadcast = new List<int>();

        static HordesMono_SendIntAsUDP()
        {

            S_AppendComputerTargetIpv4("127.0.0.1");
            S_AppendComputerTargetPort(7073);
            S_AppendComputerTargetPort(7074);
        }

        public static void S_ClearTargets()
        {
            m_ipv4ToBroadcast.Clear();
            m_portToBroadcast.Clear();
        }
        public static void S_RemoveTargetsIpv4(string ip)
        {
            m_ipv4ToBroadcast.Remove(ip);
        }
        public static void S_RemoveTargetsPort(int port)
        {
            m_portToBroadcast.Remove(port);
        }

        public static void S_AppendComputerTargetPort(int port)
        {
            if (m_portToBroadcast.Contains(port))
            {
                return;
            }
            m_portToBroadcast.Add(port);
        }

        public static void S_AppendComputerTargetIpv4(string ipv4)
        {
            if (m_ipv4ToBroadcast.Contains(ipv4))
            {
                return;
            }
            m_ipv4ToBroadcast.Add(ipv4);
        }

        public static void S_PushBytesToComputer(string ip, int port, params byte[] bytes)
        {
            // Create a UDP client 
            using (var udpClient = new System.Net.Sockets.UdpClient(ip, port))
            {
                // Send the byte array to the specified IP and port
                udpClient.Send(bytes, bytes.Length);
            }
        }

        public static void S_PushIntegerToTargets(int value)
        {
            using (var udpClient = new System.Net.Sockets.UdpClient())
            {
                // Signed integer little endiant format
                byte[] bytes = System.BitConverter.GetBytes(value);
                foreach (var ip in m_ipv4ToBroadcast)
                {
                    foreach (var port in m_portToBroadcast)
                    {
                        try
                        {
                            udpClient.Send(bytes, bytes.Length, ip, port);
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError($"Failed to send UDP packet to {ip}:{port} - {e.Message}");

                        }
                    }
                }
            }
        }

        [ContextMenu("Push Press Space")]
        public void PushIntegerToPressSpace() => S_PushIntegerToTargets(1032);
        [ContextMenu("Push Release Space")]
        public void PushIntegerToReleaseSpace() => S_PushIntegerToTargets(2032);


        [ContextMenu("Push Press Tabulation")]
        public void PushIntegerToPressTabulation() => S_PushIntegerToTargets(1009);

        [ContextMenu("Push Release Tabulation")]
        public void PushIntegerToReleaseTabulation() => S_PushIntegerToTargets(2009);


        public void S_OverrideIpv4AndPortFromThisText(string text)
        {

            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            S_ClearTargets();
            string[] lines = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                string[] parts = line.Split(':');

                if (parts.Length == 2)
                {
                    string ip = parts[0].Trim();
                    int part = ip.Split('.').Length;
                    if (part == 4 && int.TryParse(parts[1].Trim(), out int port))
                    {
                        S_AppendComputerTargetIpv4(ip);
                        S_AppendComputerTargetPort(port);
                    }
                }
                if (parts.Length == 1)
                {
                    string ip = parts[0].Trim();
                    int part = ip.Split('.').Length;
                    if (part == 4)
                    {
                        S_AppendComputerTargetIpv4(ip);
                    }
                    else if (int.TryParse(ip, out int port))
                    {
                        S_AppendComputerTargetPort(port);
                    }
                }
            }


        }

    }
}