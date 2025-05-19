using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInteractions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                break;
            case "Mushroom":
                foreach (Light2D lighter in FindObjectsByType<Light2D>(FindObjectsSortMode.None))
                {
                    lighter.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
                    lighter.intensity = 0.01f;
                    if (lighter.lightType == Light2D.LightType.Global)
                        lighter.intensity = 0.0001f;
                }
                break;
            default:
                break;
        }
    }
}
