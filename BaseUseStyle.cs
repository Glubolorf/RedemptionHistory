using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace Redemption
{
	public class BaseUseStyle
	{
		public static void SetStyleHarpoon(Player player, Item item, int projType, bool isIndex = false)
		{
			int num = isIndex ? projType : BaseAI.GetProjectile(player.Center, projType, player.whoAmI, null, -1f, null);
			if (num != -1)
			{
				Vector2 center = Main.projectile[num].Center;
				float num2 = center.X - player.Center.X;
				float num3 = center.Y - player.Center.Y;
				player.direction = ((center.X > player.Center.X) ? 1 : -1);
				player.itemRotation = (float)Math.Atan2((double)(num3 * (float)player.direction), (double)(num2 * (float)player.direction));
				if (player.whoAmI == Main.myPlayer && Main.netMode != 0)
				{
					NetMessage.SendData(13, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(41, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			BaseUseStyle.MoveItemLocationGun(player, item);
		}

		public static void SetStyleBoss(Player player, Item item, bool useItemHitbox = false, bool center = false)
		{
			Rectangle rectangle = (useItemHitbox || Main.netMode == 2 || Main.dedServ) ? item.Hitbox : new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height);
			player.itemRotation = 0f;
			player.itemLocation.X = player.position.X + (float)player.width * 0.5f + ((center ? 0f : ((float)rectangle.Width * 0.5f)) - 9f - player.itemRotation * 14f * (float)player.direction - 4f) * (float)player.direction;
			player.itemLocation.Y = player.position.Y + (float)rectangle.Height * 0.5f + 4f;
			if (player.gravDir == -1f)
			{
				player.itemRotation = -player.itemRotation;
				player.itemLocation.Y = player.position.Y + (float)player.height + (player.position.Y - player.itemLocation.Y);
			}
			if (Main.myPlayer == player.whoAmI && Main.netMode != 0)
			{
				NetMessage.SendData(13, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(41, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public static void SetFrameBoss(Player player, Item item)
		{
			player.bodyFrame.Y = player.bodyFrame.Height * 2;
		}

		public static void SetStyleGun(Player player, Item item, bool ignoreItemTime = false)
		{
			if (player.whoAmI == Main.myPlayer && (ignoreItemTime || player.itemTime == item.useTime - 1))
			{
				float num = (float)Main.mouseX + Main.screenPosition.X - player.Center.X;
				float num2 = (float)Main.mouseY + Main.screenPosition.Y - player.Center.Y;
				player.itemRotation = (float)Math.Atan2((double)(num2 * (float)player.direction), (double)(num * (float)player.direction));
				if (Main.netMode != 0)
				{
					NetMessage.SendData(13, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(41, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			BaseUseStyle.MoveItemLocationGun(player, item);
		}

		public static void SetStyleGun(Vector2 target, Vector2 center, ref Vector2 itemLocation, ref float itemRotation, Item item, int direction, int itemTime, int useTime, bool ignoreItemTime = false)
		{
			if (ignoreItemTime || itemTime == useTime - 1)
			{
				float num = target.X - center.X;
				float num2 = target.Y - center.Y;
				itemRotation = (float)Math.Atan2((double)(num2 * (float)direction), (double)(num * (float)direction));
			}
			itemLocation = BaseUseStyle.MoveItemLocationGun(center, itemLocation, direction, item);
		}

		public static void SetStyleSword(Player player, Item item, bool basedOnRot = false)
		{
			player.itemRotation = ((float)player.itemAnimation / (float)player.itemAnimationMax - 0.5f) * (float)(-(float)player.direction) * 3.5f - (float)player.direction * 0.3f;
			if (player.gravDir == -1f)
			{
				player.itemRotation *= -1f;
			}
			if (Main.myPlayer == player.whoAmI && Main.netMode != 0)
			{
				NetMessage.SendData(13, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(41, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
			BaseUseStyle.MoveItemLocationSword(player, item, basedOnRot);
		}

		public static void SetStyleSword(Vector2 position, int width, int height, ref Vector2 itemLocation, ref float itemRotation, int itemAnimation, int itemAnimationMax, int direction, float gravDir, Item item, bool basedOnRot = false)
		{
			itemRotation = ((float)itemAnimation / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f;
			if (gravDir == -1f)
			{
				itemRotation *= -1f;
			}
			itemLocation = BaseUseStyle.MoveItemLocationSword(position, width, height, itemLocation, itemAnimation, itemAnimationMax, itemRotation, direction, gravDir, item, basedOnRot);
		}

		public static void MoveItemLocationGun(Player player, Item item)
		{
			player.itemLocation = BaseUseStyle.MoveItemLocationGun(player.Center, player.itemLocation, player.direction, item);
		}

		public static Vector2 MoveItemLocationGun(Vector2 center, Vector2 itemLocation, int direction, Item item)
		{
			itemLocation.X = center.X - ((Main.netMode == 2 || Main.dedServ) ? ((float)item.width * 0.5f) : ((float)Main.itemTexture[item.type].Width * 0.5f)) - (float)(direction * 2);
			itemLocation.Y = center.Y - ((Main.netMode == 2 || Main.dedServ) ? ((float)item.height * 0.5f) : ((float)Main.itemTexture[item.type].Height * 0.5f));
			return itemLocation;
		}

		public static void MoveItemLocationSword(Player player, Item item, bool basedOnRot = false)
		{
			player.itemLocation = BaseUseStyle.MoveItemLocationSword(player.position, player.width, player.height, player.itemLocation, player.itemAnimation, player.itemAnimationMax, player.itemRotation, player.direction, player.gravDir, item, basedOnRot);
		}

		public static Vector2 MoveItemLocationSword(Vector2 position, int width, int height, Vector2 itemLocation, int itemAnimation, int itemAnimationMax, float itemRotation, int direction, float gravDir, Item item, bool basedOnRot = false)
		{
			float num = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.33f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			float num2 = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.66f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			bool flag = itemRotation > num;
			bool flag2 = itemRotation > num2;
			if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.33f) : ((gravDir == 1f && ((direction == 1 && flag) || (direction == -1 && !flag))) || (gravDir == -1f && ((direction == 1 && !flag) || (direction == -1 && flag)))))
			{
				float num3 = 10f;
				if (Main.itemTexture[item.type].Width > 64)
				{
					num3 = 28f;
				}
				else if (Main.itemTexture[item.type].Width > 32)
				{
					num3 = 14f;
				}
				itemLocation.X = position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - num3) * (float)direction;
				itemLocation.Y = position.Y + 24f;
			}
			else if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.66f) : ((gravDir == 1f && ((direction == 1 && flag2) || (direction == -1 && !flag2))) || (gravDir == -1f && ((direction == 1 && !flag2) || (direction == -1 && flag2)))))
			{
				float num4 = 10f;
				if (Main.itemTexture[item.type].Width > 64)
				{
					num4 = 28f;
				}
				else if (Main.itemTexture[item.type].Width > 32)
				{
					num4 = 18f;
				}
				itemLocation.X = position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - num4) * (float)direction;
				num4 = 10f;
				if (Main.itemTexture[item.type].Height > 64)
				{
					num4 = 14f;
				}
				else if (Main.itemTexture[item.type].Height > 32)
				{
					num4 = 8f;
				}
				itemLocation.Y = position.Y + num4;
			}
			else
			{
				float num5 = 6f;
				if (Main.itemTexture[item.type].Width > 64)
				{
					num5 = 28f;
				}
				else if (Main.itemTexture[item.type].Width > 32)
				{
					num5 = 14f;
				}
				itemLocation.X = position.X + (float)width * 0.5f - ((float)Main.itemTexture[item.type].Width * 0.5f - num5) * (float)direction;
				num5 = 10f;
				if (Main.itemTexture[item.type].Height > 64)
				{
					num5 = 14f;
				}
				itemLocation.Y = position.Y + num5;
			}
			if (gravDir == -1f)
			{
				itemLocation.Y = position.Y + (float)height + (position.Y - itemLocation.Y);
			}
			return itemLocation;
		}

		public static void SetFrameGun(Player player, Item item)
		{
			player.bodyFrame = BaseUseStyle.SetFrameGun(player.bodyFrame, player.itemRotation, player.direction, player.gravDir, item);
		}

		public static Rectangle SetFrameGun(Rectangle bodyFrame, float itemRotation, int direction, float gravDir, Item item)
		{
			float num = itemRotation * (float)direction;
			bodyFrame.Y = bodyFrame.Height * 3;
			if (num < -0.75f)
			{
				bodyFrame.Y = bodyFrame.Height * 2;
				if (gravDir == -1f)
				{
					bodyFrame.Y = bodyFrame.Height * 4;
				}
			}
			if (num > 0.6f)
			{
				bodyFrame.Y = bodyFrame.Height * 4;
				if (gravDir == -1f)
				{
					bodyFrame.Y = bodyFrame.Height * 2;
				}
			}
			return bodyFrame;
		}

		public static void SetFrameSword(Player player, Item item, bool basedOnRot = false)
		{
			player.bodyFrame = BaseUseStyle.SetFrameSword(player.bodyFrame, player.itemAnimation, player.itemAnimationMax, player.itemRotation, player.direction, player.gravDir, item, basedOnRot);
		}

		public static Rectangle SetFrameSword(Rectangle bodyFrame, int itemAnimation, int itemAnimationMax, float itemRotation, int direction, float gravDir, Item item, bool basedOnRot = false)
		{
			float num = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.33f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			float num2 = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.66f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			bool flag = itemRotation > num;
			bool flag2 = itemRotation > num2;
			if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.33f) : ((gravDir == 1f && ((direction == 1 && flag) || (direction == -1 && !flag))) || (gravDir == -1f && ((direction == 1 && !flag) || (direction == -1 && flag)))))
			{
				bodyFrame.Y = bodyFrame.Height * 3;
			}
			else if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.66f) : ((gravDir == 1f && ((direction == 1 && flag2) || (direction == -1 && !flag2))) || (gravDir == -1f && ((direction == 1 && !flag2) || (direction == -1 && flag2)))))
			{
				bodyFrame.Y = bodyFrame.Height * 2;
			}
			else
			{
				bodyFrame.Y = bodyFrame.Height;
			}
			return bodyFrame;
		}

		public static Rectangle UpdateHitBoxSword(Player player, Item item, Rectangle ItemRect, bool basedOnRot = false)
		{
			return BaseUseStyle.UpdateHitBoxSword(player.itemAnimation, player.itemAnimationMax, player.itemRotation, player.direction, player.gravDir, item, ItemRect, basedOnRot);
		}

		public static Rectangle UpdateHitBoxSword(int itemAnimation, int itemAnimationMax, float itemRotation, int direction, float gravDir, Item item, Rectangle ItemRect, bool basedOnRot = false)
		{
			float num = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.33f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			float num2 = (!basedOnRot) ? 0f : (((float)itemAnimationMax * 0.66f * 0.75f / (float)itemAnimationMax - 0.5f) * (float)(-(float)direction) * 3.5f - (float)direction * 0.3f);
			bool flag = itemRotation > num;
			bool flag2 = itemRotation > num2;
			if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.33f) : ((gravDir == 1f && ((direction == 1 && flag) || (direction == -1 && !flag))) || (gravDir == -1f && ((direction == 1 && !flag) || (direction == -1 && flag)))))
			{
				if (direction == -1)
				{
					ItemRect.X -= (int)((float)ItemRect.Width * 1.4f - (float)ItemRect.Width);
				}
				ItemRect.Width = (int)((float)ItemRect.Width * 1.4f);
				ItemRect.Height = (int)((float)ItemRect.Height * 1.1f);
				ItemRect.Y += (int)((float)ItemRect.Height * 0.5f * gravDir);
			}
			else if ((!basedOnRot) ? ((float)itemAnimation < (float)itemAnimationMax * 0.66f) : ((gravDir == 1f && ((direction == 1 && flag2) || (direction == -1 && !flag2))) || (gravDir == -1f && ((direction == 1 && !flag2) || (direction == -1 && flag2)))))
			{
				if (direction == 1)
				{
					ItemRect.X -= (int)((float)ItemRect.Width * 1.2f);
				}
				ItemRect.Y -= (int)(((float)ItemRect.Height * 1.4f - (float)ItemRect.Height) * gravDir);
				ItemRect.Width *= 2;
				ItemRect.Height = (int)((float)ItemRect.Height * 1.4f);
			}
			return ItemRect;
		}
	}
}
