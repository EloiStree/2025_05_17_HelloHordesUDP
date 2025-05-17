
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_GroupOfIpv4AndPortEvent : MonoBehaviour
    {
        public List<string> m_ipv4Group;
        public List<int> m_portGroup;

        public UnityEvent<string> m_onPushIpv4;
        public UnityEvent<int> m_onPushPort;

        public bool m_useAwake = true;
        public void Awake()
        {
            if (m_useAwake)
                PushGroupGiven();
        }

        [ContextMenu("Push Group Given")]
        public void PushGroupGiven()
        {
            foreach (var ipv4 in m_ipv4Group)
            {
                m_onPushIpv4.Invoke(ipv4);
            }
            foreach (var port in m_portGroup)
            {
                m_onPushPort.Invoke(port);
            }
        }
    }
}