using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class HazmatSuitBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Hazmat Suit");
			base.Description.SetDefault("You are protected from the Lab's water");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (modPlayer.hazmatAccessoryPrevious)
			{
				modPlayer.hazmatPower = true;
				return;
			}
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
