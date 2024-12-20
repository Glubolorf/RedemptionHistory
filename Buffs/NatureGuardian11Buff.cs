using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian11Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mud Guardian");
			base.Description.SetDefault("\"A Mud Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.statDefense += 4;
			player.thorns = 0.5f;
			player.statManaMax2 += 25;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian11")] > 0)
			{
				modPlayer.natureGuardian11 = true;
			}
			if (!modPlayer.natureGuardian11)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
