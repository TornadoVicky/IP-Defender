using UnityEngine;

public class PanelLerper : MonoBehaviour
{
    public Transform targetTransform; // The target location as a Transform
    public float lerpDuration = 1.0f; // The time it takes to lerp to the target position
    public float delayBeforeLerp = 2.0f; // Delay before starting the lerp

    private bool shouldLerp = false;
    private float lerpStartTime;

    void Start()
    {
        // Activate the panel (You can replace this with your activation code)
        gameObject.SetActive(true);

        // Start the lerp after the specified delay
        Invoke("StartLerp", delayBeforeLerp);
    }

    void Update()
    {
        if (shouldLerp)
        {
            float timeSinceStart = Time.time - lerpStartTime;
            float t = Mathf.Clamp01(timeSinceStart / lerpDuration);

            transform.position = Vector3.Lerp(transform.position, targetTransform.position, t);

            if (t >= 1.0f)
            {
                shouldLerp = false;

                // Disable the panel after reaching the target position
                gameObject.SetActive(false);
            }
        }
    }

    void StartLerp()
    {
        shouldLerp = true;
        lerpStartTime = Time.time;
    }

    // Reactivate the lerp when the panel is reactivated
    void OnEnable()
    {
        Invoke("StartLerp", delayBeforeLerp);
    }
}
