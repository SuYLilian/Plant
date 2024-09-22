using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAni : MonoBehaviour
{
    PlantItem plantItem;

    public void WaterEvent()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("isWatering", false);
    }
}
