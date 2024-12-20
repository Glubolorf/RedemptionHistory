using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class FrostBlessed : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Blessed");
			base.Description.SetDefault("You are blessed with Icar's Divine Protection...");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 5;
			player.manaRegen += 10;
			player.statDefense += 6;
		}
	}
}
