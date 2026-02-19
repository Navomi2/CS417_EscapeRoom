using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// If using newer XR Toolkit versions (2.x+), you might need:
// using UnityEngine.XR.Interaction.Toolkit.Interactables; 

public class MultiSocketDoor : MonoBehaviour
{
    [Header("Assign Components")]
    public Animator animator;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket1; // Drag Socket 1 here
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket2; // Drag Socket 2 here

    [Header("Key Names")]
    public string key1Name = "USBKey_Yellow";
    public string key2Name = "USBKey_Red";

    void Start()
    {
        // Listen for when items are plugged in
        socket1.selectEntered.AddListener(CheckSockets);
        socket2.selectEntered.AddListener(CheckSockets);

        // Optional: Listen for when items are removed (so door closes or resets)
        socket1.selectExited.AddListener(CheckSockets);
        socket2.selectExited.AddListener(CheckSockets);
    }

    public void CheckSockets(SelectEnterEventArgs args)
    {
        // Run the check logic
        CheckBothSockets();
    }

    // Overload for selectExited
    public void CheckSockets(SelectExitEventArgs args)
    {
        CheckBothSockets();
    }

    private void CheckBothSockets()
    {
        // 1. Check if Socket 1 has the correct key
        bool socket1Correct = IsCorrectKey(socket1, key1Name) || IsCorrectKey(socket1, key2Name);

        // 2. Check if Socket 2 has the correct key
        bool socket2Correct = IsCorrectKey(socket2, key1Name) || IsCorrectKey(socket2, key2Name);

        // 3. If BOTH are true, open the door
        if (socket1Correct && socket2Correct)
        {
            OpenDoor();
        }
    }

    private bool IsCorrectKey(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket, string validName)
    {
        // If socket is empty, return false
        if (!socket.hasSelection) return false;

        // Get the object inside (Compatible with XR Toolkit 2.X)
        var obj = socket.firstInteractableSelected;

        if (obj != null)
        {
            // Check if the name matches the required key
            string objName = obj.transform.name;
            return objName.Contains(validName);
        }

        return false;
    }

    public void OpenDoor()
    {
        Debug.Log("Both Keys Inserted! Opening Door.");
        animator.SetTrigger("DoorUnlock");
    }
}
