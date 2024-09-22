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
        //玩家的金幣-coinConsumeNum(玩家的金幣不是跑庫裡獲取的)
        anabiosisPanel.SetActive(false);
    }
}
