using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs plyaerInputs { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        plyaerInputs = new PlayerInputs();
        playerActions = plyaerInputs.Player;
    }

    private void OnEnable()
    {
        plyaerInputs.Enable();
    }

    private void OnDisable()
    {
        plyaerInputs.Disable();
    }
}
