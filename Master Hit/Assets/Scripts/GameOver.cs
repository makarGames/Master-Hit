using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver S;

    private void Awake()
    {
        if (S == null)
            S = this;
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
