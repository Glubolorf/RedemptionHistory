using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class VesselStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vessel Shadestaff");
			base.Tooltip.SetDefault("Hold left-click to summon Shadesouls that float around the player\nRelease left-click to make them fly towards cursor point at high speeds");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 650;
			base.item.magic = true;
			base.item.mana = 12;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item20;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<VesselStaffPro>();
			base.item.shootSpeed = 2f;
			base.item.channel = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
				return player.ownedProjectileCounts[base.item.shoot] < 20;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool AltFunctionUse(Player player)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<VesselStaffPro2>()] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.altFunctionUse == 2 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge >= 1)
			{
				type = ModContent.ProjectileType<VesselStaffPro2>();
			}
			else
			{
				type = ModContent.ProjectileType<VesselStaffPro>();
			}
			return true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			RedePlayer redePlayer = (RedePlayer)Main.player[Main.myPlayer].GetModPlayer(base.mod, "RedePlayer");
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			string text;
			if (redePlayer.shadowBinder)
			{
				text = "Right-clicking will summon a ring of shade around you that grows bigger the longer it is active, damaging enemies (Consumes all Shadowbound Souls over time)";
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VesselFrag", 30);
			modRecipe.AddIngredient(null, "Shadesoul", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
