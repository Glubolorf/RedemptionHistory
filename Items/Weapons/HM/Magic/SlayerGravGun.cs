using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class SlayerGravGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gravity Gun");
			base.Tooltip.SetDefault("Sends enemies and projectiles flying with extreme force\nCan't reflect projectiles larger than the blast");
		}

		public override void SetDefaults()
		{
			base.item.damage = 80;
			base.item.magic = true;
			base.item.width = 74;
			base.item.height = 40;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 30f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/GravGunSound");
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<GravPro>();
			base.item.shootSpeed = 22f;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = base.mod.GetTexture("Items/Weapons/HM/Magic/" + base.GetType().Name + "_Glow");
			spriteBatch.Draw(texture, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), Color.White, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}
	}
}
