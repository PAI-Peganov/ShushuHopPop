using EntityBase;
using TMPro;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField]
    private Transform Object;
    void Update()
    {
        transform.position = Object.position;
    }
}