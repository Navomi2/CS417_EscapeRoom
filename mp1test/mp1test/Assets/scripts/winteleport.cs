using UnityEngine;

public class winteleport : MonoBehaviour
{
   
    public Transform winposition;

    
    private void OnTriggerEnter(Collider other)
    {
        
        
        
            
            other.transform.position = winposition.position;
            other.transform.rotation = winposition.rotation;

            Debug.Log("Player Teleported to Win Area!");
        
    }
}
