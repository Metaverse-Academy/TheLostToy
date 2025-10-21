using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    public Transform flashlight;
    public Rigidbody flashlightRb;
    public Transform cameraTransform;

    public void DropFlashlight()
    {
        flashlight.SetParent(null);
        flashlightRb.isKinematic = false;
    }

    public void PickUpFlashlight()
    {
        flashlight.SetParent(cameraTransform);
        flashlight.localPosition = new Vector3(0, 0, 0.5f);
        flashlight.localRotation = Quaternion.identity;
        flashlightRb.isKinematic = true;
    }
}