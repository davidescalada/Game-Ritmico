using UnityEngine;
using System.Collections;

public class NoiseEffectController : MonoBehaviour
{
    public GameObject noiseEffect;
    public float duration = 5.0f;

    private OpacityAnimator[] opacityAnimators;
    private RotationAnimator[] rotationAnimators;
    private PositionAnimator[] positionAnimators;

    void Start()
    {
        opacityAnimators = noiseEffect.GetComponentsInChildren<OpacityAnimator>();
        rotationAnimators = noiseEffect.GetComponentsInChildren<RotationAnimator>();
        positionAnimators = noiseEffect.GetComponentsInChildren<PositionAnimator>();

        noiseEffect.SetActive(false);
    }

    public void Execute()
    {
        StartCoroutine(ShowNoiseEffect());
    }

    private IEnumerator ShowNoiseEffect()
    {
        noiseEffect.SetActive(true);

        foreach (var animator in opacityAnimators)
        {
            animator.enabled = true;
        }

        foreach (var animator in rotationAnimators)
        {
            animator.enabled = true;
        }

        foreach (var animator in positionAnimators)
        {
            animator.enabled = true;
        }

        yield return new WaitForSeconds(duration);

        noiseEffect.SetActive(false);
    }
}
