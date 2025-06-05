using UnityEngine;

public class Gogeta_Base : Character_Controller
{
    protected override void Ki_Kamehameha()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Kamehameha", true);
            if (!isKiFinalSound)
            {
                isKiFinalSound = true;
                characterSoundController.PlayKiKamehamehaSound();
            }
        }
    }

    protected override void Ki_Death_Flash()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.leftShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            animator.SetBool("Ki_Death_Flash", true);
            if (!isKiFinalSound)
            {
                isKiFinalSound = true;
                characterSoundController.PlayKiFinalSound();
            }
        }
    }

    protected override void UpLevel_GogetaSSJ1()
    {
        bool uplevelKey = false;
        bool uplevelPad = false;

        if (tag == "Player 1") uplevelKey = Input.GetKeyDown(KeyCode.O);
        else if (tag == "Player 2") uplevelPad = gamePad.leftTrigger.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (uplevelKey || uplevelPad)
        {
            animator.SetBool("UpLevel_GogetaSSJ1", true);
            characterSoundController.PlayUpLevelSound();
        }
    }
}
