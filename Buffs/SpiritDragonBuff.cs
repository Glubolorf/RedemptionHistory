using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiritDragonBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spirit Dragon");
			base.Description.SetDefault("Summons a Spirit Dragon to fight for you");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("SpiritDragonHead")] > 0)
			{
				modPlayer.spiritWyvern2 = true;
			}
			if (!modPlayer.spiritWyvern2)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 2400;
		}
	}
}
