using UnityEngine;
using UnityEngine.Rendering;

public class IsoSorting : MonoBehaviour
{
    private Renderer _renderer;
    private SortingGroup _sortingGroup;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _sortingGroup = GetComponent<SortingGroup>();

        if (_renderer == null && _sortingGroup == null)
        {
            Debug.LogError("Neither Renderer nor SortingGroup found on " + gameObject.name);
        }
    }

    void LateUpdate()
    {
        int sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);

        if (_sortingGroup != null)
        {
            _sortingGroup.sortingOrder = sortingOrder;
        }
        else if (_renderer != null)
        {
            _renderer.sortingOrder = sortingOrder;
        }
    }
}