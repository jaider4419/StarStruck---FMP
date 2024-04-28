using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditOver : MonoBehaviour
{
    public string sceneToLoad;

    private Animator animator;
    private bool hasAnimationFinished;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Ensure animator component exists
        if (animator == null)
        {
            Debug.LogError("No Animator component found on GameObject.");
            return;
        }

        // Subscribe to animation events
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.time = clip.length; // Set the event time to the end of the animation
            animationEvent.functionName = "OnAnimationFinish"; // Call OnAnimationFinish function
            clip.AddEvent(animationEvent);
        }
    }

    // Method to be called when animation finishes
    void OnAnimationFinish()
    {
        if (!hasAnimationFinished)
        {
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
            hasAnimationFinished = true;
        }
    }
}


