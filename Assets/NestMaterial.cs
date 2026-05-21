using UnityEngine;

public class NestMarker : MonoBehaviour
{
    [Header("Beacon Settings")]
    public float pulseSpeed = 2f;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public float rotateSpeed = 60f;
    public Color beaconColor = new Color(1f, 0.85f, 0f, 0.8f); // golden yellow

    [Header("Hide when complete")]
    public bool nestComplete = false;

    private Renderer rend;
    private float timer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = beaconColor;
            // Make it glow
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", beaconColor * 1.5f);
        }
    }

    void Update()
    {
        if (nestComplete)
        {
            gameObject.SetActive(false);
            return;
        }

        timer += Time.deltaTime;

        // Pulse scale up and down
        float pulse = Mathf.Lerp(minScale, maxScale,
                      (Mathf.Sin(timer * pulseSpeed) + 1f) / 2f);
        transform.localScale = new Vector3(pulse, pulse, pulse);

        // Slowly rotate
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}