using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFlow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }
        public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }
}
