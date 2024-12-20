using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ChainedDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Chained!");
			base.Description.SetDefault("\"You can't escape!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.velocity.X = 0f;
			player.velocity.Y = 0f;
			player.maxFallSpeed = 0f;
		}
	}
}
