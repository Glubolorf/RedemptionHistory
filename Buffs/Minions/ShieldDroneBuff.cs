using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class ShieldDroneBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Girus Shield Drone");
			base.Description.SetDefault("\"A Shield Drone to protect you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (!player.GetModPlayer<RedePlayer>().shieldDrone)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
