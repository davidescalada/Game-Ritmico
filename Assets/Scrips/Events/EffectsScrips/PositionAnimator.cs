using UnityEngine;

public class PositionAnimator : MonoBehaviour
{
    public Vector2 minPositionOffset;
    public Vector2 maxPositionOffset;
    public float animationSpeed = 1.0f;

    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Vector2 currentPosition;

    void Start()
    {
        startPosition = transform.localPosition;
        currentPosition = startPosition;
        SetNewTargetPosition();
    }

    void Update()
    {
        currentPosition = Vector2.Lerp(currentPosition, targetPosition, animationSpeed * Time.deltaTime);
        transform.localPosition = currentPosition;

        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    private void SetNewTargetPosition()
    {
        targetPosition = startPosition + new Vector2(
            Random.Range(minPositionOffset.x, maxPositionOffset.x),
            Random.Range(minPositionOffset.y, maxPositionOffset.y)
        );
    }
}
