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

    public EffectInfo[] Effects;

    [System.Serializable]
    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public Vector3 StartRotation;
        public float DestroyAfter = 10;
        //public bool UseLocalPosition = true;
    }

    void InstantiateEffect(int EffectNumber)
    {
        EffectNumber = animatorPlayer.GetInteger("AttackCombo");
        if (Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);
        /*
        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        */
        Destroy(instance, Effects[EffectNumber].DestroyAfter);
    }




    void EffectBaseComboAttack()
    {
        int EffectNumber = animatorPlayer.GetInteger("AttackCombo") - 1;
        if (Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        Quaternion rot = Quaternion.Euler(Effects[EffectNumber].StartPositionRotation.eulerAngles + Effects[EffectNumber].StartRotation);

        //Debug.Log(rot);
        //Debug.Log(Effects[EffectNumber].StartPositionRotation.localEulerAngles);
        //Debug.Log(Effects[EffectNumber].StartPositionRotation.rotation);
        //Debug.Log(Effects[EffectNumber].StartPositionRotation.localRotation);

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, rot);//Effects[EffectNumber].StartPositionRotation.rotation);
        /*
        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        */
        Destroy(instance, Effects[EffectNumber].DestroyAfter);
    }

}
