using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touched Friendly!");
                break;
            case "Finish":
                Debug.Log("Touched Finish!");
                break;
            case "Fuel":
                Debug.Log("Touched Fuel!");
                break;
            default:
                Debug.Log("hoho, you loose!");
                break;
        }
    }
}
