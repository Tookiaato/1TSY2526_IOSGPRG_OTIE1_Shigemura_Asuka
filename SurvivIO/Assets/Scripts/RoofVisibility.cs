using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofVisibility : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine fadeRoutine;

    [SerializeField] private float fadeDuration = 0.5f;
    private float targetAlpha;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            FadeTo(0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            FadeTo(1f);
        }
    }

    private void FadeTo(float alpha)
    {
        targetAlpha = alpha;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeRoutine(alpha));
    }

    private IEnumerator FadeRoutine(float alpha)
    {
        Color startColor = _spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, alpha);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsed / fadeDuration);
            yield return null;
        }

        _spriteRenderer.color = targetColor;
    }
}
