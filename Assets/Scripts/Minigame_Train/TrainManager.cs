using System.Runtime.InteropServices;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class TrainManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button cutBokeyButton;

    [SerializeField] GameObject WinPopup;
    [SerializeField] GameObject trainSection;
    [SerializeField] GameObject normalTrain;
    [SerializeField] GameObject crashedTrain;
    [SerializeField] GameObject pointer;

    [SerializeField] Transform trainInitialPosition;

    [SerializeField] Sprite unfinishedStatusImage;
    [SerializeField] Sprite finishedStatusImage;

    [SerializeField] Image[] enemyStatusImage;

    [SerializeField] ScoreManager scoreManager;

    Train[] trains;
    Vector3 pointerDiffPosition;

    Transform currentPlayerFocusTrainPos;

    int currentPointerIndex;
    int trainAmount;
    int crashedAmount;
    int rangeBetweenTrain;
    int rangeBetweenPointerAndTrain;
    int currentScorePrize;
    int[] playerScores;

    float currentEnemySuccessChance;

    bool isPointerFocusRight;
    bool isPointerFocusLeft;
    bool[] isPlayerFinished;

    int difficultLevel;

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    void Awake()
    {
        currentPointerIndex = 0;
        trainAmount = 0;
        crashedAmount = 0;
        currentScorePrize = 4;
        rangeBetweenPointerAndTrain = 300;
        rangeBetweenTrain = 700;
        isPointerFocusRight = false;
        isPointerFocusLeft = false;
        WinPopup.SetActive(false);

        difficultLevel = scoreManager.GetDifficultyLevel();
        InitTrainSection(difficultLevel);

        isPlayerFinished = new bool[4] { false, false, false, false };
        playerScores = new int[4] { 0, 0, 0, 0 };

        trains = trainSection.GetComponentsInChildren<Train>();
        SetTrainConnection(trains);

        pointerDiffPosition = new Vector3((trains[1].transform.position.x - trains[0].transform.position.x) / 2, rangeBetweenPointerAndTrain, 0);
        pointer.transform.position = trains[0].transform.position + pointerDiffPosition;

        currentEnemySuccessChance = 0.02f;
        InvokeRepeating(nameof(EnemyCountdown), 1.0f, 1.0f);
    }

    void InitTrainSection(int difficultLevel)
    {
        if (difficultLevel == 0)
        {
            // easy mode
            trainAmount = 4;
            crashedAmount = 1;
        }
        else if (difficultLevel == 1)
        {
            // normal mode
            trainAmount = 7;
            crashedAmount = 2;
        }
        else if (difficultLevel == 2)
        {
            // hard mode
            trainAmount = 10;
            crashedAmount = 3;
        }

        int currentCrashedAmount = 0;
        Vector3 genTrainPosition = trainInitialPosition.transform.position;
        for (int i = 0; i < trainAmount; i++)
        {
            GameObject newGameObject;

            if (currentCrashedAmount != crashedAmount && (Random.Range(0, 1.0f) < 0.5 || (i + 1 + crashedAmount >= trainAmount)) && i != 0)
            {
                newGameObject = Instantiate(crashedTrain, genTrainPosition, Quaternion.identity);
                currentCrashedAmount += 1;
            }
            else
            {
                newGameObject = Instantiate(normalTrain, genTrainPosition, Quaternion.identity);
            }

            newGameObject.transform.SetParent(trainSection.transform);
            genTrainPosition += new Vector3(rangeBetweenTrain, 0, 0);
        }
    }

    public int GetDifficultLevel()
    {
        return difficultLevel;
    }

    void SetTrainConnection(Train[] trains)
    {
        for (int i = 0; i < trains.Length - 1; i++)
        {
            trains[i].SetRightConnected(true);
            trains[i].SetNextTrainPosition(trains[i + 1].GetTrainPosition());
            trains[i].SetTrainIndex(i);
        }
        trains[^1].SetTrainIndex(trains.Length - 1);
    }

    public void GoLeftBokey()
    {
        if (currentPointerIndex > 0)
        {
            currentPointerIndex -= 1;
            if (isPointerFocusLeft)
            {
                Vector2 newTrainSectionPos = new(trainSection.transform.position.x + rangeBetweenTrain, trainSection.transform.position.y);
                trainSection.transform.position = newTrainSectionPos;
            }
            pointer.transform.position = trains[currentPointerIndex].transform.position + pointerDiffPosition;
            isPointerFocusLeft = true;
            isPointerFocusRight = false;
        }
    }

    public void GoRightBokey()
    {
        if (currentPointerIndex < trainAmount - 2)
        {
            currentPointerIndex += 1;
            if (isPointerFocusRight)
            {
                Vector2 newTrainSectionPos = new(trainSection.transform.position.x - rangeBetweenTrain, trainSection.transform.position.y);
                trainSection.transform.position = newTrainSectionPos;
            }
            pointer.transform.position = trains[currentPointerIndex].transform.position + pointerDiffPosition;
            isPointerFocusRight = true;
            isPointerFocusLeft = false;
        }
    }

    public void CutBokey()
    {
        trains = trainSection.GetComponentsInChildren<Train>();
        trains[currentPointerIndex].DisableTrainConnection();

        DeleteCrashedTrain();
    }

    public Train GetNextTrainInfo(int currentTrainIndex)
    {
        trains = trainSection.GetComponentsInChildren<Train>();
        if (trains[currentTrainIndex + 1] != null)
        {
            return trains[currentTrainIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public Train GetBeforeTrainInfo(int currentTrainIndex)
    {
        trains = trainSection.GetComponentsInChildren<Train>();
        if (trains[currentTrainIndex - 1] != null)
        {
            return trains[currentTrainIndex - 1];
        }
        else
        {
            return null;
        }
    }

    void DeleteCrashedTrain()
    {
        for (int i = 0; i < trains.Length; i++)
        {
            if (
                trains[i].GetTrainType() &&
                !trains[i - 1].GetConnectStatus() &&
                !trains[i].GetConnectStatus()
            )
            {
                StartCoroutine(DestroyTrainAndShift(i));
            }
        }
        trains = trainSection.GetComponentsInChildren<Train>();
    }

    IEnumerator DestroyTrainAndShift(int i)
    {
        Destroy(trains[i].gameObject);
        yield return new WaitForSeconds((float)0.25);

        trains = trainSection.GetComponentsInChildren<Train>();
        trainAmount--;
        crashedAmount--;
        if (!isPointerFocusLeft)
        {
            currentPointerIndex--;
            isPointerFocusRight = !isPointerFocusRight;
            isPointerFocusLeft = !isPointerFocusLeft;
        }
        for (int j = i; j < trains.Length; j++)
        {
            Vector3 nextTrainPos = new(trains[j - 1].transform.position.x + rangeBetweenTrain, trains[j - 1].transform.position.y, 0);
            trains[j].transform.position = nextTrainPos;
            if (j < trains.Length - 1)
            {
                trains[j].SetNextTrainPosition(trains[j + 1].GetTrainPosition());
            }
            trains[j].DecrementIndex();
        }
        pointer.transform.position = trains[currentPointerIndex].transform.position + pointerDiffPosition;
    }

    public void CheckWinCondition()
    {
        bool isAllTrainConnected = true;
        bool isCrashedTrainExist = false;

        for (int i = 0; i < trains.Length - 1; i++)
        {
            if (!trains[i].GetConnectStatus())
            {
                isAllTrainConnected = false;
            }
            if (trains[i].GetTrainType())
            {
                isCrashedTrainExist = true;
            }
        }

        if (!isCrashedTrainExist && isAllTrainConnected)
        {
            isPlayerFinished[0] = true;
            playerScores[0] += currentScorePrize;
            currentScorePrize--;
            ForceGameToEnd();
            WinPopup.SetActive(true);
            EndMiniGame();
        }
    }

    public void ForceGameToEnd()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!isPlayerFinished[i])
            {
                isPlayerFinished[i] = true;
                playerScores[i] = currentScorePrize;
                currentScorePrize--;
            }
        }
    }

    public void StartSlowCurser(Transform trainTail)
    {
        currentPlayerFocusTrainPos = trainTail;

        float frequency = 1.0f;
        if (difficultLevel == 1)
        {
            frequency = 0.7f;
        }
        else if (difficultLevel == 2)
        {
            frequency = 0.5f;
        }

        if (difficultLevel != 0)
        {
            InvokeRepeating(nameof(SlowCurser), frequency, frequency);
        }
    }

    public void StopSlowCurser()
    {
        CancelInvoke(nameof(SlowCurser));
    }

    void SlowCurser()
    {
        SetCursorPos(
            (int)((int)currentPlayerFocusTrainPos.position.x + (rangeBetweenTrain * Random.Range(0, 0.5f))),
            (int)((int)currentPlayerFocusTrainPos.position.y + (300 * Random.Range(0, 0.5f)))
        );
    }

    void EnemyCountdown()
    {
        for (int i = 1; i < 4; i++)
        {
            if (Random.Range(0.0f, 1.0f) < currentEnemySuccessChance && !isPlayerFinished[i])
            {
                isPlayerFinished[i] = true;
                playerScores[i] += currentScorePrize;
                enemyStatusImage[i - 1].sprite = finishedStatusImage;
                currentScorePrize--;
            }
        }

        float chaneIncreaseRate = 0.005f;
        if (difficultLevel != 0)
        {
            chaneIncreaseRate *= difficultLevel;
        }
        else
        {
            chaneIncreaseRate /= 2;
        }
        currentEnemySuccessChance += chaneIncreaseRate;
    }

    public void EndMiniGame()
    {
        scoreManager.UpdatePlayerScore(playerScores[0], playerScores[1], playerScores[2], playerScores[3]);
    }
}
