using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian22Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sunny Pixie");
			base.Description.SetDefault("\"A Sunny Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			player.resistCold = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian22")] > 0)
			{
				modPlayer2.natureGuardian22 = true;
			}
			if (!modPlayer2.natureGuardian22)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
