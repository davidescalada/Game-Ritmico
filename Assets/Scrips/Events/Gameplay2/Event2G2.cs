using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2G2 : MonoBehaviour, IEvent
{
    public void Execute()
    {
        // L�gica del evento 1
        Debug.Log("Evento 2 ejecutado.");
    }
}
