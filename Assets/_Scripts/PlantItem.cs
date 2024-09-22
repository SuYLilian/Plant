using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public string plantState;//seed,needWatering,flower,die
    public int plantNum;

    public Double growingTime_remain;//�Z������ɶ�a
    public Double needWateringTime_remain;//�Z���ݼ���ɶ�b
    public Double noWateringWithered_remain;//�Z���\��ɶ�(�S���)c        !!�H���������!!
    public Double haveGrownWithered_remain;//�Z���\��ɶ�(�w����)d
    public Double duration;//(�ɶ��t)e
    public Double needWateringTime_ori;//�Z���ݼ���ɶ�b-��l
    public Double noWateringWithered_ori;//�Z���\��ɶ�(�S���)c-��l
    public bool canCaculate = false;

    public DateTime pastTime, nowTime; //�ͦ����ɭԵ�pastTime����

    public Sprite dieSprite,flowerSprite;
    public Image plantImage;
    public GameObject wateringButton;
    public GameObject plantItem;
    public GameObject backpackItem, synthesisRoomItem;

    public GameObject backpackGroup, flowerGroup, illustratedBookGroup;

    public GameObject wateringAni;
    public Animator watering_ani;

    public GameObject notEnoughText;

    private void Awake()
    {
        backpackGroup = JasonManager.jasonManager.backpackGroup;
        illustratedBookGroup = JasonManager.jasonManager.illustratedBookGroup;
        flowerGroup = JasonManager.jasonManager.flowerListGroup;
        notEnoughText = JasonManager.jasonManager.notEnoughText;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCaculate)
        {
            Debug.Log("run");
            if (plantState == "seed")
            {
                Debug.Log("seed");

                IsSeedState();
            }
            else if (plantState == "needWatering")
            {
                Debug.Log("needWatering");

                IsNeedWateringState();
            }
            else if (plantState == "flower")
            {
                Debug.Log("flower");

                IsFlowerState();
            }
            else if (plantState == "die")
            {
                Debug.Log("die");

                IsDieState();
                canCaculate = false;
            }
            else
            {
                Debug.Log("else");

            }
        }
    }

    void IsSeedState()
    {
        Debug.Log("333");
        growingTime_remain -= Time.deltaTime;
        needWateringTime_remain -= Time.deltaTime;

        if (growingTime_remain <= 0)
        {
            plantState = "flower";
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            plantImage.sprite = flowerSprite;
            plantItem.GetComponent<Button>().enabled = true;

        }
        else if (needWateringTime_remain <= 0)
        {
            plantState = "needWatering";
            wateringButton.SetActive(true);
            needWateringTime_remain = needWateringTime_ori;
        }
    }
    void IsNeedWateringState()
    {
        growingTime_remain -= Time.deltaTime;
        noWateringWithered_remain -= Time.deltaTime;
        if (growingTime_remain <= 0)
        {
            plantState = "flower";
            wateringButton.SetActive(false);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            plantImage.sprite = flowerSprite;
            plantItem.GetComponent<Button>().enabled = true;

        }
        else if (noWateringWithered_remain <= 0)
        {
            plantState = "die";
            wateringButton.SetActive(false);
        }
    }
    void IsFlowerState()
    {
        haveGrownWithered_remain -= Time.deltaTime;
        if (haveGrownWithered_remain <= 0)
        {
            plantState = "die";
        }
    }
    void IsDieState()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
        plantImage.sprite = dieSprite;
        plantItem.GetComponent<Button>().enabled = true;
        wateringButton.SetActive(false);
    }

    public void ClickWateringButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        if (JasonManager.jasonManager.waterPropsNum>0)
        {
            wateringAni.SetActive(true);
            watering_ani.SetBool("isWatering", true);
            wateringButton.SetActive(false);
            needWateringTime_remain = needWateringTime_ori;
            noWateringWithered_remain = noWateringWithered_ori;
            plantState = "seed";

            JasonManager.jasonManager.waterPropsNum--;
            for (int j = 0; j < backpackGroup.transform.childCount; j++)
            {
                if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemType == "props" && backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemNum == 0)
                {
                    backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount--;
                    if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount <= 0)
                    {
                        Destroy(backpackGroup.transform.GetChild(j).gameObject);
                    }
                    else if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount > 0)
                    {
                        if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount == 1)
                        {
                            backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount_text.text = "";
                        }
                        else
                        {
                            backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount_text.text = backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount.ToString();
                        }
                    }
                }
            }
        }
        else
        {
            notEnoughText.SetActive(true);
            notEnoughText.GetComponent<Animator>().SetBool("isShow",true);
        }
        
    }
    public void ClickPlantItem()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        if (plantState == "flower")
        {
            bool haveFlowerInBackpack = false;
            GameObject flowerGrid_t = null;
            GameObject flowerGrid_syn = null;
            for (int i = 0; i < backpackGroup.transform.childCount; i++)
            {
                if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType == "flower")
                {
                    if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum == plantNum)
                    {
                        flowerGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                        haveFlowerInBackpack = true;
                        break;
                    }
                }
                else
                {
                    //flowerGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                    haveFlowerInBackpack = false;

                }
            }
          

            if (haveFlowerInBackpack)
            {
                //Ū���X���O�w�s�b����
                for (int i = 0; i < flowerGroup.transform.childCount; i++)
                {
                    if (flowerGroup.transform.GetChild(i).GetComponent<Item_SynthesisRoom>().flowerNum == plantNum)
                    {
                        flowerGrid_syn = flowerGroup.transform.GetChild(i).gameObject;
                        break;
                    }
                }
                //�b�I�]�s�W����ƶq
                flowerGrid_t.GetComponent<Item_Backpack>().itemAmount++;
                if (flowerGrid_t.GetComponent<Item_Backpack>().itemAmount >= 2)
                {
                    flowerGrid_t.GetComponent<Item_Backpack>().itemAmount_text.text = flowerGrid_t.GetComponent<Item_Backpack>().itemAmount.ToString();
                }
                flowerGrid_syn.GetComponent<Item_SynthesisRoom>().flowerAmount++;
                if (flowerGrid_syn.GetComponent<Item_SynthesisRoom>().flowerAmount >= 2)
                {
                    flowerGrid_syn.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = flowerGrid_syn.GetComponent<Item_SynthesisRoom>().flowerAmount.ToString();
                }
            }
            else
            {
                GameObject flower_backpack = Instantiate(backpackItem, backpackGroup.transform);//�b�I�]�s�W����
                flower_backpack.GetComponent<Item_Backpack>().itemType = "flower";
                flower_backpack.GetComponent<Item_Backpack>().itemNum = plantNum;
                flower_backpack.GetComponent<Item_Backpack>().itemAmount++;
                flower_backpack.GetComponent<Item_Backpack>().itemAmount_text.text = "";
                flower_backpack.GetComponent<Item_Backpack>().itemImage.sprite = flowerSprite;

                GameObject flower_syn = Instantiate(synthesisRoomItem, flowerGroup.transform);//�b�X���Ƿs�W����
                flower_syn.GetComponent<Item_SynthesisRoom>().flowerNum = plantNum;
                flower_syn.GetComponent<Item_SynthesisRoom>().flowerAmount++;
                flower_syn.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = "";
                flower_syn.GetComponent<Item_SynthesisRoom>().flowerImage.sprite = flowerSprite;
            }
            for(int i=0;i<illustratedBookGroup.transform.childCount;i++)//�Ϩ�����
            {
                if(illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().flowerNum==plantNum)
                {
                    if(illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().isUnlock==0)
                    {
                        illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().isUnlock = 1;
                        illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().lockImage.SetActive(false);
                    }
                    break;
                }
            }
            Destroy(gameObject);
        }
        else if (plantState == "die")
        {
            Destroy(gameObject);
        }
    }

}
