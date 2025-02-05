using System;
using System.Numerics;

namespace AotForms
{
    internal static class Aimbot
    {
        internal static void Work()
        {
            while (true)
            {
                // Check if aimbot is enabled
                if (!Config.AimBot)
                {
                    continue;
                }

                // Check if the aimbot key is pressed
                if ((WinAPI.GetAsyncKeyState(Config.AimbotKey) & 0x8000) == 0)
                {
                    continue;
                }

                Entity target = null;

                float distance = float.MaxValue;
                // Ensure Core dimensions and matrix are valid
                if (Core.Width == -1 || Core.Height == -1 || !Core.HaveMatrix)
                {
                    continue;
                }

                var screenCenter = new Vector2(Core.Width / 2f, Core.Height / 2f);

                foreach (var entity in Core.Entities.Values)
                {
                    // Skip entities that are not valid targets
                    if (!entity.IsKnown || entity.IsDead || (Config.IgnoreKnocked && entity.IsKnocked))
                        continue;

                    var head2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                    var root2D = W2S.WorldToScreen(Core.CameraMatrix, entity.Root, Core.Width, Core.Height);
                    var x = head2D.X - screenCenter.X;
                    var y = head2D.Y - screenCenter.Y;
                    var crosshairDist = (float)Math.Sqrt(x * x + y * y);

                    // Check if the entity is within the FOV
                    var playerDistance = Vector3.Distance(Core.LocalMainCamera, entity.Head);

                    if (playerDistance > Config.AimBotMaxDistance) continue;



                    if (crosshairDist >= distance || crosshairDist == float.MaxValue)
                    {
                        continue;
                    }
                    if (crosshairDist > Config.Aimfov)
                    {

                        continue;

                    }

                    distance = crosshairDist;
                    target = entity;
                }

                if (target != null)
                {
                    var playerLook = MathUtils.GetRotationToLocation(target.Head, 0.1f, Core.LocalMainCamera);
                    InternalMemory.Write(Core.LocalPlayer + Offsets.AimRotation, playerLook);
                }
            }
        }
    }
}
