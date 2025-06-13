using UnityEngine;

public class Vegeta_Base : Character_Controller
{
    protected override void Ki_Death_Flash()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            if (!isKiFinal)
            {
                animator.SetBool("Ki_Death_Flash", true);
                isKiFinal = true;
            }
        }
    }

    protected override void UpLevel_VegetaSSJ1()
    {
        bool uplevelKey = false;
        bool uplevelPad = false;

        if (tag == "Player 1") uplevelKey = Input.GetKeyDown(KeyCode.O);
        else if (tag == "Player 2") uplevelPad = gamePad.leftTrigger.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (uplevelKey || uplevelPad)
        {
            if (!isUpLevel)
            {
                animator.SetBool("UpLevel_VegetaSSJ1", true);
                isUpLevel = true;
            }
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
            if (!isUpLevel)
            {
                animator.SetBool("FusionDance_GogetaBase", true);
                transform.position = new Vector3(5.7f, transform.position.y, 0);
                transform.localScale = new Vector2(-1, 1);
                isUpLevel = true;
            }
        }
    }

    protected override void Create_FusionDance_GogetaBase()
    {
        Instantiate(FusionDance_GokuBase, new Vector3(-5.7f, transform.position.y, 0), Quaternion.identity);
    }

    protected override void Action_FusionDance_GogetaBase()
    {
        transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, 0);
    }

    protected override void FusionPotara_VegitoBase()
    {
        bool fusionKey = false;
        bool fusionPad = false;

        if (tag == "Player 1") fusionKey = Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2") fusionPad = gamePad.leftShoulder.isPressed && gamePad.rightShoulder.isPressed && gamePad.rightTrigger.wasPressedThisFrame;

        if (fusionKey || fusionPad)
        {
            if (!isUpLevel)
            {
                animator.SetBool("FusionPotara_VegitoBase", true);
                isUpLevel = true;
            }
        }
    }
}
