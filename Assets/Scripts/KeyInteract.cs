using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyInteract : MonoBehaviour
{
    [SerializeField] private InputActionReference pickUpKeyAction;
    [SerializeField] private InputActionReference dropKeyAction;
    public LayerMask keyLayer;
    public Text keyText;

    public GameObject keyLookedAt = null;
    public GameObject keyPicked = null;
    //[SerializeField] private Quaternion keyRotation= new Quaternion.Euler(250, -185, 60);
    public float maxRayDistance;
    public float offsetDistance;

    [SerializeField] List<Vector3> newKeyProperties = new List<Vector3>(){
        new Vector3(0.35f, -0.15f, 0.5f),
        new Vector3(250f, -185f, 60f),
        new Vector3(3f, 3f, 3f)
    };

    void Update()
    {
        FireRaycast();

        if (keyLookedAt is not null && pickUpKeyAction.action.WasPressedThisFrame()){
            PickUpKey(keyLookedAt.GetComponent<Transform>(), keyLookedAt.GetComponent<MeshCollider>(),
                    keyLookedAt.GetComponent<Rigidbody>());
            keyPicked = keyLookedAt;
        }
        if (keyPicked is not null && dropKeyAction.action.WasPressedThisFrame()){
            DropKey(keyPicked.GetComponent<Transform>(), keyPicked.GetComponent<MeshCollider>(),
                    keyPicked.GetComponent<Rigidbody>());
            keyPicked = null;
        }
    }

    void FireRaycast(){
        Vector3 direction = transform.forward;
        Vector3 orgin = transform.position + direction.normalized * offsetDistance;

        RaycastHit hit;

        Debug.DrawRay(orgin, direction * maxRayDistance, Color.red);

        if (Physics.Raycast(orgin, direction, out hit, maxRayDistance, keyLayer))
        {
            keyLookedAt = hit.collider.gameObject;
            keyText.text = keyLookedAt.GetComponent<MeshCollider>().name + " (E)";

        }
        else
        {
            keyLookedAt = null;
            keyText.text = "";
        }
    }

    private void PickUpKey(Transform keyPos, MeshCollider keyColl, Rigidbody keyRb){
        Debug.Log("Picked up key: " + keyColl.name);
        keyColl.enabled = false;
        keyPos.SetParent(transform);
        keyRb.useGravity = false;
        keyPos.localPosition = newKeyProperties[0];
        keyPos.localEulerAngles = newKeyProperties[1];
        keyPos.localScale = newKeyProperties[2];
    }

    private void DropKey(Transform keyPos, MeshCollider keyColl, Rigidbody keyRb){
        keyRb.useGravity = true;
        keyPos.SetParent(null);
        keyColl.enabled = true;
        Debug.Log("Dropped key: " + keyColl.name);
    }

}
