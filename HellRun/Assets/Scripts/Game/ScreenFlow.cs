using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFlow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void CompleteLevel()
    {
        if (Services.Settings.VibrationOn)
        {
            Handheld.Vibrate();
            Debug.Log("Vibration triggered on level completion.");
        }
            
    }

        public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }
}
