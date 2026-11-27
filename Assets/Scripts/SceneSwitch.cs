using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public void LoadHouse(GameObject overlay){
        overlay.SetActive(true);
        LoadScene("House");
    }

}
