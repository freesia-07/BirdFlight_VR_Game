using UnityEngine;

public class RiverFlow : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Color shallowColor = new Color(0.2f, 0.6f, 0.9f, 0.7f);
    public Color deepColor = new Color(0.0f, 0.3f, 0.7f, 0.9f);

    private Renderer rend;
    private float timer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = shallowColor;
    }

    void Update()
    {
        if (rend == null) return;

        // Scroll the texture to simulate water flowing
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(0, offset);

        // Gently pulse between shallow and deep color
        timer += Time.deltaTime;
        float pulse = (Mathf.Sin(timer * 0.8f) + 1f) / 2f;
        rend.material.color = Color.Lerp(shallowColor, deepColor, pulse);
    }
}