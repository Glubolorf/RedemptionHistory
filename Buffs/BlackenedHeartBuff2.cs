using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class BlackenedHeartBuff2 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soulless");
			base.Description.SetDefault("\"...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 15;
			player.blind = true;
		}
	}
}
