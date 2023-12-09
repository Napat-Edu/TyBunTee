using UnityEngine;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button cutBokeyButton;

    [SerializeField] GameObject trainSection;
    [SerializeField] GameObject normalTrain;
    [SerializeField] GameObject crashedTrain;
    [SerializeField] GameObject pointer;

    [SerializeField] Transform trainInitialPosition;

    Train[] trains;
    Vector3 pointerDiffPosition;

    int currentPointerIndex;
    int trainAmount;
    int crashedAmount;
    int rangeBetweenTrain;
    int rangeBetweenPointerAndTrain;

    void Awake()
    {
        currentPointerIndex = 0;
        trainAmount = 0;
        crashedAmount = 0;
        rangeBetweenPointerAndTrain = 300;
        rangeBetweenTrain = 700;

        InitTrainSection(0);

        trains = trainSection.GetComponentsInChildren<Train>();
        SetTrainConnection(trains);

        pointerDiffPosition = new Vector3((trains[1].transform.position.x - trains[0].transform.position.x) / 2, rangeBetweenPointerAndTrain, 0);
        pointer.transform.position = trains[0].transform.position + pointerDiffPosition;
    }

    void InitTrainSection(int mode)
    {
        if (mode == 0)
        {
            // easy mode
            trainAmount = 3;
            crashedAmount = 1;
        }

        int currentCrashedAmount = 0;
        Vector3 genTrainPosition = trainInitialPosition.transform.position;
        for (int i = 0; i < trainAmount; i++)
        {
            GameObject newGameObject;

            if (currentCrashedAmount != crashedAmount && (Random.Range(0, 1.0f) < 0.5 || (i + 1 + crashedAmount >= trainAmount)))
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

    void SetTrainConnection(Train[] trains)
    {
        for (int i = 0; i < trains.Length - 1; i++)
        {
            trains[i].SetRightConnected(true);
            trains[i].SetNextTrainPosition(trains[i + 1].GetTrainPosition());
        }
    }

    public void GoLeftBokey()
    {
        if (currentPointerIndex > 0)
        {
            currentPointerIndex -= 1;
            pointer.transform.position = trains[currentPointerIndex].transform.position + pointerDiffPosition;
        }
    }

    public void GoRightBokey()
    {
        if (currentPointerIndex < trainAmount - 2)
        {
            currentPointerIndex += 1;
            pointer.transform.position = trains[currentPointerIndex].transform.position + pointerDiffPosition;
        }
    }

    public void CutBokey()
    {
        trains = trainSection.GetComponentsInChildren<Train>();
        trains[currentPointerIndex].DisableTrainConnection();
    }

    public Train GetNextTrainInfo()
    {
        trains = trainSection.GetComponentsInChildren<Train>();
        if (trains[currentPointerIndex + 1] != null)
        {
            return trains[currentPointerIndex + 1];
        }
        else
        {
            return null;
        }
    }
}
