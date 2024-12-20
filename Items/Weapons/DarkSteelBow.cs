using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DarkSteelBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Daerel's Dark-Steel Bow");
			base.Tooltip.SetDefault("'A mighty bow made of Ancient Wood & Dark Steel'\nReplaces Wooden Arrows with Dark-Steel Arrows\n40% chance not to consume ammo\nOnly usable after Plantera have been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 85;
			base.item.ranged = true;
			base.item.width = 30;
			base.item.height = 46;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 100f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedPlantBoss;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Utils.NextFloat(Main.rand) >= 0.4f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = ModContent.ProjectileType<DarkSteelArrow>();
			}
			float numberProjectiles = (float)(2 + Main.rand.Next(1));
			float rotation = MathHelper.ToRadians(1f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}
	}
}
