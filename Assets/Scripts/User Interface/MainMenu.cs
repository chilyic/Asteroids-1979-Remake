using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    private float _progress;

    public void Play()
    {
        StartCoroutine(AsyncLoad());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            _progress = operation.progress / 0.9f;
            _scrollbar.size = _progress;
            yield return null;
        }
    }
}
