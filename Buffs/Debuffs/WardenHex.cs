using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class WardenHex : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Warden's Curse");
			base.Description.SetDefault("\"You can no longer place or break tiles\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.noBuilding = true;
		}
	}
}
