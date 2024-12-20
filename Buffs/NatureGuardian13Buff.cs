using System;
using Redemption.Items.DruidDamageClass;
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
			player.statDefense += 8;
			player.buffImmune[156] = true;
			player.manaRegen += 4;
			player.statManaMax2 += 20;
			player.statLifeMax2 += 20;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian13")] > 0)
			{
				modPlayer.natureGuardian13 = true;
			}
			if (!modPlayer.natureGuardian13)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
