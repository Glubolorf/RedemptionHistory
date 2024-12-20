using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class SkeletonAssassin2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skeleton Assassin");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[271];
		}

		public override void SetDefaults()
		{
			base.npc.width = 36;
			base.npc.height = 48;
			base.npc.damage = 45;
			base.npc.defense = 20;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = (float)Item.buyPrice(0, 0, 5, 0);
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<SkeletonAssassinBanner>();
		}

		public override void AI()
		{
			if (this.stabAttack)
			{
				this.stabCounter++;
				if (this.stabCounter > 3)
				{
					this.stabFrame++;
					this.stabCounter = 0;
				}
				if (this.stabFrame >= 6)
				{
					this.stabFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 80f && !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly && Main.rand.Next(20) == 0 && !this.stabAttack)
			{
				this.stabAttack = true;
			}
			if (!this.stabAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.stabAttack)
			{
				this.stabTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.stabTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Gray, "Stab!", true, true);
				}
				if (this.stabTimer == 6)
				{
					if (base.npc.direction == -1)
					{
						int p = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 28f, 0f, 0f, ModContent.ProjectileType<DamagePro1>(), 22, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int p2 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 28f, 0f, 0f, ModContent.ProjectileType<DamagePro1>(), 22, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.stabTimer >= 18)
				{
					this.stabAttack = false;
					this.stabTimer = 0;
					this.stabCounter = 0;
					this.stabFrame = 0;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBoneAssassin"), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 36, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D stabAni = base.mod.GetTexture("NPCs/Varients/SkeletonAssassinStab2");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.stabAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.stabAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = stabAni.Height / 6;
				int y6 = num214 * this.stabFrame;
				Main.spriteBatch.Draw(stabAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, stabAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)stabAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly;
		}

		private bool stabAttack;

		private int stabFrame;

		private int stabCounter;

		private int stabTimer;
	}
}
