using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource soundSource;
    public AudioSource musicSource;

    [Header("UI Buttons")]
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject vibroOnButton;
    public GameObject vibroOffButton;

    [Header("UI Save and Back Buttons")]
    public GameObject saveButton;
    public GameObject backButton;

    private const string SoundKey = "sound";
    private const string MusicKey = "music";
    private const string VibroKey = "vibro";

    private SceneTransition _sceneTransition;

    private void Start()
    {
        _sceneTransition = GetComponent<SceneTransition>();
        LoadSettings();
    }

    // Методы для звука
    public void EnableSound()
    {

        // Обновляем звук
        soundSource.volume = 1f;

        // Сохраняем настройки
        PlayerPrefs.SetFloat(SoundKey, soundSource.volume);
        PlayerPrefs.Save();

        // Обновляем UI
        soundOnButton.transform.GetChild(0).gameObject.SetActive(true);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(false);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    public void DisableSound()
    {

        // Обновляем звук
        soundSource.volume = 0f;

        // Сохраняем настройки
        PlayerPrefs.SetFloat(SoundKey, soundSource.volume);
        PlayerPrefs.Save();

        // Обновляем UI
        soundOnButton.transform.GetChild(0).gameObject.SetActive(false);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(true);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    // Методы для музыки
    public void EnableMusic()
    {

        // Обновляем музыку
        musicSource.volume = 1f;

        // Сохраняем настройки
        PlayerPrefs.SetFloat(MusicKey, musicSource.volume);
        PlayerPrefs.Save();

        // Обновляем UI
        musicOnButton.transform.GetChild(0).gameObject.SetActive(true);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(false);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    public void DisableMusic()
    {

        // Обновляем музыку
        musicSource.volume = 0f;

        // Сохраняем настройки
        PlayerPrefs.SetFloat(MusicKey, musicSource.volume);
        PlayerPrefs.Save();

        // Обновляем UI
        musicOnButton.transform.GetChild(0).gameObject.SetActive(false);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(true);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    // Методы для вибрации
    public void EnableVibro()
    {

        // Сохраняем вибрацию
        PlayerPrefs.SetInt(VibroKey, 1);
        PlayerPrefs.Save();

        // Обновляем UI
        vibroOnButton.transform.GetChild(0).gameObject.SetActive(true);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(false);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    public void DisableVibro()
    {

        // Сохраняем вибрацию
        PlayerPrefs.SetInt(VibroKey, 0);
        PlayerPrefs.Save();

        // Обновляем UI
        vibroOnButton.transform.GetChild(0).gameObject.SetActive(false);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(true);

        // Показываем кнопку Save
        ToggleSaveButton(true);
    }

    // Загрузка настроек
    private void LoadSettings()
    {

        // Загрузка звука
        float savedSoundVolume = PlayerPrefs.GetFloat(SoundKey, 1f);
        soundSource.volume = savedSoundVolume;
        UpdateSoundUI(savedSoundVolume > 0);

        // Загрузка музыки
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        musicSource.volume = savedMusicVolume;
        UpdateMusicUI(savedMusicVolume > 0);

        // Загрузка вибрации
        int savedVibro = PlayerPrefs.GetInt(VibroKey, 1);
        UpdateVibroUI(savedVibro > 0);
    }

    // Обновление UI
    private void UpdateSoundUI(bool isOn)
    {
        soundOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        soundOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void UpdateMusicUI(bool isOn)
    {
        musicOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        musicOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void UpdateVibroUI(bool isOn)
    {
        vibroOnButton.transform.GetChild(0).gameObject.SetActive(isOn);
        vibroOffButton.transform.GetChild(0).gameObject.SetActive(!isOn);
    }

    private void ToggleSaveButton(bool isActive)
    {
        saveButton.SetActive(isActive);
        backButton.SetActive(!isActive);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        _sceneTransition.ChangeScene("MenuScene");
    }
}