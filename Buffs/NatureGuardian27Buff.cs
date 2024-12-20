using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian27Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Statue");
			base.Description.SetDefault("\"He watches...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveSpeed += 0.35f;
			player.statDefense += 20;
			player.endurance += 0.25f;
			player.moveSpeed *= 0.5f;
			player.noKnockback = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian27")] > 0)
			{
				modPlayer2.natureGuardian27 = true;
			}
			if (!modPlayer2.natureGuardian27)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
