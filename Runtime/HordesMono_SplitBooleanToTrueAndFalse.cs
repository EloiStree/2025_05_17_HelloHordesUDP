using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_SplitBooleanToTrueAndFalse : MonoBehaviour
    {
        public UnityEvent<bool> m_onTrue;
        public UnityEvent<bool> m_onFalse;

        public void PushIn(bool value)
        {
            if (value)
            {
                m_onTrue.Invoke(value);
            }
            else
            {
                m_onFalse.Invoke(value);
            }
        }
    }
}