using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    /* states : 
        "" -- used for both menu and house
        intro
        gameover
        game won
    */
    public static string state = "";

    public static void LoadScene(string name){
        LoadScene(name, "");
    }

    public static void LoadScene(string name, string newState){
        SceneManager.LoadScene(name);
        state = newState;
    }

    public void LoadHouse(GameObject overlay){
        overlay.SetActive(true);
        LoadScene("State", "intro");
    }

}
