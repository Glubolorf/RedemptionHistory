using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Seedbags
{
	public class CreationRoseBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Druid/Seedbags/" + base.GetType().Name + "_Glow");
				CreationRoseBag.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Creation Rose Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a mystical Creation Rose");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 200;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 46;
			base.item.useAnimation = 46;
			base.item.useStyle = 1;
			base.item.mana = 35;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<Seed24>();
			base.item.shootSpeed = 14f;
			base.item.glowMask = CreationRoseBag.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
