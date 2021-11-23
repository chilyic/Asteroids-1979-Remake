using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GamePlayParametres _parametres;

    public void Continue()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        _parametres.isPause = false;

        if (_parametres.isCursorVisible)
        {
            _parametres.CursorOn();
        }
        else
        {
            _parametres.CursorOff();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Options()
    {
        _parametres.canPause = false;
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
