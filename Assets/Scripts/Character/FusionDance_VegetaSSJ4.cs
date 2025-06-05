using UnityEngine;

public class FusionDance_VegetaSSJ4 : MonoBehaviour
{
    public void Action_Fusion_Dance()
    {
        transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, 0);
    }

    public void End_Action_Fusion_Dance()
    {
        Destroy(gameObject);
    }
}
