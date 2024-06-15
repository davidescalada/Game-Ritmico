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
        int attempts = 0;
        do
        {
            selectedEvent = events[Random.Range(0, events.Count)];
            attempts++;
        } while (selectedEvent == lastEvent && attempts < events.Count);

        if (selectedEvent != lastEvent || attempts >= events.Count)
        {
            selectedEvent.Execute();
            lastEvent = selectedEvent;
        }
        else
        {
            Debug.LogWarning("No se pudo seleccionar un evento diferente al último.");
        }
    }

    public void TriggerSpecificEvent(int index)
    {
        if (index >= 0 && index < events.Count)
        {
            events[index].Execute();
            lastEvent = events[index];
        }
        else
        {
            Debug.LogError("El índice del evento está fuera de rango.");
        }
    }
}

