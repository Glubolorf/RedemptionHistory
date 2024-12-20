using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class PZGauntlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blighted Gauntlet");
			base.Tooltip.SetDefault("Shoots out Blighted Fists\nRight-clicking will produce a Blighted Shield, increasing defence and knockback immunity for 15 seconds [c/94c2ff:(Requires 150 Mana)]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 275;
			base.item.melee = true;
			base.item.width = 56;
			base.item.height = 34;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.knockBack = 5f;
			base.item.UseSound = SoundID.Item74;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.shoot = base.mod.ProjectileType("BlightedFistPro1");
			base.item.shootSpeed = 15f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 150;
				base.item.buffType = base.mod.BuffType("BlightedShieldBuff");
				base.item.buffTime = 900;
				base.item.shoot = base.mod.ProjectileType("BlightedShield");
				base.item.shootSpeed = 0f;
				return !player.HasBuff(base.mod.BuffType("BlightedShieldBuff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = base.mod.ProjectileType("BlightedFistPro1");
			base.item.shootSpeed = 10f;
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				return true;
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-10f, 0f));
		}
	}
}
