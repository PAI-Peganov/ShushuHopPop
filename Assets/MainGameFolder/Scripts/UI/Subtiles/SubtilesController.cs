using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SubtitleEntry
{
    public float Duration { set; get; }
    public string Text { set; get; }
    public SubtitleEntry(float duration, string text) 
    {
        Text = text;
        Duration = duration;
    }
}

public class SubtilesController : MonoBehaviour
{ 
    [SerializeField]
    private TextMeshProUGUI subtitleText;
    [SerializeField]
    private TextAsset srtFile;

    private Dictionary<string, List<SubtitleEntry>> subtitles;

    void Start()
    {
        WorldManager.AddSubtitlesController(this);
        var text = srtFile.text;
        string tag = null;
        subtitles = new Dictionary<string, List<SubtitleEntry>>();
        foreach (var _line in text.Split("\n"))
        {
            var line = _line.Trim();
            if (line.StartsWith("/"))
            {
                tag = line.Substring(1);
                subtitles[tag] = new List<SubtitleEntry>();
                
            }
            else if (tag != null && line.Length > 1)
            {
                var splitedLine = line.Split(' ', 2);
                subtitles[tag].Add(new SubtitleEntry(float.Parse(splitedLine[0], System.Globalization.CultureInfo.InvariantCulture), splitedLine[1]));
            }
        }
        
        PlaySubtiles("start");
    }

    private IEnumerator SubtilesWriter(string tag)
    {
        foreach (var subtitle in subtitles[tag])
        {
            var halfDuration = subtitle.Duration / 2;
            var text = subtitle.Text;
            for (var i = 0; i < text.Length; i++)
            {
                AddSymbolSubtiles(text[i]);
                yield return new WaitForSeconds(halfDuration / text.Length);
            }
            yield return new WaitForSeconds(halfDuration);
            ClearSubtiles();
        }
        yield return new WaitForSeconds(0.1f);  
    }

    private void ClearSubtiles()
    {
        subtitleText.text = string.Empty;
    }

    private void AddSymbolSubtiles(char Symbol)
    {
        subtitleText.text = subtitleText.text + Symbol;
    }

    public void PlaySubtiles(string tag)
    {
        if (subtitles.ContainsKey(tag))
            StartCoroutine(SubtilesWriter(tag));
        else
            Debug.Log($"subtiles associated with {tag} dont exists");
    }
}
