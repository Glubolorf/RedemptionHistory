using System;
using Redemption.NPCs.PreHM;
using Redemption.NPCs.Wasteland;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class BeardedHatchet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bearded Hatchet");
			base.Tooltip.SetDefault("Decapitates skeletons, greatly increasing the chance for a skull to drop\nDeals 50% more damage to skeletons");
		}

		public override void SetDefaults()
		{
			base.item.damage = 11;
			base.item.melee = true;
			base.item.width = 38;
			base.item.height = 38;
			base.item.useTime = 25;
			base.item.useAnimation = 30;
			base.item.axe = 10;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = 6550;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (target.life <= 0 && (target.type == 77 || target.type == -49 || target.type == -51 || target.type == -53 || target.type == -47 || target.type == 449 || target.type == 450 || target.type == 451 || target.type == 452 || target.type == 481 || target.type == 201 || target.type == -15 || target.type == 202 || target.type == 203 || target.type == 21 || target.type == 324 || target.type == 110 || target.type == 323 || target.type == 293 || target.type == 291 || target.type == 322 || target.type == -48 || target.type == -50 || target.type == -52 || target.type == -46 || target.type == 292 || target.type == ModContent.NPCType<Skelemies>() || target.type == ModContent.NPCType<Skelemies2>() || target.type == ModContent.NPCType<SkeletonAssassin>() || target.type == ModContent.NPCType<SkeletonDueller>() || target.type == ModContent.NPCType<SkeletonWanderer>() || target.type == ModContent.NPCType<SkeletonAssassin2>() || target.type == ModContent.NPCType<SkeletonWanderer2>() || target.type == ModContent.NPCType<HazmatSkeleton>()) && Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, 1274, 1, false, 0, false, false);
			}
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (Lists.IsAnySkeleton.Contains(target.type))
			{
				damage = damage;
			}
		}
	}
}
