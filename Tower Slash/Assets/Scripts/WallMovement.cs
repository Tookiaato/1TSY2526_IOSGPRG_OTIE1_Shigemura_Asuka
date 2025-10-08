using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float scrollSpeed = 1f;

    private Vector2 startOffset;
    private SpriteRenderer spriteRenderer;
    private float offset;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        startOffset = spriteRenderer.size;
    }

    private void Update()
    {
        offset += scrollSpeed * Time.deltaTime;

        spriteRenderer.size = new Vector2(startOffset.x, startOffset.y + offset);

        if (Mathf.Abs(offset) > 1000f)
            offset = 0f;
    }
}
