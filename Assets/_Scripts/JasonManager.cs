using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;


public class JasonManager : MonoBehaviour
{
    public static JasonManager jasonManager;
    Backpack backpack = new Backpack();
    public GameObject backpackItem_prefab;
    public Sprite[] seedSprites;
    public Sprite[] flowerSprites;
    public Sprite[] propsSprites;
    public GameObject backpackGroup;
    public int coinNum;
    public int waterPropsNum=0;
    public TextMeshProUGUI laboratoryCoin_text, storeCoin_text;

    SeedList seedList = new SeedList();
    public GameObject seedListItem_prefab;
    public GameObject seedListGroup;

    PlantArea plantArea = new PlantArea();
    public GameObject[] plantItem;
    public GameObject plantGroup;

    IllustratedBook illustratedBook = new IllustratedBook();
    public GameObject illustratedBookItem_prefab;
    public GameObject illustratedBookGroup;

    FlowerList flowerList = new FlowerList();
    public GameObject flowerListItem_prefab;
    public GameObject flowerListGroup;

    public GameObject notEnoughText;
    private void Awake()
    {
        jasonManager = this;
        if (PlayerPrefs.HasKey("Backpack"))
        {
            LoadAllBackpackItem();
        }
        if (PlayerPrefs.HasKey("SeedList"))
        {
            LoadAllSeedListItem();
        }
        if (PlayerPrefs.HasKey("PlantArea"))
        {
            LoadPlantAreaItem();
        }
        if (PlayerPrefs.HasKey("FlowerList"))
        {
            LoadAllFlowerListItem();
        }
        if(PlayerPrefs.HasKey("IllustratedBook"))
        {
            LoadAllIllustratedBookItem();
        }

        if (PlayerPrefs.HasKey("CoinNum"))
        {
            coinNum = PlayerPrefs.GetInt("CoinNum");
            laboratoryCoin_text.text = coinNum.ToString();
            storeCoin_text.text = coinNum.ToString();
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Save game data
            SaveAllBackpackItem();
            SaveAllSeedListItem();
            SavePlantAreaItem();
            SaveAllFlowerListItem();
            SaveAllIllustratedBookItem();
            PlayerPrefs.SetInt("CoinNum", coinNum);
        }
    }
    private void OnApplicationQuit()
    {
        // Save game data
        SaveAllBackpackItem();
        SaveAllSeedListItem();
        SavePlantAreaItem();
        SaveAllFlowerListItem();
        SaveAllIllustratedBookItem();
        PlayerPrefs.SetInt("CoinNum", coinNum);

    }

