using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
   public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

   public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
