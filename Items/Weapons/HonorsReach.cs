using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class HonorsReach : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Honor's Reach, Edge of the East");
			base.Tooltip.SetDefault("'A spear once wielded by a King of Anglon'\nOnly usable after Plantera is defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 58;
			base.item.useStyle = 5;
			base.item.useAnimation = 7;
			base.item.useTime = 7;
			base.item.shootSpeed = 3.5f;
			base.item.knockBack = 5.5f;
			base.item.width = 74;
			base.item.height = 84;
			base.item.scale = 1f;
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = base.mod.ProjectileType<HonorsReachPro>();
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("HonorsReach2Pro"), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
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

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1 && NPC.downedPlantBoss;
		}
	}
}
