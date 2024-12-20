using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GloomShroomDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spores: Gloom Shroom");
			base.Description.SetDefault("\"Gloom spores are consuming your skin\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -5;
			player.bleed = true;
		}
	}
}
