using thisAPP.UIElements;
using UnityEngine;

public class ResultsInfoManager : MonoBehaviour
{
    public ResultsInfo uiElements;
    public void SetResultsInfo()
    { uiElements.Winner.text = "El ganador es...." + "name"; }

}
