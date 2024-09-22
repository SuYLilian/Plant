using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int coinNum;
    //public Obstacle obstacle;
    public LoppingBG loppingBG;
    public ObstacleSpawner obstacleSpawner;

    // public float playerSpeed;
    public float distanceRecord, originalPos;
    public float historyDistance;
    public GameObject anabiosisPanel, settlePanel, runWindow, plantWindow, backGround;
    public Canvas plantCanvas;
    GameObject levelManager;
    public TextMeshProUGUI coinNum_text, coinNum_text_settle, historyDistance_text, distanceRecord_text_settle, distanceRecord_text;

    public PlayerTouch playerTouch;
    //public PlayerController playerController;
    //public GameObject anabiosisPanel;
    public int coinCosumeNum;

    public float playerSpeed_t;
    public Animator animator;

    public Vector3 playerOriginalPos, backGroundOriginalPos;

    public TextMeshProUGUI laboratoryCoin_text, storeCoin_text;
    private void Awake()
    {
        playerSpeed_t = gameObject.GetComponent<PlayerTouch>().originalPlayerSpeed;
        gameObject.GetComponent<PlayerTouch>().playerSpeed = 0;
        animator = gameObject.GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>().gameObject;
        playerTouch = FindObjectOfType<PlayerTouch>();
        playerOriginalPos = gameObject.transform.position;
        backGroundOriginalPos = backGround.transform.position;
        if(PlayerPrefs.HasKey("HistoryDistance"))
        {
            historyDistance = PlayerPrefs.GetInt("HistoryDistance");
        }
        else
        {
            historyDistance = 0;
            PlayerPrefs.SetInt("HistoryDistance", 0);
        }
        originalPos = transform.position.y;
        //loppingBG = FindObjectOfType<LoppingBG>();
    }
    private void Update()
    {
        //gameObject.transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
        distanceRecord = (int)((transform.position.y-originalPos)*10);
        distanceRecord_text.text = distanceRecord.ToString();
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.tag=="Coin")
        {
            coinNum++;
            coinNum_text.text = coinNum.ToString();
            Destroy(collision.gameObject);
        }
        else if((collision.tag=="Vine" || collision.tag == "TreeStump" || collision.tag == "HighVine" || collision.tag == "Piranha" || collision.tag == "Stem") && !playerTouch.isJumping && !playerTouch.isSlipping)
        {
            //playerTouch.canDrag = false;
            gameObject.GetComponent<PlayerTouch>().canDrag = false;
            //obstacleSpawner.canSpawn = false;
            playerSpeed_t = playerTouch.playerSpeed;
            playerTouch.playerSpeed = 0;
            animator.SetBool("isDead", true);
            for(int i=0;i<levelManager.transform.childCount;i++)
            {
                Destroy(levelManager.transform.GetChild(i).gameObject);
            }
            ClickNoButton();
            //speed_t = loppingBG.speed;
            //loppingBG.speed = 0;
            // Obstacle.obstacleSpeed = 0;
            //如果玩家局外的金幣無法購買復活，則yesButton的interable=false
            //anabiosisPanel.SetActive(true);
        }
        else if ((collision.tag == "HighVine" || collision.tag == "Piranha") && playerTouch.isJumping)
        {
            //playerTouch.canDrag = false;
            gameObject.GetComponent<PlayerTouch>().canDrag = false;
            //obstacleSpawner.canSpawn = false;
            playerSpeed_t = playerTouch.playerSpeed;
            playerTouch.playerSpeed = 0;
            animator.SetBool("isDead", true);
            for (int i = 0; i < levelManager.transform.childCount; i++)
            {
                Destroy(levelManager.transform.GetChild(i).gameObject);
            }
            ClickNoButton();
            //speed_t = loppingBG.speed;
            //loppingBG.speed = 0;
            // Obstacle.obstacleSpeed = 0;
            //如果玩家局外的金幣無法購買復活，則yesButton的interable=false
            //anabiosisPanel.SetActive(true);
        }
        else if ((collision.tag == "Vine" || collision.tag == "TreeStump" || collision.tag == "Piranha" || collision.tag == "Stem") && playerTouch.isSlipping)
        {
            //playerTouch.canDrag = false;
            gameObject.GetComponent<PlayerTouch>().canDrag = false;
            //obstacleSpawner.canSpawn = false;
            playerSpeed_t = playerTouch.playerSpeed;
            playerTouch.playerSpeed = 0;
            animator.SetBool("isDead", true);
            for (int i = 0; i < levelManager.transform.childCount; i++)
            {
                Destroy(levelManager.transform.GetChild(i).gameObject);
            }
            ClickNoButton();
            //speed_t = loppingBG.speed;
            //loppingBG.speed = 0;
            // Obstacle.obstacleSpeed = 0;
            //如果玩家局外的金幣無法購買復活，則yesButton的interable=false
            //anabiosisPanel.SetActive(true);
        }
        else if(collision.tag == "HighVine" && playerTouch.isSlipping)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    public void ClickYesButton()//要讓角色開始跑
    {
        //玩家的金幣-coinConsumeNum(玩家的金幣不是跑庫裡獲取的)

        int r = Random.Range(0, levelManager.GetComponent<LevelManager>().levels.Length);
        GameObject newLevel = Instantiate(levelManager.GetComponent<LevelManager>().levels[r], levelManager.GetComponent<LevelManager>().levelParent.transform);
        newLevel.transform.position = new Vector3(0, gameObject.transform.position.y + 8 , newLevel.transform.localPosition.z);
        GameObject newLevel_2 = Instantiate(levelManager.GetComponent<LevelManager>().levels[r], levelManager.GetComponent<LevelManager>().levelParent.transform);
        newLevel_2.transform.localPosition = new Vector3(newLevel.transform.localPosition.x, newLevel.transform.localPosition.y + levelManager.GetComponent<LevelManager>().levelSpace,newLevel_2.transform.localPosition.z);


       // anabiosisPanel.SetActive(false);
        //*loppingBG.speed = speed_t;
        //Obstacle.obstacleSpeed = speed_t*10;
        animator.SetBool("isDead", false);
        playerTouch.playerSpeed = playerSpeed_t;
        gameObject.GetComponent<PlayerTouch>().canDrag = true;
        //obstacleSpawner.canSpawn = true ;

    }

    public void ClickNoButton()
    {
        coinNum_text_settle.text = coinNum.ToString();
        JasonManager.jasonManager.coinNum += coinNum;
        laboratoryCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
        storeCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
        distanceRecord_text_settle.text = distanceRecord.ToString();
        if (distanceRecord>historyDistance)
        {
            historyDistance = distanceRecord;
            historyDistance_text.text = historyDistance.ToString();
            PlayerPrefs.SetInt("HistoryDistance", (int)historyDistance);
            PlayerPrefs.Save();
            Debug.Log(">");
        }
        else
        {
            historyDistance_text.text = historyDistance.ToString();
            Debug.Log("<");

        }
        
        settlePanel.SetActive(true);
        anabiosisPanel.SetActive(false);

    }

    public void ClickBackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);
        FindObjectOfType<SoundManager>().gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<SoundManager>().bgmClips[0];
        FindObjectOfType<SoundManager>().audioSource.Play();

        runWindow.SetActive(false);
        settlePanel.SetActive(false);
        //plantWindow.SetActive(true);
        plantCanvas.enabled = true;
        gameObject.transform.position = playerOriginalPos;
        coinNum = 0;
        distanceRecord = 0;
        coinNum_text.text = coinNum.ToString();
        distanceRecord_text.text = distanceRecord.ToString();
        //SceneManager.LoadScene("PlantScene");

    }
}
