using UnityEngine;

public class Goku_Base : Character_Controller
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

    protected override void UpLevel_GokuSSJ1()
    {
        bool uplevelKey = false;
        bool uplevelPad = false;

        if (tag == "Player 1") uplevelKey = Input.GetKeyDown(KeyCode.O);
        else if (tag == "Player 2") uplevelPad = gamePad.leftTrigger.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (uplevelKey || uplevelPad)
        {
            animator.SetBool("UpLevel_GokuSSJ1", true);
            characterSoundController.PlayUpLevelSound();
        }
    }


    protected override void FusionDance_GogetaBase()
    {
        bool fusionKey = false;
        bool fusionPad = false;

        if (tag == "Player 1") fusionKey = Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2") fusionPad = gamePad.leftShoulder.isPressed && gamePad.rightShoulder.isPressed && gamePad.leftTrigger.wasPressedThisFrame;

        if (fusionKey || fusionPad)
        {
            animator.SetBool("FusionDance_GogetaBase", true);
            transform.position = new Vector3(-5.7f, transform.position.y, 0);
            if (!isFusionSound)
            {
                isFusionSound = true;
                characterSoundController.PlayFusionDanceSound();
            }
        }
    }

    protected override void Create_FusionDance_GogetaBase()
    {
        Instantiate(FusionDance_VegetaBase, new Vector3(5.7f, transform.position.y, 0), Quaternion.Euler(0, 180, 0));
    }

    protected override void Action_FusionDance_GogetaBase()
    {
        transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, 0);
    }

    protected override void FusionPotara_VegitoBase()
    {
        bool fusionKey = false;
        bool fusionPad = false;

        if (tag == "Player 1") fusionKey = Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2") fusionPad = gamePad.leftShoulder.isPressed && gamePad.rightShoulder.isPressed && gamePad.rightTrigger.wasPressedThisFrame;

        if (fusionKey || fusionPad)
        {
            animator.SetBool("FusionPotara_VegitoBase", true);
            if (!isFusionSound)
            {
                isFusionSound = true;
                characterSoundController.PlayFusionDanceSound();
            }
        }
    }
}
