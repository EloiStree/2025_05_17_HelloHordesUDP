using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_DebugRelayInteger : MonoBehaviour
    {

        public int m_lastPushedValue = 0;
        public UnityEvent<int> m_onRelayInteger = new UnityEvent<int>();
        public void PushIn(int value)
        {
            m_lastPushedValue = value;
            m_onRelayInteger.Invoke(value);
        }
    }
}