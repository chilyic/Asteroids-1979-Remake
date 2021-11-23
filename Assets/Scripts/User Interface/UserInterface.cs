using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface instance;

    [SerializeField] private GamePlayParametres _parametres;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Slider _healthBar;

    public static bool pause;

    public int score = 0;
    public int health = 3;

    private void Awake()
    {
        instance = this;
        score = 0;
        _parametres.isPause = false;
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _scoreText.text = "Score = " + score;
        _healthBar.value = health;

        Time.timeScale = 1;

        _parametres.canPause = true;
        if (!_parametres.isCursorVisible)
            _parametres.CursorOff();            
    }
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _parametres.canPause)
        {
            Time.timeScale = _parametres.isPause ? 1 : 0;

            Pause(!_parametres.isPause, Time.timeScale);

            _parametres.isPause = !_parametres.isPause;
        }
    }

    private void Pause(bool isVisible, float mode)
    {
        _pausePanel.SetActive(isVisible);

        Cursor.visible = isVisible;

        if (mode == 0)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;

        if (_parametres.isCursorVisible)
            _parametres.CursorOn();
    }

    public void ScoreAdded(int amount)
    {
        score += amount;        
        _scoreText.text = "Score = " + score;
    }

    public void Death()
    {
        _healthBar.value -= 1;

        if (_healthBar.value == 0)
        {
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            _parametres.CursorOn();
        }
    }
}
