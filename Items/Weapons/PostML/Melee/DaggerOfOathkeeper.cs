using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class DaggerOfOathkeeper : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dagger of the Oathkeeper");
			base.Tooltip.SetDefault("Makes the target become soulless");
		}

		public override void SetDefaults()
		{
			base.item.damage = 666;
			base.item.melee = true;
			base.item.width = 40;
			base.item.height = 40;
			base.item.noUseGraphic = false;
			base.item.noMelee = false;
			base.item.useTime = 7;
			base.item.useAnimation = 7;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.altFunctionUse == 2 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge >= 2 && !player.HasBuff(ModContent.BuffType<DaggerBuff>()))
			{
				base.item.useTime = 70;
				base.item.useAnimation = 70;
				base.item.noUseGraphic = true;
				base.item.noMelee = true;
				base.item.shoot = ModContent.ProjectileType<DaggerStabPro>();
			}
			else
			{
				base.item.useTime = 7;
				base.item.useAnimation = 7;
				base.item.noUseGraphic = false;
				base.item.noMelee = false;
				base.item.shoot = 0;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.altFunctionUse == 2 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge >= 2 && !player.HasBuff(ModContent.BuffType<DaggerBuff>()))
			{
				modPlayer.shadowBinderCharge -= 2;
				return true;
			}
			return false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			RedePlayer redePlayer = (RedePlayer)Main.player[Main.myPlayer].GetModPlayer(base.mod, "RedePlayer");
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			string text;
			if (redePlayer.shadowBinder)
			{
				text = "Right-clicking will give you a damage buff at the cost of decreased max life for 2 minutes (Consumes 2 Shadowbound Souls)";
			}
			else
			{
				text = "Has a special ability if Sielukaivo Shadowbinder is equipped";
			}
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Color.DarkGray)
			};
			tooltips.Insert(tooltipLocation, line);
		}
	}
}
