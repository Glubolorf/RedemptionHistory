using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SkeletonNobleArmoured2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skeleton Noble");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 58;
			base.npc.friendly = false;
			base.npc.damage = 32;
			base.npc.defense = 26;
			base.npc.lifeMax = 85;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = 160f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<SkeletonNobleBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
		}

		public override void AI()
		{
			if (this.slashAttack)
			{
				this.slashCounter++;
				if (this.slashCounter > 3)
				{
					this.slashFrame++;
					this.slashCounter = 0;
				}
				if (this.slashFrame >= 6)
				{
					this.slashFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 80f && !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly && Main.rand.Next(20) == 0 && !this.slashAttack)
			{
				this.slashAttack = true;
			}
			if (!this.slashAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.slashAttack)
			{
				this.slashTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.slashTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Gray, "Slash!", true, true);
				}
				if (this.slashTimer == 6)
				{
					if (base.npc.direction == -1)
					{
						int p = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 4f, 0f, 0f, ModContent.ProjectileType<DamagePro2>(), 16, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int p2 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 4f, 0f, 0f, ModContent.ProjectileType<DamagePro2>(), 16, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.slashTimer >= 18)
				{
					this.slashAttack = false;
					this.slashTimer = 0;
					this.slashCounter = 0;
					this.slashFrame = 0;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(44, 600, true);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D slashAni = base.mod.GetTexture("NPCs/SkeletonNobleArmoured2Slash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.slashAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slashAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = slashAni.Height / 6;
				int y6 = num214 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, slashAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly;
		}

		private bool slashAttack;

		private int slashFrame;

		private int slashCounter;

		private int slashTimer;
	}
}
