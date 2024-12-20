using System;
using Redemption.Items.DruidDamageClass;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian9Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Crimson Fairy");
			base.Description.SetDefault("\"A Crimson Fairy is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveSpeed += 0.35f;
			if (player.ZoneCrimson)
			{
				player.moveSpeed += 5f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian9>()] > 0)
			{
				modPlayer2.natureGuardian9 = true;
			}
			if (!modPlayer2.natureGuardian9)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
