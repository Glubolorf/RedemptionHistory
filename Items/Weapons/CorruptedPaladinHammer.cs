using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedPaladinHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Paladin's Hammer");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1513);
			base.item.shootSpeed *= 1.55f;
			base.item.damage = 165;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = base.mod.ProjectileType("CorruptedPaladinHammerPro");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}
