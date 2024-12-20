using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GSSBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sky Parry");
			base.Description.SetDefault("");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.noKnockback = true;
			player.shadowDodge = true;
			player.shadowDodgeTimer = 10;
		}
	}
}
