using UnityEngine;

[CreateAssetMenu(menuName ="Parametres", fileName = "")]
public class GamePlayParametres : ScriptableObject
{
    public bool isPause = false;
    public bool canPause = true;
    public bool isCursorVisible;

    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorOn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
