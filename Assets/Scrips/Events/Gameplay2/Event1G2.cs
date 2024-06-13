using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1G2 : MonoBehaviour, IEvent
{
    public void Execute()
    {
        // Lógica del evento 1
        Debug.Log("Evento 1 ejecutado.");
    }
}
