using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ChickenCrownBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("King Chicken!");
			base.Description.SetDefault("You are the Mighty King Chicken!");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			if (p.chickenAccessoryPrevious)
			{
				p.chickenPower = true;
				return;
			}
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
