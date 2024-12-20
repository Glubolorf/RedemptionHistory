using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TotalGirus : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				TotalGirus.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = TotalGirus.customGlowMask;
			base.DisplayName.SetDefault("Total Corruption");
			base.Tooltip.SetDefault("Turns slain enemies into their Girus Corruption form (If they have one)");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.melee = true;
			base.item.width = 100;
			base.item.height = 100;
			base.item.useTime = 42;
			base.item.useAnimation = 42;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.glowMask = TotalGirus.customGlowMask;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 235, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				if (target.type == 84)
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("CorruptedBlade"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == 290)
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("CorruptedPaladin"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == 139)
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("CorruptedProbe"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == base.mod.NPCType("RogueTBot"))
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("CorruptedTBot"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == base.mod.NPCType("Android"))
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("OmegaAndroid"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == base.mod.NPCType("PrototypeSilver"))
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("OmegaPrototype"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == base.mod.NPCType("SpacePaladin"))
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, base.mod.NPCType("OmegaSpacePaladin"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VlitchBlade", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
