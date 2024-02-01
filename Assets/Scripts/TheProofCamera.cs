using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheProofCamera : MonoBehaviour
{
    [SerializeField, Header("Žæ“¾‰Â”\”ÍˆÍ"), Range(1, 5)]
    int range;
    [SerializeField, Header("Player's LayerMask")]
    LayerMask lMask;

    private void FixedUpdate()
    {
        var cond = Physics.CheckSphere(transform.position, range, lMask);
        if (cond)
        {
            var flag = GameObject.FindObjectOfType<FlagManager>();
            flag.AddActiveItem(0);
            Destroy(this.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
