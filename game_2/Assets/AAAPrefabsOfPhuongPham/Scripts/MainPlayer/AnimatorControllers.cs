using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllers : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animatorPlayer;
    private LineRenderer lr;
    public GameObject startRope;
    private bool isSwing = false;
    [SerializeField]
    private Transform parentContentVFXPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animatorPlayer = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {

        DrawRope();


    }

    public void CreateCab()
    {
        //Debug.Log("Create cab");
        isSwing = true;
    }
    public void StopCab()
    {
        isSwing = false;
        lr.positionCount = 0;

    }
    void DrawRope()
    {
        if (isSwing)
        {
            lr.positionCount = 2;
            lr.SetPosition(index: 0, startRope.transform.position);
            lr.SetPosition(index: 1, playerController.targetSwing.transform.position);
        }


    }
    [Space]
    [Header("VFX for Light Attack")]
    public GameObject effectLightAttack;
    public Transform startPositionRotationLightAttack;
    public float lightAttackDestroyAfter = 3f;

    public EffectInfo[] effectsLightAttack;

    [Space]
    [Header("VFX for Heavy Attack")]
    public GameObject effectHeavyAttack;
    public Transform startPositionRotationHeavyAttack;
    public float heavyAttackDestroyAfter = 3f;

    public EffectInfo[] effectsHeavyAttack;

    [System.Serializable]
    public class EffectInfo
    {
        public Vector3 StartRotation;
        //public bool UseLocalPosition = true;
    }


    void EffectBaseComboLightAttack()
    {
        int EffectNumber = animatorPlayer.GetInteger("AttackCombo") - 1;
        if (effectsLightAttack == null || effectsLightAttack.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        Quaternion rot = Quaternion.Euler(startPositionRotationLightAttack.eulerAngles + effectsLightAttack[EffectNumber].StartRotation);

        var instance = Instantiate(effectLightAttack, startPositionRotationLightAttack.position, rot, parentContentVFXPlayer);//Effects[EffectNumber].StartPositionRotation.rotation);


        AudioManager.instance.PlaySoundOfPlayer("Light Attack " + (int)(EffectNumber + 1));


        Destroy(instance, lightAttackDestroyAfter);
    }

    void EffectBaseComboHeavyAttack()
    {
        int EffectNumber = animatorPlayer.GetInteger("AttackCombo") - 1;
        if (effectsHeavyAttack == null || effectsHeavyAttack.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        Quaternion rot = Quaternion.Euler(startPositionRotationHeavyAttack.eulerAngles + effectsHeavyAttack[EffectNumber].StartRotation);

        var instance = Instantiate(effectHeavyAttack, startPositionRotationHeavyAttack.position, rot, parentContentVFXPlayer);//Effects[EffectNumber].StartPositionRotation.rotation);

        AudioManager.instance.PlaySoundOfPlayer("Heavy Attack " + (int)(EffectNumber + 1));


        Destroy(instance, heavyAttackDestroyAfter);
    }

}
