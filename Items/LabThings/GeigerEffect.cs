using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class GeigerEffect : ModPlayer
	{
		public override void ResetEffects()
		{
			this.effect = false;
		}

		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int num = layers.FindIndex((PlayerLayer PlayerLayer) => PlayerLayer.Name.Equals("Wings"));
			if (num != -1 && GeigerEffect.LabSpawn.X != (float)((int)((float)Main.maxTilesX * 0.6f)) && GeigerEffect.LabSpawn.Y != (float)((int)((float)Main.maxTilesY * 0.65f)))
			{
				GeigerEffect.LabPointer.visible = true;
				layers.Insert(num + 1, GeigerEffect.LabPointer);
				return;
			}
			GeigerEffect.LabPointer.visible = false;
		}

		public bool effect;

		public static Vector2 LabSpawn = new Vector2((float)((int)((float)Main.maxTilesX * 0.6f + 198f) * 16), (float)((int)((float)Main.maxTilesY * 0.65f + 16f) * 16));

		public static readonly PlayerLayer LabPointer = new PlayerLayer("Redemption", "LabLegs", PlayerLayer.Legs, delegate(PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("Redemption");
			if (drawPlayer.GetModPlayer<GeigerEffect>(mod).effect && GeigerEffect.LabSpawn.X != (float)((int)((float)Main.maxTilesX * 0.6f)) && GeigerEffect.LabSpawn.Y != (float)((int)((float)Main.maxTilesY * 0.65f)))
			{
				Texture2D texture = mod.GetTexture("Items/LabThings/LabPointer");
				float x = drawPlayer.position.X;
				float x2 = Main.screenPosition.X;
				float y = drawPlayer.position.Y;
				float y2 = Main.screenPosition.Y;
				Vector2 position = drawPlayer.position;
				Vector2 vector = Utils.Size(texture) / 2f;
				Vector2 vector2 = new Vector2((float)((int)(position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2));
				vector2.Y -= (float)drawPlayer.mount.PlayerOffset;
				float num = Utils.ToRotation(GeigerEffect.LabSpawn - drawPlayer.Center);
				DrawData item;
				item..ctor(texture, vector2, new Rectangle?(new Rectangle(0, 0, (int)Utils.Size(texture).X, (int)Utils.Size(texture).Y)), Color.White, num, vector, 1f, 0, 0);
				Main.playerDrawData.Add(item);
			}
		});
	}
}
