using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class UkkosStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko's Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Finnish him!'\nOnly usable after all Mech Bosses has been defeated\nSummons a giant Lightning Bolt at cursor point\nRight-clicking will shoot out a Lightning Blast\n[c/aa00ff:Epic]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 400;
			base.item.width = 76;
			base.item.height = 80;
			base.item.crit = 40;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("UkkosLightning");
			base.item.shootSpeed = 0f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 200;
				base.item.useTime = 25;
				base.item.useAnimation = 25;
				base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/Zap2");
				base.item.shoot = base.mod.ProjectileType("UkkosLightning2");
				base.item.shootSpeed = 10f;
			}
			else
			{
				base.item.damage = 400;
				base.item.useTime = 40;
				base.item.useAnimation = 40;
				base.item.UseSound = SoundID.Item1;
				base.item.shoot = base.mod.ProjectileType("UkkosLightning");
				base.item.shootSpeed = 0f;
			}
			return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				return true;
			}
			position = Main.MouseWorld;
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
	}
}
