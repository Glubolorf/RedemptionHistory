using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
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
			player.GetModPlayer<RedePlayer>().staveTripleShot = true;
			player.nightVision = true;
			player.manaCost *= 0.75f;
			if (!Main.dayTime)
			{
				player.moveSpeed += 10f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian14>()] > 0)
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
