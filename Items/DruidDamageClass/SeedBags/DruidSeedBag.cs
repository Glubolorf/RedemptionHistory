using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public abstract class DruidSeedBag : DruidDamageItem
	{
		public override void SecondarySetDefaults()
		{
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSeedbags)
			{
				return 1.15f;
			}
			return 1f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().moreSeeds)
			{
				int numberProjectiles = 2 + Main.rand.Next(2);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(35f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed *= scale;
					Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f)];
					projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = this.NativeTerrainIDs;
					projectile.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier = base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().extraSeed && Main.rand.Next(3) == 0)
			{
				int numberProjectiles2 = 2;
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed2 *= scale2;
					Projectile projectile2 = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f)];
					projectile2.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = this.NativeTerrainIDs;
					projectile2.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier = base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier;
				}
				return false;
			}
			Projectile projectile3 = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f)];
			projectile3.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs = this.NativeTerrainIDs;
			projectile3.GetGlobalProjectile<DruidProjectile>().seedLifetimeModifier = base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier;
			return false;
		}

		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = Enumerable.FirstOrDefault<TooltipLine>(tooltips, (TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] array = tt.text.Split(new char[]
				{
					' '
				});
				string damageValue = Enumerable.First<string>(array);
				string damageWord = Enumerable.Last<string>(array);
				tt.text = damageValue + " druidic " + damageWord;
			}
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("ItemName"));
			int tooltipLocation2 = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			if (tooltipLocation != -1)
			{
				tooltips.Insert(tooltipLocation + 1, new TooltipLine(base.mod, "IsDruid", "[c/91dc16:---Druid Class---]"));
			}
			if (tooltipLocation2 != -1)
			{
				tooltips.Insert(tooltipLocation2 + 1, new TooltipLine(base.mod, "NativeTerrain", "[c/91dc16:Native Terain: ]" + this.nativeText));
			}
		}

		public List<int> NativeTerrainIDs = new List<int>();

		public string nativeText = "None";
	}
}
