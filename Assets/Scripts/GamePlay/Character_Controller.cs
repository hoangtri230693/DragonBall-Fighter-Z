using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Controller : MonoBehaviour
{
    [Header("References")]
    protected Animator animator;
    protected GameObject opponent;


    [Header("Character UpLevel")]
    [SerializeField] protected GameObject Cell_Kid_Prefab;
    [SerializeField] protected GameObject Cell_Perfect_Prefab;
    [SerializeField] protected GameObject Cell_Stage2_Prefab;
    [SerializeField] protected GameObject Frieza_White_Prefab;
    [SerializeField] protected GameObject FutureTrunks_SSJ1_Prefab;
    [SerializeField] protected GameObject Gogeta_Base_Prefab;
    [SerializeField] protected GameObject Gogeta_SSJ1_Prefab;
    [SerializeField] protected GameObject GohanAdult_SSJ2_Prefab;
    [SerializeField] protected GameObject GohanAdult_Mystic_Prefab;
    [SerializeField] protected GameObject GohanChild_SSJ1_Prefab;
    [SerializeField] protected GameObject GohanChild_SSJ2_Prefab;
    [SerializeField] protected GameObject Goku_SSJ1_Prefab;
    [SerializeField] protected GameObject Goku_SSJ3_Prefab;
    [SerializeField] protected GameObject Goku_SSJ4_Prefab;
    [SerializeField] protected GameObject Gotenks_Base_Prefab;
    [SerializeField] protected GameObject Gotenks_SSJ1_Prefab;
    [SerializeField] protected GameObject Gotenks_SSJ3_Prefab;
    [SerializeField] protected GameObject MajinBuu_Prefab;
    [SerializeField] protected GameObject SuperBuu_Gohan_Prefab;
    [SerializeField] protected GameObject SuperBuu_Gotenks_Prefab;
    [SerializeField] protected GameObject Vegeta_SSJ1_Prefab;
    [SerializeField] protected GameObject Vegeta_SSJ4_Prefab;
    [SerializeField] protected GameObject Vegito_Base_Prefab;
    [SerializeField] protected GameObject Vegito_SSJ1_Prefab;
    [SerializeField] protected GameObject Vegito_SSJ4_Prefab;
    [SerializeField] protected GameObject Gogeta_SSJ4_Prefab;

    [Header("Character Fusion")]
    [SerializeField] protected GameObject FusionDance_VegetaBase;
    [SerializeField] protected GameObject FusionDance_GokuBase;
    [SerializeField] protected GameObject FusionDance_VegetaSSJ4;
    [SerializeField] protected GameObject FusionDance_GokuSSJ4;

    [Header("Ki Final")]
    [SerializeField] protected GameObject ki_Beam_Prefab;
    [SerializeField] protected GameObject ki_Blast_Prefab;
    [SerializeField] protected GameObject ki_Blast_Canon_Prefab;
    [SerializeField] protected GameObject ki_Death_Beam_Prefab;
    [SerializeField] protected GameObject ki_Death_Flash_Prefab;
    [SerializeField] protected GameObject ki_Final_Flash_Prefab;
    [SerializeField] protected GameObject ki_Final_Flash_X10_Prefab;
    [SerializeField] protected GameObject ki_Kamehameha_Prefab;
    [SerializeField] protected GameObject ki_Kamehameha_X10_Prefab;
    [SerializeField] protected GameObject ki_Attack_Bigbang_Prefab;
    [SerializeField] protected GameObject ki_Kamehameha_Bigbang_Prefab;
    [SerializeField] protected GameObject ki_Kamehameha_Final_Prefab;
    [SerializeField] protected GameObject ki_DragonFist_Prefab;

    [Header("Ki Attack")]
    [SerializeField] protected GameObject ki_Ghost_Prefab;
    [SerializeField] protected GameObject ki_Attack_Prefab;
    [SerializeField] protected GameObject ki_Attack_Final_Prefab;

    [Header("Attack")]
    [SerializeField] protected ParticleSystem attackEffect;
    [SerializeField] protected Transform hitPoint;
    protected float hitRadius = 0.25f;
    protected LayerMask opponentLayer;

    protected float moveSpeed = 10.0f;
    protected float doubleTapTime = 0.3f;
    protected float moveDirection = 1f;

    // Tốc độ nhấn phím theo hành động
    protected float moveLastTapTime = 0f;
    protected float attackLastTapTime = 0f;
    protected float kiAttackLastTapTime = 0f;


    // Cờ kiểm tra âm thanh
    protected bool isFlySound = false;
    protected bool isAttackSound = false;
    protected bool isEnergySound = false;
    protected bool isFusionSound = false;
    protected bool isKiFinalSound = false;

    // Cờ kiểm tra hành động
    public bool isDefending = false;
    public bool isAttacking = false;

    // Các script liên kết
    protected Player_Controller playerController;
    protected CharacterSounds_Controller characterSoundController;
    protected Character_Controller characterController_opponent;
    protected BattleStart_Controller battleStartController;
    protected Gamepad gamePad;


    protected virtual void Start()
    {
        StartPlayerTag();
    }
    protected virtual void Update()
    {
        //Kiểm tra thời gian
        if (!battleStartController.isRunning) return;

        //Kiểm tra kết nối gamepad
        if (gameObject.tag == "Player 2" && gamePad == null)
        {
            gamePad = Gamepad.current;
            return;
        }

        //Kiểm tra tình trạng đối thủ
        if (opponent == null) UpdateOpponent();

        //Kiểm soát hành động
        HandleActions();

        ForceStopAnimations();
    }

    protected void StartPlayerTag()
    {
        animator = GetComponent<Animator>();
        if (gameObject.tag == "Player 1")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 2");
            moveDirection = 1;
            transform.localScale = new Vector2(moveDirection, 1);
            playerController = GameObject.FindGameObjectWithTag("PlayerControl 1").GetComponent<Player_Controller>();
            characterSoundController = GameObject.FindGameObjectWithTag("PlayerControl 1").GetComponent<CharacterSounds_Controller>();
        }
        else if (gameObject.tag == "Player 2")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 1");
            moveDirection = -1;
            transform.localScale = new Vector2(moveDirection, 1);
            gamePad = Gamepad.current;
            playerController = GameObject.FindGameObjectWithTag("PlayerControl 2").GetComponent<Player_Controller>();
            characterSoundController = GameObject.FindGameObjectWithTag("PlayerControl 2").GetComponent<CharacterSounds_Controller>();
        }
        battleStartController = GameObject.Find("BattleStart_Controller").GetComponent<BattleStart_Controller>();
        characterController_opponent = opponent.GetComponent<Character_Controller>();
        opponentLayer = LayerMask.GetMask("Attack");
    }
    protected void UpdateOpponent()
    {
        if (gameObject.tag == "Player 1")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 2");
        }
        else if (gameObject.tag == "Player 2")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 1");
        }
        characterController_opponent = opponent.GetComponent<Character_Controller>();
    }
    protected void HandleActions()
    {
        if (playerController.canAttack)
        {
            Fly();
            Move();
            Attack();
            Defense();

            Ki_Attack();
            Ki_Ghost();
            Ki_Beam();
            Ki_Blast();
            Ki_Blast_Canon();
            Ki_Death_Beam();
            Ki_Death_Flash();
            Ki_Final_Flash();
            Ki_Final_Flash_X10();
            Ki_Kamehameha();
            Ki_Kamehameha_X10();
            Ki_Attack_Bigbang();
            Ki_Kamehameha_Bigbang();
            Ki_Kamehameha_Final();
            Ki_DragonFist();

            UpLevel_CellKid();
            UpLevel_CellStage2();
            UpLevel_CellPerfect();
            UpLevel_FriezaWhite();
            UpLevel_FutureTrunksSSJ1();
            UpLevel_GogetaSSJ1();
            UpLevel_GohanAdultSSJ2();
            UpLevel_GohanAdultMystic();
            UpLevel_GohanChildSSJ1();
            UpLevel_GohanChildSSJ2();
            UpLevel_GokuSSJ1();
            UpLevel_GokuSSJ3();
            UpLevel_GokuSSJ4();
            UpLevel_GotenksSSJ1();
            UpLevel_GotenksSSJ3();
            UpLevel_MajinBuu();
            UpLevel_SuperBuu_Gotenks();
            UpLevel_SuperBuu_Gohan();
            UpLevel_VegetaSSJ1();
            UpLevel_VegetaSSJ4();
            UpLevel_VegetaSSJ4();

            FusionDance_GogetaBase();
            FusionPotara_VegitoBase();
            FusionDance_GogetaSSJ4();
            FusionPotara_VegitoSSJ4();
        }

        if (playerController.canCharge)
        {
            Up_Energy();
        }
    }
    protected void ForceStopAnimations()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (!playerController.canAttack)
        {
            if (state.IsName("Fly")) animator.SetBool("Fly", false);
            if (state.IsName("Move")) animator.SetBool("Move", false);
            if (state.IsName("Attack")) animator.SetBool("Attack", false);
            if (state.IsName("Defense")) animator.SetBool("Defense", false);
            if (state.IsName("Ki_Attack")) animator.SetBool("Ki_Attack", false);
            if (state.IsName("Ki_Ghost")) animator.SetBool("Ki_Ghost", false);
            if (state.IsName("Ki_Beam")) animator.SetBool("Ki_Beam", false);
            if (state.IsName("Ki_Blast")) animator.SetBool("Ki_Blast", false);
            if (state.IsName("Ki_Blast_Canon")) animator.SetBool("Ki_Blast_Canon", false);
            if (state.IsName("Ki_Death_Beam")) animator.SetBool("Ki_Death_Beam", false);
            if (state.IsName("Ki_Death_Flash")) animator.SetBool("Ki_Death_Flash", false);
            if (state.IsName("Ki_Final_Flash")) animator.SetBool("Ki_Final_Flash", false);
            if (state.IsName("Ki_Final_Flash_X10")) animator.SetBool("Ki_Final_Flash_X10", false);
            if (state.IsName("Ki_Kamehameha")) animator.SetBool("Ki_Kamehameha", false);
            if (state.IsName("Ki_Kamehameha_X10")) animator.SetBool("Ki_Kamehameha_X10", false);
            if (state.IsName("Ki_Attack_Bigbang")) animator.SetBool("Ki_Attack_Bigbang", false);
            if (state.IsName("Ki_Kamehameha_Bigbang")) animator.SetBool("Ki_Kamehameha_Bigbang", false);
            if (state.IsName("Ki_Kamehameha_Final")) animator.SetBool("Ki_Kamehameha_Final", false);
            if (state.IsName("Ki_DragonFist")) animator.SetBool("Ki_DragonFist", false);

            isFlySound = false;
            isAttackSound = false;
            isFusionSound = false;
            isKiFinalSound = false;
            isDefending = false;
            isAttacking = false;
        }

        if (!playerController.canCharge)
        {
            if (state.IsName("Up_Energy"))
            {
                characterSoundController.StopSoundCharacter();
                animator.SetBool("Up_Energy", false);
                isEnergySound = false;
            }
        }
    }
    protected virtual void ForceStopAnimations_NoConditions()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Fly")) animator.SetBool("Fly", false);
        if (state.IsName("Move")) animator.SetBool("Move", false);
        if (state.IsName("Attack")) animator.SetBool("Attack", false);
        if (state.IsName("Defense")) animator.SetBool("Defense", false);
        if (state.IsName("Ki_Attack")) animator.SetBool("Ki_Attack", false);
        if (state.IsName("Ki_Ghost")) animator.SetBool("Ki_Ghost", false);
        if (state.IsName("Ki_Beam")) animator.SetBool("Ki_Beam", false);
        if (state.IsName("Ki_Blast")) animator.SetBool("Ki_Blast", false);
        if (state.IsName("Ki_Blast_Canon")) animator.SetBool("Ki_Blast_Canon", false);
        if (state.IsName("Ki_Death_Beam")) animator.SetBool("Ki_Death_Beam", false);
        if (state.IsName("Ki_Death_Flash")) animator.SetBool("Ki_Death_Flash", false);
        if (state.IsName("Ki_Final_Flash")) animator.SetBool("Ki_Final_Flash", false);
        if (state.IsName("Ki_Final_Flash_X10")) animator.SetBool("Ki_Final_Flash_X10", false);
        if (state.IsName("Ki_Kamehameha")) animator.SetBool("Ki_Kamehameha", false);
        if (state.IsName("Ki_Kamehameha_X10")) animator.SetBool("Ki_Kamehameha_X10", false);
        if (state.IsName("Ki_Attack_Bigbang")) animator.SetBool("Ki_Attack_Bigbang", false);
        if (state.IsName("Ki_Kamehameha_Bigbang")) animator.SetBool("Ki_Kamehameha_Bigbang", false);
        if (state.IsName("Ki_Kamehameha_Final")) animator.SetBool("Ki_Kamehameha_Final", false);
        if (state.IsName("Ki_DragonFist")) animator.SetBool("Ki_DragonFist", false);

        if (state.IsName("Up_Energy")) animator.SetBool("Up_Energy", false);

        isEnergySound = false;
        isFlySound = false;
        isAttackSound = false;
        isFusionSound = false;
        isKiFinalSound = false;
        isDefending = false;
        isAttacking = false;
    }


    protected virtual bool Fly()
    {
        bool leftKey = false;
        bool rightKey = false;
        bool leftPad = false;
        bool rightPad = false;

        if (tag == "Player 1")
        {
            leftKey = Input.GetKey(KeyCode.LeftArrow);
            rightKey = Input.GetKey(KeyCode.RightArrow);
        }
        else if (tag == "Player 2" && gamePad != null)
        {
            leftPad = gamePad.dpad.left.isPressed || gamePad.leftStick.left.ReadValue() > 0.1f;
            rightPad = gamePad.dpad.right.isPressed || gamePad.leftStick.right.ReadValue() > 0.1f;
        }

        if (leftKey || leftPad)
        {
            moveDirection = -1.0f;
            transform.localScale = new Vector2(moveDirection, 1);
            transform.position += new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("Fly", true);

            if (!isFlySound)
            {
                characterSoundController.PlayFlySound();
                isFlySound = true;
            }

            playerController.UseMP(0.5f);
            return true;
        }
        else if (rightKey || rightPad)
        {
            moveDirection = 1.0f;
            transform.localScale = new Vector2(moveDirection, 1);
            transform.position += new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("Fly", true);

            if (!isFlySound)
            {
                characterSoundController.PlayFlySound();
                isFlySound = true;
            }

            playerController.UseMP(0.5f);
            return true;
        }
        else if (!leftKey || !rightKey || !leftPad || !rightPad)
        {
            animator.SetBool("Fly", false);
            if (isFlySound) isFlySound = false;
        }
        return false;
    }
    protected virtual void Move()
    {
        bool leftKey = false;
        bool rightKey = false;
        bool leftPad = false;
        bool rightPad = false;

        if (tag == "Player 1")
        {
            leftKey = Input.GetKeyDown(KeyCode.LeftArrow);
            rightKey = Input.GetKeyDown(KeyCode.RightArrow);
        }
        else if (tag == "Player 2" && gamePad != null)
        {
            leftPad = gamePad.dpad.left.wasPressedThisFrame;
            rightPad = gamePad.dpad.right.wasPressedThisFrame;
        }

        if (leftKey || rightKey || leftPad || rightPad)
        {
            if (Time.time - moveLastTapTime < doubleTapTime)
            {
                animator.SetBool("Move", true);
                characterSoundController.PlayMoveSound();
                playerController.UseMP(10);
            }
            moveLastTapTime = Time.time;
        }
    }
    protected virtual void Attack()
    {
        bool attackKey = false;
        bool attackPad = false;

        if (tag == "Player 1") attackKey = Input.GetKeyDown(KeyCode.D);
        else if (tag == "Player 2" && gamePad != null) attackPad = gamePad.buttonWest.wasPressedThisFrame;

        if (attackKey || attackPad)
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            if (!isAttackSound)
            {
                characterSoundController.PlayAttackSound();
                isAttackSound = true;
            }

            attackLastTapTime = Time.time;
            playerController.UseMP(2);
            attackEffect.Play();
        }
        else if (isAttacking && Time.time - attackLastTapTime > doubleTapTime)
        {
            isAttacking = false;
            animator.SetBool("Attack", false);
            if (isAttackSound)
            {
                characterSoundController.StopSoundCharacter();
                isAttackSound = false;
            }
            attackEffect.Stop();
            if (characterController_opponent.animator.GetBool("Hurt"))
            {
                characterController_opponent.animator.SetBool("Hurt", false);
            }
        }
    }
    protected virtual void Defense()
    {
        bool defenseKey = false;
        bool defensePad = false;
        if (tag == "Player 1") defenseKey = Input.GetKey(KeyCode.A);
        else if (tag == "Player 2" && gamePad != null) defensePad = gamePad.buttonNorth.isPressed;

        if (defenseKey || defensePad)
        {
            isDefending = true;
            if (animator.GetBool("Hurt"))
            {
                animator.SetBool("Hurt", false);
            }
            animator.SetBool("Defense", true);
            playerController.UseMP(0.2f);
        }
        else if (!defenseKey || !defensePad)
        {
            isDefending = false;
            animator.SetBool("Defense", false);
        }
    }
    protected virtual void Up_Energy()
    {
        bool chargeKey = false;
        bool chargePad = false;
        if (tag == "Player 1") chargeKey = Input.GetKey(KeyCode.E);
        else if (tag == "Player 2" && gamePad != null) chargePad = gamePad.buttonSouth.isPressed;


        if (chargeKey || chargePad)
        {
            animator.SetBool("Up_Energy", true);

            if (!isEnergySound)
            {
                isEnergySound = true;
                characterSoundController.PlayUpEnergySound();
            }

            playerController.RestoreMP(2);
        }
        else if (!chargeKey || !chargePad)
        {
            animator.SetBool("Up_Energy", false);

            if (isEnergySound)
            {
                characterSoundController.StopSoundCharacter();
                isEnergySound = false;
            }
        }
    }
    protected virtual void Ki_Attack()
    {
        bool kiattackKey = false;
        bool kiattackPad = false;
        if (tag == "Player 1") kiattackKey = Input.GetKeyDown(KeyCode.S);
        else if (tag == "Player 2") kiattackPad = gamePad.buttonEast.wasPressedThisFrame;

        if (kiattackKey || kiattackPad)
        {
            animator.SetBool("Ki_Attack", true);
            characterSoundController.PlayKiBaseSound();
            kiAttackLastTapTime = Time.time;
            playerController.UseMP(10);
        }
        else if (Time.time - kiAttackLastTapTime > doubleTapTime)
        {
            animator.SetBool("Ki_Attack", false); ;
        }
    }


    // 🎯 Xử lý skill đặc biệt - override
    protected virtual void Ki_Ghost()
    {

    }
    protected virtual void Ki_Beam()
    {

    }
    protected virtual void Ki_Blast()
    {

    }
    protected virtual void Ki_Blast_Canon()
    {

    }
    protected virtual void Ki_Death_Beam()
    {

    }
    protected virtual void Ki_Death_Flash()
    {

    }
    protected virtual void Ki_Final_Flash()
    {

    }
    protected virtual void Ki_Final_Flash_X10()
    {

    }
    protected virtual void Ki_Kamehameha()
    {

    }
    protected virtual void Ki_Kamehameha_X10()
    {

    }
    protected virtual void Ki_Attack_Bigbang()
    {

    }
    protected virtual void Ki_Kamehameha_Bigbang()
    {

    }
    protected virtual void Ki_Kamehameha_Final()
    {

    }
    protected virtual void Ki_DragonFist()
    {

    }
    protected virtual void UpLevel_CellKid()
    {

    }
    protected virtual void UpLevel_CellStage2()
    {

    }
    protected virtual void UpLevel_CellPerfect()
    {

    }
    protected virtual void UpLevel_FriezaWhite()
    {

    }
    protected virtual void UpLevel_FutureTrunksSSJ1()
    {

    }
    protected virtual void UpLevel_GogetaSSJ1()
    {

    }
    protected virtual void UpLevel_GohanAdultSSJ2()
    {

    }
    protected virtual void UpLevel_GohanAdultMystic()
    {

    }
    protected virtual void UpLevel_GohanChildSSJ1()
    {

    }
    protected virtual void UpLevel_GohanChildSSJ2()
    {

    }
    protected virtual void UpLevel_GokuSSJ1()
    {

    }
    protected virtual void UpLevel_GokuSSJ3()
    {

    }
    protected virtual void UpLevel_GokuSSJ4()
    {

    }
    protected virtual void UpLevel_GotenksSSJ1()
    {

    }
    protected virtual void UpLevel_GotenksSSJ3()
    {

    }
    protected virtual void UpLevel_MajinBuu()
    {

    }
    protected virtual void UpLevel_SuperBuu_Gotenks()
    {

    }
    protected virtual void UpLevel_SuperBuu_Gohan()
    {

    }
    protected virtual void UpLevel_VegetaSSJ1()
    {

    }
    protected virtual void UpLevel_VegetaSSJ4()
    {

    }
    protected virtual void UpLevel_VegitoSSJ1()
    {

    }
    protected virtual void FusionDance_GogetaBase()
    {

    }
    protected virtual void FusionPotara_VegitoBase()
    {

    }
    protected virtual void FusionDance_GogetaSSJ4()
    {

    }
    protected virtual void FusionPotara_VegitoSSJ4()
    {

    }


    // 🎯 Xử lý event skill base
    protected virtual void Spawn_BaseSkill(GameObject ki_Prefab)
    {
        if (moveDirection == 1)
        {
            Instantiate(ki_Prefab, new Vector3((transform.position.x + 0.5f), transform.position.y, 0), Quaternion.identity);
        }
        else if (moveDirection == -1)
        {
            GameObject baseskill = null;
            baseskill = Instantiate(ki_Prefab, new Vector3((transform.position.x - 0.5f), transform.position.y, 0), Quaternion.identity);
            baseskill.transform.localScale = new Vector2(moveDirection, 1);
        }
        playerController.UseMP(10);
    }
    protected virtual void End_Move()
    {
        animator.SetBool("Move", false);
        if (moveDirection == -1.0f)
        {
            transform.position = new Vector3(opponent.transform.position.x + 1.0f, transform.position.y, 0);
        }
        else if (moveDirection == 1.0f)
        {
            transform.position = new Vector3(opponent.transform.position.x - 1.0f, transform.position.y, 0);
        }
    }
    protected virtual void End_Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, opponentLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject == this.gameObject)
            {
                continue;
            }

            var opponentCharacterController = enemy.GetComponent<Character_Controller>();
            var opponentPlayerController = enemy.GetComponent<Player_Controller>();
            if (opponentCharacterController != null)
            {
                if (opponentCharacterController.isDefending)
                {
                    opponentCharacterController.animator.SetBool("Hurt", false);
                }
                else if (opponentCharacterController.isAttacking)
                {
                    opponentCharacterController.animator.SetBool("Hurt", false);
                    playerController.UpdateScore(100);
                }
                else if (!opponentCharacterController.isDefending && !opponentCharacterController.isAttacking)
                {
                    opponentCharacterController.animator.SetBool("Hurt", true);
                    opponentCharacterController.playerController.UpdateScore(-100);
                }
            }
        }
    }
    protected virtual void End_Ki_Attack()
    {
        Spawn_BaseSkill(ki_Attack_Prefab);
    }
    protected virtual void End_Ki_Attack_Final()
    {
        if (moveDirection == 1)
        {
            Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x + 0.5f), transform.position.y, 0), Quaternion.identity);
            Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x + 0.5f), (transform.position.y - 1.0f), 0), Quaternion.identity);
            Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x + 0.5f), (transform.position.y + 1.0f), 0), Quaternion.identity);
        }
        else if (moveDirection == -1)
        {
            GameObject baseskill = null;

            baseskill = Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x - 0.5f), transform.position.y, 0), Quaternion.identity);
            baseskill.transform.localScale = new Vector2(moveDirection, 1);

            baseskill = Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x - 0.5f), (transform.position.y - 1.0f), 0), Quaternion.identity);
            baseskill.transform.localScale = new Vector2(moveDirection, 1);

            baseskill = Instantiate(ki_Attack_Final_Prefab, new Vector3((transform.position.x - 0.5f), (transform.position.y + 1.0f), 0), Quaternion.identity);
            baseskill.transform.localScale = new Vector2(moveDirection, 1);
        }
        playerController.UseMP(20);
    }



    // 🎯 Xử lý event skill đặc biệt
    protected virtual void Spawn_SpecialSkill(GameObject ki_Prefab)
    {

        if (moveDirection == 1)
        {
            Instantiate(ki_Prefab, new Vector3((transform.position.x + 3.0f), transform.position.y, 0), Quaternion.identity);
        }
        else if (moveDirection == -1)
        {
            GameObject specialskill = null;
            specialskill = Instantiate(ki_Prefab, new Vector3((transform.position.x - 3.0f), transform.position.y, 0), Quaternion.identity);
            specialskill.transform.localScale = new Vector2(moveDirection, 1);
        }
        playerController.UseMP(400);
    }
    protected virtual void End_Ki_Ghost() => Spawn_SpecialSkill(ki_Ghost_Prefab);
    protected virtual void End_Ki_Beam() => Spawn_SpecialSkill(ki_Beam_Prefab);
    protected virtual void End_Ki_Blast() => Spawn_SpecialSkill(ki_Blast_Prefab);
    protected virtual void End_Ki_Blast_Canon() => Spawn_SpecialSkill(ki_Blast_Canon_Prefab);
    protected virtual void End_Ki_Death_Beam() => Spawn_SpecialSkill(ki_Death_Beam_Prefab);
    protected virtual void End_Ki_Death_Flash() => Spawn_SpecialSkill(ki_Death_Flash_Prefab);
    protected virtual void End_Ki_Final_Flash() => Spawn_SpecialSkill(ki_Final_Flash_Prefab);
    protected virtual void End_Ki_Final_Flash_X10() => Spawn_SpecialSkill(ki_Final_Flash_X10_Prefab);
    protected virtual void End_Ki_Kamehameha() => Spawn_SpecialSkill(ki_Kamehameha_Prefab);
    protected virtual void End_Ki_Kamehameha_X10() => Spawn_SpecialSkill(ki_Kamehameha_X10_Prefab);
    protected virtual void End_Ki_Attack_Bigbang() => Spawn_SpecialSkill(ki_Attack_Bigbang_Prefab);
    protected virtual void End_Ki_Kamehameha_Bigbang() => Spawn_SpecialSkill(ki_Kamehameha_Bigbang_Prefab);
    protected virtual void End_Ki_Kamehameha_Final() => Spawn_SpecialSkill(ki_Kamehameha_Final_Prefab);
    protected virtual void Action_Ki_DragonFist()
    {
        if (moveDirection == 1)
        {
            transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, 0);
        }
        else if (moveDirection == -1)
        {
            transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, 0);
        }
    }
    protected virtual void End_Ki_DragonFist() => Spawn_SpecialSkill(ki_DragonFist_Prefab);


    // 🎯 Xử lý event skill uplevel
    protected virtual void Spawn_CharacterUpLevel(GameObject characterPrefab, string animationName)
    {
        animator.SetBool(animationName, false);

        // Tạo nhân vật mới
        GameObject newCharacter = null;
        newCharacter = Instantiate(characterPrefab, transform.position, Quaternion.identity);
        newCharacter.tag = gameObject.tag;

        // Xóa nhân vật cũ
        Destroy(gameObject);
    }
    protected virtual void End_UpLevel_CellKid() => Spawn_CharacterUpLevel(Cell_Kid_Prefab, "UpLevel_CellKid");
    protected virtual void End_UpLevel_CellStage2() => Spawn_CharacterUpLevel(Cell_Stage2_Prefab, "UpLevel_CellStage2");
    protected virtual void End_UpLevel_CellPerfect() => Spawn_CharacterUpLevel(Cell_Perfect_Prefab, "UpLevel_CellPerfect");
    protected virtual void End_UpLevel_FriezaWhite() => Spawn_CharacterUpLevel(Frieza_White_Prefab, "UpLevel_FriezaWhite");
    protected virtual void End_UpLevel_FutureTrunksSSJ1() => Spawn_CharacterUpLevel(FutureTrunks_SSJ1_Prefab, "UpLevel_FutureTrunksSSJ1");
    protected virtual void End_UpLevel_GogetaSSJ1() => Spawn_CharacterUpLevel(Gogeta_SSJ1_Prefab, "UpLevel_GogetaSSJ1");
    protected virtual void End_UpLevel_GohanAdultSSJ2() => Spawn_CharacterUpLevel(GohanAdult_SSJ2_Prefab, "UpLevel_GohanSSJ2");
    protected virtual void End_UpLevel_GohanAdultMystic() => Spawn_CharacterUpLevel(GohanAdult_Mystic_Prefab, "UpLevel_GohanMystic");
    protected virtual void End_UpLevel_GohanChildSSJ1() => Spawn_CharacterUpLevel(GohanChild_SSJ1_Prefab, "UpLevel_GohanChildSSJ1");
    protected virtual void End_UpLevel_GohanChildSSJ2() => Spawn_CharacterUpLevel(GohanChild_SSJ2_Prefab, "UpLevel_GohanChildSSJ2");
    protected virtual void End_UpLevel_GokuSSJ1() => Spawn_CharacterUpLevel(Goku_SSJ1_Prefab, "UpLevel_GokuSSJ1");
    protected virtual void End_UpLevel_GokuSSJ3() => Spawn_CharacterUpLevel(Goku_SSJ3_Prefab, "UpLevel_GokuSSJ3");
    protected virtual void End_UpLevel_GokuSSJ4() => Spawn_CharacterUpLevel(Goku_SSJ4_Prefab, "UpLevel_GokuSSJ4");
    protected virtual void End_UpLevel_GotenksSSJ1() => Spawn_CharacterUpLevel(Gotenks_SSJ1_Prefab, "UpLevel_GotenksSSJ1");
    protected virtual void End_UpLevel_GotenksSSJ3() => Spawn_CharacterUpLevel(Gotenks_SSJ3_Prefab, "UpLevel_GotenksSSJ3");
    protected virtual void End_UpLevel_MajinBuu() => Spawn_CharacterUpLevel(MajinBuu_Prefab, "UpLevel_MajinBuu");
    protected virtual void End_UpLevel_SuperBuu_Gotenks() => Spawn_CharacterUpLevel(SuperBuu_Gotenks_Prefab, "UpLevel_SuperBuu_Gotenks");
    protected virtual void End_UpLevel_SuperBuu_Gohan() => Spawn_CharacterUpLevel(SuperBuu_Gohan_Prefab, "UpLevel_SuperBuu_Gohan");
    protected virtual void End_UpLevel_VegetaSSJ1() => Spawn_CharacterUpLevel(Vegeta_SSJ1_Prefab, "UpLevel_VegetaSSJ1");
    protected virtual void End_UpLevel_VegetaSSJ4() => Spawn_CharacterUpLevel(Vegeta_SSJ4_Prefab, "UpLevel_VegetaSSJ4");
    protected virtual void End_UpLevel_VegitoSSJ1() => Spawn_CharacterUpLevel(Vegito_SSJ1_Prefab, "UpLevel_VegitoSSJ1");
    protected virtual void Create_FusionDance_GogetaBase()
    {

    }
    protected virtual void Action_FusionDance_GogetaBase()
    {

    }
    protected virtual void End_FusionDance_GogetaBase() => Spawn_CharacterUpLevel(Gogeta_Base_Prefab, "FusionDance_GogetaBase");
    protected virtual void End_FusionPotara_VegitoBase() => Spawn_CharacterUpLevel(Vegito_Base_Prefab, "FusionPotara_VegitoBase");
    protected virtual void Create_FusionDance_GogetaSSJ4()
    {

    }
    protected virtual void Action_FusionDance_GogetaSSJ4()
    {

    }
    protected virtual void End_FusionDance_GogetaSSJ4() => Spawn_CharacterUpLevel(Gogeta_SSJ4_Prefab, "FusionDance_GogetaSSJ4");
    protected virtual void End_FusionPotara_VegitoSSJ4() => Spawn_CharacterUpLevel(Vegito_SSJ4_Prefab, "FusionPotara_VegitoSSJ4");



    // 🎯 Xử lý va chạm
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ki Base")
        {
            if (isDefending)
            {
                playerController.UpdateScore(10);
            }
            else
            {
                ForceStopAnimations_NoConditions();
                animator.SetBool("Hurt", true);
                playerController.UpdateScore(-100);
            }
        }
        else if (collision.gameObject.tag == "Ki Final")
        {
            if (isDefending)
            {
                playerController.UpdateScore(100);
            }
            else
            {
                ForceStopAnimations_NoConditions();
                animator.SetBool("Dead", true);
                playerController.UpdateScore(-500);
            }
        }
        else if (collision.gameObject.tag == "Ki DragonFist")
        {
            ForceStopAnimations_NoConditions();
            animator.SetBool("Dead", true);
            playerController.UpdateScore(-500);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ki Base")
        {
            animator.SetBool("Hurt", false);
        }

        else if (collision.gameObject.tag == "Ki Final" ||
            collision.gameObject.tag == "Ki DragonFist")
        {
            animator.SetBool("Dead", false);
        }
    }
}
