using UnityEngine;

public class Vegeta_SSJ4 : Character_Controller
{
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

    protected override void FusionDance_GogetaSSJ4()
    {
        bool fusionKey = false;
        bool fusionPad = false;

        if (tag == "Player 1") fusionKey = Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2") fusionPad = gamePad.leftShoulder.isPressed && gamePad.rightShoulder.isPressed && gamePad.leftTrigger.wasPressedThisFrame;

        if (fusionKey || fusionPad)
        {
            animator.SetBool("FusionDance_GogetaSSJ4", true);
            transform.position = new Vector3(5.7f, transform.position.y, 0);
            transform.localScale = new Vector2(1, 1);
            if (!isFusionSound)
            {
                isFusionSound = true;
                characterSoundController.PlayFusionDanceSound();
            }
        }
    }

    protected override void Create_FusionDance_GogetaSSJ4()
    {
        Instantiate(FusionDance_GokuSSJ4, new Vector3(-5.7f, transform.position.y, 0), Quaternion.identity);
    }

    protected override void Action_FusionDance_GogetaSSJ4()
    {
        transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, 0);
    }

    protected override void FusionPotara_VegitoSSJ4()
    {
        bool fusionKey = false;
        bool fusionPad = false;

        if (tag == "Player 1") fusionKey = Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2") fusionPad = gamePad.leftShoulder.isPressed && gamePad.rightShoulder.isPressed && gamePad.rightTrigger.wasPressedThisFrame;

        if (fusionKey || fusionPad)
        {
            animator.SetBool("FusionPotara_VegitoSSJ4", true);
            if (!isFusionSound)
            {
                isFusionSound = true;
                characterSoundController.PlayFusionDanceSound();
            }
        }
    }
}
