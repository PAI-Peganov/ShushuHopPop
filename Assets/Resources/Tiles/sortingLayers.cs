using UnityEngine;

public class IsoSorting : MonoBehaviour
{
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
        }
    }

    void LateUpdate()
    {
        if (_renderer != null)
        {
            _renderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
    