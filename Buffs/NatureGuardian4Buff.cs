using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
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
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveStreamShot = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian4>()] > 0)
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
