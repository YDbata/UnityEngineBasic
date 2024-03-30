using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyReycast : MonoBehaviour
{
    [Header("Raycast Radius And Layer")]
    [SerializeField] private int rayRadius;
    [SerializeField] private LayerMask layerMaskCollective;
    [SerializeField] private string banLayerName = null;

    [SerializeField] private KeyObject raycastedObject;
    [SerializeField] private KeyCode InteractionKey = KeyCode.F;
    [SerializeField] private Image crosshair;

    private string collectiveTag = "CollectiveObject";

    private bool checkCrosshair;
    private bool OneTime;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitinfo;

        Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(banLayerName) | layerMaskCollective.value;
        if(Physics.Raycast(transform.position, forwardDirection, out hitinfo, rayRadius, mask))
        {
            if(hitinfo.collider.CompareTag(collectiveTag))
            {
                if(!OneTime)
                {
                    raycastedObject = hitinfo.collider.gameObject.GetComponent<KeyObject>();
                    ChangeCrosshair(true);
                }
                checkCrosshair = true;
                OneTime = true;

                if (Menus.InteractionButtonClicked){//Input.GetKeyDown(InteractionKey)){
                    if(raycastedObject) raycastedObject.FoundObject();
                }
            }
        }
        else
        {
            if(checkCrosshair)
            {
                ChangeCrosshair(false);
                OneTime = false;
            }
        }
    }

    private void ChangeCrosshair(bool change)
    {
        if(change && !OneTime)
        {
            crosshair.color = Color.blue;
        }
        else
        {
            crosshair.color = Color.white;
            checkCrosshair = false;
        }
    }
}
