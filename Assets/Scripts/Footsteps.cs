using UnityEngine;

public class FootSteps: MonoBehaviour
{
    public GameObject footsteps;
    PlayerInputActions inputControls;

    void Start()
    {
        inputControls = new PlayerInputActions();
        inputControls.Enable();
        footsteps.SetActive(false); 
    }

    void Update()
    {
        Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();
        FootStep(input);
    }

    private void FootStep(Vector2 input)
    {
        Debug.Log(input);
        if (input.x != 0 || input.y != 0)
        {
            footsteps.SetActive(true);
        }
        else
        {
            footsteps.SetActive(false);
        }
    }
}