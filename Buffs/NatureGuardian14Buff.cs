using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian14Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lunar Statuette");
			base.Description.SetDefault("\"A Lunar Statuette is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			modPlayer3.staveSpeed += 0.35f;
			modPlayer3.staveTripleShot = true;
			player.nightVision = true;
			player.manaCost *= 0.75f;
			if (!Main.dayTime)
			{
				player.moveSpeed += 10f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian14")] > 0)
			{
				modPlayer2.natureGuardian14 = true;
			}
			if (!modPlayer2.natureGuardian14)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