    #region Save&Load_BackpackItem
    void LoadAllBackpackItem()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "BackpackData.json");
        backpack = JsonMapper.ToObject<Backpack>(jsonString);

        for (int i = 0; i < backpack.allBackpackItem.Count; i++)
        {
            GameObject _backpackItem = Instantiate(backpackItem_prefab, backpackGroup.transform);
            _backpackItem.GetComponent<Item_Backpack>().itemType = backpack.allBackpackItem[i].itemType;
            _backpackItem.GetComponent<Item_Backpack>().itemNum = backpack.allBackpackItem[i].itemNum;
            _backpackItem.GetComponent<Item_Backpack>().itemAmount = backpack.allBackpackItem[i].itemAmount;
            if (backpack.allBackpackItem[i].itemAmount >= 2)
            {
                _backpackItem.GetComponent<Item_Backpack>().itemAmount_text.text = backpack.allBackpackItem[i].itemAmount.ToString();
            }
            switch (_backpackItem.GetComponent<Item_Backpack>().itemType)
            {
                case "seed":
                    _backpackItem.GetComponent<Item_Backpack>().itemImage.sprite = seedSprites[_backpackItem.GetComponent<Item_Backpack>().itemNum];
                    break;
                case "flower":
                    _backpackItem.GetComponent<Item_Backpack>().itemImage.sprite = flowerSprites[_backpackItem.GetComponent<Item_Backpack>().itemNum];
                    break;
                case "props":
                    _backpackItem.GetComponent<Item_Backpack>().itemImage.sprite = propsSprites[_backpackItem.GetComponent<Item_Backpack>().itemNum];
                    if(_backpackItem.GetComponent<Item_Backpack>().itemNum==0)
                    {
                        waterPropsNum = _backpackItem.GetComponent<Item_Backpack>().itemAmount;
                    }
                    break;
            }
        }
    }
    void SaveAllBackpackItem()
    {
        PlayerPrefs.SetInt("Backpack", 1);
        backpack = new Backpack();
        for (int i = 0; i < backpackGroup.transform.childCount; i++)
        {
            BackpackItem item = new BackpackItem();
            item.itemType = backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType;
            item.itemNum = backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum;
            item.itemAmount = backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemAmount;
            backpack.allBackpackItem.Add(item);
        }
        File.WriteAllText(Application.persistentDataPath + "BackpackData.json", JsonMapper.ToJson(backpack));
    }
    #endregion

    #region Save&Load_SeedListItem
    void LoadAllSeedListItem()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "SeedListData.json");
        seedList = JsonMapper.ToObject<SeedList>(jsonString);

        for (int i = 0; i < seedList.allSeedListItem.Count; i++)
        {
            GameObject _seedListItem = Instantiate(seedListItem_prefab, seedListGroup.transform);
            _seedListItem.GetComponent<SeedListGrid>().seedType = seedList.allSeedListItem[i].seedType;
            _seedListItem.GetComponent<SeedListGrid>().seedNum = seedList.allSeedListItem[i].seedNum;
            _seedListItem.GetComponent<SeedListGrid>().seedAmount = seedList.allSeedListItem[i].seedAmount;
            if (seedList.allSeedListItem[i].seedAmount >= 2)
            {
                _seedListItem.GetComponent<SeedListGrid>().seedAmount_text.text = seedList.allSeedListItem[i].seedAmount.ToString();
            }
            switch (_seedListItem.GetComponent<SeedListGrid>().seedType)
            {
                case "seed":
                    _seedListItem.GetComponent<SeedListGrid>().seedImage.sprite = seedSprites[_seedListItem.GetComponent<SeedListGrid>().seedNum];
                    break;
            }
        }
    }
    void SaveAllSeedListItem()
    {
        PlayerPrefs.SetInt("SeedList", 1);
        seedList = new SeedList();
        for (int i = 0; i < seedListGroup.transform.childCount; i++)
        {
            SeedListItem item = new SeedListItem();
            item.seedType = seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedType;
            item.seedNum = seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedNum;
            item.seedAmount = seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedAmount;
            seedList.allSeedListItem.Add(item);
        }
        File.WriteAllText(Application.persistentDataPath + "SeedListData.json", JsonMapper.ToJson(seedList));
    }
    #endregion

    # region Save&Load_PlantAreaItem
    void LoadPlantAreaItem()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "PlantAreaData.json");
        plantArea = JsonMapper.ToObject<PlantArea>(jsonString);
        for (int i = 0; i < plantArea.allPlantAreaItem.Count; i++)
        {
            if (plantArea.allPlantAreaItem[i].isEmpty == "Empty")
            {
                Debug.Log("empty");
            }
            else
            {
                GameObject _plantItem = Instantiate(plantItem[plantArea.allPlantAreaItem[i].plantNum], plantGroup.transform.GetChild(i).transform);
                PlantAreaItem plantAreaItem = plantArea.allPlantAreaItem[i];
                DateTime exitTime = DateTime.Parse(plantAreaItem.exitTimeString);
                DateTime currentTime = DateTime.Now;
                Double duration = (currentTime - exitTime).TotalSeconds;
                if (plantArea.allPlantAreaItem[i].plantState == "seed")
                {
                    if (plantAreaItem.growingTime_remain <= plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                    {
                        if (duration < plantAreaItem.growingTime_remain)
                        {
                            plantAreaItem.plantState = "seed";
                            plantAreaItem.growingTime_remain -= duration;
                        }
                        else if (duration >= plantAreaItem.growingTime_remain && duration <= plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                        {
                            plantAreaItem.haveGrownWithered_remain -= (duration - plantAreaItem.growingTime_remain);
                            if (plantAreaItem.haveGrownWithered_remain > 0)
                            {
                                plantAreaItem.plantState = "flower";
                            }
                            else if (plantAreaItem.haveGrownWithered_remain <= 0)
                            {
                                plantAreaItem.plantState = "die";
                            }
                        }
                    }
                    else if (plantAreaItem.growingTime_remain > plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                    {
                        if (duration < plantAreaItem.growingTime_remain && duration > plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                        {
                            plantAreaItem.plantState = "die";
                        }
                        else if (duration < plantAreaItem.growingTime_remain && duration < plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                        {
                            if (duration < plantAreaItem.needWateringTime_remain)
                            {
                                plantAreaItem.plantState = "seed";
                                plantAreaItem.growingTime_remain -= duration;
                                plantAreaItem.needWateringTime_remain -= duration;
                            }
                            else if (duration > plantAreaItem.needWateringTime_remain)
                            {
                                plantAreaItem.plantState = "needWatering";
                                plantAreaItem.growingTime_remain -= duration;
                                plantAreaItem.noWateringWithered_remain -= (duration - plantAreaItem.needWateringTime_remain);
                            }
                        }
                        else if (duration > plantAreaItem.growingTime_remain && duration > plantAreaItem.needWateringTime_remain + plantAreaItem.noWateringWithered_remain)
                        {
                            plantAreaItem.plantState = "die";
                        }
                    }
                }
                else if (plantArea.allPlantAreaItem[i].plantState == "needWatering")
                {
                    if (plantAreaItem.growingTime_remain <= plantAreaItem.noWateringWithered_remain)
                    {
                        if (plantAreaItem.growingTime_remain <= duration)
                        {
                            plantAreaItem.haveGrownWithered_remain -= (duration - plantAreaItem.growingTime_remain);
                            if (plantAreaItem.haveGrownWithered_remain > 0)
                            {
                                plantAreaItem.plantState = "flower";
                            }
                            else if (plantAreaItem.haveGrownWithered_remain <= 0)
                            {
                                plantAreaItem.plantState = "die";
                            }
                        }
                        else if (plantAreaItem.growingTime_remain > duration)
                        {
                            plantAreaItem.plantState = "seed";
                            plantAreaItem.growingTime_remain -= duration;
                            plantAreaItem.noWateringWithered_remain -= duration;
                        }
                    }
                    else if (plantAreaItem.growingTime_remain > plantAreaItem.noWateringWithered_remain)
                    {
                        if (plantAreaItem.noWateringWithered_remain <= duration)
                        {
                            plantAreaItem.plantState = "die";
                        }
                        else if (plantAreaItem.noWateringWithered_remain > duration)
                        {
                            plantAreaItem.plantState = "needWatering";
                            plantAreaItem.growingTime_remain -= duration;
                            plantAreaItem.noWateringWithered_remain -= duration;
                        }
                    }
                }
                else if (plantArea.allPlantAreaItem[i].plantState == "flower")
                {
                    if (duration >= plantAreaItem.haveGrownWithered_remain)
                    {
                        plantAreaItem.plantState = "die";
                    }
                    else
                    {
                        plantAreaItem.haveGrownWithered_remain -= duration;
                    }
                }

                _plantItem.GetComponent<PlantItem>().plantNum = plantAreaItem.plantNum;
                _plantItem.GetComponent<PlantItem>().plantState = plantAreaItem.plantState;
                _plantItem.GetComponent<PlantItem>().growingTime_remain = plantAreaItem.growingTime_remain;
                _plantItem.GetComponent<PlantItem>().needWateringTime_remain = plantAreaItem.needWateringTime_remain;
                _plantItem.GetComponent<PlantItem>().noWateringWithered_remain = plantAreaItem.noWateringWithered_remain;
                _plantItem.GetComponent<PlantItem>().haveGrownWithered_remain = plantAreaItem.haveGrownWithered_remain;

                if (plantAreaItem.plantState == "die")
                {
                    //_plantItem.GetComponent<PlantItem>().plantState = plantAreaItem.plantState;
                    _plantItem.GetComponent<Button>().enabled = true;
                    _plantItem.GetComponent<PlantItem>().plantImage.sprite = _plantItem.GetComponent<PlantItem>().dieSprite;
                    _plantItem.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
                }
                else if (plantAreaItem.plantState == "flower")
                {
                    // _plantItem.GetComponent<PlantItem>().plantState = plantAreaItem.plantState;
                    //_plantItem.GetComponent<PlantItem>().haveGrownWithered_remain = plantAreaItem.haveGrownWithered_remain;
                    _plantItem.GetComponent<Button>().enabled = true;
                    _plantItem.GetComponent<PlantItem>().plantImage.sprite = _plantItem.GetComponent<PlantItem>().flowerSprite;
                    _plantItem.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
                }
                else if (plantAreaItem.plantState == "needWatering")
                {
                    _plantItem.GetComponent<PlantItem>().wateringButton.SetActive(true);
                }
                else if (plantArea.allPlantAreaItem[i].plantState == "seed")
                {

                }
                _plantItem.GetComponent<PlantItem>().canCaculate = true;
                Debug.Log("canCa");
            }

        }
    }
    void SavePlantAreaItem()
    {
        PlayerPrefs.SetInt("PlantArea", 1);
        plantArea = new PlantArea();
        for (int i = 0; i < plantGroup.transform.childCount; i++)
        {
            if (plantGroup.transform.GetChild(i).transform.childCount > 0)
            {
                PlantItem _plantItem = plantGroup.transform.GetChild(i).transform.GetChild(0).GetComponent<PlantItem>();
                PlantAreaItem plantAreaItem = new PlantAreaItem();
                plantAreaItem.plantState = _plantItem.plantState;
                plantAreaItem.plantNum = _plantItem.plantNum;
                plantAreaItem.growingTime_remain = _plantItem.growingTime_remain;
                plantAreaItem.needWateringTime_remain = _plantItem.needWateringTime_remain;
                plantAreaItem.noWateringWithered_remain = _plantItem.noWateringWithered_remain;
                plantAreaItem.haveGrownWithered_remain = _plantItem.haveGrownWithered_remain;
                plantAreaItem.exitTimeString = DateTime.Now.ToString();
                plantArea.allPlantAreaItem.Add(plantAreaItem);
            }
            else
            {
                PlantAreaItem plantAreaItem = new PlantAreaItem();
                plantAreaItem.isEmpty = "Empty";
                plantArea.allPlantAreaItem.Add(plantAreaItem);
            }
        }
        File.WriteAllText(Application.persistentDataPath + "PlantAreaData.json", JsonMapper.ToJson(plantArea));
    }
    #endregion

    #region Save&Load_IllustratedBookItem
    void LoadAllIllustratedBookItem()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "IllustratedBookData.json");
        illustratedBook = JsonMapper.ToObject<IllustratedBook>(jsonString);
        if (illustratedBook.allIllustratedBookItem.Count<5)
        {
            illustratedBook = new IllustratedBook();
            PlayerPrefs.SetInt("IllustratedBook", 1);
            for (int i = 0; i < flowerSprites.Length; i++)
            {
                IllustratedBookItem item = new IllustratedBookItem();
                item.flowerNum = i;
                item.isUnlock = 0;
                illustratedBook.allIllustratedBookItem.Add(item);
            }
            File.WriteAllText(Application.persistentDataPath + "IllustratedBookData.json", JsonMapper.ToJson(illustratedBook));
        }

        jsonString = File.ReadAllText(Application.persistentDataPath + "IllustratedBookData.json");
        illustratedBook = JsonMapper.ToObject<IllustratedBook>(jsonString);
        for (int i = 0; i < illustratedBook.allIllustratedBookItem.Count; i++)
        {
            GameObject bookItem = Instantiate(illustratedBookItem_prefab, illustratedBookGroup.transform);
            bookItem.GetComponent<Item_IllustratedBook>().flowerNum = illustratedBook.allIllustratedBookItem[i].flowerNum;
            bookItem.GetComponent<Item_IllustratedBook>().isUnlock = illustratedBook.allIllustratedBookItem[i].isUnlock;
            if (bookItem.GetComponent<Item_IllustratedBook>().isUnlock == 0)
            {
                bookItem.GetComponent<Item_IllustratedBook>().lockImage.SetActive(true);
            }
            else
            {
                bookItem.GetComponent<Item_IllustratedBook>().lockImage.SetActive(false);
            }
            bookItem.GetComponent<Item_IllustratedBook>().itemImage.sprite = flowerSprites[bookItem.GetComponent<Item_IllustratedBook>().flowerNum];
        }
    }
    void SaveAllIllustratedBookItem()
    {
        PlayerPrefs.SetInt("IllustratedBook", 1);
        illustratedBook = new IllustratedBook();

        for (int i = 0; i < illustratedBookGroup.transform.childCount; i++)
        {
            IllustratedBookItem bookItem = new IllustratedBookItem();
            bookItem.flowerNum = illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().flowerNum;
            bookItem.isUnlock = illustratedBookGroup.transform.GetChild(i).GetComponent<Item_IllustratedBook>().isUnlock;
            illustratedBook.allIllustratedBookItem.Add(bookItem);
        }
        File.WriteAllText(Application.persistentDataPath + "IllustratedBookData.json", JsonMapper.ToJson(illustratedBook));

    }
    #endregion

    #region Save&Load_FlowerListItem
    void LoadAllFlowerListItem()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "FlowerListData.json");
        flowerList = JsonMapper.ToObject<FlowerList>(jsonString);

        for (int i = 0; i < flowerList.allFlowerListItemItem.Count; i++)
        {
            GameObject _flowerListItem = Instantiate(flowerListItem_prefab, flowerListGroup.transform);
            _flowerListItem.GetComponent<Item_SynthesisRoom>().flowerNum = flowerList.allFlowerListItemItem[i].flowerNum;
            _flowerListItem.GetComponent<Item_SynthesisRoom>().flowerAmount = flowerList.allFlowerListItemItem[i].flowerAmount;
            _flowerListItem.GetComponent<Item_SynthesisRoom>().flowerImage.sprite = flowerSprites[flowerList.allFlowerListItemItem[i].flowerNum];
            if (flowerList.allFlowerListItemItem[i].flowerAmount >= 2)
            {
                _flowerListItem.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = flowerList.allFlowerListItemItem[i].flowerAmount.ToString();
            }            
        }
    }
    void SaveAllFlowerListItem()
    {
        PlayerPrefs.SetInt("FlowerList", 1);
        flowerList = new FlowerList();
        for (int i = 0; i < flowerListGroup.transform.childCount; i++)
        {
            FlowerListItem item = new FlowerListItem();
            item.flowerNum = flowerListGroup.transform.GetChild(i).GetComponent<Item_SynthesisRoom>().flowerNum;
            item.flowerAmount = flowerListGroup.transform.GetChild(i).GetComponent<Item_SynthesisRoom>().flowerAmount;
            flowerList.allFlowerListItemItem.Add(item);
        }
        File.WriteAllText(Application.persistentDataPath + "FlowerListData.json", JsonMapper.ToJson(flowerList));
    }
    #endregion
}

