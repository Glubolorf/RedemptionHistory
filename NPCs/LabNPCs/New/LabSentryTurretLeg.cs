using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class LabSentryTurretLeg : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Sentry");
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 24;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = false;
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
			this.Target();
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
			if (RedeWorld.labSafe)
			{
				if (base.npc.ai[0] == 0f)
				{
					for (int i = 0; i < 5; i++)
					{
						int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex2].velocity *= 1.9f;
					}
					base.npc.ai[0] = 1f;
				}
				this.customGunRot = true;
				this.gunRot = 1.5708f;
				base.npc.immortal = false;
				return;
			}
			Point point = Utils.ToTileCoordinates(this.player.position);
			if (distance < 850f && distance >= 450f)
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
				if ((int)Main.tile[point.X, point.Y].wall == base.mod.WallType("HardenedlyHardenedSludgeWallTile") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("HardenedSludgeWallTile") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("LabWallTileUnsafe") || (int)Main.tile[point.X, point.Y].wall == base.mod.WallType("VentWallTile"))
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 5f == 0f)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 20f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 200;
						int type = base.mod.ProjectileType("ElectricZapPro1");
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

		public override bool CheckActive()
		{
			return !RedeWorld.labSafe;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D gunAni = base.mod.GetTexture("NPCs/LabNPCs/New/LabSentryTurretHead");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterG = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			int numG = gunAni.Height / 1;
			int yG = numG * this.gunFrame;
			spriteBatch.Draw(gunAni, drawCenterG - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, gunAni.Width, numG)), drawColor, this.customGunRot ? this.gunRot : Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)gunAni.Width / 2f, (float)numG / 2f), base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private Player player;

		private int gunFrame;

		private bool customGunRot;

		private float gunRot;
	}
}
