using UnityEngine;

public class Piccolo : Character_Controller
{
    protected override void Ki_Final_Flash()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.rightShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            if (!isKiFinal)
            {
                animator.SetBool("Ki_Final_Flash", true);
                isKiFinal = true;
            }
        }
    }

    protected override void Ki_Blast_Canon()
    {
        bool kiFinalKey = false;
        bool kiFinalPad = false;

        if (tag == "Player 1") kiFinalKey = Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Return);
        else if (tag == "Player 2" && gamePad != null) kiFinalPad = gamePad.leftShoulder.isPressed && gamePad.buttonNorth.wasPressedThisFrame;

        if (kiFinalKey || kiFinalPad)
        {
            if (!isKiFinal)
            {
                animator.SetBool("Ki_Blast_Canon", true);
                isKiFinal = true;
            }
        }
    }
}
