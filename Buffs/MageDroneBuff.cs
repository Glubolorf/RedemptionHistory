using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class MageDroneBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Jellyfish Drone");
			base.Description.SetDefault("\"A Jellyfish Drone to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("MageDrone")] > 0)
			{
				modPlayer.jellyfishDrone = true;
			}
			if (!modPlayer.jellyfishDrone)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
