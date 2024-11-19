using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField] private float dayDuration = 60f; // Dura��o de um ciclo completo de dia e noite em segundos
    [SerializeField] private Color dayColor = Color.white;
    [SerializeField] private Color nightColor = Color.blue;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.PingPong(time / dayDuration, 1f);

        // Interpola��o da cor e intensidade da luz
        globalLight.color = Color.Lerp(nightColor, dayColor, t);
        globalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
