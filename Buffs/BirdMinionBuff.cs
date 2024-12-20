using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class BirdMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Bird");
			base.Description.SetDefault("\"A bird to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("BirdMinion1")] > 0)
			{
				modPlayer.birdMinion = true;
			}
			if (!modPlayer.birdMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
