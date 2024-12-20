using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian10Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Skeletal Guardian");
			base.Description.SetDefault("\"A Skeletal Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.statDefense += 6;
			player.endurance += 0.08f;
			player.noKnockback = true;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian10")] > 0)
			{
				modPlayer.natureGuardian10 = true;
			}
			if (!modPlayer.natureGuardian10)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
