using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian28Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Moon Baron");
			base.Description.SetDefault("\"A Moon Baron is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			modPlayer3.staveSpeed += 0.35f;
			modPlayer3.moonStaves = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian28>()] > 0)
			{
				modPlayer2.natureGuardian28 = true;
			}
			if (!modPlayer2.natureGuardian28)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
