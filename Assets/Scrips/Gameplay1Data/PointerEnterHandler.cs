using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEnterHandler : MonoBehaviour, IPointerEnterHandler
{
    public delegate void PointerEnterDelegate();
    public event PointerEnterDelegate onPointerEnter;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null)
        {
            onPointerEnter.Invoke();
        }
    }
}

