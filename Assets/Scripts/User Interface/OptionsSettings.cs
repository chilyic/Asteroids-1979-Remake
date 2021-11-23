using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSettings : MonoBehaviour
{
    [SerializeField] private GamePlayParametres _parametres;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private GameObject[] _keyboardObj;
    [SerializeField] private GameObject[] _keyAndMouseObj;

    private SaveSettings _sv = new SaveSettings();
    private string _path;
        
    private void Start()
    {
        _path = Path.Combine(Application.dataPath, "OptionsSettings.json");

        if (File.Exists(_path))
        {
            _sv = JsonUtility.FromJson<SaveSettings>(File.ReadAllText(_path));

            _musicSource.volume = _sv.musicVolume;
            _musicSlider.value = _sv.musicVolume;

            _soundsSource.volume = _sv.soundVolume;
            _soundSlider.value = _sv.soundVolume;

            switch (_sv.control)
            {
                case "Keyboard":
                    {
                        KeyboardSelected();
                    }
                    break;

                case "KeyAndMouse":
                    {
                        KeyAndMouseSelected();
                    }
                    break;
            }
        }
        else
        {
            _sv.musicVolume = _musicSlider.value;
            _sv.soundVolume = _soundSlider.value;

            KeyboardSelected();
        }
    }

    public void MusicValueChange()
    {
        _musicSource.volume = _musicSlider.value;
        _sv.musicVolume = _musicSlider.value;
    }

    public void SoundValueChange()
    {
        _soundsSource.volume = _soundSlider.value;
        _sv.soundVolume = _soundSlider.value;
    }

    public void KeyboardSelected()
    {
        _sv.control = "Keyboard";
        _parametres.isCursorVisible = false;
                
        foreach (var obj in _keyboardObj) obj.SetActive(true);
        foreach (var obj in _keyAndMouseObj) obj.SetActive(false);
    }

    public void KeyAndMouseSelected()
    {
        _sv.control = "KeyAndMouse";
        _parametres.isCursorVisible = true;

        foreach (var obj in _keyboardObj) obj.SetActive(false);
        foreach (var obj in _keyAndMouseObj) obj.SetActive(true);
    }

    public void Save()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
            File.WriteAllText(_path, JsonUtility.ToJson(_sv));
        }
        else
            File.WriteAllText(_path, JsonUtility.ToJson(_sv));

        _parametres.canPause = true;
    }
}

[Serializable]
public class SaveSettings
{
    public float musicVolume;
    public float soundVolume;
    public string control;
}