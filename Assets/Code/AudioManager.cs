using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Флаги для состояния музыки и звуков
    private bool isMusicOn = true;
    private bool isSfxOn = true;

    private void Awake()
    {
        // Реализуем Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Считываем настройки из PlayerPrefs (по умолчанию 1 = включено)
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSfxOn = PlayerPrefs.GetInt("SfxOn", 1) == 1;

        UpdateAudioSettings();
    }

    // Вызываем при переключении тумблера музыки
    public void ToggleMusic(bool value)
    {
        isMusicOn = value;
        // Сохраняем в PlayerPrefs (1 = включено, 0 = выключено)
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateAudioSettings();
    }

    // Вызываем при переключении тумблера звуков
    public void ToggleSFX(bool value)
    {
        isSfxOn = value;
        PlayerPrefs.SetInt("SfxOn", isSfxOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateAudioSettings();
    }

    // Применяем настройки к AudioSource
    private void UpdateAudioSettings()
    {
        if (musicSource != null)
            musicSource.mute = !isMusicOn;

        if (sfxSource != null)
            sfxSource.mute = !isSfxOn;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}