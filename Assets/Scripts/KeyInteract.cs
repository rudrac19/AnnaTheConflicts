using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyInteract : MonoBehaviour
{
    [SerializeField] private Dictionary<GameObject, GameObject> keys = new Dictionary<GameObject, GameObject>();
    [SerializeField] private InputActionReference pickUpKeyAction;
    public LayerMask keyLayer;
    public Text keyText;

    public string keyLookedAt = null;
    public float maxRayDistance;
    public float offsetDistance;

    void Update()
    {
        FireRaycast();

        if (keyLookedAt is not null && pickUpKeyAction.action.WasPressedThisFrame()){
            PickUpKey();
        }
    }

    void FireRaycast(){
        Vector3 direction = transform.forward;
        Vector3 orgin = transform.position + direction.normalized * offsetDistance;

        RaycastHit hit;

        Debug.DrawRay(orgin, direction * maxRayDistance, Color.red);

        if (Physics.Raycast(orgin, direction, out hit, maxRayDistance, keyLayer))
        {
            keyLookedAt = hit.collider.name;
            keyText.text = keyLookedAt + " (E)";

        }
        else
        {
            keyLookedAt = null;
            keyText.text = "";
        }
    }

    private void PickUpKey(){
        Debug.Log("Picked up key: " + keyLookedAt);
    }

}
