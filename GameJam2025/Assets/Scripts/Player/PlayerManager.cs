using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovementBase playerMovement;
    [SerializeField] Camera playerCamera;
    [SerializeField] PlayerCanvasController playerCanvasController;

    [Header("Data")]
    [SerializeField] private Color defaultAimColor;
    [SerializeField] private Color aimingAtInteractableColor;

    public Camera PlayerCamera => playerCamera;
    private InteractableItemBase inteactableItem = null;

    private void Update()
    {
        UpdateAimColor();
    }

    #region Interactable Logic

    public void OnEnterTriggerOfInteractable(InteractableItemBase item, bool canInteract)
    {
       
        playerCanvasController.ChangeAimColor(aimingAtInteractableColor);
        inteactableItem = item;
        
    }

    public void OnExitTriggerOfInteractable(InteractableItemBase item, bool canInteract)
    {
        playerCanvasController.ChangeAimColor(defaultAimColor);
        inteactableItem = null;   
    }

    private void UpdateAimColor()
    {
        if (inteactableItem == null)
        {
            playerCanvasController.ChangeAimColor(defaultAimColor);
            return;
        }

        if (inteactableItem.CanInteract())
            playerCanvasController.ChangeAimColor(aimingAtInteractableColor);
        else
            playerCanvasController.ChangeAimColor(defaultAimColor);
    }

    #endregion
}
