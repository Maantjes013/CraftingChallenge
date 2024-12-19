using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private InputAction interactAction;

    [SerializeField]
    private BankUI bankUI;

    [SerializeField]
    private CraftUI craftUI;

    [SerializeField]
    private FirstPersonController characterController;

    private GameObject activePanel;

    private void Awake()
    {
        InitiateBankUI();
        InitiateCraftUI();
    }

    private void Update()
    {
        if (activePanel != null)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void InitiateBankUI()
    {
        // Create the action for the "E" key
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.performed += context => ShowUI(bankUI.gameObject, () => bankUI.FillBankUI());
        interactAction.Enable();

        bankUI.gameObject.SetActive(false);
    }

    private void InitiateCraftUI()
    {
        // Create the action for the "C" key
        interactAction = new InputAction(binding: "<Keyboard>/c");
        interactAction.performed += context => ShowUI(craftUI.gameObject, () => craftUI.FillInventoryItems());
        interactAction.Enable();

        craftUI.gameObject.SetActive(false);
    }

    private void ShowUI(GameObject interfaceToShow, Action specialAction = null)
    {
        if (!CanOpenPanel(interfaceToShow))
            return;

        HandleCursor(true);
        interfaceToShow.SetActive(true);
        activePanel = interfaceToShow;

        specialAction?.Invoke();
    }

    public void HideUI()
    {
        activePanel.SetActive(false);
        HandleCursor(false);
        activePanel = null;
    }

    public void HandleCursor(bool showCursor)
    {
        characterController.enabled = !showCursor;
        Time.timeScale = showCursor ? 0 : 1;
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private bool CanOpenPanel(GameObject interfaceToShow)
    {
        bool canOpen = true;

        if (activePanel != null)
        {
            if (activePanel.Equals(interfaceToShow))
                canOpen = false;

            HideUI();
        }

        return canOpen;
    }

    private void OnDestroy()
    {
        interactAction.Disable();
    }
}
