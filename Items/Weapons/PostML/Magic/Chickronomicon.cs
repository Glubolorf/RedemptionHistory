using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class Chickronomicon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chickronomicon");
			base.Tooltip.SetDefault("Summons a bone chicken that runs like a headless chicken");
		}

		public override void SetDefaults()
		{
			base.item.damage = 355;
			base.item.width = 28;
			base.item.height = 30;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 4;
			base.item.mana = 3;
			base.item.magic = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.UseSound = SoundID.Item8;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<BoneChickenPro>();
			base.item.shootSpeed = 7f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
