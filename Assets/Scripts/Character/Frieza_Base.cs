using UnityEngine;

public class Frieza_Base : Character_Controller
{
    protected override void Ki_Blast()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Blast", true);
            if (!isKiFinalSound)
            {
                isKiFinalSound = true;
                characterSoundController.PlayKiFinalSound();
            }
        }
    }


    protected override void UpLevel_FriezaWhite()
    {
        bool uplevelKey = false;
        bool uplevelPad = false;

        if (tag == "Player 1") uplevelKey = Input.GetKeyDown(KeyCode.O);
        else if (tag == "Player 2") uplevelPad = gamePad.leftTrigger.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (uplevelKey || uplevelPad)
        {
            animator.SetBool("UpLevel_FriezaWhite", true);
            characterSoundController.PlayUpLevelSound();
        }
    }
}
