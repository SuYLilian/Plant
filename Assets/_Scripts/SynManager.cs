using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SynManager : MonoBehaviour
{
    public static SynManager synManager;
    public GameObject synFrame;
    //public GameObject synFrame_0, synFrame_1;

    public Rate[] rates = new Rate[2];//0是成功，1是失敗
    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    public TextMeshProUGUI rate_text;

    public Image synFlowerImage;
    public Sprite[] flowerSprites;
    public GameObject synAniPanel,successAniImage,loseImage;
    public Animator cloud_ani, flower_ani, loseCloud_ani;
    public GameObject flowerGroup, backpackGroup, illustratedBookGroup;
    public GameObject synFlowerListItem_prefab, backpackItem_prefab;

    private void Start()
    {
        synManager = this;
        flowerGroup = JasonManager.jasonManager.flowerListGroup;
        backpackGroup = JasonManager.jasonManager.backpackGroup;
        illustratedBookGroup = JasonManager.jasonManager.illustratedBookGroup;
        /*CaculateWeights();
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(GetRandomRateIndex());
        }*/
    }

    public void ClickSynButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        bool haveEmpty = false;
        int r = 0;
        for (int i = 0; i < synFrame.transform.childCount; i++)
        {
            if (synFrame.transform.GetChild(i).transform.childCount <= 0)
            {
                haveEmpty = true;
                break;
            }
            else
            {
                haveEmpty = false;
            }
        }
        if (!haveEmpty)
        {
            CaculateWeights();
            r = GetRandomRateIndex();

            if(r==0)//成功
            {
                successAniImage.SetActive(true);
                int synFlowerNum = FlowerCombination(synFrame.transform.GetChild(0).GetChild(0).GetComponent<SynGrid_Item>().flowerNum,
                                                     synFrame.transform.GetChild(1).GetChild(0).GetComponent<SynGrid_Item>().flowerNum);
                synFlowerImage.sprite = flowerSprites[synFlowerNum];
                synAniPanel.SetActive(true);
                cloud_ani.SetBool("isOpenCloud", true);
                Destroy(synFrame.transform.GetChild(0).GetChild(0).gameObject);
                Destroy(synFrame.transform.GetChild(1).GetChild(0).gameObject);

                bool haveFlowerInBackpack = false;
                GameObject flowerGrid_t = null;
                GameObject flowerGrid_flowerGroup = null;

                for (int i = 0; i < backpackGroup.transform.childCount; i++)
                {
                    if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType == "flower")
                    {
                        if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum == synFlowerNum)
                        {
                            flowerGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                            haveFlowerInBackpack = true;
                            break;
                        }
                    }
                    else
                    {
                        //propsGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                        haveFlowerInBackpack = false;

                    }
                }

                if (haveFlowerInBackpack)
                {
                    //在背包新增物件數量
                    flowerGrid_t.GetComponent<Item_Backpack>().itemAmount++;
                    if (flowerGrid_t.GetComponent<Item_Backpack>().itemAmount >= 2)
                    {
                        flowerGrid_t.GetComponent<Item_Backpack>().itemAmount_text.text = flowerGrid_t.GetComponent<Item_Backpack>().itemAmount.ToString();
                    }
                    for (int i = 0; i < flowerGroup.transform.childCount; i++)
                    {
                        if (flowerGroup.transform.GetChild(i).GetComponent<Item_SynthesisRoom>().flowerNum == synFlowerNum)
                        {
                            flowerGrid_flowerGroup = flowerGroup.transform.GetChild(i).gameObject;
                            flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount++;
                            if (flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount >= 2)
                            {
                                flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount.ToString();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    GameObject flower_backpack = Instantiate(backpackItem_prefab, backpackGroup.transform);//在背包新增物件
                    flower_backpack.GetComponent<Item_Backpack>().itemType = "flower";
                    flower_backpack.GetComponent<Item_Backpack>().itemNum = synFlowerNum;
                    flower_backpack.GetComponent<Item_Backpack>().itemAmount++;
                    flower_backpack.GetComponent<Item_Backpack>().itemAmount_text.text = "";
                    flower_backpack.GetComponent<Item_Backpack>().itemImage.sprite = flowerSprites[synFlowerNum];

                    GameObject flower_synList = Instantiate(synFlowerListItem_prefab, flowerGroup.transform);//在合成室列表新增花
                    flower_synList.GetComponent<Item_SynthesisRoom>().flowerNum = synFlowerNum;
                    flower_synList.GetComponent<Item_SynthesisRoom>().flowerAmount++;
                    flower_synList.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = "";
                    flower_synList.GetComponent<Item_SynthesisRoom>().flowerImage.sprite = flowerSprites[synFlowerNum];
                }
                for (int i = 0; i < illustratedBookGroup.transform.childCount; i++)//圖見解鎖
                {
                    if (illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().flowerNum == synFlowerNum)
                    {
                        if (illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().isUnlock == 0)
                        {
                            illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().isUnlock = 1;
                            illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().lockImage.SetActive(false);
                        }
                        break;
                    }
                }
                FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[1]);

                synManager.rate_text.text = "";
            }
            else
            {
                loseImage.SetActive(true);
                synAniPanel.SetActive(true);
                loseCloud_ani.SetBool("isOpenCloud", true);
                synManager.rate_text.text = "";
                Destroy(synFrame.transform.GetChild(0).GetChild(0).gameObject);
                Destroy(synFrame.transform.GetChild(1).GetChild(0).gameObject);
            }
        }
        
    }

    public void CloudAniEvent()
    {
        flower_ani.SetBool("isShake", true);
    }
    public void FlowerAniEvent()
    {
        synAniPanel.SetActive(false);
        cloud_ani.SetBool("isOpenCloud", false);
        flower_ani.SetBool("isShake", false);
        successAniImage.SetActive(false);
    }
    public void LoseCloudEvent()
    {
        synAniPanel.SetActive(false);
        loseCloud_ani.SetBool("isOpenCloud", false);
        loseImage.SetActive(true);
    }
    private int GetRandomRateIndex()//取得機率的數值0是成功，1是失敗
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < rates.Length; i++)
        {
            if (rates[i]._weight >= r)
            {
                return i;
            }
        }

        return 0;
    }
    private void CaculateWeights() //只要有換植物就要重新呼叫，然後才可以判斷是成功還是失敗
    {
        accumulatedWeights = 0f;
        foreach (Rate _rate in rates)
        {
            accumulatedWeights += _rate.chance;
            _rate._weight = accumulatedWeights;
        }
    }

    public void HaveEmptyGrid()
    {
        bool haveEmpty = false;
        for (int i = 0; i < synFrame.transform.childCount; i++)
        {
            if (synFrame.transform.GetChild(i).transform.childCount <= 0)
            {
                haveEmpty = true;
                break;
            }
            else
            {
                haveEmpty = false;
            }
        }
        if (!haveEmpty)
        {
            rates[0].chance = FlowerSynRate(FlowerCombination(synFrame.transform.GetChild(0).GetChild(0).GetComponent<SynGrid_Item>().flowerNum,
                                           synFrame.transform.GetChild(1).GetChild(0).GetComponent<SynGrid_Item>().flowerNum));
            rates[1].chance = 100 - rates[0].chance;
            rate_text.text = rates[0].chance.ToString()+" %";
        }
        else
        {
            rate_text.text = "";
        }
    }

    public int FlowerCombination(int flower_0, int flower_1)
    {
        int combinationFlower=0;

        if (flower_0 == 0)//0:0-5
        {
            if(flower_1==0)
            {
                combinationFlower = 3;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 4;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 5;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 9;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 18;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 26;
            }
        } //0
        else if (flower_0 == 1)//1:0-5
        {
            if (flower_1 == 0)
            {
                combinationFlower = 4;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 6;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 7;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 10;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 19;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 27;
            }
        } //1
        else if (flower_0 == 2)//2:0-5
        {
            if (flower_1 == 0)
            {
                combinationFlower = 5;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 7;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 8;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 11;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 20;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 28;
            }
        } //2
        else if (flower_0 == 3)//3:0-8
        {
            if (flower_1 == 0)
            {
                combinationFlower = 9;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 10;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 11;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 12;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 13;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 14;
            }
            if (flower_1 == 6)
            {
                combinationFlower = 15; ;
            }
            if (flower_1 == 7)
            {
                combinationFlower = 16;
            }
            if (flower_1 == 8)
            {
                combinationFlower = 17;
            }
        } //3
        else if (flower_0 == 4)//4:0-8
        {
            if (flower_1 == 0)
            {
                combinationFlower = 18;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 19;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 20;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 13;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 21;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 22;
            }
            if (flower_1 == 6)
            {
                combinationFlower = 23; ;
            }
            if (flower_1 == 7)
            {
                combinationFlower = 24;
            }
            if (flower_1 == 8)
            {
                combinationFlower = 25;
            }
        } //4
        else if (flower_0 == 5)//5:0-8
        {
            if (flower_1 == 0)
            {
                combinationFlower = 26;
            }
            if (flower_1 == 1)
            {
                combinationFlower = 27;
            }
            if (flower_1 == 2)
            {
                combinationFlower = 28;
            }
            if (flower_1 == 3)
            {
                combinationFlower = 14;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 22;
            }
            if (flower_1 == 6)
            {
                combinationFlower = 29; ;
            }
            if (flower_1 == 7)
            {
                combinationFlower = 30;
            }
            if (flower_1 == 8)
            {
                combinationFlower = 31;
            }
        } //5
        else if (flower_0 == 6)//6:3-5
        {
            if (flower_1 == 3)
            {
                combinationFlower = 15;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 23;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 29;
            }
        } //6
        else if (flower_0 == 7)//7:3-5
        {
            if (flower_1 == 3)
            {
                combinationFlower = 16;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 24;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 30;
            }
        } //7
        else if (flower_0 == 8)//8:3-5
        {
            if (flower_1 == 3)
            {
                combinationFlower = 17;
            }
            if (flower_1 == 4)
            {
                combinationFlower = 25;
            }
            if (flower_1 == 5)
            {
                combinationFlower = 31;
            }
        } //8

        return combinationFlower;
    }

    public float FlowerSynRate(int combinationFlowerNum)
    {
        float _synRate = 0;

        switch(combinationFlowerNum)
        {
            case 3:
                _synRate = 90;//90
                break;
            case 4:
                _synRate = 80;
                break;
            case 5:
                _synRate = 80;
                break;
            case 6:
                _synRate = 75;//75
                break;
            case 7:
                _synRate = 70;
                break;
            case 8:
                _synRate = 70;
                break;
            case 9:
                _synRate = 60;
                break;
            case 10:
                _synRate = 60;
                break;
            case 11:
                _synRate = 60;
                break;
            case 12:
                _synRate = 50;
                break;
            case 13:
                _synRate = 50;
                break;
            case 14:
                _synRate = 45;
                break;
            case 15:
                _synRate = 45;
                break;
            case 16:
                _synRate = 40;
                break;
            case 17:
                _synRate = 40;
                break;
            case 18:
                _synRate = 30;
                break;
            case 19:
                _synRate = 30;
                break;
            case 20:
                _synRate = 30;
                break;
            case 21:
                _synRate = 30;
                break;
            case 22:
                _synRate = 20;
                break;
            case 23:
                _synRate = 20;
                break;
            case 24:
                _synRate = 10;
                break;
            case 25:
                _synRate = 10;
                break;
            case 26:
                _synRate = 5;
                break;
            case 27:
                _synRate = 5;
                break;
            case 28:
                _synRate = 5;
                break;
            case 29:
                _synRate = 2;
                break;
            case 30:
                _synRate = 2;
                break;
            case 31:
                _synRate = 1;
                break;
        }

        return _synRate;

    }
}

[System.Serializable]
public class Rate
{
    public string name;
    public float chance;
    [HideInInspector] public double _weight;
}

