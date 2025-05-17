using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.Hordes
{


public class HordesMono_BasicHordesActionKeyPusher : MonoBehaviour
{
    public EnumBasicHordesIoActionKey m_whatToPush;
    public float m_pushInterval = 1f/30f; // 30 FPS

    public UnityEvent<int> m_onKeyPushedAsInteger;

    public void SendPressKey()
    {

        m_onKeyPushedAsInteger?.Invoke((int)m_whatToPush);

    }
    public void SendReleaseKey()
    {
        m_onKeyPushedAsInteger?.Invoke(((int)m_whatToPush) + 1000);
    }


    public void SendPressAndReleaseWithDelay()
    {
        StartCoroutine(Coroutine_SendPressAndReleaseKey());
    }
    public IEnumerator Coroutine_SendPressAndReleaseKey()
    {
        m_onKeyPushedAsInteger?.Invoke((int)m_whatToPush);
        yield return new WaitForSeconds(m_pushInterval);
        m_onKeyPushedAsInteger?.Invoke(((int)m_whatToPush) + 1000);
    }
}

}