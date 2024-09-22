using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public SynManager synManager;

    private void Awake()
    {
        synManager = SynManager.synManager; 
    }

    public void AniEvent()
    {
        synManager.CloudAniEvent();
    }
}
