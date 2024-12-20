using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Ranged
{
	public class SlayerBigRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hyper-Tech Revolver");
			base.Tooltip.SetDefault("Replaces normal bullets with nano bullets");
		}

		public override void SetDefaults()
		{
			base.item.damage = 50;
			base.item.ranged = true;
			base.item.width = 62;
			base.item.height = 32;
			base.item.useTime = 5;
			base.item.useAnimation = 5;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item41;
			base.item.autoReuse = false;
			base.item.shoot = 10;
			base.item.shootSpeed = 90f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 14)
			{
				type = 285;
			}
			return true;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = base.mod.GetTexture("Items/Weapons/HM/Ranged/" + base.GetType().Name + "_Glow");
			spriteBatch.Draw(texture, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), Color.White, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}
	}
}
