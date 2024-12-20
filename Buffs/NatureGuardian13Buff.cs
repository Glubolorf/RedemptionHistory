using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian13Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Marble King Piece");
			base.Description.SetDefault("\"A Marble King Piece is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.statDefense += 18;
			player.buffImmune[156] = true;
			player.manaRegen += 4;
			player.statManaMax2 += 40;
			player.statLifeMax2 += 40;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian13>()] > 0)
			{
				modPlayer2.natureGuardian13 = true;
			}
			if (!modPlayer2.natureGuardian13)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
