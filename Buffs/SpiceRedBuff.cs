using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiceRedBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spicy Spices");
			base.Description.SetDefault("\"You ignite nearby enemies\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.AddBuff(116, 2, true);
		}
	}
}
