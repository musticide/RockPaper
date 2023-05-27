using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityIntEvent : UnityEvent<int>
{
}
public class EventManager : MonoBehaviour
{
    public UnityIntEvent OnPOneClick;
    public UnityIntEvent OnPTwoClick;

    public void OnPOneButtonClick(int i)
    {
        OnPOneClick.Invoke(i);
    }
    public void OnPTwoButtonClick(int i)
    {
        OnPTwoClick.Invoke(i);
    }
}
