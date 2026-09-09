using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInteract : MonoBehaviour
{
    [SerializeField] private Dictionary<GameObject, GameObject> keys = new Dictionary<GameObject, GameObject>();
    public LayerMask keyLayer;
    public Text keyText;

    public string keyLookedAt = "";
    public float maxRayDistance;
    public float offsetDistance;

    void Update()
    {
        FireRaycast();
        
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
            keyLookedAt = "";
            keyText.text = "";
        }

    }

}
