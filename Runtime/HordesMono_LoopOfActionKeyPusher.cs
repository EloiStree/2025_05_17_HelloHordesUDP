using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_LoopOfActionKeyPusher : MonoBehaviour

    {
        public bool m_useTheLoop;
        public EnumBasicHordesIoActionKey[] m_sequenceOfKeys;
        public float m_waitTimeBetweenActionKey = 1.6f;
        public float m_waitTimeWhenNotUse = 0.05f;
        public float m_waitTimeBetweenPressKey = 0.1f;
        public UnityEvent<int> m_onKeyPushedAsInteger;




        [ContextMenu("Toggle Loop State")]
        public void ToggleLoopState()
        {
            m_useTheLoop = !m_useTheLoop;
        }


        public void SetLoopOnActive(bool value)
        {
            m_useTheLoop = value;
        }

        [ContextMenu("Loop on")]
        public void SetLoopOnActive()
        {
            m_useTheLoop = true;
        }
        [ContextMenu("Loop off")]
        public void SetLoopOnDisable()
        {
            m_useTheLoop = false;
        }


        private void OnEnable()
        {

            StartCoroutine(Coroutine_TriggerSequenceOfAction());

        }

        public IEnumerator Coroutine_TriggerSequenceOfAction()
        {
            while (true)
            {
                if (!m_useTheLoop)
                {
                    yield return new WaitForSeconds(m_waitTimeWhenNotUse);
                }
                else
                {
                    foreach (var key in m_sequenceOfKeys)
                    {

                        if (m_useTheLoop == true)
                        {
                            m_onKeyPushedAsInteger?.Invoke((int)key);
                            yield return new WaitForSeconds(m_waitTimeBetweenPressKey);
                            m_onKeyPushedAsInteger?.Invoke(((int)key) + 1000);
                            yield return new WaitForSeconds(m_waitTimeBetweenActionKey);

                        }

                    }
                }
                yield return new WaitForEndOfFrame();
            }

        }
    }
}