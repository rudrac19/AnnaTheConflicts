using UnityEngine;

public class StateManager : MonoBehaviour
{
    [Header("Intro Properties")]
    [SerializeField] bool intro = true;
    public float introDuration = 60f;

    [Header("Game Over Properties")]
    [SerializeField] bool over = false;
    public float gameOverDuration = 10f;

    [Header("Game Won Properties")]
    [SerializeField] bool won = false;
    public float gameWonDuration = 10f;

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
            timeLeft = gameOverDuration;
            nextScene = "MainMenu";
        } else if (state.Equals("gamewon")){
            // run game won
            timeLeft = gameWonDuration;
            nextScene = "MainMenu";
        } else {
            // run intro
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
