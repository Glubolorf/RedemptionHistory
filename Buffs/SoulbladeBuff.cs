using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SoulbladeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soulblade");
			base.Description.SetDefault("\"Increased Spirit Level\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().spiritLevel++;
		}
	}
}
