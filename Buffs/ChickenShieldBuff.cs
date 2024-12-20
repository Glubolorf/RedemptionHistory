using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ChickenShieldBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shielded");
			base.Description.SetDefault("\"Increased defense and knockback immunity, but decreased speed\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 18;
			player.moveSpeed *= 0.7f;
			player.noKnockback = true;
		}
	}
}
