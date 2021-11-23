using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GamePlayParametres _parametres;
    [SerializeField] private GameObject _newRecordText;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _score;
    [SerializeField] private Slider _healthBar;

    private SaveScore _sv = new SaveScore();
    private string path;
    private UserInterface _interface;    

    private void Start()
    {
        _interface = UserInterface.instance;
        _score.text = $"Score = {_interface.score}";
        _parametres.canPause = false;

        Load();

        if (_interface.score > _sv.highScore)
        {
            _highScore.text = $"High Score = {_interface.score}";
            _newRecordText.SetActive(true);
            Save();
        }
    }
    
    private void Save()
    {
        _sv.highScore = _interface.score;

        if (File.Exists(path))
        {
            File.Delete(path);
            File.WriteAllText(path, JsonUtility.ToJson(_sv));
        }
        else
            File.WriteAllText(path, JsonUtility.ToJson(_sv));
    }

    private void OnEnable()
    {
        Start();
    }

    private void Load()
    {
        path = Path.Combine(Application.dataPath, "SaveScore.json");

        if (File.Exists(path))
        {
            _sv = JsonUtility.FromJson<SaveScore>(File.ReadAllText(path));
            _highScore.text = $"High Score = {_sv.highScore}";
        }
        else
            _highScore.text = _score.text;
    }

    public void Restart()
    {
        _healthBar.value = _interface.health;

        _interface.ScoreAdded(-_interface.score);
        _score.text = $"Score = {_interface.score}";

        Time.timeScale = 1;

        if (!_parametres.isCursorVisible)
        {
            _parametres.CursorOff();
        }

        _parametres.canPause = true;
        _newRecordText.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

[Serializable]
public class SaveScore
{
    public int highScore;
}