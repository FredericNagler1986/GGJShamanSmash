using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSelectionObject : MonoBehaviour
{
    public string InputButtonA = "";
    public string InputButtonB = "";
    public Text StatusText = null;
    private EPlayerSelectionStatus mStatus = EPlayerSelectionStatus.Inactive;

    public EPlayerSelectionStatus Status
    {
        get
        {
            return mStatus;
        }
        set
        {
            mStatus = value;
            UpdateStatusText();
        }
    }
    public int playerID = 0;
    public int MaskID = 1;


    // Use this for initialization
    void Start()
    {
        PlayerSelection.Instance.RegisterPlayerControl(this);
        UpdateStatusText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(InputButtonA))
        {
            switch (Status)
            {
                case EPlayerSelectionStatus.Inactive:
                    Status = EPlayerSelectionStatus.SelectingMask;
                    break;
                case EPlayerSelectionStatus.SelectingMask:
                    Status = EPlayerSelectionStatus.Ready;
                    break;
            }
        }
        if (Input.GetButtonDown(InputButtonB))
        {
            switch (Status)
            {
                case EPlayerSelectionStatus.SelectingMask:
                    Status = EPlayerSelectionStatus.Inactive;
                    break;
                case EPlayerSelectionStatus.Ready:
                    Status = EPlayerSelectionStatus.SelectingMask;
                    break;
            }
       }
    }

    void UpdateStatusText()
    {
        switch (Status)
        {
            case EPlayerSelectionStatus.Inactive:
                StatusText.text = "Press A to join";
                break;
            case EPlayerSelectionStatus.SelectingMask:
                StatusText.text = "Press A to confirm mask";
                break;
            case EPlayerSelectionStatus.Ready:
                StatusText.text = "Ready!";
                break;
        }
        PlayerSelection.Instance.UpdateButton();
    }


}
