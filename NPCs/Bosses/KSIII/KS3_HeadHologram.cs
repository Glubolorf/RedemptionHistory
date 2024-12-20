using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KS3_HeadHologram : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hologram");
			Main.projFrames[base.projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 30;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			switch (this.faceType)
			{
			case 0:
			{
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 2)
					{
						base.projectile.frame = 0;
					}
				}
				break;
			}
			case 1:
			{
				Projectile projectile3 = base.projectile;
				int num = projectile3.frameCounter + 1;
				projectile3.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile4 = base.projectile;
					num = projectile4.frame + 1;
					projectile4.frame = num;
					if (num >= 4)
					{
						base.projectile.frame = 2;
					}
				}
				break;
			}
			case 2:
			{
				Projectile projectile5 = base.projectile;
				int num = projectile5.frameCounter + 1;
				projectile5.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile6 = base.projectile;
					num = projectile6.frame + 1;
					projectile6.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 4;
					}
				}
				break;
			}
			case 3:
			{
				Projectile projectile7 = base.projectile;
				int num = projectile7.frameCounter + 1;
				projectile7.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile8 = base.projectile;
					num = projectile8.frame + 1;
					projectile8.frame = num;
					if (num >= 8)
					{
						base.projectile.frame = 6;
					}
				}
				break;
			}
			case 4:
			{
				Projectile projectile9 = base.projectile;
				int num = projectile9.frameCounter + 1;
				projectile9.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile10 = base.projectile;
					num = projectile10.frame + 1;
					projectile10.frame = num;
					if (num >= 10)
					{
						base.projectile.frame = 8;
					}
				}
				break;
			}
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] < 30f)
			{
				base.projectile.alpha -= 4;
			}
			if (!Main.dedServ)
			{
				if (base.projectile.localAI[0] == 30f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Hey, get lost.", 150, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
				}
				if (base.projectile.localAI[0] == 170f)
				{
					this.faceType = 1;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("You really aren't worth my time, ya know.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
				}
				if (base.projectile.localAI[0] == 350f)
				{
					this.faceType = 3;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					switch (Main.rand.Next(4))
					{
					case 0:
						Redemption.Inst.DialogueUIElement.DisplayDialogue("So stop bothering me and leave me to my 4D chess.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
						break;
					case 1:
						Redemption.Inst.DialogueUIElement.DisplayDialogue("So stop bothering me, I have a certain android I need to 'lecture'.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
						break;
					case 2:
						Redemption.Inst.DialogueUIElement.DisplayDialogue("So stop bothering me, I don't care about you.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
						break;
					case 3:
						Redemption.Inst.DialogueUIElement.DisplayDialogue("So stop bothering me and we can all go about our day.", 200, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
						break;
					}
				}
				if (base.projectile.localAI[0] == 550f)
				{
					this.faceType = 0;
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("I'll beat you up if you annoy me again.", 180, 1, 0.6f, "King Slayer III:", 0.4f, new Color?(RedeColor.SlayerColour), null, null, new Vector2?(base.projectile.Center), 0, 0);
				}
			}
			if (base.projectile.localAI[0] > 730f)
			{
				if (RedeWorld.slayerDeath < 1)
				{
					RedeWorld.slayerDeath = 1;
				}
				base.projectile.alpha += 2;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			if (MoRDialogueUI.Visible)
			{
				Redemption.Inst.DialogueUIElement.PointPos = new Vector2?(base.projectile.Center);
				Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.SlayerColour);
			}
		}

		public int faceType;
	}
}
