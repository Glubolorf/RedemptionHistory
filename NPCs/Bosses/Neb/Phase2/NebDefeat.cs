using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Phase2
{
	public class NebDefeat : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 64;
			base.projectile.height = 62;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 20)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.projectile.Center;
			player.GetModPlayer<ScreenPlayer>().lockScreen = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.X = 0f;
			if (RedeWorld.nebDeath >= 8 || RedeConfigClient.Instance.NoLoreElements)
			{
				if (base.projectile.localAI[0] >= 120f)
				{
					base.projectile.Kill();
				}
			}
			else
			{
				if (!Main.dedServ)
				{
					if (base.projectile.localAI[0] == 180f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("... That was all I had.", 150, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
						Redemption.GirusSilence = true;
					}
					if (base.projectile.localAI[0] == 330f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("And yet I still lost...", 150, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 480f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Fighting you and that traitor have confirmed my suspicions.", 200, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 680f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I'm weak.", 140, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 820f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I don't deserve being part of their group. I was only a burden.", 200, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 1020f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("If the Demigod ever comes for you, don't let him know I'm still alive.", 240, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 1260f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I fear what he might think of me, and I don't want to face him.", 240, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
					if (base.projectile.localAI[0] == 1500f)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I don't expect sympathy from you, but I'll be gone for good now. Goodbye.", 260, 1, 0.6f, "Nebuleus:", 1f, new Color?(RedeColor.NebColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
					}
				}
				if (base.projectile.localAI[0] >= 1760f)
				{
					this.fadeAlpha -= 5f;
					if (this.fadeAlpha <= 0f)
					{
						base.projectile.Kill();
					}
				}
			}
			if (MoRDialogueUI.Visible)
			{
				Redemption.Inst.DialogueUIElement.PointPos = new Vector2?(base.projectile.Center);
				Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.NebColour);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/Neb/Phase2/NebDefeatFade");
			SpriteEffects effects = (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int num215 = texture.Height / 4;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, effects, 0f);
			Main.spriteBatch.Draw(glowMask, base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), RedeColor.NebColour * ((255f - this.fadeAlpha) / 255f), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, effects, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 58, 0f, 0f, 100, default(Color), 5f);
				Main.dust[dustIndex].velocity *= 2.9f;
			}
			RedeWorld.nebDeath = 8;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}

		public float fadeAlpha = 255f;
	}
}
