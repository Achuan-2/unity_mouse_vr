using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.SceneManagement;



public class MouseLickingDetect : MonoBehaviour
{
    public TrackControl TC;
    public FileWrite FW;
    public int lickPin = 4;
    public int waterPin = 12;
    public float rewardRigonBack;
    public float rewardRigonFront;
    public float punishLockTime = 5f;
    public string logFileName = "licking_log.csv";
    public int active = 1 ;
    public int lickNumToFeedback = 5;
    public float mousePosition;
    public int punishLickCount = 0;
    public int rewardLickCount = 0;
    private float lickPostion;
    private int isLicking;



    // Start is called before the first frame update
    void Start()
    {
        // Test: set Arduino 
        UduinoManager.Instance.pinMode(lickPin, PinMode.Input);
        UduinoManager.Instance.pinMode(waterPin, PinMode.Output);

    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = TC.mousePosition;

        // If active =0 , then cancel the feedback of reward or punishment.
        if (active == 0)
        {
            return;
        }

        // Assign a value to isLicking
        isLicking = ReadisLicking();


        // Count the number of licks
        if (isLicking == 1)
        {
            // Record the position of licking
            lickPostion = DetectLicking();
            Debug.Log("Mouse is Licking at " + lickPostion);

            // Give feedback to lick, and return reward value, 1 stands for licking in reward region,while 0 stands for other region.
            int reward = LickingFeedback(lickPostion);
            if(reward >=0)
            {
                SaveLickInfo(reward);
            }


        }

    }


    int ReadisLicking()
    {
        int a;
        if (Input.GetKeyDown(KeyCode.J))
        {
            // ***** Enter J in keyboard will simulate mouse licking ******//
            a = 1;
        }
        else
        {
            // ***** Read from Arduino lickPin ******//
            a = UduinoManager.Instance.digitalRead(lickPin);
        }
        return a;
    }
        
    float DetectLicking()
    {
        // ***** Record the position of licking *****//
        float position;
        position = mousePosition;

        return position;
    }

    bool IsInRewardRegion(float position)
    {
        return position > rewardRigonBack && position < rewardRigonFront;
    }



    int LickingFeedback(float postion)
    {
        // Count the number of licks, considering the reward area and the punishment area respectively
        if (IsInRewardRegion(lickPostion))
        {
            rewardLickCount += 1;
            punishLickCount = 0;

            if (rewardLickCount >= lickNumToFeedback)
            {
                RewardFeedback();
                rewardLickCount = 0;
                return 1;
            }

        }
        else
        {
            punishLickCount += 1;
            rewardLickCount = 0;


            if (punishLickCount >= lickNumToFeedback)
            {
                PunishFeedback();
                punishLickCount = 0;
                return 0;

            }
            
        }
        return -1;
    }

    void SaveLickInfo(int reward)
    {
        string sceneName = SceneManager.GetActiveScene().name; // Get the name of active scene
        string header = "date,scene,lickPosition,reward\n";
        string content = System.DateTime.Now + "," + sceneName + "," + lickPostion + "," + reward + "\n";
        FW.WriteToText(logFileName, header, content);

    }

    void RewardFeedback()
    {
        UduinoManager.Instance.digitalWrite(waterPin, State.HIGH);
        Debug.Log("Water out");
    }



    void PunishFeedback()
    {
        UduinoManager.Instance.digitalWrite(waterPin, State.LOW);
        Punishement();
    }


    public void Punishement()
    {
        // punish
        Debug.Log("Back to starting point");
        // SceneManager.LoadScene(0); // when load scene, Uduino need to find board again.
        TC.GotoStartPoint(TC.startPoint);
        Debug.Log("Game Locked");
        RefreshGameWait();
        
        //❓这里有一个问题，明明GotoStartPoint先，结果RefreshGameWait的Wait会导致回到原点要过一会才显示，我猜测估计是循环太多的缘故，因为之后显示的Debug.Log的时间是正常的
    }



    void RefreshGameWait()
    {
        GamePause();
        TimeWait(punishLockTime);
        GameResume();
        Debug.Log("Finished Locking");


    }
    void TimeWait(float pauseTime)
    {
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            continue;
        }
    }

    void GamePause()
    {
        // game pause and can lock the track,  Invoke() can not be used
        Time.timeScale = 0f;
    }

    void GameResume()
    {
        // game restart
        Time.timeScale = 1f;
    }


}
