using UnityEngine;

public class StateManager : MonoBehaviour
{
    void Start(){
        /* states : 
        "" -- used for both menu and house
        intro
        gameover
        game won
        */
        string state = SceneSwitch.state;
        
        if (state.Equals("intro")){
            // run intro
        } else if (state.Equals("gameover")){
            // run gameover
        } else if (state.Equals("game won")){
            // run game won
        }

    }
}
