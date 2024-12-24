using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Toggles")]
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private void Start()
    {
        // Сразу инициализируем состояние кнопок на старте
        // Если musicSource не замьючен - значит музыка включена
        if (AudioManager.Instance != null)
        {
            // musicToggle.isOn = !musicSource.mute
            musicToggle.isOn = !AudioManager.Instance.musicSource.mute;
            sfxToggle.isOn = !AudioManager.Instance.sfxSource.mute;
        }

        // Подписываемся на изменения значений тумблеров
        musicToggle.onValueChanged.AddListener(delegate { OnMusicToggleChanged(); });
        sfxToggle.onValueChanged.AddListener(delegate { OnSfxToggleChanged(); });
    }

    private void OnMusicToggleChanged()
    {
        // При изменении переключателя зовём метод AudioManager
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleMusic(musicToggle.isOn);
    }

    private void OnSfxToggleChanged()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleSFX(sfxToggle.isOn);
    }
}