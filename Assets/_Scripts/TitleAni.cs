using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAni : MonoBehaviour
{

    public Animator title_ani;

    public void TitleAniEvent()
    {
        title_ani.SetBool("isShow", false);
    }
}
