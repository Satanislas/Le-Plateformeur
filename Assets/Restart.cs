using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public static void RestartGame() => SceneManager.LoadScene("LevelParser");
    

    public static void Quit() => Application.Quit();
}
