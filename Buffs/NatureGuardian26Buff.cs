using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian26Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Boreal Statuette");
			base.Description.SetDefault("\"A Boreal Statuette is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveSpeed += 0.35f;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian26>()] > 0)
			{
				modPlayer2.natureGuardian26 = true;
			}
			if (!modPlayer2.natureGuardian26)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
