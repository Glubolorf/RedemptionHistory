using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class HEVSuitBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("HEV Suit");
			base.Description.SetDefault("You are protected from all infection and radiation");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (modPlayer.HEVAccessoryPrevious)
			{
				modPlayer.HEVPower = true;
				return;
			}
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
