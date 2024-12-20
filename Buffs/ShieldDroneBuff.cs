using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ShieldDroneBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Girus Shield Drone");
			base.Description.SetDefault("\"A Shield Drone to protect you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("ShieldDrone")] > 0)
			{
				modPlayer.shieldDrone = true;
			}
			if (!modPlayer.shieldDrone)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
