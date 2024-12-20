using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class MossyWimpGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mossy Screecher");
			base.Tooltip.SetDefault("'Do the roar'\nFires soundwaves that confuse enemies\nCan pierce through tiles and enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.useTime = 5;
			base.item.useAnimation = 20;
			base.item.reuseDelay = 50;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<WimpScreech>();
			base.item.shootSpeed = 5f;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/Roar1Item");
			base.item.ranged = true;
			base.item.width = 40;
			base.item.height = 28;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 1, 50, 0);
			base.item.rare = 3;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}
	}
}
