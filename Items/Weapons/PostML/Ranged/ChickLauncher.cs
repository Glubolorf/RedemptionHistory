using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class ChickLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chick Launcher");
			base.Tooltip.SetDefault("'No chicks were harmed in the making of this weapon'\nLaunches an explosive baby chicken\nUses chicken eggs as ammo");
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
			base.item.shoot = ModContent.ProjectileType<ChickPro>();
			base.item.shootSpeed = 18f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
			base.item.useAmmo = ModContent.ItemType<ChickenEgg>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<ChickPro>(), damage, knockBack, Main.myPlayer, 0f, 0f);
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-3f, 0f));
		}
	}
}
