using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void Normal()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Easy()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Hard()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
