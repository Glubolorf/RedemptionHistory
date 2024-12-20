using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class HeartEmblemBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Heart Rush");
			base.Description.SetDefault("\"Life regen greatly increased\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 15;
		}
	}
}
