using UnityEngine;

public class ValveTurn : MonoBehaviour
{
    public Animator animator;
    public float rotSpeed = 90f;
    public Vector3 rotAxis = Vector3.down; //direction

    private bool turnning = false;
    private float currentRot = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnning)
        {
            float rotAmount = rotSpeed * Time.deltaTime;
            transform.Rotate(rotAxis * rotAmount, Space.Self);
            currentRot += rotAmount;
        }
    }

    public void startTurnning()
    {
        turnning = true;
        animator.SetTrigger("DoorUnlock");
    }

}
