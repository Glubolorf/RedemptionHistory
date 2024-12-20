using System;
using Redemption.Items.DruidDamageClass;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian17Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("True Holy Statuette");
			base.Description.SetDefault("\"He watches...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			modPlayer3.staveStreamShot = true;
			modPlayer3.staveTripleShot = true;
			player.nightVision = true;
			player.dangerSense = true;
			player.detectCreature = true;
			player.statDefense += 32;
			player.endurance += 0.12f;
			player.noFallDmg = true;
			player.noKnockback = true;
			player.lifeRegen += 3;
			player.statLifeMax2 += 50;
			if (Main.dayTime)
			{
				player.moveSpeed += 20f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian17>()] > 0)
			{
				modPlayer2.natureGuardian17 = true;
			}
			if (!modPlayer2.natureGuardian17)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
