using UnityEngine;
using UnityEngine.UI;

public class OpacityAnimator : MonoBehaviour
{
    public float minOpacity ;
    public float maxOpacity;
    public float animationSpeed = 1.0f;

    private Image image;
    private float targetOpacity;
    private float currentOpacity;

    void Start()
    {
        image = GetComponent<Image>();
        currentOpacity = image.color.a;
        SetNewTargetOpacity();
    }

    void Update()
    {
        currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, animationSpeed * Time.deltaTime);
        Color color = image.color;
        color.a = currentOpacity;
        image.color = color;

        if (Mathf.Abs(currentOpacity - targetOpacity) < 0.01f)
        {
            SetNewTargetOpacity();
        }
    }

    private void SetNewTargetOpacity()
    {
        targetOpacity = Random.Range(minOpacity, maxOpacity);
    }
}
