using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ChickenSwarmerMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Chicken Swarmer");
			base.Description.SetDefault("\"A flying chicken to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("ChickenSwarmerMinion")] > 0)
			{
				modPlayer.chickenSwarmerMinion = true;
			}
			if (!modPlayer.chickenSwarmerMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
