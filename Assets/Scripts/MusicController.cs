using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource[] musicAudios;
    public float[] musicDurations;

    private int _index = -1;
    private float _timer;

    #region Unity Events

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        PlayRandomMusic();
    }

    private void Update()
    {
        if (_index == -1) return;

        _timer += Time.unscaledDeltaTime;
        if (_timer >= musicDurations[_index])
        {
            PlayRandomMusic();
            _timer = 0f;
        }
    }

    #endregion

    private void PlayRandomMusic()
    {
        if (_index != -1) musicAudios[_index].Stop();

        _index = Random.Range(0, musicAudios.Length);
        musicAudios[_index].Play();
    }
}
