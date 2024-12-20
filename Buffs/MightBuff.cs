using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class MightBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Aura of Might");
			base.Description.SetDefault("\"Damage reduction increased, you are immune to knockback\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.noKnockback = true;
			player.endurance += 0.06f;
		}
	}
}
