using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PlanterasStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plantera's Stave");
			base.Tooltip.SetDefault("Shoots pink petals.");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 44;
			base.item.mana = 5;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 11;
			base.item.useAnimation = 16;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<MiniPlanteraSeed1>();
			base.item.shootSpeed = 17f;
			this.defaultShoot = ModContent.ProjectileType<MiniPlanteraSeed1>();
			this.singleShotStave = true;
			this.rightClickStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 64.2f;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (player.altFunctionUse == 2)
			{
				flat += 28f;
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 8;
				base.item.useTime = 35;
				base.item.useAnimation = 35;
				base.item.UseSound = SoundID.Item5;
				base.item.shoot = ModContent.ProjectileType<MiniThornBall1>();
				base.item.shootSpeed = 12f;
			}
			else
			{
				base.item.mana = 5;
				base.item.useTime = 11;
				base.item.useAnimation = 16;
				base.item.UseSound = SoundID.Item17;
				base.item.shoot = ModContent.ProjectileType<MiniPlanteraSeed1>();
				base.item.shootSpeed = 17f;
			}
			return NPC.downedMoonlord;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[base.item.owner];
			Texture2D texture = base.mod.GetTexture("Items/DruidDamageClass/PlanterasStave");
			Texture2D texture2 = base.mod.GetTexture("Items/DruidDamageClass/PlanterasStave2");
			if (player.altFunctionUse != 2)
			{
				spriteBatch.Draw(texture, position, null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
			}
			else
			{
				spriteBatch.Draw(texture2, position, null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Player player = Main.player[base.item.owner];
			Texture2D texture = base.mod.GetTexture("Items/DruidDamageClass/PlanterasStave");
			Texture2D texture2 = base.mod.GetTexture("Items/DruidDamageClass/PlanterasStave2");
			if (player.altFunctionUse != 2)
			{
				spriteBatch.Draw(texture, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), lightColor, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
				return false;
			}
			spriteBatch.Draw(texture2, new Vector2(base.item.position.X - Main.screenPosition.X + (float)base.item.width * 0.5f, base.item.position.Y - Main.screenPosition.Y + (float)base.item.height - (float)texture.Height * 0.5f + 2f), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), lightColor, rotation, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
