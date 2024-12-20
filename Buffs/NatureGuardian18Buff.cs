using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian18Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Tree of Creation");
			base.Description.SetDefault("\"A Tree of Creation is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			player.endurance *= 0.1f;
			player.noKnockback = true;
			player.lifeRegen += 5;
			player.manaRegen += 5;
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			player.moveSpeed += 50f;
			player.longInvince = true;
			modPlayer.fasterSeedbags = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian18")] > 0)
			{
				modPlayer2.natureGuardian18 = true;
			}
			if (!modPlayer2.natureGuardian18)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 36000;
		}
	}
}
