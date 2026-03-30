using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event
{

}

public class ShowUI : Event
{
    public string text;
}

public class PlaySound : Event
{
    public AudioClip sound;
}
public class UIResponseData
{
    public string text;
    public UnityAction buttonAction;
}
public class ShowResponses : Event
{
    public List<UIResponseData> responses;
}
public class ToggleLock : Event
{
    public bool value;
}
public class HideUI : Event
{

}

public class EventDispatcher : Singleton<EventDispatcher>
{
    public delegate void EventDelegate<T>(T e) where T : Event;

    private Dictionary<System.Type, System.Delegate> m_eventDelegates =
        new Dictionary<System.Type, System.Delegate>();
    public void AddListener<T>(EventDelegate<T> listener) where T: Event
    {
        System.Type type = typeof(T);
        System.Delegate del;

        if (m_eventDelegates.TryGetValue(type, out del))
        {
            del = System.Delegate.Combine(del, listener);
            m_eventDelegates[type] = del;
        }
        else
        {
            m_eventDelegates.Add(type, listener);
        }
    }

    public void RemoveListener<T>(EventDelegate<T> listener) where T : Event
    {
        System.Type type = typeof(T);
        System.Delegate del;

        if (m_eventDelegates.TryGetValue(type, out del))
        {
            System.Delegate newDel = System.Delegate.Remove(del, listener);

            if (newDel != null)
            {
                m_eventDelegates[type] = newDel;
            }
            else
            {
                m_eventDelegates.Remove(type);
            }
        }
    }
    public void SendEvent<T>(T evtData) where T: Event
    {
        System.Delegate del;

        if(m_eventDelegates.TryGetValue(typeof(T), out del))
        {
            EventDelegate<T> callback = del as EventDelegate<T>;
            if(callback != null)
            {
                callback(evtData);
            }
        }
    }
}