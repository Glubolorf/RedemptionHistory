using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SniperDroneBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Girus Sniper Drone");
			base.Description.SetDefault("\"A Girus Sniper Drone to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SniperDrone>()] > 0)
			{
				modPlayer.girusSniperDrone = true;
			}
			if (!modPlayer.girusSniperDrone)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
