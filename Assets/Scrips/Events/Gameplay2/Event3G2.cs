using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3G2 : MonoBehaviour, IEvent
{
    public void Execute()
    {
        // L�gica del evento 1
        Debug.Log("Evento 3 ejecutado.");
    }
}
