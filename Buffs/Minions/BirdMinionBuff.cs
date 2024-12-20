using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class BirdMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Bird");
			base.Description.SetDefault("\"A bird to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (!player.GetModPlayer<RedePlayer>().birdMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
