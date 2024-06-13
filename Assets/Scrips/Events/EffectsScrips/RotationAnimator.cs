using UnityEngine;

public class RotationAnimator : MonoBehaviour
{
    public float minRotation ;
    public float maxRotation ;
    public float animationSpeed = 1.0f;

    private float targetRotation;
    private float currentRotation;

    void Start()
    {
        currentRotation = transform.rotation.eulerAngles.z;
        SetNewTargetRotation();
    }

    void Update()
    {
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, animationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        if (Mathf.Abs(currentRotation - targetRotation) < 0.1f)
        {
            SetNewTargetRotation();
        }
    }

    private void SetNewTargetRotation()
    {
        targetRotation = Random.Range(minRotation, maxRotation);
    }
}

