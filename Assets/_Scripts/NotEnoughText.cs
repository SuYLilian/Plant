using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughText : MonoBehaviour
{
   public void NotEnoughEvent()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("isShow", false);
    }
}
