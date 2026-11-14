using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);       // Services survives every scene change

        // Start every service here, in order (next follow-along)
        Services.Save = new PlayerPrefsSaveService();
        Services.Settings = new SettingsService(Services.Save);

        Debug.Log("Bootstrap: services ready");
        SceneManager.LoadScene("Home");      // only now: the first screen
    }

}
