using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian4Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Crimson Pixie");
			base.Description.SetDefault("\"A Crimson Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.1f;
			modPlayer.fasterStaves = true;
			modPlayer.fasterSpirits = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian4")] > 0)
			{
				modPlayer2.natureGuardian4 = true;
			}
			if (!modPlayer2.natureGuardian4)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
