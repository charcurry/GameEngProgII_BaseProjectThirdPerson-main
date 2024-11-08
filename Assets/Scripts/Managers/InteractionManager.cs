using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public Camera playerCamera;

    public int maxRayDistance;

    public UIManager uIManager;

    public Text pickups;
    public int pickupsCount;

    [SerializeField]
    private GameObject target;

    private Interactable targetInteractable;

    public bool interactionPossible;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        uIManager = FindObjectOfType<UIManager>();
        playerCamera = cameraManager.playerCamera;

    }

    // Start is called before the first frame update
    void Start()
    {
        interactionPossible = true;
        pickupsCount = 0;
        pickups.text = pickupsCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        pickups.text = pickupsCount.ToString();
    }

    public IEnumerator ResetInteractibility()
    {
        yield return new WaitForSeconds(0.5f);
        interactionPossible = true;
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.forward * maxRayDistance, Color.green);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            if (hit.transform.gameObject.CompareTag("Interactable"))
            {
                //Debug.Log("Looking at " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
            }
            else
            {
                target = null;
                targetInteractable = null;
            }
            SetGameplayMessage();
        }
    }

    public void Interact()
    {
        if (targetInteractable != null)
        {
            switch (targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    target.SetActive(false);
                    break;
                case Interactable.InteractionType.Button:
                    Debug.Log("Pressed " + target.name);
                    break;
                case Interactable.InteractionType.Pickup:
                    target.SetActive(false);
                    pickupsCount++;
                    break;
            }
            interactionPossible = false;
            StartCoroutine(ResetInteractibility());
        }
    }

    private void SetGameplayMessage()
    {
        string message = "";
        if (target != null)
        {
            switch(targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    message = "Press LMB to open door";
                    break;
                case Interactable.InteractionType.Button:
                    message = "Press LMB to press button";
                    break;
                case Interactable.InteractionType.Pickup:
                    message = "Press LMB to pickup object";
                    break;
            }
        }
        uIManager.UpdateGameplayMessage(message);
    }
}
