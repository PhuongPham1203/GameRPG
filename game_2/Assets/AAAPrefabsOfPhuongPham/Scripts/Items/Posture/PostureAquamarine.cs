using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Posture Aquamarine Item", menuName = "Inventory/Items/Posture Aquamarine")]

public class PostureAquamarine : SourceItemSlot
{
    [Range(0,10000)]
    public int posture = 0;

    public override void Use()
    {
        base.Use();
        // heal HP 
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.AddHPandPosture(0,this.posture);
        //playerStats.vfxHP.Play();
    }

}
