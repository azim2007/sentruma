using UnityEngine;

/// <summary>
/// контроллер для источников звука в диалогах
/// </summary>
public class AudioSourceController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// начинает проигрывать композицию
    /// </summary>
    /// <param name="clip">композиция</param>
    /// <param name="volume">уровень громкости</param>
    /// <param name="isLoop">надо ли зацикливать</param>
    public void Play(AudioClip clip, float volume, bool isLoop)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = isLoop;
        audioSource.Play();
    }

    /// <summary>
    /// изменяет громкость проигрывания
    /// </summary>
    /// <param name="volume">уровень громкости</param>
    public void ChangeVolume(float volume) => audioSource.volume = volume;

    /// <summary>
    /// останавливает проигрывание композиции
    /// </summary>
    public void Stop() => audioSource.Stop();
}
