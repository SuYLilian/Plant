using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUIManagerment : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject anabiosisPanel;
    public int coinCosumeNum;
    public void ClickYesButton()
    {
        //���a������-coinConsumeNum(���a���������O�]�w�������)
        anabiosisPanel.SetActive(false);
    }
}
