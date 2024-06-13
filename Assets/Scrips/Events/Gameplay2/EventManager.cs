using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private List<IEvent> events = new List<IEvent>();
    private IEvent lastEvent;

    void Start()
    {
        // Añadir todos los eventos a la lista
        events.Add(GetComponent<Event1G2>());
        events.Add(GetComponent<Event2G2>());
        events.Add(GetComponent<Event3G2>());
        events.Add(GetComponent<Event4G2>());
    }

    public void TriggerRandomEvent()
    {
        if (events.Count == 0)
        {
            Debug.LogWarning("No hay eventos registrados.");
            return;
        }

        IEvent selectedEvent;
        do
        {
            selectedEvent = events[Random.Range(0, events.Count)];
        } while (selectedEvent == lastEvent);

        selectedEvent.Execute();
        lastEvent = selectedEvent;
    }
}
