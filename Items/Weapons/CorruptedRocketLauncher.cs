using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedRocketLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Annihilator");
		}

		public override void SetDefaults()
		{
			base.item.damage = 450;
			base.item.ranged = true;
			base.item.width = 106;
			base.item.height = 38;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.UseSound = SoundID.Item11;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.shoot = 134;
			base.item.shootSpeed = 16f;
			base.item.useAmmo = 771;
			base.item.autoReuse = true;
			base.item.noMelee = true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-40f, 0f));
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("EmpoweredBuff"), Main.rand.Next(50, 60), true);
		}
	}
}
