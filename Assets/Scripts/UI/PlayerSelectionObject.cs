using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerSelectionObject : MonoBehaviour
{
    public string InputButtonA = "";
    public string InputButtonB = "";
    public string InputHorizontalAxes = "";
    public Text StatusText = null;
    private EPlayerSelectionStatus mStatus = EPlayerSelectionStatus.Inactive;
    public Image MaskImage = null;

    private bool horizontalLeftDown = false;
    private bool horizontalRightDown = false;
    private bool horizontalLeftPressed = false;
    private bool horizontalRightPressed = false;

    private float AxisChange = 0.0f;
    

    public EPlayerSelectionStatus Status
    {
        get
        {
            return mStatus;
        }
        set
        {
            mStatus = value;
            UpdateMaskImage();
            UpdateStatusText();
        }
    }
    public int playerID = 0;
    public int MaskID = 0;


    // Use this for initialization
    void Start()
    {
        PlayerSelection.Instance.RegisterPlayerControl(this);
        UpdateStatusText();
        UpdateMaskImage();
    }

    private void UpdateMaskImage()
    {
        if (Status == EPlayerSelectionStatus.Inactive)
        {
            MaskImage.gameObject.SetActive(false);
        }
        else
        {
            MaskImage.gameObject.SetActive(true);
            MaskImage.sprite = Main.Instance.GameContent.Masks[MaskID];
        }

    }

    public void NextMask()
    {
        MaskID++;
        if (MaskID >= Main.Instance.GameContent.Masks.Count)
        {
            MaskID = 0;
        }
        UpdateMaskImage();
    }

    public void PreviousMask()
    {
        MaskID--;
        if (MaskID < 0)
        {
            MaskID = Main.Instance.GameContent.Masks.Count - 1;
        }
        UpdateMaskImage();
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
        if (Input.GetAxis(InputHorizontalAxes) >= 0.1f)
        {
            Debug.Log("Right");
            horizontalRightDown =  true;
            AxisChange += Time.deltaTime;
            if (AxisChange >= 0.5f)
            {
                Debug.Log("Horizontal Pressed");
                horizontalRightPressed = true;
                NextMask();
                AxisChange = 0.0f;
            }
        }
        else if (Input.GetAxis(InputHorizontalAxes) <= -0.1f)
        {
            horizontalLeftDown = true;
            AxisChange += Time.deltaTime;
            if (AxisChange >= 0.5f)
            {
               
                horizontalLeftPressed = true;
                PreviousMask();
                AxisChange = 0.0f;
            }
        }
        else
        {
            Debug.Log(horizontalRightPressed + " - " + horizontalRightDown);
            if (horizontalLeftDown && !horizontalLeftPressed)
            {
                PreviousMask();
            }
            if (horizontalRightDown && !horizontalRightPressed)
            {
                NextMask();
            }
            horizontalLeftDown = false;
            horizontalRightDown = false;
            horizontalLeftPressed = false;
            horizontalRightPressed = false;
            AxisChange = 0.0f;
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
