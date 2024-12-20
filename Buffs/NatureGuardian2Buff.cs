using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian2Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nature Guardian");
			base.Description.SetDefault("\"A Nature Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			player.statDefense += 8;
			modPlayer.rapidStave = true;
			player.dryadWard = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian2")] > 0)
			{
				modPlayer2.natureGuardian2 = true;
			}
			if (!modPlayer2.natureGuardian2)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
