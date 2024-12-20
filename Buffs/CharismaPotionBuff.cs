using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class CharismaPotionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Charisma");
			base.Description.SetDefault("\"Shops have lower prices and enemies drop more gold\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			player.discount = true;
			modPlayer.charisma = true;
		}
	}
}
