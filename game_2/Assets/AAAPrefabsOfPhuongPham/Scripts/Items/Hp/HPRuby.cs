using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HP Ruby Item", menuName = "Inventory/Items/HP Ruby")]
public class HPRuby : SourceItemSlot
{
    [Range(0,10000)]
    public int hp = 0;

    public override void Use()
    {
        base.Use();
        // heal HP 
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.AddHPandPosture(this.hp,0);
        playerStats.vfxHP.Play();
    }

    

}
