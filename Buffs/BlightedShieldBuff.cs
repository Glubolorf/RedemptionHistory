using System;
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
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("BlightedShield")] > 0)
			{
				modPlayer.blightedShield = true;
			}
			if (!modPlayer.blightedShield)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
