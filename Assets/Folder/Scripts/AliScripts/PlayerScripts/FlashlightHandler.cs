using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightHandler : MonoBehaviour
{
    [Header("References")]
    public Transform flashlight;
    public Rigidbody flashlightRb;
    public Transform holdPoint;
    public Transform cameraTransform;

    [Header("Settings")]
    public float pickUpDistance = 3f;

    private bool isHolding = false;

    void Start()
    {
        isHolding = flashlight.parent == holdPoint;
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
            if (hit.collider.transform == flashlight)
            {
                PickUpFlashlight();
            }
        }
    }

    private void PickUpFlashlight()
    {
        flashlight.SetParent(holdPoint);
        flashlight.localPosition = Vector3.zero;
        flashlight.localRotation = Quaternion.identity;
        flashlightRb.isKinematic = true;

        isHolding = true;
    }

    private void DropFlashlight()
    {
        flashlight.SetParent(null);
        flashlightRb.isKinematic = false;

        flashlightRb.linearVelocity = Vector3.zero;
        flashlightRb.angularVelocity = Vector3.zero;

        isHolding = false;
    }
}