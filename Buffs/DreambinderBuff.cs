using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DreambinderBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Dreambound");
			base.Description.SetDefault("\"Increases length of invincibility\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.longInvince = true;
		}
	}
}
