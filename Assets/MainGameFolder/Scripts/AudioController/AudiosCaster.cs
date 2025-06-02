using System.Linq;
using UnityEngine;

public class AudiosCaster : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    public void PlayAudioByName(string audioName)
    {
        var audioClip = clips.FirstOrDefault(x => x.name == audioName);
        if (audioClip != null)
            audioSource.PlayOneShot(audioClip);
    }
}
