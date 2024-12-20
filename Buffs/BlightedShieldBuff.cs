using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class BlightedShieldBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Blighted Shield");
			base.Description.SetDefault("\"Defence is increased!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 18;
			player.noKnockback = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BlightedShield>()] > 0)
			{
				modPlayer2.blightedShield = true;
			}
			if (!modPlayer2.blightedShield)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
