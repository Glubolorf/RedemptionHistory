using System;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class InsulatiumPotionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Insulated");
			base.Description.SetDefault("\"You are immune to Electrified\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().zapField = true;
			player.buffImmune[144] = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ZapFieldPro>()] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<ZapFieldPro>(), 500, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
