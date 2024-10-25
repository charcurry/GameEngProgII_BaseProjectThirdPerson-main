using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask cubeFilter;
    [SerializeField] private LayerMask groundFilter;

    public float distance;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = cameraManager.playerCamera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 10, groundFilter.value))
        //{
        //    distance = hit.distance;
        //}

        //Debug.Log("Distance: " + distance);

        Vector3 camPos = playerCam.transform.position;
        Debug.DrawLine(camPos, camPos + playerCam.transform.forward * 10, color);
        //Debug.Log(hit.collider.name);

        //if (hit.collider.TryGetComponent(out Renderer renderer))
        //{
        //    renderer.material.color = color.red;
        //}

        RaycastHit[] hits = Physics.RaycastAll(playerCam.transform.position, playerCam.transform.forward, distance, cubeFilter.value);

        foreach (RaycastHit raycastHit in hits)
        {
            if (raycastHit.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.red;
                Debug.Log("Name: " + raycastHit.collider.name);
                Debug.Log("Position: " + raycastHit.collider.transform.position);
                Debug.Log("Distance: " + raycastHit.distance);
            }
        }
        //Debug.Log("Hit " + hits.Length + " Cubes");
    }
}
