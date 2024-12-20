using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class ForestNymphsSickle : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Nymph's Sickle");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nHitting an enemy will drain a tenth of the damage dealt\nOnly usable after Eye of Cthulhu has been defeated\n[c/1c4dff:Rare]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 27;
			base.item.width = 64;
			base.item.height = 72;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("ForestSicklePro4");
			base.item.shootSpeed = 7f;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			player.statLife += (int)Math.Floor((double)damage / 10.0);
			player.HealEffect((int)Math.Floor((double)damage / 10.0), true);
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedBoss1;
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
	}
}
