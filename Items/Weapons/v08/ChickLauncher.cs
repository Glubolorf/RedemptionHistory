using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class ChickLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chick Launcher");
			base.Tooltip.SetDefault("'No chicks were harmed in the making of this weapon'\nLaunches an explosive baby chicken");
		}

		public override void SetDefaults()
		{
			base.item.damage = 950;
			base.item.ranged = true;
			base.item.width = 46;
			base.item.height = 28;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 9f;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/Launch1");
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.shoot = base.mod.ProjectileType("ChickPro");
			base.item.shootSpeed = 18f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-3f, 0f));
		}
	}
}
