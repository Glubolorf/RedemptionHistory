using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class BurntHands : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Burnt Hands");
			base.Description.SetDefault("\"Hot hot hot!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -2;
			player.bleed = true;
		}
	}
}
