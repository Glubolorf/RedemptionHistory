using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption
{
	public class BaseAI
	{
		public static void AIDive(Projectile projectile, ref int chargeTime, int chargeTimeMax, Vector2 targetCenter)
		{
			chargeTime = Math.Max(0, chargeTime - 1);
			if (chargeTime > 0)
			{
				Vector2 position = projectile.position - projectile.velocity;
				float num = (float)chargeTime / (float)chargeTimeMax;
				float num2 = (float)Math.Sin((double)(3.1415927f * num));
				float num3 = Math.Abs(targetCenter.X - projectile.Center.X);
				float num4 = Math.Abs(targetCenter.Y - projectile.Center.Y);
				float num5 = num3 / (float)chargeTimeMax;
				float num6 = num4 / (float)chargeTimeMax;
				projectile.velocity = new Vector2(num5 * (float)chargeTime * (float)projectile.direction, num2 * num6);
				projectile.position = position;
			}
		}

		public static void AIMinionPlant(Projectile projectile, ref float[] ai, Entity owner, Vector2 endPoint, bool setTime = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2), Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (setTime)
			{
				projectile.timeLeft = 10;
			}
			Entity entity = (GetTarget == null) ? null : GetTarget(projectile, owner);
			if (entity == null)
			{
				entity = owner;
			}
			bool flag = entity == owner;
			ai[0] += 1f;
			if (ai[0] > (float)vineTimeExtend)
			{
				vineLength = vineLengthLong;
				if (ai[0] > (float)vineTimeMax)
				{
					ai[0] = 0f;
				}
			}
			Vector2 vector = entity.Center + targetOffset + ((entity == owner) ? new Vector2(0f, (owner is Player) ? ((Player)owner).gfxOffY : ((owner is NPC) ? ((NPC)owner).gfxOffY : ((Projectile)owner).gfxOffY)) : default(Vector2));
			if (!flag)
			{
				float num = vector.X - endPoint.X;
				float num2 = vector.Y - endPoint.Y;
				float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
				if (num3 > vineLength)
				{
					projectile.velocity *= 0.85f;
					projectile.velocity += owner.velocity;
					num3 = vineLength / num3;
					num *= num3;
					num2 *= num3;
				}
				if (ShootTarget == null || !ShootTarget(projectile, owner, entity))
				{
					if (projectile.position.X < endPoint.X + num)
					{
						projectile.velocity.X = projectile.velocity.X + moveInterval;
						if (projectile.velocity.X < 0f && num > 0f)
						{
							projectile.velocity.X = projectile.velocity.X + moveInterval * 1.5f;
						}
					}
					else if (projectile.position.X > endPoint.X + num)
					{
						projectile.velocity.X = projectile.velocity.X - moveInterval;
						if (projectile.velocity.X > 0f && num < 0f)
						{
							projectile.velocity.X = projectile.velocity.X - moveInterval * 1.5f;
						}
					}
					if (projectile.position.Y < endPoint.Y + num2)
					{
						projectile.velocity.Y = projectile.velocity.Y + moveInterval;
						if (projectile.velocity.Y < 0f && num2 > 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y + moveInterval * 1.5f;
						}
					}
					else if (projectile.position.Y > endPoint.Y + num2)
					{
						projectile.velocity.Y = projectile.velocity.Y - moveInterval;
						if (projectile.velocity.Y > 0f && num2 < 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y - moveInterval * 1.5f;
						}
					}
				}
				else
				{
					projectile.velocity *= 0.85f;
					if (Math.Abs(projectile.velocity.X) < moveInterval + 0.01f)
					{
						projectile.velocity.X = 0f;
					}
					if (Math.Abs(projectile.velocity.Y) < moveInterval + 0.01f)
					{
						projectile.velocity.Y = 0f;
					}
				}
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
				if (num > 0f)
				{
					projectile.spriteDirection = 1;
					projectile.rotation = (float)Math.Atan2((double)num2, (double)num);
				}
				else if (num < 0f)
				{
					projectile.spriteDirection = -1;
					projectile.rotation = (float)Math.Atan2((double)num2, (double)num) + 3.14f;
				}
				if (projectile.tileCollide)
				{
					Vector4 vector2 = Collision.SlopeCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, 0f, false);
					projectile.position = new Vector2(vector2.X, vector2.Y);
					projectile.velocity = new Vector2(vector2.Z, vector2.W);
				}
				projectile.position += owner.position - owner.oldPosition;
				return;
			}
			projectile.position += owner.position - owner.oldPosition;
			projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? -1 : 1);
			projectile.velocity = BaseAI.AIVelocityLinear(projectile, vector, moveInterval, speedMax, true);
			if (Vector2.Distance(projectile.Center, vector) < speedMax * 1.1f)
			{
				projectile.rotation = 0f;
				projectile.velocity *= 0f;
				projectile.Center = vector;
				return;
			}
			projectile.rotation = BaseUtility.RotationTo(vector, projectile.Center) + ((projectile.spriteDirection == -1) ? 3.14f : 0f);
		}

		public static void TileCollidePlant(Projectile projectile, ref Vector2 velocity, float speedMax)
		{
			if (projectile.velocity.X != velocity.X)
			{
				projectile.netUpdate = true;
				projectile.velocity.X = projectile.velocity.X * -0.7f;
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
			}
			if (projectile.velocity.Y != velocity.Y)
			{
				projectile.netUpdate = true;
				projectile.velocity.Y = projectile.velocity.Y * -0.7f;
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
			}
		}

		public static void AIMinionFlier(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, bool movementFixed = false, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, bool autoSpriteDir = true, bool dummyTileCollide = false, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (moveInterval == -1f)
			{
				moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed;
			}
			if (maxSpeed == -1f)
			{
				maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed);
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed));
			}
			projectile.timeLeft = 10;
			bool tileCollide = projectile.tileCollide;
			BaseAI.AIMinionFlier(projectile, ref ai, owner, ref tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, movementFixed, hover, hoverHeight, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget, ShootTarget);
			if (!dummyTileCollide)
			{
				projectile.tileCollide = tileCollide;
			}
			if (autoSpriteDir)
			{
				projectile.spriteDirection = projectile.direction;
			}
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && Math.Abs(projectile.velocity.X + projectile.velocity.Y) <= 0.025f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionFlier(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, bool movementFixed, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			float num = Vector2.Distance(codable.Center, owner.Center);
			if (num > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int num2 = (int)(codable.Center.X / 16f);
			int num3 = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[num2, num3];
			bool flag = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float num4 = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (num > Math.Max((float)lineDist, (float)returnDist / 2f) || !BaseUtility.CanHit(codable.Hitbox, owner.Hitbox))) || num > (float)returnDist || flag) ? 1 : 0);
			if (ai[0] != num4)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f || ai[0] == 1f)
			{
				if (ai[0] == 1f)
				{
					moveInterval *= 1.5f;
					maxSpeedFlying *= 1.5f;
				}
				tileCollide = (ai[0] == 0f);
				Entity entity = (GetTarget == null) ? owner : GetTarget(codable, owner);
				if (entity == null)
				{
					entity = owner;
				}
				Vector2 center = entity.Center;
				bool flag2 = entity == owner;
				bool flag3 = ai[0] == 0f && ShootTarget != null && ShootTarget(codable, owner, entity);
				if (flag2)
				{
					center.Y -= (float)hoverHeight;
					if (hover)
					{
						center.X += (float)((lineDist + lineDist * minionPos) * -(float)entity.direction);
					}
				}
				if (!hover || !flag2)
				{
					float num5 = hover ? 1.2f : 1.8f;
					float num6 = (num < (float)(lineDist * minionPos) + (float)lineDist * num5) ? ((codable.velocity.X > 0f) ? 1f : -1f) : ((entity.Center.X > codable.Center.X) ? 1f : -1f);
					center.X += ((minionPos == 0) ? 0f : ((minionPos % 5 == 0) ? ((float)lineDist / 4f) : ((minionPos % 4 == 0) ? ((float)lineDist / 2f) : ((minionPos % 3 == 0) ? ((float)lineDist / 3f) : 0f)))) * num6;
					center.X += (float)lineDist * 2f * num6;
					center.Y -= (float)hoverHeight / 4f * (float)minionPos;
					center.Y -= ((codable.velocity.X < 0f) ? ((float)lineDist * 0.25f) : ((float)(-(float)lineDist) * 0.25f)) * (float)((minionPos % 2 == 0) ? 1 : -1);
				}
				float num7 = Math.Abs(codable.Center.X - center.X);
				float num8 = Math.Abs(codable.Center.Y - center.Y);
				bool flag4 = hover && owner.velocity.X < 0.025f && num7 < 8f * Math.Max(1f, maxSpeed / 4f);
				bool flag5 = hover && owner.velocity.Y < 0.025f && num8 < 8f * Math.Max(1f, maxSpeed / 4f);
				Vector2 vector = BaseAI.AIVelocityLinear(codable, center, moveInterval, (ai[0] == 0f) ? maxSpeed : maxSpeedFlying, true);
				if (!flag3 && !flag4)
				{
					codable.velocity.X = codable.velocity.X + vector.X * 0.125f;
				}
				if (!flag3 && !flag5)
				{
					codable.velocity.Y = codable.velocity.Y + vector.Y * 0.125f;
				}
				if (flag3 || flag4)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.85f : 0f);
				}
				if ((vector.X > 0f && codable.velocity.X > vector.X) || (vector.X < 0f && codable.velocity.X < vector.X))
				{
					codable.velocity.X = vector.X;
				}
				if (flag3 || flag5)
				{
					codable.velocity.Y = codable.velocity.Y * ((Math.Abs(codable.velocity.Y) > 0.01f) ? 0.85f : 0f);
				}
				if ((vector.Y > 0f && codable.velocity.Y > vector.Y) || (vector.Y < 0f && codable.velocity.X < vector.Y))
				{
					codable.velocity.Y = vector.Y;
				}
			}
		}

		public static void AIMinionFighter(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (moveInterval == -1f)
			{
				moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed;
			}
			if (maxSpeed == -1f)
			{
				maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed);
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed));
			}
			projectile.timeLeft = 10;
			BaseAI.AIMinionFighter(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, ref projectile.gfxOffY, ref projectile.stepSpeed, pet ? 0 : projectile.minionPos, jumpDistX, jumpDistY, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionFighter(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, ref float gfxOffY, ref float stepSpeed, int minionPos, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float num = Vector2.Distance(codable.Center, owner.Center);
			if (num > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int num2 = (int)(codable.Center.X / 16f);
			int num3 = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[num2, num3];
			bool flag = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float num4 = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (owner.velocity.Y != 0f || num > Math.Max((float)lineDist, (float)returnDist / 10f))) || num > (float)returnDist || flag) ? 1 : 0);
			if (ai[0] != num4)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f)
			{
				tileCollide = true;
				Entity entity = (GetTarget == null) ? null : GetTarget(codable, owner);
				Vector2 vector = (entity == null) ? default(Vector2) : entity.Center;
				bool flag2 = entity == null || vector == owner.Center;
				if (vector == default(Vector2))
				{
					vector = owner.Center;
					vector.X += (float)((owner.width + 10 + lineDist * minionPos) * -(float)owner.direction);
				}
				float num5 = Math.Abs(codable.Center.X - vector.X);
				float num6 = Math.Abs(codable.Center.Y - vector.Y);
				int num7 = (vector.X > codable.Center.X) ? 1 : -1;
				int num8 = (vector.Y > codable.Center.Y) ? 1 : -1;
				if (flag2 && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && num5 < 8f)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.8f : 0f);
				}
				else if (codable.velocity.X < -maxSpeed || codable.velocity.X > maxSpeed)
				{
					if (codable.velocity.Y == 0f)
					{
						codable.velocity *= 0.85f;
					}
				}
				else if (codable.velocity.X < maxSpeed && num7 == 1)
				{
					if (codable.velocity.X < 0f)
					{
						codable.velocity.X = codable.velocity.X * 0.85f;
					}
					codable.velocity.X = codable.velocity.X + moveInterval * ((codable.velocity.X < 0f) ? 2f : 1f);
					if (codable.velocity.X > maxSpeed)
					{
						codable.velocity.X = maxSpeed;
					}
				}
				else if (codable.velocity.X > -maxSpeed && num7 == -1)
				{
					if (codable.velocity.X > 0f)
					{
						codable.velocity.X = codable.velocity.X * 0.8f;
					}
					codable.velocity.X = codable.velocity.X - moveInterval * ((codable.velocity.X > 0f) ? 2f : 1f);
					if (codable.velocity.X < -maxSpeed)
					{
						codable.velocity.X = -maxSpeed;
					}
				}
				BaseAI.WalkupHalfBricks(codable, ref gfxOffY, ref stepSpeed);
				if (!BaseAI.HitTileOnSide(codable, 3, true))
				{
					codable.velocity.Y = codable.velocity.Y + 0.35f;
					return;
				}
				if ((codable.velocity.X < 0f && num7 == -1) || (codable.velocity.X > 0f && num7 == 1))
				{
					bool ignoreTiles = entity != null && !flag2 && num5 < 50f && num6 > (float)(codable.height + codable.height / 2) && num6 < 16f * (float)(jumpDistY + 1) && BaseUtility.CanHit(codable.Hitbox, entity.Hitbox);
					Vector2 vector2 = BaseAI.AttemptJump(codable.position, codable.velocity, codable.width, codable.height, num7, (float)num8, jumpDistX, jumpDistY, maxSpeed, true, entity, ignoreTiles);
					if (tileCollide)
					{
						vector2 = Collision.TileCollision(codable.position, vector2, codable.width, codable.height, false, false, 1);
						Vector4 vector3 = Collision.SlopeCollision(codable.position, vector2, codable.width, codable.height, 0f, false);
						codable.position = new Vector2(vector3.X, vector3.Y);
						codable.velocity = new Vector2(vector3.Z, vector3.W);
					}
					if (codable.velocity != vector2)
					{
						codable.velocity = vector2;
						netUpdate = true;
						return;
					}
				}
			}
			else
			{
				tileCollide = false;
				Vector2 endPos = owner.Center;
				if (owner.velocity.Y != 0f && num < 80f)
				{
					endPos = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 vector4 = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, endPos));
				if (owner.velocity.Y != 0f && ((vector4.X > 0f && codable.velocity.X < 0f) || (vector4.X < 0f && codable.velocity.X > 0f)))
				{
					codable.velocity *= 0.98f;
					vector4 *= 0.02f;
					codable.velocity += vector4;
				}
				else
				{
					codable.velocity = vector4;
				}
				codable.position += owner.velocity;
			}
		}

		public static void AIMinionSlime(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = -1f, float jumpVelY = 20f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (jumpVelX == -1f)
			{
				jumpVelX = 2f + Main.player[projectile.owner].velocity.X;
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(jumpVelX, jumpVelY);
			}
			projectile.timeLeft = 10;
			BaseAI.AIMinionSlime(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, lineDist, returnDist, teleportDist, jumpVelX, jumpVelY, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionSlime(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = 2f, float jumpVelY = 20f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float num = Vector2.Distance(codable.Center, owner.Center);
			if (num > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int num2 = (int)(codable.Center.X / 16f);
			int num3 = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[num2, num3];
			bool flag = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float num4 = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (owner.velocity.Y != 0f || num > Math.Max((float)lineDist, (float)returnDist / 10f))) || num > (float)returnDist || flag) ? 1 : 0);
			if (ai[0] != num4)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f)
			{
				tileCollide = true;
				Entity entity = (GetTarget == null) ? null : GetTarget(codable, owner);
				Vector2 vector = (entity == null) ? default(Vector2) : entity.Center;
				bool flag2 = entity == null || vector == owner.Center;
				if (vector == default(Vector2))
				{
					vector = owner.Center;
					vector.X += (float)((lineDist + lineDist * minionPos) * -(float)owner.direction);
				}
				float num5 = Math.Abs(codable.Center.X - vector.X);
				Math.Abs(codable.Center.Y - vector.Y);
				int num6 = (vector.X > codable.Center.X) ? 1 : -1;
				float y = codable.Center.Y;
				if (flag2 && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && num5 < 8f)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.8f : 0f);
				}
				else if (codable.velocity.Y == 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.8f;
					if (codable.velocity.X > -0.1f && codable.velocity.X < 0.1f)
					{
						codable.velocity.X = 0f;
					}
					codable.velocity.Y = -jumpVelY;
					codable.velocity.X = codable.velocity.X + jumpVelX * (float)num6;
					codable.position += codable.velocity;
				}
				if (!BaseAI.HitTileOnSide(codable, 3, true))
				{
					codable.velocity.Y = codable.velocity.Y + 0.35f;
					return;
				}
				if ((codable.velocity.X < 0f && num6 == -1) || (codable.velocity.X > 0f && num6 == 1))
				{
					Vector2 vector2 = codable.velocity;
					if (tileCollide)
					{
						vector2 = Collision.TileCollision(codable.position, vector2, codable.width, codable.height, false, false, 1);
						Vector4 vector3 = Collision.SlopeCollision(codable.position, vector2, codable.width, codable.height, 0f, false);
						codable.position = new Vector2(vector3.X, vector3.Y);
						codable.velocity = new Vector2(vector3.Z, vector3.W);
					}
					if (codable.velocity != vector2)
					{
						codable.velocity = vector2;
						netUpdate = true;
						return;
					}
				}
			}
			else
			{
				tileCollide = false;
				Vector2 endPos = owner.Center;
				if (owner.velocity.Y != 0f && num < 80f)
				{
					endPos = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 vector4 = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, endPos));
				if (owner.velocity.Y != 0f && ((vector4.X > 0f && codable.velocity.X < 0f) || (vector4.X < 0f && codable.velocity.X > 0f)))
				{
					codable.velocity *= 0.98f;
					vector4 *= 0.02f;
					codable.velocity += vector4;
				}
				else
				{
					codable.velocity = vector4;
				}
				codable.position += owner.velocity;
			}
		}

		public static void AIRotate(Entity codable, ref float rotation, ref float moveRot, Vector2 rotateCenter, bool absolute = false, float rotDistance = 50f, float rotThreshold = 20f, float rotAmount = 0.024f, bool moveTowards = true)
		{
			if (absolute)
			{
				moveRot += rotAmount;
				Vector2 center = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
				codable.Center = center;
				center.Normalize();
				rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
				codable.velocity *= 0f;
				return;
			}
			float num = Vector2.Distance(codable.Center, rotateCenter);
			if (num < rotDistance)
			{
				if (rotDistance - num > rotThreshold)
				{
					moveRot += rotAmount;
					Vector2 endPos = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
					float rot = BaseUtility.RotationTo(codable.Center, endPos);
					codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot);
					rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
					return;
				}
				moveRot += rotAmount;
				Vector2 endPos2 = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
				float rot2 = BaseUtility.RotationTo(codable.Center, endPos2);
				codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot2);
				rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
				return;
			}
			else
			{
				if (moveTowards)
				{
					codable.velocity = BaseAI.AIVelocityLinear(codable, rotateCenter, rotAmount, rotAmount, true);
					rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
					return;
				}
				codable.velocity *= 0.95f;
				return;
			}
		}

		public static void AIPounce(Entity codable, Player player, float pounceScalar = 3f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50f, float maxDistance = 60f)
		{
			if (player == null || !player.active || player.dead)
			{
				return;
			}
			BaseAI.AIPounce(codable, player.Center, pounceScalar, maxSpeed, yBoost, minDistance, maxDistance);
		}

		public static void AIPounce(Entity codable, Vector2 pounceCenter, float pounceScalar = 3.5f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50f, float maxDistance = 60f)
		{
			int num = (codable is NPC) ? ((NPC)codable).direction : ((codable is Projectile) ? ((Projectile)codable).direction : 0);
			float num2 = Vector2.Distance(codable.Center, pounceCenter);
			if (pounceCenter.Y <= codable.Center.Y && num2 > minDistance && num2 < maxDistance)
			{
				bool flag = pounceCenter.X < codable.Center.X;
				if (codable.velocity.Y == 0f && ((flag && num == -1) || (!flag && num == 1)))
				{
					codable.velocity.X = codable.velocity.X * pounceScalar;
					if (codable.velocity.X > maxSpeed)
					{
						codable.velocity.X = maxSpeed;
					}
					if (codable.velocity.X < -maxSpeed)
					{
						codable.velocity.X = -maxSpeed;
					}
					codable.velocity.Y = yBoost;
					if (codable is NPC)
					{
						((NPC)codable).netUpdate = true;
					}
				}
			}
		}

		public static void AIPath(NPC npc, ref float[] ai, Vector2[] points, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false)
		{
			Vector2 vector;
			vector..ctor(ai[0], ai[1]);
			if (Main.netMode != 1 && vector != default(Vector2) && Vector2.Distance(npc.Center, vector) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.45f))
			{
				ai[0] = 0f;
				ai[1] = 0f;
				vector = default(Vector2);
			}
			if (npc.ai[2] < (float)points.Length)
			{
				if (vector == default(Vector2))
				{
					npc.velocity *= 0.95f;
					if (Main.netMode != 1)
					{
						vector = points[(int)npc.ai[2]];
						ai[0] = vector.X;
						ai[1] = vector.Y;
						ai[2] += 1f;
						npc.netUpdate = true;
						return;
					}
				}
				else
				{
					npc.velocity = BaseAI.AIVelocityLinear(npc, vector, moveInterval, maxSpeed, direct);
				}
			}
		}

		public static void AITackle(NPC npc, ref float[] ai, Vector2 point, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false, int tackleDelay = 50, float drift = 0.95f)
		{
			Vector2 vector;
			vector..ctor(ai[0], ai[1]);
			if (vector != default(Vector2) && Vector2.Distance(npc.Center, vector) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.45f))
			{
				ai[0] = 0f;
				ai[1] = 0f;
				vector = default(Vector2);
			}
			if (vector == default(Vector2))
			{
				npc.velocity *= drift;
				ai[2] -= 1f;
				if (ai[2] <= 0f)
				{
					ai[2] = (float)tackleDelay;
					vector = point;
					ai[0] = vector.X;
					ai[1] = vector.Y;
				}
				if (Main.netMode == 2)
				{
					npc.netUpdate = true;
					return;
				}
			}
			else
			{
				npc.velocity = BaseAI.AIVelocityLinear(npc, vector, moveInterval, maxSpeed, direct);
			}
		}

		public static Random GetSyncedRand(NPC npc)
		{
			return new Random(npc.whoAmI);
		}

		public static void AIGravitate(NPC npc, ref float[] ai, UnifiedRandom rand, Vector2 point, float moveInterval = 0.06f, float maxSpeed = 2f, bool canCrossCenter = true, bool direct = false, int minDistance = 50, int maxDistance = 200)
		{
			Vector2 vector;
			vector..ctor(ai[0], ai[1]);
			bool flag = false;
			if (Main.netMode != 1)
			{
				if (!flag && vector != default(Vector2) && Vector2.Distance(npc.Center, vector) <= Math.Max(12f, (float)(npc.width + npc.height) / 2f * 3f * (moveInterval / 0.06f)))
				{
					ai[2] += 1f;
					if (ai[2] > 100f)
					{
						ai[2] = 0f;
						flag = true;
					}
				}
				if (flag || (vector != default(Vector2) && Vector2.Distance(npc.Center, vector) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.75f)))
				{
					ai[0] = 0f;
					ai[1] = 0f;
					vector = default(Vector2);
				}
			}
			if (vector == default(Vector2))
			{
				if (npc.velocity.X > 0.3f || npc.velocity.Y > 0.3f)
				{
					npc.velocity.X = npc.velocity.X * 0.95f;
				}
				if (canCrossCenter)
				{
					vector = BaseUtility.GetRandomPosNear(point, rand, minDistance, maxDistance, false);
				}
				else
				{
					int num = maxDistance - minDistance;
					Vector2 vector2;
					vector2..ctor(point.X - (float)(minDistance + rand.Next(num)), point.Y - (float)(minDistance + rand.Next(num)));
					Vector2 vector3;
					vector3..ctor(point.X + (float)(minDistance + rand.Next(num)), vector2.Y);
					Vector2 vector4;
					vector4..ctor(vector2.X, point.Y + (float)(minDistance + rand.Next(num)));
					Vector2 vector5;
					vector5..ctor(vector3.X, vector4.Y);
					float num2 = 9999999f;
					Vector2 vector6 = default(Vector2);
					for (int i = 0; i < 4; i++)
					{
						Vector2 vector7 = (i == 0) ? vector2 : ((i == 1) ? vector3 : ((i == 2) ? vector4 : vector5));
						if (Vector2.Distance(npc.Center, vector7) < num2)
						{
							num2 = Vector2.Distance(npc.Center, vector7);
							vector6 = vector7;
						}
					}
					if (vector6 == vector2 || vector6 == vector5)
					{
						vector = ((rand.Next(2) == 0) ? vector3 : vector4);
					}
					else if (vector6 == vector3 || vector6 == vector4)
					{
						vector = ((rand.Next(2) == 0) ? vector2 : vector5);
					}
				}
				ai[0] = vector.X;
				ai[1] = vector.Y;
				if (Main.netMode == 2)
				{
					npc.netUpdate = true;
					return;
				}
			}
			else if (vector != default(Vector2))
			{
				npc.velocity = BaseAI.AIVelocityLinear(npc, vector, moveInterval, maxSpeed, direct);
			}
		}

		public static Vector2 AIVelocityLinear(Entity codable, Vector2 destVec, float moveInterval, float maxSpeed, bool direct = false)
		{
			Vector2 vector = codable.velocity;
			bool flag = (codable is NPC) ? (!((NPC)codable).noTileCollide) : (codable is Projectile && ((Projectile)codable).tileCollide);
			if (direct)
			{
				Vector2 vector2 = BaseUtility.RotateVector(codable.Center, codable.Center + new Vector2(maxSpeed, 0f), BaseUtility.RotationTo(codable.Center, destVec));
				vector = vector2 - codable.Center;
			}
			else
			{
				if (codable.Center.X > destVec.X)
				{
					vector.X = Math.Max(-maxSpeed, vector.X - moveInterval);
				}
				else if (codable.Center.X < destVec.X)
				{
					vector.X = Math.Min(maxSpeed, vector.X + moveInterval);
				}
				if (codable.Center.Y > destVec.Y)
				{
					vector.Y = Math.Max(-maxSpeed, vector.Y - moveInterval);
				}
				else if (codable.Center.Y < destVec.Y)
				{
					vector.Y = Math.Min(maxSpeed, vector.Y + moveInterval);
				}
			}
			if (flag)
			{
				vector = Collision.TileCollision(codable.position, vector, codable.width, codable.height, false, false, 1);
			}
			return vector;
		}

		public static void AIProjWorm(Projectile p, ref float[] ai, int[] wormTypes, int wormLength, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
			int[] array = new int[(wormTypes.Length == 1) ? 1 : wormLength];
			array[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				array[array.Length - 1] = wormTypes[2];
				for (int i = 1; i < array.Length - 1; i++)
				{
					array[i] = wormTypes[1];
				}
			}
			int num = -1;
			BaseAI.AIProjWorm(p, ref ai, ref num, array, velScalar, velScalarIdle, velocityMax, velocityMaxIdle);
		}

		public static void AIProjWorm(Projectile p, ref float[] ai, ref int npcTargetToAttack, int[] wormTypes, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
			Player player = Main.player[p.owner];
			if ((int)Main.time % 120 == 0)
			{
				p.netUpdate = true;
			}
			if (!player.active)
			{
				p.active = false;
				return;
			}
			bool flag = p.type == wormTypes[0];
			bool flag2 = BaseUtility.InArray(wormTypes, p.type);
			bool flag3 = p.type == wormTypes[wormTypes.Length - 1];
			int num = 10;
			if (flag2)
			{
				p.timeLeft = 2;
				num = 30;
			}
			if (flag)
			{
				Vector2 center = player.Center;
				float num2 = 700f;
				float num3 = 1000f;
				float num4 = 2000f;
				int num5 = -1;
				if (p.Distance(center) > num4)
				{
					p.Center = center;
					p.netUpdate = true;
				}
				bool flag4 = true;
				if (flag4)
				{
					NPC ownerMinionAttackTargetNPC = p.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(p, false))
					{
						float num6 = p.Distance(ownerMinionAttackTargetNPC.Center);
						if (num6 < num2 * 2f)
						{
							num5 = ownerMinionAttackTargetNPC.whoAmI;
						}
					}
					if (num5 < 0)
					{
						int num8;
						for (int i = 0; i < 200; i = num8 + 1)
						{
							NPC npc = Main.npc[i];
							if (npc.CanBeChasedBy(p, false) && player.Distance(npc.Center) < num3)
							{
								float num7 = p.Distance(npc.Center);
								if (num7 < num2)
								{
									num5 = i;
									bool boss = npc.boss;
								}
							}
							num8 = i;
						}
					}
				}
				npcTargetToAttack = num5;
				if (num5 != -1)
				{
					NPC npc2 = Main.npc[num5];
					Vector2 vector = npc2.Center - p.Center;
					Utils.ToDirectionInt(vector.X > 0f);
					Utils.ToDirectionInt(vector.Y > 0f);
					float num9 = 0.4f;
					if (vector.Length() < 600f)
					{
						num9 = 0.6f;
					}
					if (vector.Length() < 300f)
					{
						num9 = 0.8f;
					}
					num9 *= velScalar;
					if (vector.Length() > npc2.Size.Length() * 0.75f)
					{
						p.velocity += Vector2.Normalize(vector) * num9 * 1.5f;
						if (Vector2.Dot(p.velocity, vector) < 0.25f)
						{
							p.velocity *= 0.8f;
						}
					}
					if (p.velocity.Length() > velocityMax)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMax;
					}
				}
				else
				{
					float num10 = 0.2f;
					Vector2 vector2 = center - p.Center;
					if (vector2.Length() < 200f)
					{
						num10 = 0.12f;
					}
					if (vector2.Length() < 140f)
					{
						num10 = 0.06f;
					}
					num10 *= velScalarIdle;
					if (vector2.Length() > 100f)
					{
						if (Math.Abs(center.X - p.Center.X) > 20f)
						{
							p.velocity.X = p.velocity.X + num10 * (float)Math.Sign(center.X - p.Center.X);
						}
						if (Math.Abs(center.Y - p.Center.Y) > 10f)
						{
							p.velocity.Y = p.velocity.Y + num10 * (float)Math.Sign(center.Y - p.Center.Y);
						}
					}
					else if (p.velocity.Length() > 2f)
					{
						p.velocity *= 0.96f;
					}
					if (Math.Abs(p.velocity.Y) < 1f)
					{
						p.velocity.Y = p.velocity.Y - 0.1f;
					}
					if (p.velocity.Length() > velocityMaxIdle)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMaxIdle;
					}
				}
				p.rotation = Utils.ToRotation(p.velocity) + 1.5707964f;
				int direction = p.direction;
				p.direction = (p.spriteDirection = ((p.velocity.X > 0f) ? 1 : -1));
				if (direction != p.direction)
				{
					p.netUpdate = true;
				}
				float num11 = MathHelper.Clamp(p.localAI[0], 0f, 50f);
				p.position = p.Center;
				p.scale = 1f + num11 * 0.01f;
				p.width = (p.height = (int)((float)num * p.scale));
				p.Center = p.position;
				if (p.alpha > 0)
				{
					p.alpha -= 42;
					if (p.alpha < 0)
					{
						p.alpha = 0;
						return;
					}
				}
			}
			else
			{
				bool flag5 = false;
				Vector2 vector3 = Vector2.Zero;
				float num12 = 0f;
				float num13 = 0f;
				float num14 = 1f;
				if (p.ai[1] == 1f)
				{
					p.ai[1] = 0f;
					p.netUpdate = true;
				}
				int byUUID = Projectile.GetByUUID(p.owner, (int)p.ai[0]);
				if (flag2 && byUUID >= 0 && Main.projectile[byUUID].active && !flag3)
				{
					flag5 = true;
					vector3 = Main.projectile[byUUID].Center;
					num12 = Main.projectile[byUUID].rotation;
					float num15 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
					num14 = num15;
					num13 = 16f;
					int alpha = Main.projectile[byUUID].alpha;
					Main.projectile[byUUID].localAI[0] = p.localAI[0] + 1f;
					if (Main.projectile[byUUID].type != wormTypes[0])
					{
						Main.projectile[byUUID].localAI[1] = (float)p.whoAmI;
					}
					if (p.owner == Main.myPlayer && Main.projectile[byUUID].type == wormTypes[0] && p.type == wormTypes[wormTypes.Length - 1])
					{
						Main.projectile[byUUID].Kill();
						p.Kill();
						return;
					}
				}
				if (!flag5)
				{
					return;
				}
				p.alpha -= 42;
				if (p.alpha < 0)
				{
					p.alpha = 0;
				}
				p.velocity = Vector2.Zero;
				Vector2 vector4 = vector3 - p.Center;
				if (num12 != p.rotation)
				{
					float num16 = MathHelper.WrapAngle(num12 - p.rotation);
					vector4 = Utils.RotatedBy(vector4, (double)(num16 * 0.1f), default(Vector2));
				}
				p.rotation = Utils.ToRotation(vector4) + 1.5707964f;
				p.position = p.Center;
				p.scale = num14;
				p.width = (p.height = (int)((float)num * p.scale));
				p.Center = p.position;
				if (vector4 != Vector2.Zero)
				{
					p.Center = vector3 - Vector2.Normalize(vector4) * num13 * num14;
				}
				p.spriteDirection = ((vector4.X > 0f) ? 1 : -1);
			}
		}

		public static void AIProjSpaceOctopus(Projectile p, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, Action<int, Projectile> SpawnDust = null)
		{
			BaseAI.AIProjSpaceOctopus(p, ref ai, parentNPCType, fireProjType, shootVelocity, hoverTime, xMult, yMult, -1, true, false, SpawnDust);
		}

		public static void AIProjSpaceOctopus(Projectile projectile, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, int fireDmg = -1, bool useParentTarget = true, bool noParentHover = false, Action<int, Projectile> SpawnDust = null)
		{
			if (fireDmg == -1)
			{
				fireDmg = projectile.damage;
			}
			ai[0] += 1f;
			if (ai[0] < hoverTime)
			{
				bool flag = true;
				int num = (int)ai[1];
				if (Main.npc[num].active && Main.npc[num].type == parentNPCType)
				{
					if (!noParentHover && Main.npc[num].oldPos[1] != Vector2.Zero)
					{
						projectile.position += Main.npc[num].position - Main.npc[num].oldPos[1];
					}
				}
				else
				{
					ai[0] = hoverTime;
					flag = false;
				}
				if (flag && !noParentHover)
				{
					projectile.velocity += new Vector2((float)Math.Sign(Main.npc[num].Center.X - projectile.Center.X), (float)Math.Sign(Main.npc[num].Center.Y - projectile.Center.Y)) * new Vector2(xMult, yMult);
					if (projectile.velocity.Length() > 6f)
					{
						projectile.velocity *= 6f / projectile.velocity.Length();
					}
				}
				if (SpawnDust != null)
				{
					SpawnDust(0, projectile);
				}
				projectile.rotation = projectile.velocity.X * 0.1f;
			}
			if (ai[0] == hoverTime)
			{
				bool flag2 = true;
				int num2 = -1;
				if (!useParentTarget)
				{
					int num3 = (int)ai[1];
					if (Main.npc[num3].active && Main.npc[num3].type == parentNPCType)
					{
						num2 = Main.npc[num3].target;
					}
					else
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = false;
				}
				if (!flag2)
				{
					num2 = (int)Player.FindClosest(projectile.position, projectile.width, projectile.height);
				}
				Vector2 vector = Main.player[num2].Center - projectile.Center;
				vector.X += (float)Main.rand.Next(-50, 51);
				vector.Y += (float)Main.rand.Next(-50, 51);
				vector.X *= (float)Main.rand.Next(80, 121) * 0.01f;
				vector.Y *= (float)Main.rand.Next(80, 121) * 0.01f;
				Vector2 vector2 = Vector2.Normalize(vector);
				if (Utils.HasNaNs(vector2))
				{
					vector2 = Vector2.UnitY;
				}
				if (fireProjType == -1)
				{
					projectile.velocity = vector2 * shootVelocity;
					projectile.netUpdate = true;
				}
				else
				{
					if (Main.netMode != 1 && Collision.CanHitLine(projectile.Center, 0, 0, Main.player[num2].Center, 0, 0))
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X * shootVelocity, vector2.Y * shootVelocity, fireProjType, fireDmg, 1f, Main.myPlayer, 0f, 0f);
					}
					ai[0] = 0f;
				}
			}
			if (ai[0] >= hoverTime)
			{
				projectile.rotation = Utils.AngleLerp(projectile.rotation, Utils.ToRotation(projectile.velocity) + 1.5707964f, 0.4f);
				if (SpawnDust != null)
				{
					SpawnDust(1, projectile);
				}
			}
		}

		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, float yoyoTimeMax = -1f, float maxRange = -1f, float topSpeed = -1f, bool dontChannel = false, float rotAmount = 0.45f)
		{
			if (yoyoTimeMax == -1f)
			{
				yoyoTimeMax = ProjectileID.Sets.YoyosLifeTimeMultiplier[p.type];
			}
			if (maxRange == -1f)
			{
				maxRange = ProjectileID.Sets.YoyosMaximumRange[p.type];
			}
			if (topSpeed == -1f)
			{
				topSpeed = ProjectileID.Sets.YoyosTopSpeed[p.type];
			}
			BaseAI.AIYoyo(p, ref ai, ref localAI, Main.player[p.owner], Main.player[p.owner].channel, default(Vector2), yoyoTimeMax, maxRange, topSpeed, dontChannel, rotAmount);
		}

		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, bool isChanneling, Vector2 targetPos = default(Vector2), float yoyoTimeMax = 120f, float maxRange = 150f, float topSpeed = 8f, bool dontChannel = false, float rotAmount = 0.45f)
		{
			bool flag = owner is Player;
			Player player = flag ? ((Player)owner) : null;
			float num = flag ? player.meleeSpeed : 1f;
			Vector2 vector = targetPos;
			if (flag && Main.myPlayer == p.owner && targetPos == default(Vector2))
			{
				vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
			}
			bool flag2 = false;
			if (owner is Player)
			{
				for (int i = 0; i < p.whoAmI; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == p.owner && Main.projectile[i].type == p.type)
					{
						flag2 = true;
					}
				}
			}
			if ((flag && p.owner == Main.myPlayer) || (!flag && Main.netMode != 1))
			{
				localAI[0] += 1f;
				if (flag2)
				{
					localAI[0] += (float)Main.rand.Next(10, 31) * 0.1f;
				}
				float num2 = localAI[0] / 60f;
				num2 /= (1f + num) / 2f;
				if (yoyoTimeMax != -1f && num2 > yoyoTimeMax)
				{
					ai[0] = -1f;
				}
			}
			if ((flag && player.dead) || (!flag && !owner.active))
			{
				p.Kill();
				return;
			}
			if (flag && !dontChannel && !flag2)
			{
				player.heldProj = p.whoAmI;
				player.itemAnimation = 2;
				player.itemTime = 2;
				if (p.position.X + (float)(p.width / 2) > player.position.X + (float)(player.width / 2))
				{
					player.ChangeDir(1);
					p.direction = 1;
				}
				else
				{
					player.ChangeDir(-1);
					p.direction = -1;
				}
			}
			if (Utils.HasNaNs(p.velocity))
			{
				p.Kill();
			}
			p.timeLeft = 6;
			float num3 = maxRange;
			if (flag && player.yoyoString)
			{
				num3 = num3 * 1.25f + 30f;
			}
			num3 /= (1f + num * 3f) / 4f;
			float num4 = topSpeed / ((1f + num * 3f) / 4f);
			float num5 = 14f - num4 / 2f;
			float num6 = 5f + num4 / 2f;
			if (flag2)
			{
				num6 += 20f;
			}
			if (ai[0] >= 0f)
			{
				if (p.velocity.Length() > num4)
				{
					p.velocity *= 0.98f;
				}
				bool flag3 = false;
				bool flag4 = false;
				Vector2 vector2 = owner.Center - p.Center;
				if (vector2.Length() > num3)
				{
					flag3 = true;
					if ((double)vector2.Length() > (double)num3 * 1.3)
					{
						flag4 = true;
					}
				}
				if ((flag && p.owner == Main.myPlayer) || (!flag && Main.netMode != 1))
				{
					if ((flag && (!isChanneling || player.stoned || player.frozen)) || (!flag && !isChanneling))
					{
						ai[0] = -1f;
						ai[1] = 0f;
						p.netUpdate = true;
					}
					else
					{
						Vector2 vector3 = vector;
						float x = vector3.X;
						float y = vector3.Y;
						Vector2 vector4 = new Vector2(x, y) - owner.Center;
						if (vector4.Length() > num3)
						{
							vector4.Normalize();
							vector4 *= num3;
							vector4 = owner.Center + vector4;
							x = vector4.X;
							y = vector4.Y;
						}
						if (ai[0] != x || ai[1] != y)
						{
							Vector2 vector5;
							vector5..ctor(x, y);
							Vector2 vector6 = vector5 - owner.Center;
							if (vector6.Length() > num3 - 1f)
							{
								vector6.Normalize();
								vector6 *= num3 - 1f;
								vector5 = owner.Center + vector6;
								x = vector5.X;
								y = vector5.Y;
							}
							ai[0] = x;
							ai[1] = y;
							p.netUpdate = true;
						}
					}
				}
				if (flag4 && p.owner == Main.myPlayer)
				{
					ai[0] = -1f;
					p.netUpdate = true;
				}
				if (ai[0] >= 0f)
				{
					if (flag3)
					{
						num5 /= 2f;
						num4 *= 2f;
						if (p.Center.X > owner.Center.X && p.velocity.X > 0f)
						{
							p.velocity.X = p.velocity.X * 0.5f;
						}
						if (p.Center.Y > owner.Center.Y && p.velocity.Y > 0f)
						{
							p.velocity.Y = p.velocity.Y * 0.5f;
						}
						if (p.Center.X < owner.Center.X && p.velocity.X > 0f)
						{
							p.velocity.X = p.velocity.X * 0.5f;
						}
						if (p.Center.Y < owner.Center.Y && p.velocity.Y > 0f)
						{
							p.velocity.Y = p.velocity.Y * 0.5f;
						}
					}
					Vector2 vector7;
					vector7..ctor(ai[0], ai[1]);
					Vector2 vector8 = vector7 - p.Center;
					p.velocity.Length();
					float num7 = vector8.Length();
					if (num7 > num6)
					{
						vector8.Normalize();
						float num8 = (num7 > num4 * 2f) ? num4 : (num7 / 2f);
						vector8 *= num8;
						p.velocity = (p.velocity * (num5 - 1f) + vector8) / num5;
					}
					else if (flag2)
					{
						if ((double)p.velocity.Length() < (double)num4 * 0.6)
						{
							vector8 = p.velocity;
							vector8.Normalize();
							vector8 *= num4 * 0.6f;
							p.velocity = (p.velocity * (num5 - 1f) + vector8) / num5;
						}
					}
					else
					{
						p.velocity *= 0.8f;
					}
					if (flag2 && !flag3 && (double)p.velocity.Length() < (double)num4 * 0.6)
					{
						p.velocity.Normalize();
						p.velocity *= num4 * 0.6f;
					}
				}
			}
			else
			{
				num5 = (float)((int)((double)num5 * 0.8));
				num4 *= 1.5f;
				p.tileCollide = false;
				Vector2 vector9 = owner.position - p.Center;
				float num9 = vector9.Length();
				if (num9 < num4 + 10f || num9 == 0f)
				{
					p.Kill();
				}
				else
				{
					vector9.Normalize();
					vector9 *= num4;
					p.velocity = (p.velocity * (num5 - 1f) + vector9) / num5;
				}
			}
			p.rotation += rotAmount;
		}

		public static void TileCollideYoyo(Projectile p, ref Vector2 velocity, Vector2 newVelocity)
		{
			bool flag = false;
			if (velocity.X != newVelocity.X)
			{
				flag = true;
				velocity.X = newVelocity.X * -1f;
			}
			if (velocity.Y != newVelocity.Y)
			{
				flag = true;
				velocity.Y = newVelocity.Y * -1f;
			}
			if (flag)
			{
				Vector2 vector = Main.player[p.owner].Center - p.Center;
				vector.Normalize();
				vector *= velocity.Length();
				vector *= 0.25f;
				velocity *= 0.75f;
				velocity += vector;
				if (velocity.Length() > 6f)
				{
					velocity *= 0.5f;
				}
			}
		}

		public static void EntityCollideYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, Entity target, bool spawnCounterweight = true, float velMult = 1f)
		{
			if (owner is Player && spawnCounterweight)
			{
				((Player)owner).Counterweight(target.Center, p.damage, p.knockBack);
			}
			if (target.Center.X < owner.Center.X)
			{
				p.direction = -1;
			}
			else
			{
				p.direction = 1;
			}
			if (ai[0] >= 0f)
			{
				Vector2 vector = p.Center - target.Center;
				vector.Normalize();
				float num = 16f;
				p.velocity *= -0.5f;
				p.velocity += vector * num;
				p.velocity *= velMult;
				p.netUpdate = true;
				localAI[0] += 20f;
				if (!Collision.CanHit(p.position, p.width, p.height, owner.position, owner.width, owner.height))
				{
					localAI[0] += 40f;
				}
			}
		}

		public static void AIStar(Projectile p, ref float[] ai, float landingHorizon = -1f, bool fadein = true)
		{
			if (landingHorizon != -1f)
			{
				if (p.position.Y > landingHorizon)
				{
					p.tileCollide = true;
				}
			}
			else
			{
				if (ai[0] == 0f && !Collision.SolidCollision(p.position, p.width, p.height))
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}
				if (ai[0] != 0f)
				{
					p.tileCollide = true;
				}
			}
			if (fadein)
			{
				p.alpha = Math.Max(0, p.alpha - 25);
			}
		}

		public static void AIExplosive(Projectile p, ref float[] ai, bool rocket = false, bool rotate = true, int beginGravity = 10, float slowdownX = 0.97f, float gravity = 0.2f)
		{
			if (rocket && Math.Abs(p.velocity.X) < 15f && Math.Abs(p.velocity.Y) < 15f)
			{
				p.velocity *= 1.1f;
			}
			ai[0] += 1f;
			if (rocket)
			{
				if (p.velocity.X < 0f)
				{
					p.spriteDirection = -1;
					p.rotation = (float)Math.Atan2(-(double)p.velocity.Y, -(double)p.velocity.X) - 1.57f;
				}
				else
				{
					p.spriteDirection = 1;
					p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
				}
			}
			else if (ai[0] > (float)beginGravity)
			{
				ai[0] = (float)beginGravity;
				if (p.velocity.Y == 0f && p.velocity.X != 0f)
				{
					p.velocity.X = p.velocity.X * slowdownX;
					if (p.velocity.X > -0.01f && p.velocity.X < 0.01f)
					{
						p.velocity.X = 0f;
						p.netUpdate = true;
					}
				}
				p.velocity.Y = p.velocity.Y + gravity;
			}
			if (rotate)
			{
				p.rotation += p.velocity.X * 0.1f;
			}
		}

		public static void TileCollideExplosive(Projectile p, ref Vector2 velocity, bool bomb = false)
		{
			if (p.velocity.X != velocity.X)
			{
				p.velocity.X = velocity.X * -0.4f;
			}
			if (p.velocity.Y != velocity.Y && velocity.Y > 0.7f && !bomb)
			{
				p.velocity.Y = velocity.Y * -0.4f;
			}
		}

		public static void AIArrow(Entity codable, ref float[] ai, int gravApplyInterval = 50, float gravity = 0.1f, float maxSpeedY = 16f)
		{
			ai[0] += 1f;
			if (ai[0] >= (float)gravApplyInterval)
			{
				codable.velocity.Y = codable.velocity.Y + gravity;
			}
			if (codable.velocity.Y > maxSpeedY)
			{
				codable.velocity.Y = maxSpeedY;
			}
		}

		public static void AIDemonScythe(Entity codable, ref float[] ai, int startSpeedupInterval = 30, int stopSpeedupInterval = 100, float rotateScalar = 0.8f, float speedupScalar = 1.06f, float maxSpeed = 8f)
		{
			if (codable is Projectile)
			{
				((Projectile)codable).rotation += (float)codable.direction * rotateScalar;
			}
			if (codable is NPC)
			{
				((NPC)codable).rotation += (float)codable.direction * rotateScalar;
			}
			ai[0] += 1f;
			if (ai[0] >= (float)startSpeedupInterval)
			{
				if (ai[0] < (float)stopSpeedupInterval)
				{
					codable.velocity *= speedupScalar;
				}
				else
				{
					ai[0] = (float)stopSpeedupInterval;
				}
			}
			if ((Math.Abs(codable.velocity.X) + Math.Abs(codable.velocity.Y)) * 0.5f > maxSpeed)
			{
				codable.velocity.Normalize();
				codable.velocity *= maxSpeed;
			}
		}

		public static void AIVilethorn(Projectile p, int alphaInterval = 50, int alphaReduction = 4, int length = 8)
		{
			if (p.ai[0] == 0f)
			{
				p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
				p.alpha -= alphaInterval;
				if (p.alpha <= 0)
				{
					p.alpha = 0;
					p.ai[0] = 1f;
					if (p.ai[1] == 0f)
					{
						p.ai[1] += 1f;
						p.position += p.velocity;
					}
					if (p.ai[1] < (float)length && Main.myPlayer == p.owner)
					{
						Vector2 velocity = p.velocity;
						int num = Projectile.NewProjectile(p.Center.X + p.velocity.X, p.Center.Y + p.velocity.Y, velocity.X, velocity.Y, p.type, p.damage, p.knockBack, p.owner, 0f, 0f);
						Main.projectile[num].damage = p.damage;
						Main.projectile[num].ai[1] = p.ai[1] + 1f;
						NetMessage.SendData(27, -1, -1, NetworkText.FromLiteral(""), num, 0f, 0f, 0f, 0, 0, 0);
						p.position -= p.velocity;
						return;
					}
				}
			}
			else
			{
				p.alpha += alphaReduction;
				if (p.alpha >= 255)
				{
					p.Kill();
					return;
				}
			}
			p.position -= p.velocity;
		}

		public static void AIStream(Projectile p, float scaleReduce = 0.04f, float gravity = 0.075f, bool goldenShower = false, int start = 3, Func<Projectile, Vector2, int, int, int> SpawnDust = null)
		{
			if (goldenShower)
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f)
				{
					p.Kill();
				}
				if (p.ai[0] <= (float)start)
				{
					p.ai[0] += 1f;
					return;
				}
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null)
				{
					for (int i = 0; i < 3; i++)
					{
						float num = p.velocity.X / 3f * (float)i;
						float num2 = p.velocity.Y / 3f * (float)i;
						int num3 = 1;
						Vector2 arg;
						arg..ctor(p.position.X - (float)num3, p.position.Y - (float)num3);
						int arg2 = p.width + num3 * 2;
						int arg3 = p.height + num3 * 2;
						int num4 = SpawnDust(p, arg, arg2, arg3);
						if (num4 != -1)
						{
							Main.dust[num4].noGravity = true;
							Main.dust[num4].velocity *= 0.1f;
							Main.dust[num4].velocity += p.velocity * 0.5f;
							Dust dust = Main.dust[num4];
							dust.position.X = dust.position.X - num;
							Dust dust2 = Main.dust[num4];
							dust2.position.Y = dust2.position.Y - num2;
						}
					}
					if (Main.rand.Next(8) == 0)
					{
						int num5 = 1;
						Vector2 arg4;
						arg4..ctor(p.position.X - (float)num5, p.position.Y - (float)num5);
						int arg5 = p.width + num5 * 2;
						int arg6 = p.height + num5 * 2;
						int num6 = SpawnDust(p, arg4, arg5, arg6);
						if (num6 != -1)
						{
							Main.dust[num6].velocity *= 0.25f;
							Main.dust[num6].velocity += p.velocity * 0.5f;
							return;
						}
					}
				}
			}
			else
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f)
				{
					p.Kill();
				}
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null)
				{
					SpawnDust(p, p.position, p.width, p.height);
				}
			}
		}

		public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
		{
			p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * (float)p.direction;
			ai[0] += 1f;
			if (ai[0] >= (float)timeUntilDrop)
			{
				p.velocity.Y = p.velocity.Y + yIncrement;
				p.velocity.X = p.velocity.X * xScalar;
			}
			else if (!spin)
			{
				p.rotation = BaseUtility.RotationTo(p.Center, p.Center + p.velocity) + 1.57f;
			}
			if (p.velocity.Y > maxSpeedY)
			{
				p.velocity.Y = maxSpeedY;
			}
		}

		public static void AISpear(Projectile p, ref float[] ai, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false)
		{
			Player player = Main.player[p.owner];
			Item item = player.inventory[player.selectedItem];
			if (Main.myPlayer == p.owner && item != null && item.autoReuse && player.itemAnimation == 1)
			{
				p.Kill();
				return;
			}
			Main.player[p.owner].heldProj = p.whoAmI;
			Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 vector;
			vector..ctor(0f, player.gfxOffY);
			BaseAI.AISpear(p, ref ai, player.Center + vector, player.direction, player.itemAnimation, player.itemAnimationMax, initialSpeed, moveOutward, moveInward, overrideKill, player.frozen);
		}

		public static void AISpear(Projectile p, ref float[] ai, Vector2 center, int ownerDirection, int itemAnimation, int itemAnimationMax, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false, bool frozen = false)
		{
			p.direction = ownerDirection;
			p.position.X = center.X - (float)p.width * 0.5f;
			p.position.Y = center.Y - (float)p.height * 0.5f;
			if (ai[0] == 0f)
			{
				ai[0] = initialSpeed;
				p.netUpdate = true;
			}
			if (!frozen)
			{
				if ((float)itemAnimation < (float)itemAnimationMax * 0.33f)
				{
					ai[0] -= moveInward;
				}
				else
				{
					ai[0] += moveOutward;
				}
			}
			p.position += p.velocity * ai[0];
			if (!overrideKill && Main.player[p.owner].itemAnimation == 0)
			{
				p.Kill();
			}
			p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 2.355f;
			if (p.direction == -1)
			{
				p.rotation -= 0f;
				return;
			}
			if (p.direction == 1)
			{
				p.rotation -= 1.57f;
			}
		}

		public static void AIBoomerang(Projectile p, ref float[] ai, Vector2 position = default(Vector2), int width = -1, int height = -1, bool playSound = true, float maxDistance = 9f, int returnDelay = 35, float speedInterval = 0.4f, float rotationInterval = 0.4f, bool direct = false)
		{
			if (position == default(Vector2))
			{
				position = Main.player[p.owner].position;
			}
			if (width == -1)
			{
				width = Main.player[p.owner].width;
			}
			if (height == -1)
			{
				height = Main.player[p.owner].height;
			}
			Vector2 endPos = position + new Vector2((float)width * 0.5f, (float)height * 0.5f);
			if (playSound && p.soundDelay == 0)
			{
				p.soundDelay = 8;
				Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 7, 1f, 0f);
			}
			if (ai[0] == 0f)
			{
				ai[1] += 1f;
				if (ai[1] >= (float)returnDelay)
				{
					ai[0] = 1f;
					ai[1] = 0f;
					p.netUpdate = true;
				}
			}
			else
			{
				p.tileCollide = false;
				float num = endPos.X - p.Center.X;
				float num2 = endPos.Y - p.Center.Y;
				float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
				if (num3 > 3000f)
				{
					p.Kill();
				}
				if (direct)
				{
					p.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(speedInterval, 0f), BaseUtility.RotationTo(p.Center, endPos));
				}
				else
				{
					num3 = maxDistance / num3;
					num *= num3;
					num2 *= num3;
					if (p.velocity.X < num)
					{
						p.velocity.X = p.velocity.X + speedInterval;
						if (p.velocity.X < 0f && num > 0f)
						{
							p.velocity.X = p.velocity.X + speedInterval;
						}
					}
					else if (p.velocity.X > num)
					{
						p.velocity.X = p.velocity.X - speedInterval;
						if (p.velocity.X > 0f && num < 0f)
						{
							p.velocity.X = p.velocity.X - speedInterval;
						}
					}
					if (p.velocity.Y < num2)
					{
						p.velocity.Y = p.velocity.Y + speedInterval;
						if (p.velocity.Y < 0f && num2 > 0f)
						{
							p.velocity.Y = p.velocity.Y + speedInterval;
						}
					}
					else if (p.velocity.Y > num2)
					{
						p.velocity.Y = p.velocity.Y - speedInterval;
						if (p.velocity.Y > 0f && num2 < 0f)
						{
							p.velocity.Y = p.velocity.Y - speedInterval;
						}
					}
				}
				if (Main.myPlayer == p.owner)
				{
					Rectangle hitbox = p.Hitbox;
					Rectangle rectangle;
					rectangle..ctor((int)position.X, (int)position.Y, width, height);
					if (hitbox.Intersects(rectangle))
					{
						p.Kill();
					}
				}
			}
			p.rotation += rotationInterval * (float)p.direction;
		}

		public static void TileCollideBoomerang(Projectile p, ref Vector2 velocity, bool bounce = false)
		{
			if (bounce)
			{
				if (p.velocity.X != velocity.X)
				{
					p.velocity.X = -velocity.X;
				}
				if (p.velocity.Y != velocity.Y)
				{
					p.velocity.Y = -velocity.Y;
				}
			}
			else
			{
				p.ai[0] = 1f;
				p.velocity.X = -velocity.X;
				p.velocity.Y = -velocity.Y;
			}
			p.netUpdate = true;
		}

		public static void AIFlail(Projectile p, ref float[] ai, bool noKill = false, float chainDistance = 160f)
		{
			if (Main.player[p.owner] != null)
			{
				if (Main.player[p.owner].dead)
				{
					p.Kill();
					return;
				}
				Main.player[p.owner].itemAnimation = 10;
				Main.player[p.owner].itemTime = 10;
			}
			BaseAI.AIFlail(p, ref ai, Main.player[p.owner].Center, Main.player[p.owner].velocity, Main.player[p.owner].meleeSpeed, Main.player[p.owner].channel, noKill, chainDistance);
			Main.player[p.owner].direction = p.direction;
		}

		public static void AIFlail(Projectile p, ref float[] ai, Vector2 connectedPoint, Vector2 connectedPointVelocity, float meleeSpeed, bool channel, bool noKill = false, float chainDistance = 160f)
		{
			p.direction = ((p.Center.X > connectedPoint.X) ? 1 : -1);
			float num = connectedPoint.X - p.Center.X;
			float num2 = connectedPoint.Y - p.Center.Y;
			float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
			if (ai[0] == 0f)
			{
				p.tileCollide = true;
				if (num3 > chainDistance)
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}
				else if (!channel)
				{
					if (p.velocity.Y < 0f)
					{
						p.velocity.Y = p.velocity.Y * 0.9f;
					}
					p.velocity.Y = p.velocity.Y + 1f;
					p.velocity.X = p.velocity.X * 0.9f;
				}
			}
			else if (ai[0] == 1f)
			{
				float num4 = 14f / meleeSpeed;
				float num5 = 0.9f / meleeSpeed;
				float num6 = chainDistance + 140f;
				Math.Abs(num);
				Math.Abs(num2);
				if (ai[1] == 1f)
				{
					p.tileCollide = false;
				}
				if (!channel || num3 > num6 || !p.tileCollide)
				{
					ai[1] = 1f;
					if (p.tileCollide)
					{
						p.netUpdate = true;
					}
					p.tileCollide = false;
					if (!noKill && num3 < 20f)
					{
						p.Kill();
					}
				}
				if (!p.tileCollide)
				{
					num5 *= 2f;
				}
				if (num3 > 60f || !p.tileCollide)
				{
					num3 = num4 / num3;
					num *= num3;
					num2 *= num3;
					new Vector2(p.velocity.X, p.velocity.Y);
					float num7 = num - p.velocity.X;
					float num8 = num2 - p.velocity.Y;
					float num9 = (float)Math.Sqrt((double)(num7 * num7 + num8 * num8));
					num9 = num5 / num9;
					num7 *= num9;
					num8 *= num9;
					p.velocity.X = p.velocity.X * 0.98f;
					p.velocity.Y = p.velocity.Y * 0.98f;
					p.velocity.X = p.velocity.X + num7;
					p.velocity.Y = p.velocity.Y + num8;
				}
				else
				{
					if (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y) < 6f)
					{
						p.velocity.X = p.velocity.X * 0.96f;
						p.velocity.Y = p.velocity.Y + 0.2f;
					}
					if (connectedPointVelocity.X == 0f)
					{
						p.velocity.X = p.velocity.X * 0.96f;
					}
				}
			}
			p.rotation = (float)Math.Atan2((double)num2, (double)num) - p.velocity.X * 0.1f;
		}

		public static void TileCollideFlail(Projectile p, ref Vector2 velocity, bool playSound = true)
		{
			if (velocity != p.velocity)
			{
				bool flag = false;
				if (velocity.X != p.velocity.X)
				{
					if (Math.Abs(velocity.X) > 4f)
					{
						flag = true;
					}
					p.position.X = p.position.X + p.velocity.X;
					p.velocity.X = -velocity.X * 0.2f;
				}
				if (velocity.Y != p.velocity.Y)
				{
					if (Math.Abs(velocity.Y) > 4f)
					{
						flag = true;
					}
					p.position.Y = p.position.Y + p.velocity.Y;
					p.velocity.Y = -velocity.Y * 0.2f;
				}
				p.ai[0] = 1f;
				if (flag)
				{
					p.netUpdate = true;
					Collision.HitTiles(p.position, p.velocity, p.width, p.height);
					if (playSound)
					{
						Main.PlaySound(0, (int)p.position.X, (int)p.position.Y, 1, 1f, 0f);
					}
				}
			}
		}

		public static bool StickToTiles(Vector2 position, ref Vector2 velocity, int width, int height, Func<int, int, bool> CanStick = null)
		{
			int num = (int)(position.X / 16f) - 1;
			int num2 = (int)((position.X + (float)width) / 16f) + 2;
			int num3 = (int)(position.Y / 16f) - 1;
			int num4 = (int)((position.Y + (float)height) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			bool flag = false;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].nactive() && ((CanStick != null) ? CanStick(i, j) : (Main.tileSolid[(int)Main.tile[i, j].type] || (Main.tileSolidTop[(int)Main.tile[i, j].type] && Main.tile[i, j].frameY == 0))))
					{
						Vector2 vector;
						vector..ctor((float)(i * 16), (float)(j * 16));
						if (position.X + (float)width - 4f > vector.X && position.X + 4f < vector.X + 16f && position.Y + (float)height - 4f > vector.Y && position.Y + 4f < vector.Y + 16f)
						{
							flag = true;
							velocity *= 0f;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		public static void AISpaceOctopus(NPC npc, ref float[] ai, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			npc.TargetClosest(true);
			BaseAI.AISpaceOctopus(npc, ref ai, Main.player[npc.target].Center, moveSpeed, velMax, hoverDistance, shootProjInterval, FireProj);
		}

		public static void AISpaceOctopus(NPC npc, ref float[] ai, Vector2 targetCenter = default(Vector2), float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			Vector2 vector = targetCenter - npc.Center + new Vector2(0f, -hoverDistance);
			float num = vector.Length();
			if (num < 20f)
			{
				vector = npc.velocity;
			}
			else if (num < 40f)
			{
				vector.Normalize();
				vector *= velMax * 0.35f;
			}
			else if (num < 80f)
			{
				vector.Normalize();
				vector *= velMax * 0.65f;
			}
			else
			{
				vector.Normalize();
				vector *= velMax;
			}
			npc.SimpleFlyMovement(vector, moveSpeed);
			npc.rotation = npc.velocity.X * 0.1f;
			if (FireProj != null && shootProjInterval > -1f && (ai[0] += 1f) >= shootProjInterval)
			{
				ai[0] = 0f;
				if (Main.netMode != 1)
				{
					Vector2 arg = Vector2.Zero;
					while (Math.Abs(arg.X) < 1.5f)
					{
						arg = Utils.RotatedByRandom(Vector2.UnitY, 1.5707963705062866) * new Vector2(5f, 3f);
					}
					FireProj(npc, arg);
				}
			}
		}

		public static void AIElemental(NPC npc, ref float[] ai, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			int num = (int)npc.localAI[0];
			BaseAI.AIElemental(npc, ref ai, ref num, noDamageMode, noDamageTimeMax, gravityChange, tileCollideChange, startPhaseDist, stopPhaseDist, idleTooLong, velSpeed);
			npc.localAI[0] = (float)num;
		}

		public static void AIElemental(NPC npc, ref float[] ai, ref int idleTimer, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			bool flag = (noDamageMode == null) ? Main.expertMode : noDamageMode.Value;
			if (gravityChange)
			{
				npc.noGravity = true;
			}
			if (tileCollideChange)
			{
				npc.noTileCollide = false;
			}
			if (flag)
			{
				npc.dontTakeDamage = false;
			}
			Player player = (npc.target < 0) ? null : Main.player[npc.target];
			Vector2 vector = (player == null) ? (npc.Center + new Vector2(0f, 5f)) : player.Center;
			vector - npc.Center;
			if (npc.justHit && Main.netMode != 1 && flag && Main.rand.Next(6) == 0)
			{
				npc.netUpdate = true;
				ai[0] = -1f;
				ai[1] = 0f;
			}
			if (ai[0] == -1f)
			{
				if (flag)
				{
					npc.dontTakeDamage = true;
				}
				if (gravityChange)
				{
					npc.noGravity = false;
				}
				npc.velocity.X = npc.velocity.X * 0.98f;
				ai[1] += 1f;
				if (ai[1] >= (float)noDamageTimeMax)
				{
					ai[0] = (ai[1] = (ai[2] = (ai[3] = 0f)));
					return;
				}
			}
			else if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				player = Main.player[npc.target];
				vector = player.Center;
				vector - npc.Center;
				if (Collision.CanHit(npc.Center, 1, 1, vector, 1, 1))
				{
					ai[0] = 1f;
					return;
				}
				Vector2 vector2 = vector - npc.Center;
				vector2.Y -= (float)(player.height / 4);
				float num = vector2.Length();
				if (num > startPhaseDist)
				{
					ai[0] = 2f;
					return;
				}
				Vector2 center = npc.Center;
				center.X = vector.X;
				Vector2 vector3 = center - npc.Center;
				if (vector3.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center, 1, 1))
				{
					ai[0] = 3f;
					ai[1] = center.X;
					ai[2] = center.Y;
					Vector2 center2 = npc.Center;
					center2.Y = vector.Y;
					if (vector3.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center2, 1, 1) && Collision.CanHit(center2, 1, 1, player.position, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = center2.X;
						ai[2] = center2.Y;
					}
				}
				else
				{
					center = npc.Center;
					center.Y = vector.Y;
					if ((center - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = center.X;
						ai[2] = center.Y;
					}
				}
				if (ai[0] == 0f)
				{
					npc.localAI[0] = 0f;
					vector2.Normalize();
					vector2 *= 0.5f;
					npc.velocity += vector2;
					ai[0] = 4f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 1f)
			{
				Vector2 vector4 = vector - npc.Center;
				float num2 = vector4.Length();
				float num3 = velSpeed + num2 / 200f;
				float num4 = 50f;
				vector4.Normalize();
				vector4 *= num3;
				npc.velocity = (npc.velocity * (num4 - 1f) + vector4) / num4;
				if (!Collision.CanHit(npc.Center, 1, 1, vector, 1, 1))
				{
					ai[0] = 0f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 2f)
			{
				npc.noTileCollide = true;
				Vector2 vector5 = vector - npc.Center;
				float num5 = vector5.Length();
				float num6 = 4f;
				vector5.Normalize();
				vector5 *= velSpeed;
				npc.velocity = (npc.velocity * (num6 - 1f) + vector5) / num6;
				if (num5 < stopPhaseDist && !Collision.SolidCollision(npc.position, npc.width, npc.height))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 3f)
			{
				Vector2 vector6;
				vector6..ctor(ai[1], ai[2]);
				Vector2 vector7 = vector6 - npc.Center;
				float num7 = vector7.Length();
				float num8 = (velSpeed < 1f) ? (velSpeed * 0.5f) : Math.Max(0.1f, velSpeed - 1f);
				float num9 = 3f;
				vector7.Normalize();
				vector7 *= num8;
				npc.velocity = (npc.velocity * (num9 - 1f) + vector7) / num9;
				if (npc.collideX || npc.collideY)
				{
					ai[0] = 4f;
					ai[1] = 0f;
				}
				if (num7 < num8 || num7 > startPhaseDist || Collision.CanHit(npc.Center, 1, 1, vector, 1, 1))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 4f)
			{
				if (npc.collideX)
				{
					npc.velocity.X = npc.velocity.X * -0.8f;
				}
				if (npc.collideY)
				{
					npc.velocity.Y = npc.velocity.Y * -0.8f;
				}
				Vector2 vector8;
				if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
				{
					vector8 = vector - npc.Center;
					vector8.Y -= (float)(player.height / 4);
					vector8.Normalize();
					npc.velocity = vector8 * 0.1f;
				}
				float num10 = (velSpeed < 1f) ? (velSpeed * 0.75f) : Math.Max(0.1f, velSpeed - 0.5f);
				float num11 = 20f;
				vector8 = npc.velocity;
				vector8.Normalize();
				vector8 *= num10;
				npc.velocity = (npc.velocity * (num11 - 1f) + vector8) / num11;
				ai[1] += 1f;
				if (ai[1] > (float)idleTooLong)
				{
					ai[0] = 0f;
					ai[1] = 0f;
				}
				if (Collision.CanHit(npc.Center, 1, 1, vector, 1, 1))
				{
					ai[0] = 0f;
				}
				idleTimer++;
				if (idleTimer >= 5 && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
				{
					idleTimer = 0;
					Vector2 center3 = npc.Center;
					center3.X = vector.X;
					if (Collision.CanHit(npc.Center, 1, 1, center3, 1, 1) && Collision.CanHit(npc.Center, 1, 1, center3, 1, 1) && Collision.CanHit(vector, 1, 1, center3, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = center3.X;
						ai[2] = center3.Y;
						return;
					}
					center3 = npc.Center;
					center3.Y = vector.Y;
					if (Collision.CanHit(npc.Center, 1, 1, center3, 1, 1) && Collision.CanHit(vector, 1, 1, center3, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = center3.X;
						ai[2] = center3.Y;
					}
				}
			}
		}

		public static void AIWeapon(NPC npc, ref float[] ai, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			BaseAI.AIWeapon(npc, ref ai, ref npc.rotation, Main.player[npc.target].Center, npc.justHit, rotTime, moveTime, maxSpeed, movementScalar, rotScalar);
		}

		public static void AIWeapon(Entity codable, ref float[] ai, ref float rotation, Vector2 targetPos, bool justHit = false, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (ai[0] == 0f)
			{
				Vector2 center = codable.Center;
				float num = targetPos.X - center.X;
				float num2 = targetPos.Y - center.Y;
				float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
				float num4 = 9f / num3;
				codable.velocity.X = num * num4 * movementScalar;
				codable.velocity.Y = num2 * num4 * movementScalar;
				if (codable.velocity.X > maxSpeed)
				{
					codable.velocity.X = maxSpeed;
				}
				if (codable.velocity.X < -maxSpeed)
				{
					codable.velocity.X = -maxSpeed;
				}
				if (codable.velocity.Y > maxSpeed)
				{
					codable.velocity.Y = maxSpeed;
				}
				if (codable.velocity.Y < -maxSpeed)
				{
					codable.velocity.Y = -maxSpeed;
				}
				rotation = (float)Math.Atan2((double)codable.velocity.Y, (double)codable.velocity.X);
				ai[0] = 1f;
				ai[1] = 0f;
				return;
			}
			if (ai[0] == 1f)
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0f;
				}
				codable.velocity *= 0.99f;
				ai[1] += 1f;
				if (ai[1] < (float)moveTime)
				{
					return;
				}
				ai[0] = 2f;
				ai[1] = 0f;
				codable.velocity.X = 0f;
				codable.velocity.Y = 0f;
				return;
			}
			else
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0f;
				}
				codable.velocity *= 0.96f;
				ai[1] += 1f;
				rotation += (float)(0.1 + (double)(ai[1] / (float)rotTime) * 0.4000000059604645) * (float)codable.direction * rotScalar;
				if (ai[1] < (float)rotTime)
				{
					return;
				}
				if (codable is NPC)
				{
					((NPC)codable).netUpdate = true;
				}
				else if (codable is Projectile)
				{
					((Projectile)codable).netUpdate = true;
				}
				ai[0] = 0f;
				ai[1] = 0f;
				return;
			}
		}

		public static void AISnail(NPC npc, ref float[] ai, ref int snailStatus, float moveInterval = 0.3f, float rotAmt = 0.1f)
		{
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.directionY = 1;
				ai[0] = 1f;
				if (npc.direction > 0)
				{
					npc.spriteDirection = 1;
				}
			}
			bool flag = false;
			if (Main.netMode != 1)
			{
				if (ai[2] == 0f && Main.rand.Next(7200) == 0)
				{
					ai[2] = 2f;
					npc.netUpdate = true;
				}
				if (!npc.collideX && !npc.collideY)
				{
					npc.localAI[3] += 1f;
					if (npc.localAI[3] > 5f)
					{
						ai[2] = 2f;
						npc.netUpdate = true;
					}
				}
				else
				{
					npc.localAI[3] = 0f;
				}
			}
			if (ai[2] > 0f)
			{
				ai[1] = 0f;
				ai[0] = 1f;
				npc.directionY = 1;
				if (npc.velocity.Y > moveInterval)
				{
					npc.rotation += (float)npc.direction * 0.1f;
				}
				else
				{
					npc.rotation = 0f;
				}
				npc.spriteDirection = npc.direction;
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.noGravity = false;
				snailStatus = 0;
				if (npc.collideX && npc.velocity.Y == 0f)
				{
					flag = true;
					ai[2] = 0f;
					ai[1] = 1f;
					npc.directionY = -1;
				}
				if (npc.velocity.Y == 0f)
				{
					if (npc.localAI[1] == npc.position.X)
					{
						npc.localAI[2] += 1f;
						if (npc.localAI[2] > 10f)
						{
							npc.direction = 1;
							npc.velocity.X = (float)npc.direction * moveInterval;
							npc.localAI[2] = 0f;
						}
					}
					else
					{
						npc.localAI[2] = 0f;
						npc.localAI[1] = npc.position.X;
					}
				}
			}
			if (ai[2] == 0f)
			{
				npc.noGravity = true;
				if (ai[1] == 0f)
				{
					if (npc.collideY)
					{
						ai[0] = 2f;
					}
					if (!npc.collideY && ai[0] == 2f)
					{
						npc.direction = -npc.direction;
						ai[1] = 1f;
						ai[0] = 1f;
					}
					if (npc.collideX)
					{
						npc.directionY = -npc.directionY;
						ai[1] = 1f;
					}
				}
				else
				{
					if (npc.collideX)
					{
						ai[0] = 2f;
					}
					if (!npc.collideX && ai[0] == 2f)
					{
						npc.directionY = -npc.directionY;
						ai[1] = 0f;
						ai[0] = 1f;
					}
					if (npc.collideY)
					{
						npc.direction = -npc.direction;
						ai[1] = 0f;
					}
				}
				if (!flag)
				{
					float rotation = npc.rotation;
					if (npc.directionY < 0)
					{
						if (npc.direction < 0)
						{
							if (npc.collideX)
							{
								npc.rotation = 1.57f;
								npc.spriteDirection = -1;
							}
							else if (npc.collideY)
							{
								npc.rotation = 3.14f;
								npc.spriteDirection = 1;
							}
						}
						else if (npc.collideY)
						{
							npc.rotation = 3.14f;
							npc.spriteDirection = -1;
						}
						else if (npc.collideX)
						{
							npc.rotation = 4.71f;
							npc.spriteDirection = 1;
						}
					}
					else if (npc.direction < 0)
					{
						if (npc.collideY)
						{
							npc.rotation = 0f;
							npc.spriteDirection = -1;
						}
						else if (npc.collideX)
						{
							npc.rotation = 1.57f;
							npc.spriteDirection = 1;
						}
					}
					else if (npc.collideX)
					{
						npc.rotation = 4.71f;
						npc.spriteDirection = -1;
					}
					else if (npc.collideY)
					{
						npc.rotation = 0f;
						npc.spriteDirection = 1;
					}
					float rotation2 = npc.rotation;
					npc.rotation = rotation;
					if ((double)npc.rotation > 6.28)
					{
						npc.rotation -= 6.28f;
					}
					if (npc.rotation < 0f)
					{
						npc.rotation += 6.28f;
					}
					float num = Math.Abs(npc.rotation - rotation2);
					if (npc.rotation > rotation2)
					{
						if ((double)num > 3.14)
						{
							npc.rotation += rotAmt;
						}
						else
						{
							npc.rotation -= rotAmt;
							if (npc.rotation < rotation2)
							{
								npc.rotation = rotation2;
							}
						}
					}
					if (npc.rotation < rotation2)
					{
						if ((double)num > 3.14)
						{
							npc.rotation -= rotAmt;
						}
						else
						{
							npc.rotation += rotAmt;
							if (npc.rotation > rotation2)
							{
								npc.rotation = rotation2;
							}
						}
					}
				}
				if (npc.directionY == -1 && !npc.collideX)
				{
					snailStatus = 1;
				}
				else if (npc.directionY == 1 && !npc.collideX)
				{
					snailStatus = 0;
				}
				else if (npc.direction == 1 && !npc.collideY)
				{
					snailStatus = 3;
				}
				else
				{
					snailStatus = 2;
				}
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.velocity.Y = moveInterval * (float)npc.directionY;
			}
		}

		public static void CollisionTest(NPC npc, ref bool left, ref bool right, ref bool up, ref bool down)
		{
			up = (down = (left = (right = false)));
			int num = npc.width / 16 + ((npc.width % 16 > 0) ? 1 : 0);
			int num2 = npc.height / 16 + ((npc.height % 16 > 0) ? 1 : 0);
			int num3 = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(npc.position.X / 16f) - 1));
			int num4 = Math.Max(0, Math.Min(Main.maxTilesX - 1, num3 + num + 1));
			int num5 = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(npc.position.Y / 16f) - 1));
			int num6 = Math.Max(0, Math.Min(Main.maxTilesY - 1, num5 + num2 + 1));
			for (int i = num3; i < num4; i++)
			{
				Tile tile = Main.tile[i, num5];
				Tile tile2 = Main.tile[i, num6];
				if (tile != null && tile.nactive() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
				{
					up = true;
				}
				if (tile2 != null && tile2.nactive() && Main.tileSolid[(int)tile2.type])
				{
					down = true;
				}
				if (up && down)
				{
					break;
				}
			}
			for (int j = num5; j < num6; j++)
			{
				Tile tile3 = Main.tile[num3, j];
				Tile tile4 = Main.tile[num4, j];
				if (tile3 != null && tile3.nactive() && Main.tileSolid[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type])
				{
					left = true;
				}
				if (tile4 != null && tile4.nactive() && Main.tileSolid[(int)tile4.type] && !Main.tileSolidTop[(int)tile4.type])
				{
					right = true;
				}
				if (left && right)
				{
					return;
				}
			}
		}

		public static void AISpore(NPC npc, ref float[] ai, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			npc.TargetClosest(true);
			BaseAI.AISpore(npc, ref ai, Main.player[npc.target].Center, Main.player[npc.target].width, moveIntervalX, moveIntervalY, maxSpeedX, maxSpeedY);
		}

		public static void AISpore(Entity codable, ref float[] ai, Vector2 target, int targetWidth = 16, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			codable.velocity.Y = codable.velocity.Y + moveIntervalY;
			if (codable.velocity.Y < 0f)
			{
				codable.velocity.Y = codable.velocity.Y * 0.99f;
			}
			if (codable.velocity.Y > 1f)
			{
				codable.velocity.Y = 1f;
			}
			int num = targetWidth / 2;
			if (codable.position.X + (float)codable.width < target.X - (float)num)
			{
				if (codable.velocity.X < 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.98f;
				}
				codable.velocity.X = codable.velocity.X + moveIntervalX;
			}
			else if (codable.position.X > target.X + (float)num)
			{
				if (codable.velocity.X > 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.98f;
				}
				codable.velocity.X = codable.velocity.X - moveIntervalX;
			}
			if (codable.velocity.X > maxSpeedX || codable.velocity.X < -maxSpeedX)
			{
				codable.velocity.X = codable.velocity.X * 0.97f;
			}
		}

		public static void AICharger(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 6f, bool allowBoredom = true, int ticksUntilBoredom = 30)
		{
			bool flag = false;
			if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
			{
				flag = true;
				ai[3] += 1f;
			}
			if (npc.position.X == npc.oldPosition.X || ai[3] >= (float)ticksUntilBoredom || flag)
			{
				ai[3] += 1f;
			}
			else if (ai[3] > 0f)
			{
				ai[3] -= 1f;
			}
			if (ai[3] > (float)(ticksUntilBoredom * 10))
			{
				ai[3] = 0f;
			}
			if (npc.justHit)
			{
				ai[3] = 0f;
			}
			if (ai[3] == (float)ticksUntilBoredom)
			{
				npc.netUpdate = true;
			}
			Vector2 center = npc.Center;
			float num = Main.player[npc.target].Center.X - center.X;
			float num2 = Main.player[npc.target].Center.Y - center.Y;
			float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
			if (num3 < 200f)
			{
				ai[3] = 0f;
			}
			if (!allowBoredom || ai[3] < (float)ticksUntilBoredom)
			{
				npc.TargetClosest(true);
			}
			else
			{
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						ai[0] += 1f;
						if ((double)ai[0] >= 2.0)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							ai[0] = 0f;
						}
					}
				}
				else
				{
					ai[0] = 0f;
				}
				npc.directionY = -1;
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			if (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0))
			{
				if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
						return;
					}
				}
				else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
						return;
					}
				}
				else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if (npc.velocity.X < -maxSpeed)
					{
						npc.velocity.X = -maxSpeed;
					}
				}
			}
		}

		public static void AIFriendly(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 1f, bool critter = false, bool? seekHome = null, bool canTeleportHome = true, bool canFindHouse = true, bool canOpenDoors = true)
		{
			bool flag = Main.raining;
			if (!Main.dayTime || Main.eclipse)
			{
				flag = true;
			}
			if (seekHome != null)
			{
				flag = seekHome.Value;
			}
			int num = (int)((double)npc.position.X + (double)(npc.width / 2)) / 16;
			int num2 = (int)((double)npc.position.Y + (double)npc.height + 1.0) / 16;
			if (critter && npc.target == 255)
			{
				npc.TargetClosest(true);
				npc.direction = ((npc.Center.X < Main.player[npc.target].Center.X) ? 1 : -1);
				npc.spriteDirection = npc.direction;
				if (npc.homeTileX == -1)
				{
					npc.homeTileX = (int)(npc.Center.X / 16f);
				}
			}
			bool flag2 = false;
			npc.directionY = -1;
			if (npc.direction == 0)
			{
				npc.direction = 1;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active && Main.player[i].talkNPC == npc.whoAmI)
				{
					flag2 = true;
					if (ai[0] != 0f)
					{
						npc.netUpdate = true;
					}
					ai[0] = 0f;
					ai[1] = 300f;
					ai[2] = 100f;
					npc.direction = ((Main.player[i].Center.X >= npc.Center.X) ? 1 : -1);
				}
			}
			if (ai[3] > 0f)
			{
				npc.life = -1;
				npc.HitEffect(0, 10.0);
				npc.active = false;
				if (npc.type == 37)
				{
					Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
				}
			}
			if ((npc.type >= 580 && npc.homeTileX == -1 && npc.homeTileY == -1) || (npc.homeTileX == 65535 && npc.homeTileY == 65535))
			{
				npc.homeTileX = (int)npc.Center.X / 16;
				npc.homeTileY = (int)npc.Center.Y / 16;
			}
			int num3 = npc.homeTileY;
			if (Main.netMode != 1 && npc.homeTileY > 0)
			{
				while (!WorldGen.SolidTile(npc.homeTileX, num3) && num3 < Main.maxTilesY - 20)
				{
					num3++;
				}
			}
			if (Main.netMode != 1 && canTeleportHome && flag && (num != npc.homeTileX || num2 != num3) && !npc.homeless)
			{
				bool flag3 = true;
				for (int j = 0; j < 2; j++)
				{
					Rectangle rectangle;
					rectangle..ctor((int)(npc.Center.X - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(npc.Center.Y - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					if (j == 1)
					{
						rectangle..ctor(npc.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					}
					for (int k = 0; k < 255; k++)
					{
						if (Main.player[k].active && Main.player[k].Hitbox.Intersects(rectangle))
						{
							flag3 = false;
							break;
						}
						if (!flag3)
						{
							break;
						}
					}
				}
				if (flag3)
				{
					if (!Collision.SolidTiles(npc.homeTileX - 1, npc.homeTileX + 1, num3 - 3, num3 - 1))
					{
						npc.velocity.X = 0f;
						npc.velocity.Y = 0f;
						npc.position.X = (float)(npc.homeTileX * 16 + 8 - npc.width / 2);
						npc.position.Y = (float)(num3 * 16 - npc.height) - 0.1f;
						npc.netUpdate = true;
					}
					else if (canFindHouse)
					{
						npc.homeless = true;
						WorldGen.QuickFindHome(npc.whoAmI);
					}
				}
			}
			if (ai[0] == 0f)
			{
				if (ai[2] > 0f)
				{
					ai[2] -= 1f;
				}
				if (flag && !flag2 && !critter)
				{
					if (Main.netMode != 1)
					{
						if (num == npc.homeTileX && num2 == num3)
						{
							if (npc.velocity.X != 0f)
							{
								npc.netUpdate = true;
							}
							if (npc.velocity.X > 0.1f)
							{
								npc.velocity.X = npc.velocity.X - 0.1f;
							}
							else if (npc.velocity.X < -0.1f)
							{
								npc.velocity.X = npc.velocity.X + 0.1f;
							}
							else
							{
								npc.velocity.X = 0f;
							}
						}
						else
						{
							npc.direction = ((num <= npc.homeTileX) ? 1 : -1);
							ai[0] = 1f;
							ai[1] = (float)(200 + Main.rand.Next(200));
							ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				else
				{
					if (npc.velocity.X > 0.1f)
					{
						npc.velocity.X = npc.velocity.X - 0.1f;
					}
					else if (npc.velocity.X < -0.1f)
					{
						npc.velocity.X = npc.velocity.X + 0.1f;
					}
					else
					{
						npc.velocity.X = 0f;
					}
					if (Main.netMode != 1)
					{
						if (ai[1] > 0f)
						{
							ai[1] -= 1f;
						}
						if (ai[1] <= 0f)
						{
							ai[0] = 1f;
							ai[1] = (float)(200 + Main.rand.Next(200));
							if (critter)
							{
								ai[1] += (float)Main.rand.Next(200, 400);
							}
							ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				if (Main.netMode == 1 || (flag && (num != npc.homeTileX || num2 != num3)))
				{
					return;
				}
				if (num < npc.homeTileX - 25 || num > npc.homeTileX + 25)
				{
					if (ai[2] != 0f)
					{
						return;
					}
					if (num < npc.homeTileX - 50 && npc.direction == -1)
					{
						npc.direction = 1;
						npc.netUpdate = true;
						return;
					}
					if (num <= npc.homeTileX + 50 || npc.direction != 1)
					{
						return;
					}
					npc.direction = -1;
					npc.netUpdate = true;
					return;
				}
				else
				{
					if (Main.rand.Next(80) != 0 || (double)ai[2] != 0.0)
					{
						return;
					}
					ai[2] = 200f;
					npc.direction *= -1;
					npc.netUpdate = true;
					return;
				}
			}
			else
			{
				if (ai[0] != 1f)
				{
					return;
				}
				if (Main.netMode != 1 && !critter && flag && num == npc.homeTileX && num2 == npc.homeTileY)
				{
					ai[0] = 0f;
					ai[1] = (float)(200 + Main.rand.Next(200));
					ai[2] = 60f;
					npc.netUpdate = true;
					return;
				}
				if (Main.netMode != 1 && !npc.homeless && !Main.tileDungeon[(int)Main.tile[num, num2].type] && (num < npc.homeTileX - 35 || num > npc.homeTileX + 35))
				{
					if (npc.Center.X < (float)(npc.homeTileX * 16) && npc.direction == -1)
					{
						ai[1] -= 5f;
					}
					else if (npc.Center.X > (float)(npc.homeTileX * 16) && npc.direction == 1)
					{
						ai[1] -= 5f;
					}
				}
				ai[1] -= 1f;
				if (ai[1] <= 0f)
				{
					ai[0] = 0f;
					ai[1] = (float)(300 + Main.rand.Next(300));
					if (critter)
					{
						ai[1] -= (float)Main.rand.Next(100);
					}
					ai[2] = 60f;
					npc.netUpdate = true;
				}
				if (npc.closeDoor && (npc.Center.X / 16f > (float)(npc.doorX + 2) || npc.Center.X / 16f < (float)(npc.doorX - 2)))
				{
					if (WorldGen.CloseDoor(npc.doorX, npc.doorY, false))
					{
						npc.closeDoor = false;
						NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 1, (float)npc.doorX, (float)npc.doorY, (float)npc.direction, 0, 0, 0);
					}
					if (npc.Center.X / 16f > (float)(npc.doorX + 4) || npc.Center.X / 16f < (float)(npc.doorX - 4) || npc.Center.Y / 16f > (float)(npc.doorY + 4) || npc.Center.Y / 16f < (float)(npc.doorY - 4))
					{
						npc.closeDoor = false;
					}
				}
				if (npc.Center.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
					}
				}
				else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
					}
				}
				BaseAI.WalkupHalfBricks(npc);
				if (npc.velocity.Y != 0f)
				{
					return;
				}
				if (npc.position.X == ai[2])
				{
					npc.direction *= -1;
				}
				ai[2] = -1f;
				int num4 = (int)((npc.Center.X + (float)(15 * npc.direction)) / 16f);
				int num5 = (int)((npc.position.Y + (float)npc.height - 16f) / 16f);
				if (Main.tile[num4, num5] == null)
				{
					Main.tile[num4, num5] = new Tile();
				}
				if (Main.tile[num4, num5 - 1] == null)
				{
					Main.tile[num4, num5 - 1] = new Tile();
				}
				if (Main.tile[num4, num5 - 2] == null)
				{
					Main.tile[num4, num5 - 2] = new Tile();
				}
				if (Main.tile[num4, num5 - 3] == null)
				{
					Main.tile[num4, num5 - 3] = new Tile();
				}
				if (Main.tile[num4, num5 + 1] == null)
				{
					Main.tile[num4, num5 + 1] = new Tile();
				}
				if (Main.tile[num4 - npc.direction, num5 + 1] == null)
				{
					Main.tile[num4 - npc.direction, num5 + 1] = new Tile();
				}
				if (Main.tile[num4 + npc.direction, num5 - 1] == null)
				{
					Main.tile[num4 + npc.direction, num5 - 1] = new Tile();
				}
				if (Main.tile[num4 + npc.direction, num5 + 1] == null)
				{
					Main.tile[num4 + npc.direction, num5 + 1] = new Tile();
				}
				if (!canOpenDoors || !Main.tile[num4, num5 - 2].nactive() || Main.tile[num4, num5 - 2].type != 10 || (Main.rand.Next(10) != 0 && !flag))
				{
					if ((npc.velocity.X < 0f && npc.spriteDirection == -1) || (npc.velocity.X > 0f && npc.spriteDirection == 1))
					{
						if (Main.tile[num4, num5 - 2].nactive() && Main.tileSolid[(int)Main.tile[num4, num5 - 2].type] && !Main.tileSolidTop[(int)Main.tile[num4, num5 - 2].type])
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(num4 - 2, num4 - 1, num5 - 5, num5 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(num4 + 1, num4 + 2, num5 - 5, num5 - 1)))
							{
								if (!Collision.SolidTiles(num4, num4, num5 - 5, num5 - 3))
								{
									npc.velocity.Y = -6f;
									npc.netUpdate = true;
								}
								else
								{
									npc.direction *= -1;
									npc.netUpdate = true;
								}
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						else if (Main.tile[num4, num5 - 1].nactive() && Main.tileSolid[(int)Main.tile[num4, num5 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num4, num5 - 1].type])
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(num4 - 2, num4 - 1, num5 - 4, num5 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(num4 + 1, num4 + 2, num5 - 4, num5 - 1)))
							{
								if (!Collision.SolidTiles(num4, num4, num5 - 4, num5 - 2))
								{
									npc.velocity.Y = -5f;
									npc.netUpdate = true;
								}
								else
								{
									npc.direction *= -1;
									npc.netUpdate = true;
								}
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						else if (npc.position.Y + (float)npc.height - (float)num5 * 16f > 20f && Main.tile[num4, num5].nactive() && Main.tileSolid[(int)Main.tile[num4, num5].type] && Main.tile[num4, num5].slope() == 0)
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(num4 - 2, num4, num5 - 3, num5 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(num4, num4 + 2, num5 - 3, num5 - 1)))
							{
								npc.velocity.Y = -4.4f;
								npc.netUpdate = true;
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						try
						{
							if (Main.tile[num4, num5 + 1] == null)
							{
								Main.tile[num4, num5 + 1] = new Tile();
							}
							if (Main.tile[num4 - npc.direction, num5 + 1] == null)
							{
								Main.tile[num4 - npc.direction, num5 + 1] = new Tile();
							}
							if (Main.tile[num4, num5 + 2] == null)
							{
								Main.tile[num4, num5 + 2] = new Tile();
							}
							if (Main.tile[num4 - npc.direction, num5 + 2] == null)
							{
								Main.tile[num4 - npc.direction, num5 + 2] = new Tile();
							}
							if (Main.tile[num4, num5 + 3] == null)
							{
								Main.tile[num4, num5 + 3] = new Tile();
							}
							if (Main.tile[num4 - npc.direction, num5 + 3] == null)
							{
								Main.tile[num4 - npc.direction, num5 + 3] = new Tile();
							}
							if (Main.tile[num4, num5 + 4] == null)
							{
								Main.tile[num4, num5 + 4] = new Tile();
							}
							if (Main.tile[num4 - npc.direction, num5 + 4] == null)
							{
								Main.tile[num4 - npc.direction, num5 + 4] = new Tile();
							}
							if (!critter && num >= npc.homeTileX - 35 && num <= npc.homeTileX + 35 && (!Main.tile[num4, num5 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num4, num5 + 1].type]) && (!Main.tile[num4 - npc.direction, num5 + 1].active() || !Main.tileSolid[(int)Main.tile[num4 - npc.direction, num5 + 1].type]) && (!Main.tile[num4, num5 + 2].nactive() || !Main.tileSolid[(int)Main.tile[num4, num5 + 2].type]) && (!Main.tile[num4 - npc.direction, num5 + 2].active() || !Main.tileSolid[(int)Main.tile[num4 - npc.direction, num5 + 2].type]) && (!Main.tile[num4, num5 + 3].nactive() || !Main.tileSolid[(int)Main.tile[num4, num5 + 3].type]) && (!Main.tile[num4 - npc.direction, num5 + 3].active() || !Main.tileSolid[(int)Main.tile[num4 - npc.direction, num5 + 3].type]) && (!Main.tile[num4, num5 + 4].nactive() || !Main.tileSolid[(int)Main.tile[num4, num5 + 4].type]) && (!Main.tile[num4 - npc.direction, num5 + 4].nactive() || !Main.tileSolid[(int)Main.tile[num4 - npc.direction, num5 + 4].type]))
							{
								npc.direction *= -1;
								npc.velocity.X = npc.velocity.X * -1f;
								npc.netUpdate = true;
							}
						}
						catch
						{
						}
						if ((double)npc.velocity.Y < 0.0)
						{
							ai[2] = npc.position.X;
						}
					}
					if (npc.velocity.Y < 0f && npc.wet)
					{
						npc.velocity.Y = npc.velocity.Y * 1.2f;
					}
					npc.velocity.Y = npc.velocity.Y * 1.2f;
					return;
				}
				if (Main.netMode == 1)
				{
					return;
				}
				if (WorldGen.OpenDoor(num4, num5 - 2, npc.direction))
				{
					npc.closeDoor = true;
					npc.doorX = num4;
					npc.doorY = num5 - 2;
					NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)num4, (float)(num5 - 2), (float)npc.direction, 0, 0, 0);
					npc.netUpdate = true;
					ai[1] += 80f;
					return;
				}
				if (WorldGen.OpenDoor(num4, num5 - 2, -npc.direction))
				{
					npc.closeDoor = true;
					npc.doorX = num4;
					npc.doorY = num5 - 2;
					NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)num4, (float)(num5 - 2), (float)(-(float)npc.direction), 0, 0, 0);
					npc.netUpdate = true;
					ai[1] += 80f;
					return;
				}
				npc.direction *= -1;
				npc.netUpdate = true;
				return;
			}
		}

		public static void AIEater(NPC npc, ref float[] ai, float moveInterval = 0.022f, float distanceDivider = 4.2f, float bounceScalar = 0.7f, bool fleeAtDay = false, bool ignoreWet = false)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			float x = Main.player[npc.target].Center.X;
			float y = Main.player[npc.target].Center.Y;
			Vector2 center = npc.Center;
			float num = (float)((int)(x / 8f)) * 8f;
			float num2 = (float)((int)(y / 8f)) * 8f;
			center.X = (float)((int)(center.X / 8f)) * 8f;
			center.Y = (float)((int)(center.Y / 8f)) * 8f;
			float num3 = num - center.X;
			float num4 = num2 - center.Y;
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			float num6;
			float num7;
			if (num5 == 0f)
			{
				num6 = npc.velocity.X;
				num7 = npc.velocity.Y;
			}
			else
			{
				float num8 = distanceDivider / num5;
				num6 = num3 * num8;
				num7 = num4 * num8;
			}
			ai[0] += 1f;
			if (ai[0] > 0f)
			{
				npc.velocity.Y = npc.velocity.Y + 0.023f;
			}
			else
			{
				npc.velocity.Y = npc.velocity.Y - 0.023f;
			}
			if (ai[0] < -100f || (double)ai[0] > 100.0)
			{
				npc.velocity.X = npc.velocity.X + 0.023f;
			}
			else
			{
				npc.velocity.X = npc.velocity.X - 0.023f;
			}
			if (ai[0] > 200f)
			{
				ai[0] = -200f;
			}
			if (num5 < 150f)
			{
				npc.velocity.X = npc.velocity.X + num6 * 0.007f;
				npc.velocity.Y = npc.velocity.Y + num7 * 0.007f;
			}
			if (Main.player[npc.target].dead)
			{
				num6 = (float)npc.direction * distanceDivider / 2f;
				num7 = -distanceDivider / 2f;
			}
			if (npc.velocity.X < num6)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
			}
			else if (npc.velocity.X > num6)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
			}
			if (npc.velocity.Y < num7)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
			}
			else if (npc.velocity.Y > num7)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
			}
			npc.rotation = (float)Math.Atan2((double)num7, (double)num6) - 1.57f;
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
				{
					npc.velocity.Y = 2f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.95f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.3f;
				if (npc.velocity.Y < -2f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if ((fleeAtDay && Main.dayTime) || Main.player[npc.target].dead)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval * 2f;
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
			}
			if (((npc.velocity.X <= 0f || npc.oldVelocity.X >= 0f) && (npc.velocity.X >= 0f || npc.oldVelocity.X <= 0f) && (npc.velocity.Y <= 0f || npc.oldVelocity.Y >= 0f) && ((double)npc.velocity.Y >= 0.0 || npc.oldVelocity.Y <= 0f)) || npc.justHit)
			{
				return;
			}
			npc.netUpdate = true;
		}

		public static void AIWheel(NPC npc, ref float[] ai, float moveInterval = 6f, bool rotate = false)
		{
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.directionY = 1;
				ai[0] = 1f;
			}
			if (ai[1] == 0f)
			{
				if (rotate)
				{
					npc.rotation += (float)(npc.direction * npc.directionY) * 0.13f;
				}
				if (npc.collideY)
				{
					ai[0] = 2f;
				}
				if (!npc.collideY && ai[0] == 2f)
				{
					npc.direction = -npc.direction;
					ai[1] = 1f;
					ai[0] = 1f;
				}
				if (npc.collideX)
				{
					npc.directionY = -npc.directionY;
					ai[1] = 1f;
				}
			}
			else
			{
				if (rotate)
				{
					npc.rotation -= (float)(npc.direction * npc.directionY) * 0.13f;
				}
				if (npc.collideX)
				{
					ai[0] = 2f;
				}
				if (!npc.collideX && ai[0] == 2f)
				{
					npc.directionY = -npc.directionY;
					ai[1] = 0f;
					ai[0] = 1f;
				}
				if (npc.collideY)
				{
					npc.direction = -npc.direction;
					ai[1] = 0f;
				}
			}
			npc.velocity.X = moveInterval * (float)npc.direction;
			npc.velocity.Y = moveInterval * (float)npc.directionY;
		}

		public static void AISpider(NPC npc, ref float[] ai, bool ignoreSight = false, float moveInterval = 0.08f, float slowSpeed = 1.5f, float maxSpeed = 3f, float distanceDivider = 2f, float bounceScalar = 0.5f, int transformType = -1)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			Vector2 center = npc.Center;
			float x = Main.player[npc.target].Center.X;
			float y = Main.player[npc.target].Center.Y;
			float num = (float)((int)(x / 8f)) * 8f;
			float num2 = (float)((int)(y / 8f)) * 8f;
			center.X = (float)((int)(center.X / 8f)) * 8f;
			center.Y = (float)((int)(center.Y / 8f)) * 8f;
			float num3 = num - center.X;
			float num4 = num2 - center.Y;
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			float num6;
			float num7;
			if (num5 == 0f)
			{
				num6 = npc.velocity.X;
				num7 = npc.velocity.Y;
			}
			else
			{
				float num8 = distanceDivider / num5;
				num6 = num3 * num8;
				num7 = num4 * num8;
			}
			if (Main.player[npc.target].dead)
			{
				num6 = (float)npc.direction * distanceDivider / 2f;
				num7 = -distanceDivider / 2f;
			}
			npc.spriteDirection = -1;
			if (!ignoreSight && !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
			{
				ai[0] += 1f;
				if (ai[0] > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + 0.023f;
				}
				else
				{
					npc.velocity.Y = npc.velocity.Y - 0.023f;
				}
				if (ai[0] < -100f || (double)ai[0] > 100.0)
				{
					npc.velocity.X = npc.velocity.X + 0.023f;
				}
				else
				{
					npc.velocity.X = npc.velocity.X - 0.023f;
				}
				if (ai[0] > 200f)
				{
					ai[0] = -200f;
				}
				npc.velocity.X = npc.velocity.X + num6 * 0.007f;
				npc.velocity.Y = npc.velocity.Y + num7 * 0.007f;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
				if (npc.velocity.X > slowSpeed || npc.velocity.X < -slowSpeed)
				{
					npc.velocity.X = npc.velocity.X * 0.9f;
				}
				if (npc.velocity.Y > slowSpeed || npc.velocity.Y < -slowSpeed)
				{
					npc.velocity.Y = npc.velocity.Y * 0.9f;
				}
				if (npc.velocity.X > maxSpeed)
				{
					npc.velocity.X = maxSpeed;
				}
				else if (npc.velocity.X < -maxSpeed)
				{
					npc.velocity.X = -maxSpeed;
				}
				if (npc.velocity.Y > maxSpeed)
				{
					npc.velocity.Y = maxSpeed;
				}
				else if (npc.velocity.Y < -maxSpeed)
				{
					npc.velocity.Y = -maxSpeed;
				}
			}
			else
			{
				if ((double)npc.velocity.X < (double)num6)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if ((double)npc.velocity.X < 0.0 && (double)num6 > 0.0)
					{
						npc.velocity.X = npc.velocity.X + moveInterval;
					}
				}
				else if ((double)npc.velocity.X > (double)num6)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if ((double)npc.velocity.X > 0.0 && (double)num6 < 0.0)
					{
						npc.velocity.X = npc.velocity.X - moveInterval;
					}
				}
				if ((double)npc.velocity.Y < (double)num7)
				{
					npc.velocity.Y = npc.velocity.Y + moveInterval;
					if ((double)npc.velocity.Y < 0.0 && (double)num7 > 0.0)
					{
						npc.velocity.Y = npc.velocity.Y + moveInterval;
					}
				}
				else if ((double)npc.velocity.Y > (double)num7)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval;
					if ((double)npc.velocity.Y > 0.0 && (double)num7 < 0.0)
					{
						npc.velocity.Y = npc.velocity.Y - moveInterval;
					}
				}
				npc.rotation = (float)Math.Atan2((double)num7, (double)num6);
			}
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
				{
					npc.velocity.Y = 2f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
			{
				npc.netUpdate = true;
			}
			if (Main.netMode != 1 && transformType != -1)
			{
				int num9 = (int)npc.Center.X / 16;
				int num10 = (int)npc.Center.Y / 16;
				bool flag = false;
				for (int i = num9 - 1; i <= num9 + 1; i++)
				{
					for (int j = num10 - 1; j <= num10 + 1; j++)
					{
						if (Main.tile[i, j].wall > 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					npc.Transform(transformType);
				}
			}
		}

		public static void AISkull(NPC npc, ref float[] ai, bool tacklePlayer = true, float maxDistanceAmt = 4f, float maxDistance = 350f, float increment = 0.011f, float closeIncrement = 0.019f)
		{
			float num = 1f;
			npc.TargetClosest(true);
			float num2 = Main.player[npc.target].Center.X - npc.Center.X;
			float num3 = Main.player[npc.target].Center.Y - npc.Center.Y;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			ai[1] += 1f;
			if (ai[1] > 600f)
			{
				if (tacklePlayer)
				{
					increment *= 8f;
					num = 4f;
					if (ai[1] > 650f)
					{
						ai[1] = 0f;
					}
				}
				else
				{
					ai[1] = 0f;
				}
			}
			else if (num4 < 250f)
			{
				ai[0] += 0.9f;
				if (ai[0] > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + closeIncrement;
				}
				else
				{
					npc.velocity.Y = npc.velocity.Y - closeIncrement;
				}
				if (ai[0] < -100f || ai[0] > 100f)
				{
					npc.velocity.X = npc.velocity.X + closeIncrement;
				}
				else
				{
					npc.velocity.X = npc.velocity.X - closeIncrement;
				}
				if (ai[0] > 200f)
				{
					ai[0] = -200f;
				}
			}
			if (num4 > maxDistance)
			{
				num = maxDistanceAmt + maxDistanceAmt / 4f;
				increment = 0.3f;
			}
			else if (num4 > maxDistance - maxDistance / 7f)
			{
				num = maxDistanceAmt - maxDistanceAmt / 4f;
				increment = 0.2f;
			}
			else if (num4 > maxDistance - 2f * (maxDistance / 7f))
			{
				num = maxDistanceAmt / 2.66f;
				increment = 0.1f;
			}
			num4 = num / num4;
			num2 *= num4;
			num3 *= num4;
			if (Main.player[npc.target].dead)
			{
				num2 = (float)npc.direction * num / 2f;
				num3 = -num / 2f;
			}
			if (npc.velocity.X < num2)
			{
				npc.velocity.X = npc.velocity.X + increment;
			}
			else if (npc.velocity.X > num2)
			{
				npc.velocity.X = npc.velocity.X - increment;
			}
			if (npc.velocity.Y < num3)
			{
				npc.velocity.Y = npc.velocity.Y + increment;
				return;
			}
			if (npc.velocity.Y > num3)
			{
				npc.velocity.Y = npc.velocity.Y - increment;
			}
		}

		public static void AIFloater(NPC npc, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
		{
			bool flag = false;
			if (npc.justHit)
			{
				ai[2] = 0f;
			}
			if (ai[2] >= 0f)
			{
				int num = 16;
				bool flag2 = false;
				bool flag3 = false;
				if (npc.position.X > ai[0] - (float)num && npc.position.X < ai[0] + (float)num)
				{
					flag2 = true;
				}
				else if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0))
				{
					flag2 = true;
				}
				num += 24;
				if (npc.position.Y > ai[1] - (float)num && npc.position.Y < ai[1] + (float)num)
				{
					flag3 = true;
				}
				if (flag2 && flag3)
				{
					ai[2] += 1f;
					if (ai[2] >= 30f && num == 16)
					{
						flag = true;
					}
					if (ai[2] >= 60f)
					{
						ai[2] = -200f;
						npc.direction *= -1;
						npc.velocity.X = npc.velocity.X * -1f;
						npc.collideX = false;
					}
				}
				else
				{
					ai[0] = npc.position.X;
					ai[1] = npc.position.Y;
					ai[2] = 0f;
				}
				npc.TargetClosest(true);
			}
			else
			{
				ai[2] += 1f;
				if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
				{
					npc.direction = -1;
				}
				else
				{
					npc.direction = 1;
				}
			}
			int num2 = (int)(npc.Center.X / 16f) + npc.direction * 2;
			int num3 = (int)((npc.position.Y + (float)npc.height) / 16f);
			bool flag4 = true;
			for (int i = num3; i < num3 + hoverHeight; i++)
			{
				if (Main.tile[num2, i] == null)
				{
					Main.tile[num2, i] = new Tile();
				}
				if ((Main.tile[num2, i].nactive() && Main.tileSolid[(int)Main.tile[num2, i].type]) || Main.tile[num2, i].liquid > 0)
				{
					flag4 = false;
					break;
				}
			}
			if (flag)
			{
				flag4 = true;
			}
			if (flag4)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
				if (npc.velocity.Y > maxSpeedY)
				{
					npc.velocity.Y = maxSpeedY;
				}
			}
			else
			{
				if (npc.directionY < 0 && npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval;
				}
				if (npc.velocity.Y < -maxSpeedY)
				{
					npc.velocity.Y = -maxSpeedY;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
				if (npc.velocity.Y < -maxSpeedY * 0.75f)
				{
					npc.velocity.Y = -maxSpeedY * 0.75f;
				}
			}
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.4f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f)
				{
					npc.velocity.X = 1f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f)
				{
					npc.velocity.X = -1f;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
			}
			if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
			{
				npc.velocity.X = npc.velocity.X - moveInterval * 0.5f;
				if (npc.velocity.X > maxSpeedX)
				{
					npc.velocity.X = npc.velocity.X - 0.1f;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + 0.05f;
				}
				if (npc.velocity.X < -maxSpeedX)
				{
					npc.velocity.X = -maxSpeedX;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
			{
				npc.velocity.X = npc.velocity.X + moveInterval * 0.5f;
				if (npc.velocity.X < -maxSpeedX)
				{
					npc.velocity.X = npc.velocity.X + 0.1f;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - 0.05f;
				}
				if (npc.velocity.X > maxSpeedX)
				{
					npc.velocity.X = maxSpeedX;
				}
			}
			if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)hoverMaxSpeed))
			{
				npc.velocity.Y = npc.velocity.Y - hoverInterval;
				if ((double)npc.velocity.Y > (double)hoverMaxSpeed)
				{
					npc.velocity.Y = npc.velocity.Y - 0.05f;
				}
				else if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + (hoverInterval - 0.01f);
				}
				if ((double)npc.velocity.Y < (double)(-(double)hoverMaxSpeed))
				{
					npc.velocity.Y = -hoverMaxSpeed;
					return;
				}
			}
			else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)hoverMaxSpeed)
			{
				npc.velocity.Y = npc.velocity.Y + hoverInterval;
				if ((double)npc.velocity.Y < (double)(-(double)hoverMaxSpeed))
				{
					npc.velocity.Y = npc.velocity.Y + 0.05f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - (hoverInterval - 0.01f);
				}
				if ((double)npc.velocity.Y > (double)hoverMaxSpeed)
				{
					npc.velocity.Y = hoverMaxSpeed;
				}
			}
		}

		public static void AIFlier(NPC npc, ref float[] ai, bool sporadic = true, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float maxSpeedX = 4f, float maxSpeedY = 1.5f, bool canBeBored = true, int timeUntilBoredom = 300)
		{
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				float num = maxSpeedX * 0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < num)
				{
					npc.velocity.X = num;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -num)
				{
					npc.velocity.X = -num;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				float num2 = maxSpeedY * 0.66f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < num2)
				{
					npc.velocity.Y = num2;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -num2)
				{
					npc.velocity.Y = -num2;
				}
			}
			npc.TargetClosest(true);
			Action action = delegate()
			{
				if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
				{
					NPC npc2 = npc;
					npc2.velocity.X = npc2.velocity.X - moveIntervalX;
					if (npc.velocity.X > maxSpeedX)
					{
						NPC npc3 = npc;
						npc3.velocity.X = npc3.velocity.X - moveIntervalX;
					}
					else if (npc.velocity.X > 0f)
					{
						NPC npc4 = npc;
						npc4.velocity.X = npc4.velocity.X + moveIntervalX * 0.5f;
					}
					if (npc.velocity.X < -maxSpeedX)
					{
						npc.velocity.X = -maxSpeedX;
					}
				}
				else if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
				{
					NPC npc5 = npc;
					npc5.velocity.X = npc5.velocity.X + moveIntervalX;
					if (npc.velocity.X < -maxSpeedX)
					{
						NPC npc6 = npc;
						npc6.velocity.X = npc6.velocity.X + moveIntervalX;
					}
					else if (npc.velocity.X < 0f)
					{
						NPC npc7 = npc;
						npc7.velocity.X = npc7.velocity.X - moveIntervalX * 0.5f;
					}
					if (npc.velocity.X > maxSpeedX)
					{
						npc.velocity.X = maxSpeedX;
					}
				}
				if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)maxSpeedY))
				{
					NPC npc8 = npc;
					npc8.velocity.Y = npc8.velocity.Y - moveIntervalY;
					if ((double)npc.velocity.Y > (double)maxSpeedY)
					{
						NPC npc9 = npc;
						npc9.velocity.Y = npc9.velocity.Y - moveIntervalY;
					}
					else if (npc.velocity.Y > 0f)
					{
						NPC npc10 = npc;
						npc10.velocity.Y = npc10.velocity.Y + moveIntervalY * 0.5f;
					}
					if ((double)npc.velocity.Y < (double)(-(double)maxSpeedY))
					{
						npc.velocity.Y = -maxSpeedY;
						return;
					}
				}
				else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)maxSpeedY)
				{
					NPC npc11 = npc;
					npc11.velocity.Y = npc11.velocity.Y + moveIntervalY;
					if ((double)npc.velocity.Y < (double)(-(double)maxSpeedY))
					{
						NPC npc12 = npc;
						npc12.velocity.Y = npc12.velocity.Y + moveIntervalY;
					}
					else if (npc.velocity.Y < 0f)
					{
						NPC npc13 = npc;
						npc13.velocity.Y = npc13.velocity.Y - moveIntervalY * 0.5f;
					}
					if ((double)npc.velocity.Y > (double)maxSpeedY)
					{
						npc.velocity.Y = maxSpeedY;
					}
				}
			};
			if (canBeBored)
			{
				ai[0] += 1f;
			}
			if (canBeBored && ai[0] > (float)timeUntilBoredom)
			{
				if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					ai[0] = 0f;
				}
				if (ai[0] > (float)(timeUntilBoredom * 2))
				{
					ai[0] = 0f;
				}
				npc.direction = ((Main.player[npc.target].Center.X < npc.Center.X) ? 1 : -1);
				npc.directionY = ((Main.player[npc.target].Center.Y < npc.Center.Y) ? 1 : -1);
				action();
				return;
			}
			action();
			if (sporadic)
			{
				if (npc.wet)
				{
					if (npc.velocity.Y > 0f)
					{
						npc.velocity.Y = npc.velocity.Y * 0.95f;
					}
					npc.velocity.Y = npc.velocity.Y - 0.5f;
					if (npc.velocity.Y < -maxSpeedX)
					{
						npc.velocity.Y = -maxSpeedX;
					}
					npc.TargetClosest(true);
				}
				action();
			}
		}

		public static void AIPlant(NPC npc, ref float[] ai, bool checkTilePoint = true, Vector2 endPoint = default(Vector2), bool isTilePos = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2))
		{
			if (endPoint != default(Vector2))
			{
				ai[0] = endPoint.X;
				ai[1] = endPoint.Y;
			}
			if (checkTilePoint)
			{
				Vector2 vector = isTilePos ? new Vector2(ai[0], ai[1]) : new Vector2(ai[0] / 16f, ai[1] / 16f);
				int num = (int)vector.X;
				int num2 = (int)vector.Y;
				if (Main.tile[num, num2] == null)
				{
					Main.tile[num, num2] = new Tile();
				}
				if (!Main.tile[num, num2].nactive() || !Main.tileSolid[(int)Main.tile[num, num2].type] || (Main.tileSolid[(int)Main.tile[num, num2].type] && Main.tileSolidTop[(int)Main.tile[num, num2].type]))
				{
					if (npc.DeathSound != null)
					{
						Main.PlaySound(npc.DeathSound, (int)npc.Center.X, (int)npc.Center.Y);
					}
					npc.life = -1;
					npc.HitEffect(0, 10.0);
					npc.active = false;
					return;
				}
			}
			npc.TargetClosest(true);
			ai[2] += 1f;
			if (ai[2] > (float)vineTimeExtend)
			{
				vineLength = vineLengthLong;
				if (ai[2] > (float)vineTimeMax)
				{
					ai[2] = 0f;
				}
			}
			Vector2 vector2 = isTilePos ? new Vector2(ai[0] * 16f + 8f, ai[1] * 16f + 8f) : new Vector2(ai[0], ai[1]);
			Vector2 vector3 = Main.player[npc.target].Center + targetOffset;
			float num3 = vector3.X - (float)(npc.width / 2) - vector2.X;
			float num4 = vector3.Y - (float)(npc.height / 2) - vector2.Y;
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			if (num5 > vineLength)
			{
				num5 = vineLength / num5;
				num3 *= num5;
				num4 *= num5;
			}
			if (npc.position.X < vector2.X + num3)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
				if (npc.velocity.X < 0f && num3 > 0f)
				{
					npc.velocity.X = npc.velocity.X + moveInterval * 1.5f;
				}
			}
			else if (npc.position.X > vector2.X + num3)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
				if (npc.velocity.X > 0f && num3 < 0f)
				{
					npc.velocity.X = npc.velocity.X - moveInterval * 1.5f;
				}
			}
			if (npc.position.Y < vector2.Y + num4)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
				if (npc.velocity.Y < 0f && num4 > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + moveInterval * 1.5f;
				}
			}
			else if (npc.position.Y > vector2.Y + num4)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
				if (npc.velocity.Y > 0f && num4 < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval * 1.5f;
				}
			}
			npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
			if (num3 > 0f)
			{
				npc.spriteDirection = 1;
				npc.rotation = (float)Math.Atan2((double)num4, (double)num3);
			}
			else if (num3 < 0f)
			{
				npc.spriteDirection = -1;
				npc.rotation = (float)Math.Atan2((double)num4, (double)num3) + 3.14f;
			}
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -0.7f;
				npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -0.7f;
				npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
			}
		}

		public static void AIWorm(NPC npc, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool flag = false;
			BaseAI.AIWorm(npc, ref flag, wormTypes, wormLength, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			int[] array = new int[(wormTypes.Length == 1) ? 1 : wormLength];
			array[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				array[array.Length - 1] = wormTypes[2];
				for (int i = 1; i < array.Length - 1; i++)
				{
					array[i] = wormTypes[1];
				}
			}
			BaseAI.AIWorm(npc, ref isDigging, array, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool flag = false;
			BaseAI.AIWorm(npc, ref flag, wormTypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool flag = wormTypes.Length == 1;
			int num = wormTypes.Length;
			if (split)
			{
				npc.realLife = -1;
			}
			else if (npc.ai[3] > 0f)
			{
				npc.realLife = (int)npc.ai[3];
			}
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			if (Main.player[npc.target].dead && npc.timeLeft > 300)
			{
				npc.timeLeft = 300;
			}
			if (Main.netMode != 1)
			{
				if (!flag)
				{
					if (fly && npc.type == wormTypes[0] && npc.ai[0] == 0f)
					{
						npc.ai[3] = (float)npc.whoAmI;
						npc.realLife = npc.whoAmI;
						int num2 = npc.whoAmI;
						for (int i = 1; i < num - 1; i++)
						{
							int num3 = (i == num) ? wormTypes[wormTypes.Length - 1] : wormTypes[i];
							int num4 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, num3, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
							Main.npc[num4].ai[3] = (float)npc.whoAmI;
							Main.npc[num4].realLife = npc.whoAmI;
							Main.npc[num4].ai[1] = (float)num2;
							Main.npc[num2].ai[0] = (float)num4;
							NetMessage.SendData(23, -1, -1, NetworkText.FromLiteral(""), num4, 0f, 0f, 0f, 0, 0, 0);
							num2 = num4;
						}
						npc.netUpdate = true;
					}
					else if ((npc.type == wormTypes[0] || (npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1])) && npc.ai[0] == 0f)
					{
						if (npc.type == wormTypes[0])
						{
							if (!split)
							{
								npc.ai[3] = (float)npc.whoAmI;
								npc.realLife = npc.whoAmI;
							}
							npc.ai[2] = (float)(num - 1);
							int num5 = (wormTypes.Length <= 2) ? wormTypes[wormTypes.Length - 1] : wormTypes[1];
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, num5, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
						}
						else if (npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1] && npc.ai[2] > 0f)
						{
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, wormTypes[num - (int)npc.ai[2]], npc.whoAmI, 0f, 0f, 0f, 0f, 255);
						}
						else
						{
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, wormTypes[wormTypes.Length - 1], npc.whoAmI, 0f, 0f, 0f, 0f, 255);
						}
						if (!split)
						{
							Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
							Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
						}
						Main.npc[(int)npc.ai[0]].ai[1] = (float)npc.whoAmI;
						Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;
						npc.netUpdate = true;
					}
				}
				if (!flag && split)
				{
					if (!Main.npc[(int)npc.ai[1]].active && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type == wormTypes[0] && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type == wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[1]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[1]].active)
					{
						int type = npc.type;
						npc.type = wormTypes[0];
						int whoAmI = npc.whoAmI;
						float num6 = (float)npc.life / (float)npc.lifeMax;
						float num7 = npc.ai[0];
						npc.SetDefaults(npc.type, -1f);
						npc.life = (int)((float)npc.lifeMax * num6);
						npc.ai[0] = num7;
						npc.TargetClosest(true);
						npc.netUpdate = true;
						npc.whoAmI = whoAmI;
						if (onChangeType != null)
						{
							onChangeType(npc, type, true);
						}
					}
					else if (npc.type != wormTypes[0] && npc.type != wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[0]].active)
					{
						int type2 = npc.type;
						npc.type = wormTypes[wormTypes.Length - 1];
						int whoAmI2 = npc.whoAmI;
						float num8 = (float)npc.life / (float)npc.lifeMax;
						float num9 = npc.ai[1];
						npc.SetDefaults(npc.type, -1f);
						npc.life = (int)((float)npc.lifeMax * num8);
						npc.ai[1] = num9;
						npc.TargetClosest(true);
						npc.netUpdate = true;
						npc.whoAmI = whoAmI2;
						if (onChangeType != null)
						{
							onChangeType(npc, type2, false);
						}
					}
				}
				else if (!flag)
				{
					if (npc.type != wormTypes[0] && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type != wormTypes[wormTypes.Length - 1] && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
				}
				if (!npc.active && Main.netMode == 2)
				{
					NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, 0f, 0f, -1, 0, 0);
				}
			}
			int num10 = (int)(npc.position.X / 16f) - 1;
			int num11 = (int)(npc.Center.X / 16f) + 2;
			int num12 = (int)(npc.position.Y / 16f) - 1;
			int num13 = (int)(npc.Center.Y / 16f) + 2;
			if (num10 < 0)
			{
				num10 = 0;
			}
			if (num11 > Main.maxTilesX)
			{
				num11 = Main.maxTilesX;
			}
			if (num12 < 0)
			{
				num12 = 0;
			}
			if (num13 > Main.maxTilesY)
			{
				num13 = Main.maxTilesY;
			}
			bool flag2 = false;
			if (fly || ignoreTiles)
			{
				flag2 = true;
			}
			if (!flag2 || spawnTileDust)
			{
				for (int j = num10; j < num11; j++)
				{
					for (int k = num12; k < num13; k++)
					{
						if (Main.tile[j, k] != null && ((Main.tile[j, k].nactive() && (Main.tileSolid[(int)Main.tile[j, k].type] || (Main.tileSolidTop[(int)Main.tile[j, k].type] && Main.tile[j, k].frameY == 0))) || Main.tile[j, k].liquid > 64))
						{
							Vector2 vector;
							vector.X = (float)(j * 16);
							vector.Y = (float)(k * 16);
							if (npc.position.X + (float)npc.width > vector.X && npc.position.X < vector.X + 16f && npc.position.Y + (float)npc.height > vector.Y && npc.position.Y < vector.Y + 16f)
							{
								flag2 = true;
								if (spawnTileDust && Main.rand.Next(100) == 0 && Main.tile[j, k].nactive())
								{
									WorldGen.KillTile(j, k, true, true, false);
								}
							}
						}
					}
				}
			}
			if (!flag2 && npc.type == wormTypes[0])
			{
				Rectangle rectangle;
				rectangle..ctor((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
				int num14 = 1000;
				bool flag3 = true;
				for (int l = 0; l < 255; l++)
				{
					if (Main.player[l].active)
					{
						Rectangle rectangle2;
						rectangle2..ctor((int)Main.player[l].position.X - num14, (int)Main.player[l].position.Y - num14, num14 * 2, num14 * 2);
						if (rectangle.Intersects(rectangle2))
						{
							flag3 = false;
							break;
						}
					}
				}
				if (flag3)
				{
					flag2 = true;
				}
			}
			if (fly)
			{
				if (npc.velocity.X < 0f)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.spriteDirection = -1;
				}
			}
			Vector2 center = npc.Center;
			float num15 = Main.player[npc.target].Center.X;
			float num16 = Main.player[npc.target].Center.Y;
			num15 = (float)((int)(num15 / 16f) * 16);
			num16 = (float)((int)(num16 / 16f) * 16);
			center.X = (float)((int)(center.X / 16f) * 16);
			center.Y = (float)((int)(center.Y / 16f) * 16);
			num15 -= center.X;
			num16 -= center.Y;
			float num17 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
			isDigging = flag2;
			if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					center = npc.Center;
					num15 = Main.npc[(int)npc.ai[1]].Center.X - center.X;
					num16 = Main.npc[(int)npc.ai[1]].Center.Y - center.Y;
				}
				catch
				{
				}
				if (!rotateAverage || npc.type == wormTypes[0])
				{
					npc.rotation = (float)Math.Atan2((double)num16, (double)num15) + 1.57f;
				}
				else
				{
					NPC npc2 = Main.npc[(int)npc.ai[1]];
					Vector2 endPos = BaseUtility.RotateVector(npc2.Center, npc2.Center + new Vector2(0f, 30f), npc2.rotation);
					npc.rotation = BaseUtility.RotationTo(npc.Center, endPos) + 1.57f;
				}
				num17 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
				num17 = (num17 - (float)npc.width - partDistanceAddon) / num17;
				num15 *= num17;
				num16 *= num17;
				npc.velocity = default(Vector2);
				npc.position.X = npc.position.X + num15;
				npc.position.Y = npc.position.Y + num16;
				if (fly)
				{
					if (num15 < 0f)
					{
						npc.spriteDirection = 1;
						return;
					}
					if (num15 > 0f)
					{
						npc.spriteDirection = -1;
						return;
					}
				}
			}
			else
			{
				if (!flag2)
				{
					npc.TargetClosest(true);
					npc.velocity.Y = npc.velocity.Y + 0.11f;
					if (npc.velocity.Y > maxSpeed)
					{
						npc.velocity.Y = maxSpeed;
					}
					if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.4)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X = npc.velocity.X - gravityResist * 1.1f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X + gravityResist * 1.1f;
						}
					}
					else if (npc.velocity.Y == maxSpeed)
					{
						if (npc.velocity.X < num15)
						{
							npc.velocity.X = npc.velocity.X + gravityResist;
						}
						else if (npc.velocity.X > num15)
						{
							npc.velocity.X = npc.velocity.X - gravityResist;
						}
					}
					else if (npc.velocity.Y > 4f)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X = npc.velocity.X + gravityResist * 0.9f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X - gravityResist * 0.9f;
						}
					}
				}
				else
				{
					if (soundEffects && npc.soundDelay == 0)
					{
						float num18 = num17 / 40f;
						if (num18 < 10f)
						{
							num18 = 10f;
						}
						if (num18 > 20f)
						{
							num18 = 20f;
						}
						npc.soundDelay = (int)num18;
						Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
					}
					num17 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
					float num19 = Math.Abs(num15);
					float num20 = Math.Abs(num16);
					float num21 = maxSpeed / num17;
					num15 *= num21;
					num16 *= num21;
					bool flag4 = false;
					if (fly)
					{
						if (((npc.velocity.X > 0f && num15 < 0f) || (npc.velocity.X < 0f && num15 > 0f) || (npc.velocity.Y > 0f && num16 < 0f) || (npc.velocity.Y < 0f && num16 > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > gravityResist / 2f && num17 < 300f)
						{
							flag4 = true;
							if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed)
							{
								npc.velocity *= 1.1f;
							}
						}
						if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
						{
							flag4 = true;
							if (Math.Abs(npc.velocity.X) < maxSpeed / 2f)
							{
								if (npc.velocity.X == 0f)
								{
									npc.velocity.X = npc.velocity.X - (float)npc.direction;
								}
								npc.velocity.X = npc.velocity.X * 1.1f;
							}
							else if (npc.velocity.Y > -maxSpeed)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist;
							}
						}
					}
					if (!flag4)
					{
						if ((npc.velocity.X > 0f && num15 > 0f) || (npc.velocity.X < 0f && num15 < 0f) || (npc.velocity.Y > 0f && num16 > 0f) || (npc.velocity.Y < 0f && num16 < 0f))
						{
							if (npc.velocity.X < num15)
							{
								npc.velocity.X = npc.velocity.X + gravityResist;
							}
							else if (npc.velocity.X > num15)
							{
								npc.velocity.X = npc.velocity.X - gravityResist;
							}
							if (npc.velocity.Y < num16)
							{
								npc.velocity.Y = npc.velocity.Y + gravityResist;
							}
							else if (npc.velocity.Y > num16)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist;
							}
							if ((double)Math.Abs(num16) < (double)maxSpeed * 0.2 && ((npc.velocity.X > 0f && num15 < 0f) || (npc.velocity.X < 0f && num15 > 0f)))
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + gravityResist * 2f;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - gravityResist * 2f;
								}
							}
							if ((double)Math.Abs(num15) < (double)maxSpeed * 0.2 && ((npc.velocity.Y > 0f && num16 < 0f) || (npc.velocity.Y < 0f && num16 > 0f)))
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X + gravityResist * 2f;
								}
								else
								{
									npc.velocity.X = npc.velocity.X - gravityResist * 2f;
								}
							}
						}
						else if (num19 > num20)
						{
							if (npc.velocity.X < num15)
							{
								npc.velocity.X = npc.velocity.X + gravityResist * 1.1f;
							}
							else if (npc.velocity.X > num15)
							{
								npc.velocity.X = npc.velocity.X - gravityResist * 1.1f;
							}
							if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + gravityResist;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - gravityResist;
								}
							}
						}
						else
						{
							if (npc.velocity.Y < num16)
							{
								npc.velocity.Y = npc.velocity.Y + gravityResist * 1.1f;
							}
							else if (npc.velocity.Y > num16)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist * 1.1f;
							}
							if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X + gravityResist;
								}
								else
								{
									npc.velocity.X = npc.velocity.X - gravityResist;
								}
							}
						}
					}
				}
				if (!rotateAverage || npc.type == wormTypes[0])
				{
					npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				}
				else
				{
					NPC npc3 = Main.npc[(int)npc.ai[1]];
					Vector2 endPos2 = BaseUtility.RotateVector(npc3.Center, npc3.Center + new Vector2(0f, 30f), npc3.rotation);
					npc.rotation = BaseUtility.RotationTo(npc.Center, endPos2) + 1.57f;
				}
				if (npc.type == wormTypes[0])
				{
					if (flag2)
					{
						if (npc.localAI[0] != 1f)
						{
							npc.netUpdate = true;
						}
						npc.localAI[0] = 1f;
					}
					else
					{
						if (npc.localAI[0] != 0f)
						{
							npc.netUpdate = true;
						}
						npc.localAI[0] = 0f;
					}
					if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
					{
						npc.netUpdate = true;
					}
				}
			}
		}

		public static void AITeleporter(NPC npc, ref float[] ai, bool checkGround = true, bool immobile = true, int distFromPlayer = 20, int teleportInterval = 650, int attackInterval = 100, int stopAttackInterval = 500, bool delayOnHit = true, Action<bool> TeleportEffects = null, Func<int, int, bool> CanTeleportTo = null, Action Attack = null)
		{
			npc.TargetClosest(true);
			if (immobile)
			{
				npc.velocity.X = npc.velocity.X * 0.93f;
				if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
				{
					npc.velocity.X = 0f;
				}
			}
			if (ai[0] == 0f)
			{
				ai[0] = (float)Math.Max(0, Math.Max(teleportInterval, teleportInterval - 150));
			}
			if (ai[2] != 0f && ai[3] != 0f)
			{
				if (TeleportEffects != null)
				{
					TeleportEffects(true);
				}
				npc.position.X = ai[2] * 16f - (float)(npc.width / 2) + 8f;
				npc.position.Y = ai[3] * 16f - (float)npc.height;
				npc.velocity.X = 0f;
				npc.velocity.Y = 0f;
				ai[2] = 0f;
				ai[3] = 0f;
				if (TeleportEffects != null)
				{
					TeleportEffects(false);
				}
			}
			if (npc.justHit)
			{
				ai[0] = 0f;
			}
			ai[0] += 1f;
			if (attackInterval != -1 && ai[0] < (float)stopAttackInterval && ai[0] % (float)attackInterval == 0f)
			{
				ai[1] = 30f;
				npc.netUpdate = true;
			}
			else if (ai[0] >= (float)teleportInterval && Main.netMode != 1)
			{
				ai[0] = 1f;
				int num = (int)Main.player[npc.target].position.X / 16;
				int num2 = (int)Main.player[npc.target].position.Y / 16;
				int num3 = (int)npc.position.X / 16;
				int num4 = (int)npc.position.Y / 16;
				int num5 = 0;
				bool flag = false;
				if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 2000f)
				{
					num5 = 100;
					flag = true;
				}
				while (!flag && num5 < 100)
				{
					num5++;
					int num6 = Main.rand.Next(num - distFromPlayer, num + distFromPlayer);
					int num7 = Main.rand.Next(num2 - distFromPlayer, num2 + distFromPlayer);
					for (int i = num7; i < num2 + distFromPlayer; i++)
					{
						if ((i < num2 - 4 || i > num2 + 4 || num6 < num - 4 || num6 > num + 4) && (i < num4 - 1 || i > num4 + 1 || num6 < num3 - 1 || num6 > num3 + 1) && (!checkGround || Main.tile[num6, i].nactive()) && ((CanTeleportTo != null && CanTeleportTo(num6, i)) || (!Main.tile[num6, i - 1].lava() && (!checkGround || Main.tileSolid[(int)Main.tile[num6, i].type]) && !Collision.SolidTiles(num6 - 1, num6 + 1, i - 4, i - 1))))
						{
							if (attackInterval != -1)
							{
								ai[1] = 20f;
							}
							ai[2] = (float)num6;
							ai[3] = (float)i;
							flag = true;
							break;
						}
					}
				}
				npc.netUpdate = true;
			}
			if (Attack != null && attackInterval != -1 && ai[1] > 0f)
			{
				ai[1] -= 1f;
				if (ai[1] == 25f)
				{
					Attack();
				}
			}
		}

		public static void AIFish(NPC npc, ref float[] ai, bool hostile = false, bool ignoreNonWetPlayer = true, bool ignoreWater = false, float velMaxX = 3f, float velMaxY = 2f)
		{
			if (hostile && npc.direction == 0)
			{
				npc.TargetClosest(true);
			}
			if (ignoreWater || npc.wet)
			{
				bool flag = false;
				if (hostile)
				{
					npc.TargetClosest(false);
					if ((!ignoreNonWetPlayer || Main.player[npc.target].wet) && !Main.player[npc.target].dead)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (npc.collideX)
					{
						npc.velocity.X = npc.velocity.X * -1f;
						npc.direction *= -1;
						npc.netUpdate = true;
					}
					if (npc.collideY)
					{
						npc.netUpdate = true;
						int num = (npc.velocity.Y > 0f) ? -1 : 1;
						npc.velocity.Y = Math.Abs(npc.velocity.Y) * (float)num;
						npc.directionY = num;
						ai[0] = 1f * (float)num;
					}
				}
				if (flag)
				{
					npc.TargetClosest(true);
					npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
					npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * 0.1f;
					if (npc.velocity.X > velMaxX)
					{
						npc.velocity.X = velMaxX;
					}
					if (npc.velocity.X < -velMaxX)
					{
						npc.velocity.X = -velMaxX;
					}
					if (npc.velocity.Y > velMaxY)
					{
						npc.velocity.Y = velMaxY;
					}
					if (npc.velocity.Y < -velMaxY)
					{
						npc.velocity.Y = -velMaxY;
					}
				}
				else
				{
					npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
					if (npc.velocity.X < -1f || npc.velocity.X > 1f)
					{
						npc.velocity.X = npc.velocity.X * 0.95f;
					}
					if (ai[0] == -1f)
					{
						npc.velocity.Y = npc.velocity.Y - 0.01f;
						if ((double)npc.velocity.Y < -0.3)
						{
							ai[0] = 1f;
						}
					}
					else
					{
						npc.velocity.Y = npc.velocity.Y + 0.01f;
						if ((double)npc.velocity.Y > 0.3)
						{
							ai[0] = -1f;
						}
					}
					int num2 = (int)(npc.Center.X / 16f);
					int num3 = (int)(npc.Center.Y / 16f);
					for (int i = -1; i < 3; i++)
					{
						if (Main.tile[num2, num3 + i] == null)
						{
							Main.tile[num2, num3 + i] = new Tile();
						}
					}
					if (Main.tile[num2, num3 - 1].liquid > 128 && (Main.tile[num2, num3 + 1].nactive() || Main.tile[num2, num3 + 2].nactive()))
					{
						ai[0] = -1f;
					}
					if (npc.velocity.Y > velMaxY || npc.velocity.Y < -velMaxY)
					{
						npc.velocity.Y = npc.velocity.Y * 0.95f;
					}
				}
			}
			else
			{
				if (Main.netMode != 1 && npc.velocity.Y == 0f)
				{
					npc.velocity.Y = (float)Main.rand.Next(-50, -20) * 0.1f;
					npc.velocity.X = (float)Main.rand.Next(-20, 20) * 0.1f;
					npc.netUpdate = true;
				}
				npc.velocity.Y = npc.velocity.Y + 0.3f;
				if (npc.velocity.Y > 10f)
				{
					npc.velocity.Y = 10f;
				}
				ai[0] = 1f;
			}
			npc.rotation = npc.velocity.Y * (float)npc.direction * 0.1f;
			if ((double)npc.rotation < -0.2)
			{
				npc.rotation = -0.2f;
			}
			if ((double)npc.rotation > 0.2)
			{
				npc.rotation = 0.2f;
			}
		}

		public static void AIZombie(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
		{
			bool flag = false;
			if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
			{
				flag = true;
			}
			if (npc.position.X == npc.oldPosition.X || ai[3] >= (float)ticksUntilBoredom || flag)
			{
				ai[3] += 1f;
			}
			else if ((double)Math.Abs(npc.velocity.X) > 0.9 && ai[3] > 0f)
			{
				ai[3] -= 1f;
			}
			if (ai[3] > (float)(ticksUntilBoredom * 10))
			{
				ai[3] = 0f;
			}
			if (npc.justHit)
			{
				ai[3] = 0f;
			}
			if (ai[3] == (float)ticksUntilBoredom)
			{
				npc.netUpdate = true;
			}
			bool flag2 = ai[3] < (float)ticksUntilBoredom;
			if (targetPlayers && (!fleeWhenDay || !Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0) && ((fleeWhenDay && Main.dayTime) ? flag2 : (!allowBoredom || flag2)))
			{
				npc.TargetClosest(true);
			}
			else if (ai[2] <= 0f)
			{
				if (fleeWhenDay && Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						ai[0] += 1f;
						if (ai[0] >= 2f)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							ai[0] = 0f;
						}
					}
				}
				else
				{
					ai[0] = 0f;
				}
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			if (npc.velocity.X < -velMax || npc.velocity.X > velMax)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else if (npc.velocity.X < velMax && npc.direction == 1)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
				if (npc.velocity.X > velMax)
				{
					npc.velocity.X = velMax;
				}
			}
			else if (npc.velocity.X > -velMax && npc.direction == -1)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
				if (npc.velocity.X < -velMax)
				{
					npc.velocity.X = -velMax;
				}
			}
			BaseAI.WalkupHalfBricks(npc);
			if (openDoors != -1 && BaseAI.AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], (float)ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
			{
				npc.velocity.X = 0f;
			}
			else if (openDoors != -1)
			{
				ai[1] = 0f;
				ai[2] = 0f;
			}
			if (BaseAI.HitTileOnSide(npc, 3, true) && ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1)))
			{
				Vector2 vector = BaseAI.AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, (float)npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, (jumpUpPlatforms && flag2) ? Main.player[npc.target] : null, ignoreJumpTiles);
				if (!npc.noTileCollide)
				{
					vector = Collision.TileCollision(npc.position, vector, npc.width, npc.height, false, false, 1);
					Vector4 vector2 = Collision.SlopeCollision(npc.position, vector, npc.width, npc.height, 0f, false);
					Vector2 vector3;
					vector3..ctor(vector2.Z, vector2.W);
					if (onTileCollide != null && npc.velocity != vector3)
					{
						onTileCollide(npc.velocity.X != vector3.X, npc.velocity.Y != vector3.Y, npc.velocity, vector3);
					}
					npc.position = new Vector2(vector2.X, vector2.Y);
					npc.velocity = vector3;
				}
				if (npc.velocity != vector)
				{
					npc.velocity = vector;
					npc.netUpdate = true;
				}
			}
		}

		public static void AIEye(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool ignoreWet = false, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f, float bounceScalarX = 1f, float bounceScalarY = 1f)
		{
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
				npc.velocity.X = npc.velocity.X * bounceScalarX;
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
				npc.velocity.Y = npc.velocity.Y * bounceScalarY;
			}
			if (fleeWhenDay && Main.dayTime && (double)npc.position.Y <= Main.worldSurface * 16.0)
			{
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				npc.directionY = -1;
				if (npc.velocity.Y > 0f)
				{
					npc.direction = 1;
				}
				npc.direction = -1;
				if (npc.velocity.X > 0f)
				{
					npc.direction = 1;
				}
			}
			else
			{
				npc.TargetClosest(true);
				if (Main.player[npc.target].dead)
				{
					if (npc.timeLeft > 10)
					{
						npc.timeLeft = 10;
					}
					npc.directionY = -1;
					if (npc.velocity.Y > 0f)
					{
						npc.direction = 1;
					}
					npc.direction = -1;
					if (npc.velocity.X > 0f)
					{
						npc.direction = 1;
					}
				}
			}
			if (npc.direction == -1 && npc.velocity.X > -velMaxX)
			{
				npc.velocity.X = npc.velocity.X - moveIntervalX;
				if (npc.velocity.X > 4f)
				{
					npc.velocity.X = npc.velocity.X - 0.1f;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + 0.05f;
				}
				if (npc.velocity.X < -4f)
				{
					npc.velocity.X = -velMaxX;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < velMaxX)
			{
				npc.velocity.X = npc.velocity.X + moveIntervalX;
				if (npc.velocity.X < -velMaxX)
				{
					npc.velocity.X = npc.velocity.X + 0.1f;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - 0.05f;
				}
				if (npc.velocity.X > velMaxX)
				{
					npc.velocity.X = velMaxX;
				}
			}
			if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)velMaxY))
			{
				npc.velocity.Y = npc.velocity.Y - moveIntervalY;
				if ((double)npc.velocity.Y > (double)velMaxY)
				{
					npc.velocity.Y = npc.velocity.Y - 0.05f;
				}
				else if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + 0.03f;
				}
				if ((double)npc.velocity.Y < (double)(-(double)velMaxY))
				{
					npc.velocity.Y = -velMaxY;
				}
			}
			else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)velMaxY)
			{
				npc.velocity.Y = npc.velocity.Y + moveIntervalY;
				if ((double)npc.velocity.Y < (double)(-(double)velMaxY))
				{
					npc.velocity.Y = npc.velocity.Y + 0.05f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - 0.03f;
				}
				if ((double)npc.velocity.Y > (double)velMaxY)
				{
					npc.velocity.Y = velMaxY;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.95f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.5f;
				if (npc.velocity.Y < -velMaxY * 1.5f)
				{
					npc.velocity.Y = -velMaxY * 1.5f;
				}
				npc.TargetClosest(true);
			}
		}

		public static void AISlime(NPC npc, ref float[] ai, bool fleeWhenDay = false, int jumpTime = 200, float jumpVelX = 2f, float jumpVelY = 6f, float jumpVelHighX = 3f, float jumpVelHighY = 8f)
		{
			bool flag = false;
			if ((fleeWhenDay && !Main.dayTime) || npc.life != npc.lifeMax || (double)npc.position.Y > Main.worldSurface * 16.0)
			{
				flag = true;
			}
			if (ai[2] > 1f)
			{
				ai[2] -= 1f;
			}
			if (npc.wet)
			{
				if (npc.collideY)
				{
					npc.velocity.Y = -2f;
				}
				if (npc.velocity.Y < 0f && ai[3] == npc.position.X)
				{
					npc.direction *= -1;
					ai[2] = 200f;
				}
				if (npc.velocity.Y > 0f)
				{
					ai[3] = npc.position.X;
				}
				if (npc.velocity.Y > 2f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.9f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.5f;
				if (npc.velocity.Y < -4f)
				{
					npc.velocity.Y = -4f;
				}
				if (ai[2] == 1f && flag)
				{
					npc.TargetClosest(true);
				}
			}
			npc.aiAction = 0;
			if (ai[2] == 0f)
			{
				ai[0] = -100f;
				ai[2] = 1f;
				npc.TargetClosest(true);
			}
			if (npc.velocity.Y == 0f)
			{
				if (ai[3] == npc.position.X)
				{
					npc.direction *= -1;
					ai[2] = 200f;
				}
				ai[3] = 0f;
				npc.velocity.X = npc.velocity.X * 0.8f;
				if (npc.velocity.X > -0.1f && npc.velocity.X < 0.1f)
				{
					npc.velocity.X = 0f;
				}
				if (flag)
				{
					ai[0] += 1f;
				}
				ai[0] += 1f;
				if (ai[0] >= 0f)
				{
					npc.netUpdate = true;
					if (ai[2] == 1f && flag)
					{
						npc.TargetClosest(true);
					}
					if (ai[1] == 2f)
					{
						npc.velocity.Y = jumpVelHighY;
						npc.velocity.X = npc.velocity.X + jumpVelHighX * (float)npc.direction;
						ai[0] = (float)(-(float)jumpTime);
						ai[1] = 0f;
						ai[3] = npc.position.X;
						return;
					}
					npc.velocity.Y = jumpVelY;
					npc.velocity.X = npc.velocity.X + jumpVelX * (float)npc.direction;
					ai[0] = (float)(-(float)jumpTime) - 80f;
					ai[1] += 1f;
					return;
				}
				else if (ai[0] >= -30f)
				{
					npc.aiAction = 1;
					return;
				}
			}
			else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
			{
				if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
				{
					npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
					return;
				}
				npc.velocity.X = npc.velocity.X * 0.93f;
			}
		}

		public static void WalkupHalfBricks(NPC npc)
		{
			BaseAI.WalkupHalfBricks(npc, ref npc.gfxOffY, ref npc.stepSpeed);
		}

		public static void WalkupHalfBricks(Entity codable, ref float gfxOffY, ref float stepSpeed)
		{
			if (codable.velocity.Y >= 0f)
			{
				int num = 0;
				if (codable.velocity.X < 0f)
				{
					num = -1;
				}
				if (codable.velocity.X > 0f)
				{
					num = 1;
				}
				Vector2 position = codable.position;
				position.X += codable.velocity.X;
				int num2 = (int)(((double)position.X + (double)(codable.width / 2) + (double)((codable.width / 2 + 1) * num)) / 16.0);
				int num3 = (int)(((double)position.Y + (double)codable.height - 1.0) / 16.0);
				if (Main.tile[num2, num3] == null)
				{
					Main.tile[num2, num3] = new Tile();
				}
				if (Main.tile[num2, num3 - 1] == null)
				{
					Main.tile[num2, num3 - 1] = new Tile();
				}
				if (Main.tile[num2, num3 - 2] == null)
				{
					Main.tile[num2, num3 - 2] = new Tile();
				}
				if (Main.tile[num2, num3 - 3] == null)
				{
					Main.tile[num2, num3 - 3] = new Tile();
				}
				if (Main.tile[num2, num3 + 1] == null)
				{
					Main.tile[num2, num3 + 1] = new Tile();
				}
				if (Main.tile[num2 - num, num3 - 3] == null)
				{
					Main.tile[num2 - num, num3 - 3] = new Tile();
				}
				if ((double)(num2 * 16) >= (double)position.X + (double)codable.width || (double)(num2 * 16 + 16) <= (double)position.X || ((!Main.tile[num2, num3].nactive() || Main.tile[num2, num3].slope() != 0 || Main.tile[num2, num3 - 1].slope() != 0 || !Main.tileSolid[(int)Main.tile[num2, num3].type] || Main.tileSolidTop[(int)Main.tile[num2, num3].type]) && (!Main.tile[num2, num3 - 1].halfBrick() || !Main.tile[num2, num3 - 1].nactive())) || (Main.tile[num2, num3 - 1].nactive() && Main.tileSolid[(int)Main.tile[num2, num3 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num2, num3 - 1].type] && (!Main.tile[num2, num3 - 1].halfBrick() || (Main.tile[num2, num3 - 4].nactive() && Main.tileSolid[(int)Main.tile[num2, num3 - 4].type] && !Main.tileSolidTop[(int)Main.tile[num2, num3 - 4].type]))) || (Main.tile[num2, num3 - 2].nactive() && Main.tileSolid[(int)Main.tile[num2, num3 - 2].type] && !Main.tileSolidTop[(int)Main.tile[num2, num3 - 2].type]) || (Main.tile[num2, num3 - 3].nactive() && Main.tileSolid[(int)Main.tile[num2, num3 - 3].type] && !Main.tileSolidTop[(int)Main.tile[num2, num3 - 3].type]) || (Main.tile[num2 - num, num3 - 3].nactive() && Main.tileSolid[(int)Main.tile[num2 - num, num3 - 3].type]))
				{
					gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
					return;
				}
				float num4 = (float)(num3 * 16);
				if (Main.tile[num2, num3].halfBrick())
				{
					num4 += 8f;
				}
				if (Main.tile[num2, num3 - 1].halfBrick())
				{
					num4 -= 8f;
				}
				if ((double)num4 >= (double)position.Y + (double)codable.height)
				{
					gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
					return;
				}
				float num5 = position.Y + (float)codable.height - num4;
				float num6 = 16.1f;
				if ((double)num5 <= (double)num6)
				{
					gfxOffY += codable.position.Y + (float)codable.height - num4;
					codable.position.Y = num4 - (float)codable.height;
					stepSpeed = (((double)num5 >= 9.0) ? 2f : 1f);
					return;
				}
			}
			else
			{
				gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
			}
		}

		public static Vector2 AttemptJump(Vector2 position, Vector2 velocity, int width, int height, int direction, float directionY = 0f, int tileDistX = 3, int tileDistY = 4, float maxSpeedX = 1f, bool jumpUpPlatforms = false, Entity target = null, bool ignoreTiles = false)
		{
			Vector2 result;
			try
			{
				tileDistX -= 2;
				Vector2 vector = velocity;
				int num = Math.Max(10, Math.Min(Main.maxTilesX - 10, (int)((position.X + (float)width * 0.5f + ((float)width * 0.5f + 8f) * (float)direction) / 16f)));
				int num2 = Math.Max(10, Math.Min(Main.maxTilesY - 10, (int)((position.Y + (float)height - 15f) / 16f)));
				int num3 = Math.Max(10, Math.Min(Main.maxTilesX - 10, num + direction * tileDistX));
				int num4 = Math.Max(10, Math.Min(Main.maxTilesY - 10, num2 - tileDistY));
				int num5 = num2;
				int num6 = (int)((float)height / 16f);
				if (height > num6 * 16)
				{
					num6++;
				}
				Rectangle rectangle;
				rectangle..ctor((int)position.X, (int)position.Y, width, height);
				if (ignoreTiles && target != null && Math.Abs(position.X + (float)width * 0.5f - target.Center.X) < (float)(width + 120))
				{
					float num7 = (float)((int)Math.Abs(position.Y + (float)height * 0.5f - target.Center.Y) / 16);
					if (num7 < (float)(tileDistY + 2))
					{
						vector.Y = -8f + num7 * -0.5f;
					}
				}
				if (vector.Y == velocity.Y)
				{
					for (int i = num2; i >= num4; i--)
					{
						Tile tile = Main.tile[num, i];
						Tile tile2 = Main.tile[Math.Min(Main.maxTilesX, num - direction), i];
						if (tile == null)
						{
							tile = (Main.tile[num, i] = new Tile());
						}
						if (tile2 == null)
						{
							tile2 = (Main.tile[Math.Min(Main.maxTilesX, num - direction), i] = new Tile());
						}
						if (tile.nactive() && (i != num2 || (!tile.halfBrick() && tile.slope() == 0)) && Main.tileSolid[(int)tile.type] && (jumpUpPlatforms || !Main.tileSolidTop[(int)tile.type]))
						{
							if (!Main.tileSolidTop[(int)tile.type])
							{
								Rectangle rectangle2;
								rectangle2..ctor(num * 16, i * 16, 16, 16);
								rectangle2.Y = rectangle.Y;
								if (rectangle2.Intersects(rectangle))
								{
									vector = velocity;
									break;
								}
							}
							if (tile2.nactive() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
							{
								vector = velocity;
								break;
							}
							if (target == null || (float)(i * 16) >= target.Center.Y)
							{
								num5 = i;
								vector.Y = -(5f + (float)(num2 - i) * ((num2 - i > 3) ? (1f - (float)(num2 - i - 2) * 0.0525f) : 1f));
							}
						}
						else if (num5 - i >= num6)
						{
							break;
						}
					}
				}
				if (vector.Y == velocity.Y)
				{
					if (Main.tile[num, num2 + 1] == null)
					{
						Main.tile[num, num2 + 1] = new Tile();
					}
					if (Main.tile[num + direction, num2 + 1] == null)
					{
						Main.tile[num, num2 + 1] = new Tile();
					}
					if (Main.tile[num + direction, num2 + 2] == null)
					{
						Main.tile[num, num2 + 2] = new Tile();
					}
					if (directionY < 0f && (!Main.tile[num, num2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num, num2 + 1].type]) && (!Main.tile[num + direction, num2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num + direction, num2 + 1].type]) && (!Main.tile[num + direction, num2 + 2].nactive() || !Main.tileSolid[(int)Main.tile[num, num2 + 2].type] || target == null || target.Center.Y + (float)target.height * 0.25f < (float)num2 * 16f))
					{
						vector.Y = -8f;
						vector.X *= 1.5f * (1f / maxSpeedX);
						if (num <= num3)
						{
							for (int j = num; j < num3; j++)
							{
								Tile tile3 = Main.tile[j, num2 + 1];
								if (tile3 == null)
								{
									tile3 = (Main.tile[j, num2 + 1] = new Tile());
								}
								if (j != num && !tile3.nactive())
								{
									vector.Y -= 0.0325f;
									vector.X += (float)direction * 0.255f;
								}
							}
						}
						else if (num > num3)
						{
							for (int k = num3; k < num; k++)
							{
								Tile tile4 = Main.tile[k, num2 + 1];
								if (tile4 == null)
								{
									tile4 = (Main.tile[k, num2 + 1] = new Tile());
								}
								if (k != num3 && !tile4.nactive())
								{
									vector.Y -= 0.0325f;
									vector.X += (float)direction * 0.255f;
								}
							}
						}
					}
				}
				result = vector;
			}
			catch (Exception ex)
			{
				ErrorLogger.Log(ex.Message);
				ErrorLogger.Log(ex.StackTrace);
				result = velocity;
			}
			return result;
		}

		public static bool AttemptOpenDoor(NPC npc, ref float doorBeatCounter, ref float doorCounter, ref float tickUpdater, float ticksUntilBoredom, int doorBeatCounterMax = 10, int doorCounterMax = 60, int interactDoorStyle = 0)
		{
			bool flag = BaseAI.HitTileOnSide(npc, 3, true);
			if (flag)
			{
				int num = (int)((npc.Center.X + ((float)(npc.width / 2) + 8f) * (float)npc.direction) / 16f);
				int num2 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
				for (int i = 1; i >= -3; i--)
				{
					if (i == 1 && Main.tile[num + npc.direction, num2 + i] == null)
					{
						Main.tile[num + npc.direction, num2 + i] = new Tile();
					}
					else if (i == -1 && Main.tile[num + npc.direction, num2 + i] == null)
					{
						Main.tile[num + npc.direction, num2 + i] = new Tile();
					}
					if (Main.tile[num, num2 + i] == null)
					{
						Main.tile[num, num2 + i] = new Tile();
					}
				}
				if (Main.tile[num, num2 - 1].nactive() && Main.tile[num, num2 - 1].type == 10)
				{
					doorCounter += 1f;
					tickUpdater = 0f;
					if (doorCounter >= (float)doorCounterMax)
					{
						npc.velocity.X = 0.5f * -(float)npc.direction;
						doorBeatCounter += 1f;
						doorCounter = 0f;
						bool flag2 = false;
						if (doorBeatCounter >= (float)doorBeatCounterMax)
						{
							flag2 = true;
							doorBeatCounter = 10f;
						}
						WorldGen.KillTile(num, num2 - 1, true, false, false);
						if (flag2 && Main.netMode != 1)
						{
							bool flag3 = false;
							if (interactDoorStyle != 0)
							{
								if (interactDoorStyle == 1)
								{
									WorldGen.KillTile(num, num2, false, false, false);
									flag3 = !Main.tile[num, num2].nactive();
								}
								else
								{
									flag3 = WorldGen.OpenDoor(num, num2, npc.direction);
								}
							}
							if (!flag3)
							{
								tickUpdater = ticksUntilBoredom;
								npc.netUpdate = true;
							}
							if (Main.netMode == 2 && flag3)
							{
								NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)num, (float)num2, (float)npc.direction, 0, 0, 0);
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		public static bool EmptyTiles(Rectangle rect)
		{
			int num = rect.X / 16;
			int num2 = rect.Y / 16;
			for (int i = num; i < num + rect.Width; i++)
			{
				int num3 = num2;
				while (i < num2 + rect.Height)
				{
					Tile tile = Main.tile[i, num3];
					if (tile != null && tile.nactive() && Main.tileSolid[(int)tile.type])
					{
						return false;
					}
					num3++;
				}
			}
			return true;
		}

		public static bool HitTileOnSide(Entity codable, int dir, bool noYMovement = true)
		{
			if (!noYMovement || codable.velocity.Y == 0f)
			{
				Vector2 vector = default(Vector2);
				return BaseAI.HitTileOnSide(codable.position, codable.width, codable.height, dir, ref vector);
			}
			return false;
		}

		public static bool HitTileOnSide(Vector2 position, int width, int height, int dir, ref Vector2 hitTilePos)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (dir == 0)
			{
				num = (int)(position.X - 8f) / 16;
				num2 = (int)position.Y / 16;
				num3 = num + 1;
				num4 = (int)(position.Y + (float)height) / 16;
			}
			else if (dir == 1)
			{
				num = (int)(position.X + (float)width + 8f) / 16;
				num2 = (int)position.Y / 16;
				num3 = num + 1;
				num4 = (int)(position.Y + (float)height) / 16;
			}
			else if (dir == 2)
			{
				num = (int)position.X / 16;
				num2 = (int)(position.Y - 8f) / 16;
				num3 = (int)(position.X + (float)width) / 16;
				num4 = num2 + 1;
			}
			else if (dir == 3)
			{
				num = (int)position.X / 16;
				num2 = (int)(position.Y + (float)height + 8f) / 16;
				num3 = (int)(position.X + (float)width) / 16;
				num4 = num2 + 1;
			}
			for (int i = num; i < num3; i++)
			{
				for (int j = num2; j < num4; j++)
				{
					if (Main.tile[i, j] == null)
					{
						return false;
					}
					if (Main.tile[i, j].nactive() && Main.tileSolid[(int)Main.tile[i, j].type])
					{
						hitTilePos = new Vector2((float)i, (float)j);
						return true;
					}
				}
			}
			return false;
		}

		public static int DropItemBossBag(Player player, Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false)
		{
			if (player != null)
			{
				if ((float)Main.rand.NextDouble() <= chance)
				{
					player.QuickSpawnItem(type, amt);
				}
				return -2;
			}
			return BaseAI.DropItem(codable, type, amt, maxStack, chance, clusterItem, false);
		}

		public static int DropItem(Entity codable, int type, int amt, int maxStack, int chance, bool clusterItem = false)
		{
			return BaseAI.DropItem(codable, type, amt, maxStack, (float)chance / 100f, clusterItem, false);
		}

		public static int DropItem(Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false, bool sync = false)
		{
			int num = -1;
			if ((sync || Main.netMode != 1) && (float)Main.rand.NextDouble() <= chance)
			{
				if (clusterItem)
				{
					int num2 = 0;
					int num3 = 0;
					while (num2 != amt)
					{
						num2++;
						num3++;
						if (num2 == amt || num3 == maxStack)
						{
							num = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, num3, false, 0, false, false);
							if (sync)
							{
								NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
							}
							num3 = 0;
						}
					}
				}
				else
				{
					int i = 0;
					while (i < amt)
					{
						i++;
						num = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, 1, false, 0, false, false);
						if (sync)
						{
							NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			return num;
		}

		public static void DamagePlayer(Player player, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			int hitDirection = (damager == null) ? 0 : damager.direction;
			BaseAI.DamagePlayer(player, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense, 0, 1f);
		}

		public static void DamagePlayer(Player player, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, int critChance = 0, float critMult = 1f)
		{
			if (hitThroughDefense)
			{
				dmgAmt += (int)((float)player.statDefense * 0.5f);
			}
			if (damager == null)
			{
				int num = dmgAmt;
				if (dmgVariation)
				{
					num = Main.DamageVar((float)dmgAmt);
				}
				player.Hurt(PlayerDeathReason.ByOther(-1), num, hitDirection, false, false, false, 0);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(26, -1, -1, PlayerDeathReason.LegacyDefault().GetDeathText(player.name), player.whoAmI, (float)hitDirection, 1f, knockback, num, 0, 0);
					return;
				}
			}
			else
			{
				if (damager is Player)
				{
					Player player2 = (Player)damager;
					int num2 = dmgAmt;
					if (dmgVariation)
					{
						num2 = Main.DamageVar((float)dmgAmt);
					}
					player.Hurt(PlayerDeathReason.ByPlayer(player2.whoAmI), num2, hitDirection, true, false, false, 0);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByPlayer(player2.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, 1f, knockback, num2, 0, 0);
					}
					player2.attackCD = (int)((float)player2.itemAnimationMax * 0.33f);
					return;
				}
				if (damager is Projectile)
				{
					Projectile projectile = (Projectile)damager;
					if (projectile.friendly)
					{
						int num3 = dmgAmt;
						if (dmgVariation)
						{
							num3 = Main.DamageVar((float)dmgAmt);
						}
						player.Hurt(PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI), num3, hitDirection, true, false, false, 0);
						if (Main.netMode != 0)
						{
							NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, 1f, knockback, num3, 0, 0);
						}
						projectile.playerImmune[player.whoAmI] = 40;
						return;
					}
					if (projectile.hostile)
					{
						int num4 = dmgAmt;
						if (dmgVariation)
						{
							num4 = Main.DamageVar((float)dmgAmt);
						}
						player.Hurt(PlayerDeathReason.ByProjectile(-1, projectile.whoAmI), num4, hitDirection, false, false, false, 0);
						if (Main.netMode != 0)
						{
							NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByProjectile(projectile.owner, projectile.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, 1f, knockback, num4, 0, 0);
							return;
						}
					}
				}
				else if (damager is NPC)
				{
					NPC npc = (NPC)damager;
					int num5 = dmgAmt;
					if (dmgVariation)
					{
						num5 = Main.DamageVar((float)dmgAmt);
					}
					player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), num5, hitDirection, false, false, false, 0);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(26, -1, -1, PlayerDeathReason.ByNPC(npc.whoAmI).GetDeathText(player.name), player.whoAmI, (float)hitDirection, 1f, knockback, num5, 0, 0);
					}
				}
			}
		}

		public static void DamageNPC(NPC npc, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			int hitDirection = (damager == null) ? 0 : damager.direction;
			BaseAI.DamageNPC(npc, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense);
		}

		public static void DamageNPC(NPC npc, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			if (hitThroughDefense)
			{
				dmgAmt += (int)((float)npc.defense * 0.5f);
			}
			if (damager == null)
			{
				int num = dmgAmt;
				if (dmgVariation)
				{
					num = Main.DamageVar((float)dmgAmt);
				}
				npc.StrikeNPC(num, knockback, hitDirection, false, false, false);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, num, 0, 0);
					return;
				}
			}
			else if (damager is Projectile)
			{
				Projectile projectile = (Projectile)damager;
				if (projectile.owner == Main.myPlayer)
				{
					int num2 = dmgAmt;
					if (dmgVariation)
					{
						num2 = Main.DamageVar((float)dmgAmt);
					}
					npc.StrikeNPC(num2, knockback, hitDirection, false, false, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, num2, 0, 0);
					}
					if (projectile.penetrate != 1)
					{
						npc.immune[projectile.owner] = 10;
						return;
					}
				}
			}
			else if (damager is Player)
			{
				Player player = (Player)damager;
				if (player.whoAmI == Main.myPlayer)
				{
					int num3 = dmgAmt;
					if (dmgVariation)
					{
						num3 = Main.DamageVar((float)dmgAmt);
					}
					npc.StrikeNPC(num3, knockback, hitDirection, false, false, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, num3, 0, 0);
					}
					npc.immune[player.whoAmI] = player.itemAnimation;
				}
			}
		}

		public static void KillNPCWithLoot(NPC npc)
		{
			BaseAI.DamageNPC(npc, npc.lifeMax + npc.defense + 1, 0f, 0, null, false, true);
		}

		public static void KillNPC(NPC npc)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			npc.active = false;
			int whoAmI = npc.whoAmI;
			Main.npc[whoAmI] = new NPC();
			if (Main.netMode == 2)
			{
				NetMessage.SendData(23, -1, -1, null, whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public static void KillProjectile(Projectile p)
		{
			if (p.owner == Main.myPlayer)
			{
				NetMessage.SendData(29, -1, -1, NetworkText.FromLiteral(""), p.identity, (float)p.owner, 0f, 0f, 0, 0, 0);
			}
			p.active = false;
		}

		public static void SpawnSmoke(Vector2 start, float width, float height, int loopAmount = 2, float scale = 1f)
		{
			Vector2 vector = start + new Vector2(width * 0.5f, height * 0.5f);
			UnifiedRandom rand = Main.rand;
			for (int i = 0; i < loopAmount; i++)
			{
				Vector2 vector2;
				vector2..ctor(vector.X - 24f, vector.Y - 24f);
				Vector2 vector3 = default(Vector2);
				int num = Gore.NewGore(vector2, vector3, Main.rand.Next(61, 64), 1f);
				Gore gore = Main.gore[num];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				gore.velocity.Y = gore.velocity.Y + 1.5f;
				num = Gore.NewGore(vector2, vector3, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				gore.velocity.Y = gore.velocity.Y + 1.5f;
				num = Gore.NewGore(vector2, vector3, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				gore.velocity.Y = gore.velocity.Y + 1.5f;
				num = Gore.NewGore(vector2, vector3, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				gore.velocity.Y = gore.velocity.Y + 1.5f;
			}
		}

		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, float distance = -1f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectile(center, projType, owner, null, distance, CanAdd);
		}

		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = null, float distance = -1f, Func<Projectile, bool> CanAdd = null)
		{
			int result = -1;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile != null && projectile.active && (projType == -1 || projectile.type == projType) && ((float)owner == -1f || projectile.owner == owner) && (distance == -1f || projectile.Distance(center) < distance))
				{
					bool flag = true;
					if (projsToExclude != null)
					{
						foreach (int num in projsToExclude)
						{
							if (num == projectile.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(projectile)) && flag)
					{
						distance = projectile.Distance(center);
						result = i;
					}
				}
			}
			return result;
		}

		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectiles(center, projType, owner, null, distance, CanAdd);
		}

		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = null, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile != null && projectile.active && (projType == -1 || projectile.type == projType) && (owner == -1 || projectile.owner == owner) && (distance == -1f || projectile.Distance(center) < distance))
				{
					bool flag = true;
					if (projsToExclude != null)
					{
						foreach (int num in projsToExclude)
						{
							if (num == projectile.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(projectile)) && flag)
					{
						list.Add(i);
					}
				}
			}
			return list.ToArray();
		}

		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectiles(center, projTypes, owner, null, distance, CanAdd);
		}

		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, int[] projsToExclude = null, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile != null && projectile.active && (owner == -1 || projectile.owner == owner) && (distance == -1f || projectile.Distance(center) < distance))
				{
					bool flag = false;
					foreach (int num in projTypes)
					{
						if (projectile.type == num)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						bool flag2 = true;
						if (projsToExclude != null)
						{
							foreach (int num2 in projsToExclude)
							{
								if (num2 == projectile.whoAmI)
								{
									flag2 = false;
									break;
								}
							}
						}
						if ((!flag2 || CanAdd == null || CanAdd(projectile)) && flag2)
						{
							list.Add(i);
						}
					}
				}
			}
			return list.ToArray();
		}

		public static int[] GetNPCsInBox(Rectangle rect, int npcType = -1, int[] npcsToExclude = null, Func<NPC, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && rect.Intersects(npc.Hitbox))
				{
					bool flag = true;
					if (npcsToExclude != null)
					{
						foreach (int num in npcsToExclude)
						{
							if (num == npc.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(npc)) && flag)
					{
						list.Add(i);
					}
				}
			}
			return list.ToArray();
		}

		public static int GetNPC(Vector2 center, int npcType = -1, float distance = -1f, Func<NPC, bool> CanAdd = null)
		{
			return BaseAI.GetNPC(center, npcType, null, distance, CanAdd);
		}

		public static int GetNPC(Vector2 center, int npcType = -1, int[] npcsToExclude = null, float distance = -1f, Func<NPC, bool> CanAdd = null)
		{
			int result = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && (distance == -1f || npc.Distance(center) < distance))
				{
					bool flag = true;
					if (npcsToExclude != null)
					{
						foreach (int num in npcsToExclude)
						{
							if (num == npc.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(npc)) && flag)
					{
						distance = npc.Distance(center);
						result = i;
					}
				}
			}
			return result;
		}

		public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500f, Func<NPC, bool> CanAdd = null)
		{
			return BaseAI.GetNPCs(center, npcType, new int[0], distance, CanAdd);
		}

		public static int[] GetNPCs(Vector2 center, int npcType = -1, int[] npcsToExclude = null, float distance = 500f, Func<NPC, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && (distance == -1f || npc.Distance(center) < distance))
				{
					bool flag = true;
					if (npcsToExclude != null)
					{
						foreach (int num in npcsToExclude)
						{
							if (num == npc.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(npc)) && flag)
					{
						list.Add(i);
					}
				}
			}
			return list.ToArray();
		}

		public static int[] GetPlayersInBox(Rectangle rect, int[] playersToExclude = null, Func<Player, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active && !player.dead && rect.Intersects(player.Hitbox))
				{
					bool flag = true;
					if (playersToExclude != null)
					{
						foreach (int num in playersToExclude)
						{
							if (num == player.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(player)) && flag)
					{
						list.Add(i);
					}
				}
			}
			return list.ToArray();
		}

		public static int GetPlayerByName(string name, bool aliveOnly = true)
		{
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active && (!aliveOnly || !player.dead) && player.name == name)
				{
					return i;
				}
			}
			return -1;
		}

		public static int GetPlayer(Vector2 center, float distance = -1f, Func<Player, bool> CanAdd = null)
		{
			return BaseAI.GetPlayer(center, null, true, distance, CanAdd);
		}

		public static int GetPlayer(Vector2 center, int[] playersToExclude = null, bool activeOnly = true, float distance = -1f, Func<Player, bool> CanAdd = null)
		{
			int result = -1;
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && (!activeOnly || (player.active && !player.dead)) && (distance == -1f || player.Distance(center) < distance))
				{
					bool flag = true;
					if (playersToExclude != null)
					{
						foreach (int num in playersToExclude)
						{
							if (num == player.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(player)) && flag)
					{
						distance = player.Distance(center);
						result = i;
					}
				}
			}
			return result;
		}

		public static int[] GetPlayers(Vector2 center, float distance = 500f, Func<Player, bool> CanAdd = null)
		{
			return BaseAI.GetPlayers(center, null, true, distance, CanAdd);
		}

		public static int[] GetPlayers(Vector2 center, int[] playersToExclude = null, bool aliveOnly = true, float distance = 500f, Func<Player, bool> CanAdd = null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active && (!aliveOnly || !player.dead) && player.Distance(center) < distance)
				{
					bool flag = true;
					if (playersToExclude != null)
					{
						foreach (int num in playersToExclude)
						{
							if (num == player.whoAmI)
							{
								flag = false;
								break;
							}
						}
					}
					if ((!flag || CanAdd == null || CanAdd(player)) && flag)
					{
						list.Add(i);
					}
				}
			}
			return list.ToArray();
		}

		public static bool CanTarget(Player player, Entity codable)
		{
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return npc.life > 0 && (!npc.friendly || (npc.type == 22 && player.killGuide)) && !npc.dontTakeDamage;
			}
			if (codable is Player)
			{
				Player player2 = (Player)codable;
				return player2.statLife > 0 && !player2.immune && player2.hostile && (player.team == 0 || player2.team == 0 || player.team != player2.team);
			}
			return false;
		}

		public static void SetTarget(NPC npc, int target)
		{
			npc.target = target;
			if (npc.target < 0 || npc.target >= 255)
			{
				npc.target = 0;
			}
			npc.targetRect = Main.player[npc.target].Hitbox;
			if (npc.target != npc.oldTarget && !npc.collideX && !npc.collideY)
			{
				npc.netUpdate = true;
			}
		}

		public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true, Vector2 offset = default(Vector2))
		{
			int result = -1;
			if (damage == -1)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(projType);
				damage = projectile.damage;
			}
			bool flag = (codable is NPC) ? (Main.netMode != 1) : (!(codable is Projectile) || ((Projectile)codable).owner == Main.myPlayer);
			if (flag)
			{
				Vector2 vector = position + new Vector2((float)width * 0.5f, (float)height * 0.5f);
				delayTimer -= 1f;
				if (delayTimer <= 0f)
				{
					if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
					{
						Vector2 vector2 = codable.Center + offset;
						float rot = BaseUtility.RotationTo(codable.Center, vector);
						vector2 = BaseUtility.RotateVector(codable.Center, vector2, rot);
						result = BaseAI.FireProjectile(vector, vector2, projType, damage, 0f, speed, 0, -1, default(Vector2));
					}
					delayTimer = delayTimerMax;
					if (codable is NPC)
					{
						((NPC)codable).netUpdate = true;
					}
				}
			}
			return result;
		}

		public static int FireProjectile(Vector2 fireTarget, NPC npc, int projectileType, int damage, float knockback, float speedScalar = 1f, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
		{
			if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
			{
				Main.PlaySound(soundGroup, (int)npc.Center.X, (int)npc.Center.Y, sound, 1f, 0f);
			}
			if (Main.netMode != 1)
			{
				int result = BaseAI.FireProjectile(fireTarget, npc.Center, projectileType, damage, knockback, speedScalar, hostility, owner, default(Vector2));
				npc.netUpdate = true;
				return result;
			}
			return -1;
		}

		public static int FireProjectile(Vector2 fireTarget, Projectile p, int projectileType, int damage, float knockback, float speedScalar = 1f, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
		{
			if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
			{
				Main.PlaySound(soundGroup, (int)p.Center.X, (int)p.Center.Y, sound, 1f, 0f);
			}
			if (p.owner == Main.myPlayer)
			{
				return BaseAI.FireProjectile(fireTarget, p.Center, projectileType, damage, knockback, speedScalar, hostility, owner, default(Vector2));
			}
			return -1;
		}

		public static int FireProjectile(Vector2 fireTarget, Vector2 position, int projectileType, int damage, float knockback, float speedScalar = 1f, int hostility = 0, int owner = -1, Vector2 targetOffset = default(Vector2))
		{
			Vector2 vector = BaseUtility.RotateVector(position, position + new Vector2(speedScalar, 0f), BaseUtility.RotationTo(position, fireTarget));
			vector -= position;
			int num = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, projectileType, damage, knockback, (owner != -1) ? owner : Main.myPlayer, 0f, 0f);
			Projectile projectile = Main.projectile[num];
			projectile.velocity = vector;
			if (hostility != 0)
			{
				projectile.friendly = (hostility == 1 || hostility == 2);
				projectile.hostile = (hostility == -1 || hostility == 2);
				if (Main.netMode != 0)
				{
					MNet.SendBaseNetMessage(0, new object[]
					{
						projectile.owner,
						projectile.identity,
						projectile.friendly,
						projectile.hostile
					});
				}
			}
			projectile.netUpdate2 = true;
			return num;
		}

		public static void Look(Projectile p, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.Look(p, ref p.rotation, ref p.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void Look(NPC npc, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.Look(npc, ref npc.rotation, ref npc.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void Look(Entity c, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.LookAt(c.position + c.velocity, c.position, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void LookAt(Vector2 lookTarget, Entity c, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			int spriteDirection = (c is NPC) ? ((NPC)c).spriteDirection : ((c is Projectile) ? ((Projectile)c).spriteDirection : 0);
			float rotation = (c is NPC) ? ((NPC)c).rotation : ((c is Projectile) ? ((Projectile)c).rotation : 0f);
			BaseAI.LookAt(lookTarget, c.Center, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
			if (c is NPC)
			{
				((NPC)c).spriteDirection = spriteDirection;
				((NPC)c).rotation = rotation;
				return;
			}
			if (c is Projectile)
			{
				((Projectile)c).spriteDirection = spriteDirection;
				((Projectile)c).rotation = rotation;
			}
		}

		public static void LookAt(Vector2 lookTarget, Vector2 center, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.075f, bool flipSpriteDir = false)
		{
			if (lookType == 0)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
				}
				float num = lookTarget.X - center.X;
				float num2 = lookTarget.Y - center.Y;
				rotation = -((float)Math.Atan2((double)num, (double)num2) - 1.57f + rotAddon);
				if (spriteDirection == 1)
				{
					rotation -= 3.1415927f;
					return;
				}
			}
			else if (lookType == 1)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
					return;
				}
			}
			else
			{
				if (lookType == 2)
				{
					float num3 = lookTarget.X - center.X;
					float num4 = lookTarget.Y - center.Y;
					rotation = -((float)Math.Atan2((double)num3, (double)num4) - 1.57f + rotAddon);
					return;
				}
				if (lookType == 3 || lookType == 4)
				{
					int num5 = spriteDirection;
					if (lookType == 3 && lookTarget.X > center.X)
					{
						spriteDirection = -1;
					}
					else
					{
						spriteDirection = 1;
					}
					if (lookType == 3 && flipSpriteDir)
					{
						spriteDirection *= -1;
					}
					if (num5 != spriteDirection)
					{
						rotation += 3.1415927f * (float)spriteDirection;
					}
					float num6 = 6.2831855f;
					float num7 = lookTarget.X - center.X;
					float num8 = lookTarget.Y - center.Y;
					float num9 = (float)Math.Atan2((double)num8, (double)num7) + rotAddon;
					if (spriteDirection == 1)
					{
						num9 += 3.1415927f;
					}
					if (num9 > num6)
					{
						num9 -= num6;
					}
					else if (num9 < 0f)
					{
						num9 += num6;
					}
					if (rotation > num6)
					{
						rotation -= num6;
					}
					else if (rotation < 0f)
					{
						rotation += num6;
					}
					if (rotation < num9)
					{
						if ((double)(num9 - rotation) > 3.1415927410125732)
						{
							rotation -= rotAmount;
						}
						else
						{
							rotation += rotAmount;
						}
					}
					else if (rotation > num9)
					{
						if ((double)(rotation - num9) > 3.1415927410125732)
						{
							rotation += rotAmount;
						}
						else
						{
							rotation -= rotAmount;
						}
					}
					if (rotation > num9 - rotAmount && rotation < num9 + rotAmount)
					{
						rotation = num9;
					}
				}
			}
		}

		public static void RotateTo(ref float rotation, float rotDestination, float rotAmount = 0.075f)
		{
			float num = 6.2831855f;
			float num2 = rotDestination;
			if (num2 > num)
			{
				num2 -= num;
			}
			else if (num2 < 0f)
			{
				num2 += num;
			}
			if (rotation > num)
			{
				rotation -= num;
			}
			else if (rotation < 0f)
			{
				rotation += num;
			}
			if (rotation < num2)
			{
				if ((double)(num2 - rotation) > 3.1415927410125732)
				{
					rotation -= rotAmount;
				}
				else
				{
					rotation += rotAmount;
				}
			}
			else if (rotation > num2)
			{
				if ((double)(rotation - num2) > 3.1415927410125732)
				{
					rotation += rotAmount;
				}
				else
				{
					rotation -= rotAmount;
				}
			}
			if (rotation > num2 - rotAmount && rotation < num2 + rotAmount)
			{
				rotation = num2;
			}
		}

		public static Vector2 TraceTile(Vector2 start, float distance, float rotation, Vector2 ignoreTile, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignoreTile, 1, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 TracePlayer(Vector2 start, float distance, float rotation, int ignorePlayer, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignorePlayer, 0, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 TraceNPC(Vector2 start, float distance, float rotation, int ignoreNPC, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignoreNPC, 2, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, float Jump = 1f, bool ignorePlatforms = true)
		{
			return BaseAI.Trace(start, end, ignore, ignoreType, null, npcCheck, tileCheck, playerCheck, false, Jump, ignorePlatforms);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, float Jump = 1f, bool ignorePlatforms = true)
		{
			return BaseAI.Trace(start, end, ignore, ignoreType, dim, npcCheck, tileCheck, playerCheck, returnCenter, ignorePlatforms ? new int[]
			{
				19
			} : null, Jump);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, int[] tileTypesToIgnore = null, float Jump = 1f)
		{
			try
			{
				if (ignore == null)
				{
					return start;
				}
				if (dim == null)
				{
					dim = new Rectangle(0, 0, 1, 1);
				}
				if (start.X < 0f)
				{
					start.X = 0f;
				}
				if (start.X > (float)(Main.maxTilesX * 16))
				{
					start.X = (float)(Main.maxTilesX * 16);
				}
				if (start.Y < 0f)
				{
					start.Y = 0f;
				}
				if (start.Y > (float)(Main.maxTilesY * 16))
				{
					start.Y = (float)(Main.maxTilesY * 16);
				}
				if (end.X < 0f)
				{
					end.X = 0f;
				}
				if (end.X > (float)(Main.maxTilesX * 16))
				{
					end.X = (float)(Main.maxTilesX * 16);
				}
				if (end.Y < 0f)
				{
					end.Y = 0f;
				}
				if (end.Y > (float)(Main.maxTilesY * 16))
				{
					end.Y = (float)(Main.maxTilesY * 16);
				}
				Vector2 vector;
				vector..ctor(1f, 1f);
				Vector2 vector2 = start;
				Vector2 vector3 = end;
				Vector2 vector4 = vector3 - vector2;
				vector4.Normalize();
				float num = Vector2.Distance(vector2, vector3);
				for (float num2 = 0f; num2 < num; num2 += Jump)
				{
					Vector2 vector5 = vector2 + vector4 * num2 + vector;
					Rectangle rectangle = (Rectangle)dim;
					Rectangle rectangle2;
					rectangle2..ctor((int)vector5.X - ((rectangle.Width == 1) ? 0 : (rectangle.Width / 2)), (int)vector5.Y - ((rectangle.Height == 1) ? 0 : (rectangle.Height / 2)), rectangle.Width, rectangle.Height);
					if (tileCheck)
					{
						int num3 = (int)vector5.X / 16;
						int num4 = (int)vector5.Y / 16;
						Rectangle rectangle3;
						rectangle3..ctor((int)vector5.X, (int)vector5.Y, 16, 16);
						if (rectangle2.Intersects(rectangle3))
						{
							Vector2 vector6 = (ignoreType == 1) ? ((Vector2)ignore) : new Vector2(-1f, -1f);
							if ((int)vector6.X != num3 && (int)vector6.Y != num4)
							{
								Tile tile = Main.tile[num3, num4];
								if (tile != null && tile.nactive() && (tileTypesToIgnore == null || tileTypesToIgnore.Length <= 0 || !BaseUtility.InArray(tileTypesToIgnore, (int)tile.type)) && Main.tileSolid[(int)tile.type])
								{
									return returnCenter ? new Vector2((float)(num3 * 16 + 8), (float)(num4 * 16 + 8)) : vector5;
								}
							}
						}
					}
					if (npcCheck)
					{
						int[] npcs = BaseAI.GetNPCs(vector5, -1, 5f, null);
						for (int i = 0; i < npcs.Length; i++)
						{
							NPC npc = Main.npc[npcs[i]];
							if (npc.active && npc.life > 0 && (ignoreType != 2 || npc.whoAmI != (int)ignore))
							{
								Rectangle rectangle4;
								rectangle4..ctor((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
								if (rectangle2.Intersects(rectangle4))
								{
									return returnCenter ? npc.Center : vector5;
								}
							}
						}
					}
					if (playerCheck)
					{
						int[] players = BaseAI.GetPlayers(vector5, 5f, null);
						for (int j = 0; j < players.Length; j++)
						{
							Player player = Main.player[players[j]];
							if (!player.dead && player.active && (ignoreType != 0 || player.whoAmI != (int)ignore))
							{
								Rectangle rectangle5;
								rectangle5..ctor((int)player.position.X, (int)player.position.Y, player.width, player.height);
								if (rectangle2.Intersects(rectangle5))
								{
									return returnCenter ? player.Center : vector5;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLogger.Log("TRACE ERROR: " + ex.Message);
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("--------");
			}
			return end;
		}

		public static Vector2[] GetLinePoints(Vector2 start, Vector2 end, float jump = 1f)
		{
			Vector2 vector = end - start;
			vector.Normalize();
			float num = Vector2.Distance(start, end);
			float num2 = 0f;
			BaseUtility.RotationTo(start, end);
			List<Vector2> list = new List<Vector2>();
			while (num2 < num)
			{
				list.Add(start + vector * num2);
				num2 += jump;
			}
			return list.ToArray();
		}
	}
}
