using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheProofItem : MonoBehaviour
{
    [SerializeField, Header("�擾�\�͈�"), Range(1, 5)]
    int range;
    [SerializeField, Header("�C���f�b�N�X"), Range(0, 2)]
    int index;
    [SerializeField, Header("������̗F�i�r")]
    GameObject TomoNavi;
    [SerializeField, Header("Player's LayerMask")]
    LayerMask lMask;

    public int Index => index;

    private void FixedUpdate()
    {
        var cond = Physics.CheckSphere(transform.position, range, lMask);
        if (cond)
        {
            var flag = GameObject.FindObjectOfType<FlagManager>();
            flag.AddActiveItem(index);
            if (TomoNavi != null)
            {
                var go = Instantiate(TomoNavi);
                go.transform.position = Vector3.zero;
            }
            Destroy(this.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}