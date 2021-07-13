using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Collider))]
public class RotationBehaviour : MonoBehaviour
{
    // Whether the object is currently grabbed
    private bool isGrabbed = false;

    // The rotation when the object is grabbed
    private Quaternion currentRotation;

    // The XR Interactor that is grabbing the object
    private Transform currentGrabInteractor;

    // The distance from the object to the grabbing interactor when the object is grabbed
    private Vector3 startDistance;

    // The clostest point from the object to the grabbing interactor during the rotation
    private Vector3 clostestPoint = Vector3.zero;

    // The XRGrabInteractable component necessary to perceive the grab interaction. Used to set the necessary options.
    private XRGrabInteractable interactable;

    // The Rigidbody component necessary to make the XRGrabInteractable working. Used to set the necessary options.
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        // Save the current rotation
        currentRotation = transform.rotation;

        // Setup interactable component
        interactable = GetComponent<XRGrabInteractable>();
        interactable.trackPosition = false;
        interactable.trackRotation = false;
        interactable.throwOnDetach = false;
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
        interactable.enabled = true;


        // Setup rigid body component
        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.useGravity = false;
        rigidbodyComponent.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            // While the object is grabbed, compute the distance between the object and the interactor to compute the corresponding object rotation
            clostestPoint = Vector3.Normalize(currentGrabInteractor.transform.position - transform.position);
            transform.rotation = Quaternion.FromToRotation(startDistance, clostestPoint) * currentRotation;
        }
    }

    /// <summary>
    /// Function that is called when the grab starts to set the current parameters of this object.
    /// </summary>
    /// <param name="interactor">The grabbing interactor</param>
    public void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        currentGrabInteractor = args.interactor.transform;
        startDistance = Vector3.Normalize(args.interactor.transform.position - transform.position);
        currentRotation = transform.rotation;
    }

    /// <summary>
    /// Function that is called when the grab stops to clear the grab-relevant parameters of this object.
    /// </summary>
    /// <param name="interactor">The grabbing interactor</param>
    public void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        currentGrabInteractor = null;
        startDistance = Vector3.zero;
    }

}
