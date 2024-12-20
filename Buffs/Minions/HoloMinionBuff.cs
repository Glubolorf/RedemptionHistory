using System;
using Redemption.Projectiles.Minions.HoloMinions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class HoloMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Hologram");
			base.Description.SetDefault("\"Wait, how do holograms hit things!?\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HoloProjector>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<HoloMinion1>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<HoloMinion2>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<HoloMinion3>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<HoloMinion4>()] > 0)
			{
				modPlayer.holoMinion = true;
			}
			if (!modPlayer.holoMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
