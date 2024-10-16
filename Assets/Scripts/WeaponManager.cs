using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask cubeFilter;
    [SerializeField] private LayerMask groundFilter;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = cameraManager.playerCamera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = 10;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 10, groundFilter.value))
        {
            distance = hit.distance;
        }

        Debug.Log("Distance: " + distance);

        Vector3 camPos = playerCam.transform.position;
        Debug.DrawLine(camPos, camPos + playerCam.transform.forward * 10, Color.red);
        //Debug.Log(hit.collider.name);

        //if (hit.collider.TryGetComponent(out Renderer renderer))
        //{
        //    renderer.material.color = Color.red;
        //}

        RaycastHit[] hits = Physics.RaycastAll(playerCam.transform.position, playerCam.transform.forward, distance, cubeFilter.value);

        foreach (RaycastHit raycastHit in hits)
        {
            if (raycastHit.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.red;
            }
        }
        Debug.Log("Hit " + hits.Length + " Cubes");
    }
}
