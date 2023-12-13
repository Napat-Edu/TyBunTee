using UnityEngine;

public class WinnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] winnerImage;

    public void OpenWinnerIcon(int index)
    {
        winnerImage[index].SetActive(true);
    }
}
