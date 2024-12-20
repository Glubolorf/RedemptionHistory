using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian6Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Moonflare");
			base.Description.SetDefault("\"A Moonflare is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveTripleShot = true;
			player.nightVision = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian6")] > 0)
			{
				modPlayer2.natureGuardian6 = true;
			}
			if (!modPlayer2.natureGuardian6)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
