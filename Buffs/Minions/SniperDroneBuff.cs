using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class SniperDroneBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Girus Sniper Drone");
			base.Description.SetDefault("\"A Girus Sniper Drone to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (!player.GetModPlayer<RedePlayer>().girusSniperDrone)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
