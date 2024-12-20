using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian16Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Holy Statuette");
			base.Description.SetDefault("\"He watches...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.nightVision = true;
			player.statDefense += 16;
			player.endurance *= 0.8f;
			player.noFallDmg = true;
			player.noKnockback = true;
			if (Main.dayTime)
			{
				player.moveSpeed += 10f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian16")] > 0)
			{
				modPlayer.natureGuardian16 = true;
			}
			if (!modPlayer.natureGuardian16)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
