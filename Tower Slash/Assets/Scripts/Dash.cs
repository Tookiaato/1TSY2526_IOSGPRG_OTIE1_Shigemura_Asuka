using UnityEngine;

public class Dash : MonoBehaviour
{
    [HideInInspector] public float dashGauge;
    [HideInInspector] public bool isDash;
    [HideInInspector] public float dashTimer;

    private void Start()
    {
        dashTimer = 3f;
        dashGauge = 0;
    }

    private void Update()
    {
        if (dashGauge >= 1)
        {
            dashGauge = 1;
        }
    }
}
