using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class HallamSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hallam's Steel Sword");
			base.Tooltip.SetDefault("'Still shining...'\nOnly usable after any Mech Boss has been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.melee = true;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 6;
			base.item.useAnimation = 6;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 90f;
			base.item.shoot = base.mod.ProjectileType("HallamSwordPro");
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedMechBossAny;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = 29f;
			float num2 = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int num3 = 0;
			while ((float)num3 < num)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				num3++;
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 91, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(170, 0, 255));
			}
		}
	}
}
