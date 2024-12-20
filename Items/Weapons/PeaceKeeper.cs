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
	public class PeaceKeeper : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Peacekeeper");
			base.Tooltip.SetDefault("'A spear used by the Generals of Anglon'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.useStyle = 5;
			base.item.useAnimation = 14;
			base.item.useTime = 18;
			base.item.shootSpeed = 3.7f;
			base.item.knockBack = 5.5f;
			base.item.width = 48;
			base.item.height = 48;
			base.item.scale = 1f;
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = base.mod.ProjectileType<PeaceKeeperPro>();
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(0, 120, 255));
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(30, 300, false);
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
