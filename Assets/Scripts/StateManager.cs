using UnityEngine;

public class StateManager : MonoBehaviour
{
    [Header("Intro Properties")]
    [SerializeField] GameObject introAnimation;
    public float introDuration = 60f;

    [Header("Game Over Properties")]
    [SerializeField] GameObject overAnimation;
    public float gameOverDuration = 10f;

    [Header("Game Won Properties")]
    [SerializeField] GameObject wonAnimation;
    public float gameWonDuration = 30f;

    private float timeLeft = 60f;
    private string nextScene = "MainMenu";

    void Start(){
        /* states : 
        "" -- used for both menu and house
        intro
        gameover
        gamewon
        */
        string state = SceneSwitch.state;
        
        if (state.Equals("gameover")){
            // run game over
            overAnimation.SetActive(true);
            timeLeft = gameOverDuration;
            nextScene = "MainMenu";
        } else if (state.Equals("gamewon") && wonAnimation != null){
            // run game won
            wonAnimation.SetActive(true);
            timeLeft = gameWonDuration;
            nextScene = "MainMenu";
        } else {
            // run intro
            introAnimation.SetActive(true);
            timeLeft = introDuration;
            nextScene = "House";

        }

    }
    
    void Update(){
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0){
            SceneSwitch.LoadScene(nextScene);
        }
    }
}
