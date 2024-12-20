using System;
using Redemption.Items.DruidDamageClass;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian7Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Hallowed Guardian");
			base.Description.SetDefault("\"A Hallowed Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			p.staveSpeed += 0.35f;
			player.lifeRegen += 2;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian7>()] > 0)
			{
				modPlayer2.natureGuardian7 = true;
			}
			if (!modPlayer2.natureGuardian7)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
