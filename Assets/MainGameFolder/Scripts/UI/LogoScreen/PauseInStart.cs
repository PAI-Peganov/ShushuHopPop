using System.Collections;
using UnityEngine;


public class PauseInStart : MonoBehaviour
{   private IEnumerator Unpause()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
    }

    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(Unpause());
    }
}

