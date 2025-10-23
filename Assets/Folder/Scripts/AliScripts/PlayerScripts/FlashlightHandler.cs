using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightHandler : MonoBehaviour
{
    [Header("References")]
    public Transform flashlight;
    public Rigidbody flashlightRb;
    public Transform holdPoint;
    public Transform cameraTransform;
    private Collider flashlightCollider;

    [Header("Camera")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private GameObject interactUI;

    [Header("Settings")]
    public float pickUpDistance = 3f;

    private bool isHolding = false;

    void Start()
    {
        isHolding = flashlight.parent == holdPoint;

        flashlightCollider = flashlight.GetComponent<Collider>();
        if (flashlightCollider != null)
        {
            flashlightCollider.isTrigger = true;
        }
    }
    public void OnFLToggle(InputValue value)
    {
        if (!value.isPressed) return;

        if (isHolding)
        {
            Debug.Log("Flashlight Dropped");
            DropFlashlight();
            return;
        }

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, pickUpDistance))
        {
            // if (interactUI = null) return;

            if (hit.collider.transform == flashlight)
            {
                // interactUI.gameObject.SetActive(true);
                PickUpFlashlight();
            }
            // else
            // {
            //     interactUI.gameObject.SetActive(false);
            // }
        }
        else
        {
            interactUI.gameObject.SetActive(false);
        }
    }
    public void PickUpFlashlight()
    {
        flashlight.SetParent(holdPoint);
        flashlight.localPosition = Vector3.zero;
        flashlight.localRotation = Quaternion.identity;
        flashlightRb.isKinematic = true;
        if (flashlightCollider != null)
        {
            flashlightCollider.isTrigger = true;
        }

        isHolding = true;
        Debug.Log($"Flashlight Picked Up and is holding: {isHolding}");
    }

    public void DropFlashlight()
    {
        flashlight.SetParent(null);
        flashlightRb.isKinematic = false;

        flashlightRb.linearVelocity = Vector3.zero;
        flashlightRb.angularVelocity = Vector3.zero;
        if (flashlightCollider != null)
        {
            flashlightCollider.isTrigger = false;
        }

        isHolding = false;
        Debug.Log($"Flashlight Dropped and is holding: {isHolding}");
    }
}