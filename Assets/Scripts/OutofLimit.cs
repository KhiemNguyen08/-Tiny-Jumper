using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(Tagconst.PLATFORM))
        {
            Destroy(col.gameObject);
        }
    }
}
