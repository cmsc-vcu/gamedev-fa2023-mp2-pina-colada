using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabbedObj;

    // Update is called once per frame
    void Update()
    {
        Vector3 localScale = transform.localScale;
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, localScale, rayDistance);

        if(hitInfo.collider!=null && hitInfo.collider.gameObject.tag == "Object")
        {
            if(Input.GetKeyDown(KeyCode.F) && grabbedObj == null)
            {
                grabbedObj=hitInfo.collider.gameObject;
                grabbedObj.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObj.transform.position=grabPoint.position;
                grabbedObj.transform.SetParent(transform);
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                grabbedObj.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObj.transform.SetParent(null);
                grabbedObj = null;
            }
        }
    }
}
