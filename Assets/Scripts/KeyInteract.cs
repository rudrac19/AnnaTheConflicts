using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour
{
    public List<GameObject> keys = new List<GameObject>();
    public List<GameObject> keyUI = new List<GameObject>();

    void Update()
    {
        // Use Raycast to see if the player is looking at the key or not
        // if the player is then uipdate the ui to show the key
        // the then the player can grab the key
        // Raycast will go on abother scipt
        // the same raycast will be used in the doors, or the other thinds
        // maybe add interactable lightes after post-porcessing is done.
    }

}