public class Backpack
{
    public List<BackpackItem> allBackpackItem = new List<BackpackItem>();
}

public class BackpackItem
{
    public string itemType;
    public int itemNum;
    public int itemAmount;
}
public class SeedList
{
    public List<SeedListItem> allSeedListItem = new List<SeedListItem>();
}
public class SeedListItem
{
    public string seedType;
    public int seedNum;
    public int seedAmount;
}

public class PlantArea
{
    public List<PlantAreaItem> allPlantAreaItem = new List<PlantAreaItem>();
}
public class PlantAreaItem
{
    public string isEmpty;
    public string plantState;//seed,needWatering,flower,die
    public int plantNum;

    public Double growingTime_remain;//距離成花時間a
    public Double needWateringTime_remain;//距離需澆水時間b
    public Double noWateringWithered_remain;//距離枯萎時間(沒澆水)c        !!以分鐘為單位!!
    public Double haveGrownWithered_remain;//距離枯萎時間(已成花)d
    public string exitTimeString;
}

public class IllustratedBook
{
    public List<IllustratedBookItem> allIllustratedBookItem = new List<IllustratedBookItem>();

}
public class IllustratedBookItem
{
    public int flowerNum;
    public int isUnlock;
}

public class FlowerList
{
    public List<FlowerListItem> allFlowerListItemItem = new List<FlowerListItem>();
}
public class FlowerListItem
{
    public int flowerNum;
    public int flowerAmount;
}
