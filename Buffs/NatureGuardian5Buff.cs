using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian5Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pixie");
			base.Description.SetDefault("\"An Ancient Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			modPlayer.rapidStave = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian5")] > 0)
			{
				modPlayer2.natureGuardian5 = true;
			}
			if (!modPlayer2.natureGuardian5)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
