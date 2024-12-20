using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NightshadePotionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Vendetta");
			base.Description.SetDefault("\"Attackers also take damage, and get inflicted by poison\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).vendetta = true;
			player.thorns += 0.4f;
		}
	}
}
