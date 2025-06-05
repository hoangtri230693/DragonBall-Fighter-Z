using UnityEngine;

public class Vegito_SSJ4 : Character_Controller
{
    protected override void Ki_Kamehameha_X10()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Kamehameha_X10", true);
            if (!isKiFinalSound)
            {
                characterSoundController.StopSoundCharacter();
                isKiFinalSound = true;
                characterSoundController.PlayKiKamehamehaSound();
            }
        }
    }
    protected override void Ki_Final_Flash_X10()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.leftShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Final_Flash_X10", true);
            if (!isKiFinalSound)
            {
                isKiFinalSound = true;
                characterSoundController.PlayKiFinalSound();
            }
        }
    }

    protected override void Ki_Kamehameha_Final()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightTrigger.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Kamehameha_Final", true);
            if (!isKiFinalSound)
            {
                characterSoundController.StopSoundCharacter();
                isKiFinalSound = true;
                characterSoundController.PlayKiKamehamehaFinalSound();
            }
        }
    }
}
