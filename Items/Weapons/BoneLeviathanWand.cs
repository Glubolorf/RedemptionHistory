using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BoneLeviathanWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vile Leviathan Wand");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(3051);
			base.item.shootSpeed *= 1.05f;
			base.item.damage = (int)((double)base.item.damage * 1.3);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = base.mod.ProjectileType("BoneSpinePro");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}
