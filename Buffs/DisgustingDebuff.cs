using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DisgustingDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Disgusting");
			base.Description.SetDefault("\"You've tasted something absolutely vile.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -100;
			player.moveSpeed *= 0.01f;
			player.bleed = true;
		}
	}
}
