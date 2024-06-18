using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInfo
{
    // Campos para almacenar información de la nota
    public GameObject noteObject;
    public float startTime;
    public float targetFrequency;

    // Constructor para inicializar los campos
    public NoteInfo(GameObject noteObject, float startTime, float targetFrequency)
    {
        this.noteObject = noteObject;
        this.startTime = startTime;
        this.targetFrequency = targetFrequency;
    }
}


