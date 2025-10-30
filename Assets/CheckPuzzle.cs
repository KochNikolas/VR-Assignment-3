using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Oculus.Interaction;
using Oculus.Interaction.Collections;

// Unity Script (C#)
public class CheckPuzzle : MonoBehaviour
{
    // The target puzzle goal
    public Transform targetPuzzle;

    // Boolean variable to indicate whether this puzzle is solved or not
    public bool isSolved;

    // In this lab, we will read components of this game object
    private AudioSource audioSource;
    private Rigidbody rigidbody;
    private BoxCollider boxCollider;

    void Start()
    {
        // By using 'GetComponent<>()' function, we can read a component of this game object
        // The below lines read audio source, rigid body, and box collider
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // We can calculate the distance between points in 3D space by using Vector3.Distance(Vector3 A, Vector3 B)
        float distance = Vector3.Distance(transform.position, targetPuzzle.position);

        // If the distance between this puzzle piece and puzzle goal is less than 0.05f and it is the first time to detect it
        if (distance < 0.05f && !isSolved)
        {
            // These two lines force the controller to release the grabbed puzzle piece
            // The first line accesses the first child object which has 'GrabInteractable' script and reads one of its variables, 'Interactors'
            // The second line checks each interactable variable in 'Interactors' and runs Unselect()
            IEnumerable<GrabInteractor> setInteractors = transform.GetChild(0).GetComponent<GrabInteractable>().Interactors;
            foreach (GrabInteractor interactor in setInteractors) { interactor.Unselect(); }

            // To place the puzzle piece with the right position and orientation
            transform.SetPositionAndRotation(targetPuzzle.position, targetPuzzle.rotation);
            targetPuzzle.gameObject.SetActive(false);

            // To fix it on the position, we set the rigidbody to freeze all constraints
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            // To disable grabbing again, we turn off the box collider
            boxCollider.enabled = false;

            // For our sound effect, we play an audio clip
            audioSource.Play();

            // To run the above lines only one time, we set 'isSolved' true
            isSolved = true;
        }
    }
}