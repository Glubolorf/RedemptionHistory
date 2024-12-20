using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.HM;
using Redemption.NPCs.PostML;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class TotalGirus : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Melee/" + base.GetType().Name + "_Glow");
				TotalGirus.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = TotalGirus.customGlowMask;
			base.DisplayName.SetDefault("Total Corruption");
			base.Tooltip.SetDefault("Turns slain enemies into their Girus Corruption form (If they have one)");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.melee = true;
			base.item.width = 98;
			base.item.height = 98;
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
				if (target.type == 139)
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<CorruptedProbe>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == ModContent.NPCType<RogueTBot>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<CorruptedTBot>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == ModContent.NPCType<Android>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<OmegaAndroid>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == ModContent.NPCType<PrototypeSilver>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<OmegaPrototype>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (target.type == ModContent.NPCType<SpacePaladin>())
				{
					NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<OmegaSpacePaladin>(), 0, 0f, 0f, 0f, 0f, 255);
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
