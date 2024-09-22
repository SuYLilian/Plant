using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlantUIManagerment : MonoBehaviour
{
    public GameObject plantWindow, runWindow, background;
    public Canvas plantCanvas;
    public LevelManager levelManager;
    public GameObject player;
    public PlayerController playerController;
    public PlayerTouch playerTouch;

    public SeedStore seedStore;
    public GameObject storePanel, storeCanvas;

    public Animator seedList_ani,backpack_ani, illustratedBook_ani;

    public Animator propsTitle_ani, seedTitle_ani;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerController>().gameObject;
       // levelManager = FindObjectOfType<LevelManager>();
    }

    public void ClickStarButton()
    {
        //plantWindow.SetActive(false);
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        FindObjectOfType<SoundManager>().gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<SoundManager>().bgmClips[1];
        FindObjectOfType<SoundManager>().audioSource.Play();
        plantCanvas.enabled = false;
        player.transform.position=playerController.playerOriginalPos;
        background.GetComponent<LoppingBG>().startPos = playerController.backGroundOriginalPos.y;
        for (int i = 0; i < 2; i++)
        {
            int r = Random.Range(0, levelManager.levels.Length);

            GameObject newLevel = Instantiate(levelManager.levels[r], levelManager.levelParent.transform);
            newLevel.transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y + levelManager.levelSpace * (i + 1.5f), newLevel.transform.localPosition.z);
        }
        playerTouch.playerSpeed = playerTouch.originalPlayerSpeed;
        runWindow.SetActive(true);
        playerController.animator.SetBool("isDead", false);
        playerTouch.canDrag = true;
        //SceneManager.LoadScene("RunScene");
    }
    public void OpenSystem(GameObject system)
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        seedList_ani.SetBool("isOpen", false);
        system.SetActive(true);
    }
    public void CloseSystem(GameObject system)
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        system.SetActive(false);
    }
    public void ClickSeedListButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        seedList_ani.SetBool("isOpen", true);
    }
    public void ClickSeedListBackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        seedList_ani.SetBool("isOpen", false);
    }
    public void ClickBackpackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        backpack_ani.SetBool("isOpen", true);
        seedList_ani.SetBool("isOpen", false);
    }
    public void ClickBackpackBackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        backpack_ani.SetBool("isOpen", false);
    }
    public void ClickIllustratedBookButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        illustratedBook_ani.SetBool("isOpen", true);
        seedList_ani.SetBool("isOpen", false);
    }
    public void ClickIllustratedBookBackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        illustratedBook_ani.SetBool("isOpen", false);
    }
    public void ClickStoreIcon()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        seedList_ani.SetBool("isOpen", false);
        storeCanvas.SetActive(true);
        if (seedStore.propsTitleIsShow)
        {
            propsTitle_ani.SetBool("isShow", true);
            seedTitle_ani.SetBool("isShow", false);
        }
        else
        {
            propsTitle_ani.SetBool("isShow", false);
            seedTitle_ani.SetBool("isShow", true);
        }
        for (int i=0;i<seedStore.storeSeed_ani.Length;i++)
        {
            if(i!=seedStore.seedIndex)
            {
                seedStore.storeSeed_ani[i].SetBool("ClickItem", false);
            }
            else
            {
                seedStore.storeSeed_ani[i].SetBool("ClickItem", true);
            }
        }
        for (int i = 0; i < seedStore.storeProps_ani.Length; i++)
        {
            if (i != seedStore.propsIndex)
            {
                seedStore.storeProps_ani[i].SetBool("ClickItem", false);
            }
            else
            {
                seedStore.storeProps_ani[i].SetBool("ClickItem", true);
            }
        }
    }
}
