using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_SequenceOfActionKeyPusher : MonoBehaviour

    {
        public EnumBasicHordesIoActionKey[] m_sequenceOfKeys;
        public float m_waitTimePressingKey = 0.1f;
        public float m_waitTimeBetweenPower = 1.6f;
        public UnityEvent<int> m_onKeyPushedAsInteger;
        public void TriggerSequenceOfAction()
        {
            StartCoroutine(Coroutine_TriggerSequenceOfAction());
        }
        public IEnumerator Coroutine_TriggerSequenceOfAction()
        {
            foreach (var key in m_sequenceOfKeys)
            {
                m_onKeyPushedAsInteger?.Invoke((int)key);
                yield return new WaitForSeconds(m_waitTimePressingKey);
                m_onKeyPushedAsInteger?.Invoke(((int)key) + 1000);
                yield return new WaitForSeconds(m_waitTimeBetweenPower);
            }
        }
    }
}