using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.v08;
using Redemption.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class LabSentryDrone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Drone");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 36;
			base.npc.height = 20;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.aiStyle = -1;
			base.npc.immortal = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex].velocity *= 1.9f;
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 3.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 30;
				if (base.npc.frame.Y > 90)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			this.Move(new Vector2(this.movX, this.movY));
			base.npc.rotation = base.npc.velocity.X * 0.05f;
			base.npc.ai[3] += 1f;
			if (base.npc.ai[3] % 60f == 0f)
			{
				this.movX = (float)(Main.rand.Next(-100, 100) * 2);
				this.movY = (float)(Main.rand.Next(-80, 80) * 2);
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
			if (!((RedePlayer)this.player.GetModPlayer(base.mod, "RedePlayer")).ZoneLab)
			{
				base.npc.active = false;
				base.npc.velocity = new Vector2(0f, -10f);
			}
			if (RedeWorld.labSafe)
			{
				base.npc.active = false;
				base.npc.velocity = new Vector2(0f, -50f);
				return;
			}
			Point point = Utils.ToTileCoordinates(this.player.position);
			if (distance < 650f && distance >= 450f)
			{
				this.customGunRot = false;
				if (base.npc.ai[1] == 0f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "INTRUDER DETECTED...", true, false);
					base.npc.ai[1] = 1f;
					return;
				}
			}
			else if (distance < 450f)
			{
				this.customGunRot = false;
				if ((int)Main.tile[point.X, point.Y].wall == ModContent.WallType<HardenedlyHardenedSludgeWallTile>() || (int)Main.tile[point.X, point.Y].wall == ModContent.WallType<HardenedSludgeWallTile>() || (int)Main.tile[point.X, point.Y].wall == ModContent.WallType<LabWallTileUnsafe>() || ((int)Main.tile[point.X, point.Y].wall == ModContent.WallType<VentWallTile>() && !this.player.dead))
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 8f == 0f)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 20f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 100;
						int type = ModContent.ProjectileType<ElectricZapPro1>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
						Main.projectile[num54].tileCollide = false;
						Main.projectile[num54].timeLeft = 200;
						return;
					}
				}
			}
			else
			{
				base.npc.ai[2] = 0f;
				this.customGunRot = true;
				this.gunRot = 1.5708f;
			}
		}

		private void Move(Vector2 offset)
		{
			this.speed = 15f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 30f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D gunAni = base.mod.GetTexture("NPCs/LabNPCs/New/LabSentryTurretHead");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterG = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			int numG = gunAni.Height / 1;
			int yG = 0;
			spriteBatch.Draw(gunAni, drawCenterG - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, gunAni.Width, numG)), drawColor, this.customGunRot ? this.gunRot : Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)gunAni.Width / 2f, (float)numG / 2f), base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private Player player;

		private float speed;

		private bool customGunRot;

		private float gunRot;

		private float movX;

		private float movY;
	}
}
