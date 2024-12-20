using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian12Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lava Guardian");
			base.Description.SetDefault("\"A Lava Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 8;
			modPlayer.fasterStaves = true;
			modPlayer.burnStaves = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian12")] > 0)
			{
				modPlayer2.natureGuardian12 = true;
			}
			if (!modPlayer2.natureGuardian12)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
