using AotForms;
using ImGuiNET;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using static AotForms.WinAPI;

namespace AotForms
{

    internal class ESP : ClickableTransparentOverlay.Overlay
    {
        IntPtr hWnd;
        IntPtr HDPlayer;
        private const short DefaultMaxHealth = 200;
        protected override unsafe void Render()
        {
            ImGui.GetForegroundDrawList().AddText(new Vector2(Core.Width / 2f - 40, 20), ColorToUint32(Config.NameCheat), "BRUUUH CHEATS");
            ImGui.GetForegroundDrawList().AddText(new Vector2(Core.Width / 2f - 40, 40), ColorToUint32(Config.NameCheat), "</Dev: BRUUUH >");
            if (!Core.HaveMatrix) return;

            CreateHandle();

            var drawList = ImGui.GetForegroundDrawList();



            if (Config.Aimfovc)
            {
                DrawSmoothCircle(Config.Aimfov, ColorToUint32(Config.Aimfovcolor), 1.0f);
            }

            // Handle window styles
            string windowName = "Overlay";
            hWnd = FindWindow(null, windowName);
            HDPlayer = FindWindow("BlueStacksApp", null);

            if (hWnd != IntPtr.Zero)
            {
                long extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
                SetWindowLong(hWnd, GWL_EXSTYLE, (extendedStyle | WS_EX_TOOLWINDOW) & ~WS_EX_APPWINDOW);
            }
            else
            {
                Console.WriteLine("The window was not found.");
            }
            var tmp = Core.Entities;
            foreach (var entity in tmp.Values)
            {
                if (entity.IsDead || !entity.IsKnown)
                {
                    continue;
                }
                var dist = Vector3.Distance(Core.LocalMainCamera, entity.Head);

                if (dist > Config.espran) continue;

                var headScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                var bottomScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Root, Core.Width, Core.Height);

                if (headScreenPos.X < 1 || headScreenPos.Y < 1) continue;
                if (bottomScreenPos.X < 1 || bottomScreenPos.Y < 1) continue;

                float CornerHeight = Math.Abs(headScreenPos.Y - bottomScreenPos.Y);
                float CornerWidth = (float)(CornerHeight * 0.65);

                if (Config.ESPLine)
                {
                    if (Config.espcfx == false)
                    {
                        Vector2 lineStartPos = Vector2.Zero;

                        switch (Config.linePosition)
                        {
                            case "Up":
                                lineStartPos = new Vector2(Core.Width / 2f, 0f);  
                                break;

                            case "Bottom":
                                lineStartPos = new Vector2(Core.Width / 2f, Core.Height - 0f); 
                                break;

                            case "Left":
                                lineStartPos = new Vector2(0f, Core.Height / 2f); 
                                break;

                            case "Right":
                                lineStartPos = new Vector2(Core.Width - 0f, Core.Height / 2f); 
                                break;
                        }

                        if (!entity.IsKnocked)
                        {
                            ImGui.GetBackgroundDrawList().AddLine(lineStartPos, headScreenPos, ColorToUint32(Config.ESPLineColor), 1f);
                        }
                    }
                    else
                    {
                        Vector2 lineStartPos = Vector2.Zero;

                        // Same logic as above for line position based on Config.linePosition
                        switch (Config.linePosition)
                        {
                            case "Up":
                                lineStartPos = new Vector2(Core.Width / 2f, 0f);  // Top-center of the screen
                                break;

                            case "Bottom":
                                lineStartPos = new Vector2(Core.Width / 2f, Core.Height - 0f);  // Bottom-center of the screen
                                break;

                            case "Left":
                                lineStartPos = new Vector2(0f, Core.Height / 2f);  // Left-center of the screen
                                break;

                            case "Right":
                                lineStartPos = new Vector2(Core.Width - 0f, Core.Height / 2f);  // Right-center of the screen
                                break;
                        }

                        // Draw the line from lineStartPos to headScreenPos
                        if (!entity.IsKnocked)
                        {
                            ImGui.GetBackgroundDrawList().AddLine(lineStartPos, headScreenPos, ColorToUint32(Config.ESPLineColor), 1f);
                        }
                    }
                }









                if (Config.ESPBox)
                {
                    uint boxColor = ColorToUint32(Config.ESPBoxColor);
                    DrawCorneredBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, boxColor, 1f);
                }

                if (Config.ESPFillBox)
                {
                    uint boxColor = ColorToUint32(Color.FromArgb((int)(0.2f * 255), Config.ESPFillBoxColor));
                    DrawFilledBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, boxColor);
                }

                if (Config.ESPBox2)
                {
                    uint boxColor = ColorToUint32(Config.ESPBoxColor);
                    Draw3dBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, boxColor, 1f);
                }
                Vector2 fixedNameSize = new Vector2(95, 16);
                float healthBarHeight = 4;

                if (entity.Name == "")
                    entity.Name = "BRUUUH.BOT";
                if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 && headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                {
                    Vector2 namePos = new Vector2(headScreenPos.X - fixedNameSize.X / 2, headScreenPos.Y - fixedNameSize.Y - 15);



                    Vector2 textSizeName = ImGui.CalcTextSize(entity.Name);

                    Vector2 textSizeDistance = ImGui.CalcTextSize($" ({MathF.Round(Vector3.Distance(Core.LocalMainCamera, entity.Head))}m)");

                    Vector2 textPosName = new Vector2(namePos.X + 5, namePos.Y + (fixedNameSize.Y - textSizeName.Y) / 2);
                    Vector2 textPosDistance = new Vector2(namePos.X + fixedNameSize.X - textSizeDistance.X + 5, namePos.Y + (fixedNameSize.Y - textSizeDistance.Y) / 2);



                    if (Config.ESPName)
                    {
                        ImGui.GetForegroundDrawList().AddRectFilled(namePos, namePos + new Vector2(fixedNameSize.X, fixedNameSize.Y), ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.7f)), 3f);
                        ImGui.GetForegroundDrawList().AddText(textPosName, ColorToUint32(Config.ESPNameColor), entity.Name);
                        ImGui.GetForegroundDrawList().AddText(textPosDistance, ColorToUint32(Config.ESPNameColor), $" {MathF.Round(Vector3.Distance(Core.LocalMainCamera, entity.Head))}m");
                    }
                    if (Config.ESPHealth && Config.ESPName)
                    {
                        Vector2 barPos = new Vector2(namePos.X, namePos.Y + fixedNameSize.Y);
                        var vList = ImGui.GetForegroundDrawList();
                        float healthPercentage = entity.Health > 1000 ? 1f :
                        entity.Health < 0 ? 1f :
                        (float)entity.Health / (entity.Health > 230 ? 500 : 200);
                        float barWidth = fixedNameSize.X * healthPercentage;
                        uint barColor;
                        if (healthPercentage > 0.8f)
                        {
                            barColor = ColorToUint32(Color.GreenYellow);
                        }
                        else if (healthPercentage > 0.4f)
                        {
                            barColor = ColorToUint32(Color.Orange);
                        }
                        else
                        {
                            barColor = ColorToUint32(Color.Red);
                        }

                        vList.AddRectFilled(new Vector2(barPos.X, barPos.Y), new Vector2(barPos.X + fixedNameSize.X, barPos.Y + 2), 0x90000000);

                        barColor = entity.IsKnocked ? ColorToUint32(Color.Red) : barColor;
                        vList.AddRectFilled(new Vector2(barPos.X, barPos.Y), new Vector2(barPos.X + barWidth, barPos.Y + 2), barColor);
                    }
                    if (Config.ESPSkeleton)
                    {
                        DrawSkeleton(entity);
                    }
                }
            }
        }
        public void DrawFilledBox(float X, float Y, float W, float H, uint color)
        {
            var vList = ImGui.GetForegroundDrawList();
            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + W, Y + H), color);
        }

        public void DrawFilledCircle(float centerY, float radius, int numSegments = 64)
        {
            var vList = ImGui.GetBackgroundDrawList();

            // Set the center of the circle at the middle of the screen horizontally (Core.Width / 2f)

            float centerX = Core.Width / 2f;

            uint colorR = ColorToUint32(Color.FromArgb((int)(1f * 255), 225, 0, 0)); // Red color with full opacity
            uint colorG = ColorToUint32(Color.FromArgb((int)(1f * 255), 0, 255, 0)); // LimeGreen color with full opacity

            // Shadow parameters
            float shadowOffset = 1.5f; // The subtle offset of the shadow from the circle
            uint shadowColor = ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 1f)); // Semi-transparent black for a soft shadow

            // Draw shadow (a larger circle slightly offset behind the main one)
            vList.AddCircleFilled(new Vector2(centerX, centerY), radius + shadowOffset, shadowColor, numSegments);

            if (Config.AimBot)
            {
                // Draw main circle
                vList.AddCircleFilled(new Vector2(centerX, centerY), radius, colorR, numSegments);
            }
            else
            {
                // Draw main circle
                vList.AddCircleFilled(new Vector2(centerX, centerY), radius, colorG, numSegments);
            }
        }
        public void DrawCorneredBox(float X, float Y, float W, float H, uint color, float thickness)
        {
            var vList = ImGui.GetForegroundDrawList();

            float lineW = W / 3;
            float lineH = H / 3;

            vList.AddLine(new Vector2(X, Y - thickness / 2), new Vector2(X, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y), new Vector2(X + lineW, Y), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y), new Vector2(X + W + thickness / 2, Y), color, thickness);
            vList.AddLine(new Vector2(X + W, Y - thickness / 2), new Vector2(X + W, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X, Y + H - lineH), new Vector2(X, Y + H + thickness / 2), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y + H), new Vector2(X + lineW, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y + H), new Vector2(X + W + thickness / 2, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W, Y + H - lineH), new Vector2(X + W, Y + H + thickness / 2), color, thickness);
        }

        public void Draw3dBox(float X, float Y, float W, float H, uint color, float thickness)
        {

            var vList = ImGui.GetForegroundDrawList();

            Vector3[] screentions = new Vector3[]
            {
        new Vector3(X, Y, 0),                // Top-left front
        new Vector3(X, Y + H, 0),            // Bottom-left front
        new Vector3(X + W, Y + H, 0),        // Bottom-right front
        new Vector3(X + W, Y, 0),            // Top-right front
        new Vector3(X, Y, -W),               // Top-left back
        new Vector3(X, Y + H, -W),           // Bottom-left back
        new Vector3(X + W, Y + H, -W),       // Bottom-right back
        new Vector3(X + W, Y, -W)            // Top-right back
            };

            // Draw front face
            vList.AddLine(new Vector2(screentions[0].X, screentions[0].Y), new Vector2(screentions[1].X, screentions[1].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[1].X, screentions[1].Y), new Vector2(screentions[2].X, screentions[2].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[2].X, screentions[2].Y), new Vector2(screentions[3].X, screentions[3].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[3].X, screentions[3].Y), new Vector2(screentions[0].X, screentions[0].Y), color, thickness);

            // Draw back face
            vList.AddLine(new Vector2(screentions[4].X, screentions[4].Y), new Vector2(screentions[5].X, screentions[5].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[5].X, screentions[5].Y), new Vector2(screentions[6].X, screentions[6].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[6].X, screentions[6].Y), new Vector2(screentions[7].X, screentions[7].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[7].X, screentions[7].Y), new Vector2(screentions[4].X, screentions[4].Y), color, thickness);

            // Draw connecting lines
            vList.AddLine(new Vector2(screentions[0].X, screentions[0].Y), new Vector2(screentions[4].X, screentions[4].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[1].X, screentions[1].Y), new Vector2(screentions[5].X, screentions[5].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[2].X, screentions[2].Y), new Vector2(screentions[6].X, screentions[6].Y), color, thickness);
            vList.AddLine(new Vector2(screentions[3].X, screentions[3].Y), new Vector2(screentions[7].X, screentions[7].Y), color, thickness);

        }
        private void DrawSkeleton(Entity entity)
        {
            var drawList = ImGui.GetForegroundDrawList();
            uint lineColor = ColorToUint32(Config.ESPSkeletonColor); // Color for the skeleton lines
            uint circleColor = ColorToUint32(Color.Red); // Color for the circle around the head

            // Convert entity positions to screen space
            var headScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
            var leftWristScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightWrist, Core.Width, Core.Height); // Adjust as per actual mapping
            var spineScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Spine, Core.Width, Core.Height);
            var hipScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Hip, Core.Width, Core.Height); // Adjust as per actual mapping
            var rootScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Root, Core.Width, Core.Height);
            var rightCalfScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightCalf, Core.Width, Core.Height);
            var leftCalfScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftCalf, Core.Width, Core.Height);
            var rightFootScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightFoot, Core.Width, Core.Height);
            var leftFootScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftFoot, Core.Width, Core.Height);
            var rightWristScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightWrist, Core.Width, Core.Height);
            var leftHandScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftHand, Core.Width, Core.Height);
            var leftShoulderScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftSholder, Core.Width, Core.Height);
            var rightShoulderScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightSholder, Core.Width, Core.Height);
            var rightWristJointScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightWristJoint, Core.Width, Core.Height);
            var leftWristJointScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftWristJoint, Core.Width, Core.Height);
            var leftElbowScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftElbow, Core.Width, Core.Height);
            var rightElbowScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.RightElbow, Core.Width, Core.Height); // Adjust if needed

            // Draw skeleton lines


            DrawLine(drawList, spineScreenPos, rightShoulderScreenPos, lineColor); // Spine to Right Shoulder
            DrawLine(drawList, spineScreenPos, hipScreenPos, lineColor);// Spine to hip


            DrawLine(drawList, spineScreenPos, leftShoulderScreenPos, lineColor); // Spine to Left Shoulder
            DrawLine(drawList, leftShoulderScreenPos, rightElbowScreenPos, lineColor); // Left Shoulder to Left Elbow
            DrawLine(drawList, leftElbowScreenPos, rightWristJointScreenPos, lineColor); // Left Elbow to Left Wrist Joint
            // Left Wrist Joint to Left Wrist

            DrawLine(drawList, rightShoulderScreenPos, leftElbowScreenPos, lineColor); // Right Shoulder to Left Elbow
                                                                                       //  DrawLine(drawList, rightElbowScreenPos, leftWristJointScreenPos, lineColor); // Right Elbow to Left Wrist Joint
                                                                                       // Right Wrist Joint to Left Wrist

            DrawLine(drawList, hipScreenPos, rightFootScreenPos, lineColor);// Hip to Right Calf
            DrawLine(drawList, hipScreenPos, leftFootScreenPos, lineColor);// Hip to Left Calf


            // Draw a small circle around the head
            float distance = entity.Distance; // Assume entity.Distance is the distance to the player in game units

            // Calculate the circle radius based on distance (e.g., closer = larger, farther = smaller)
            float baseRadius = 50.0f; // Adjust this base value as needed
            float circleRadius = baseRadius / distance;

            // Draw the circle on the head if the head is visible on screen
            if (headScreenPos.X > 0 && headScreenPos.Y > 0)
            {
                drawList.AddCircle(headScreenPos, circleRadius, circleColor, 30); // 30 segments for the circle
            }

        }
        private void DrawLine(ImDrawListPtr drawList, Vector2 startPos, Vector2 endPos, uint color)
        {
            if (startPos.X > 0 && startPos.Y > 0 && endPos.X > 0 && endPos.Y > 0)
            {
                float baseThickness = 1.5f;

                for (int i = 0; i < 5; i++)
                {
                    float glowThickness = baseThickness + i * 2.0f;
                    uint glowColor = (uint)((192 << 24) | (0 << 16) | (192 << 8) | (255 - i * 50));

                    drawList.AddLine(startPos, endPos, glowColor, glowThickness);
                }

                drawList.AddLine(startPos, endPos, color, baseThickness);
            }
        }

        public void DrawSmoothCircle(float radius, uint color, float thickness, int segments = 64)
        {
            var vList = ImGui.GetForegroundDrawList();
            var io = ImGui.GetIO();
            float centerX = io.DisplaySize.X / 2;
            float centerY = io.DisplaySize.Y / 2;

            vList.AddCircle(new Vector2(centerX, centerY), radius, color, segments, thickness);
        }

        public void HealthBar(short health, short maxHealth, float X, float Y, float height)
        {
            var vList = ImGui.GetForegroundDrawList();
            if (maxHealth <= 0) maxHealth = 200; // Fallback to a default max health
            float healthPercentage = (float)health / maxHealth;
            float barHeight = height * healthPercentage;

            // Set the color for the outline and the background
            uint outlineColor = ColorToUint32(Color.Black); // Black outline
            uint backgroundColor = ColorToUint32(Color.Black); // Black background

            // Set the color based on the health percentage
            uint barColor;

            if (healthPercentage > 0.8f)
            {
                // Yellow for 80% - 100%
                barColor = ColorToUint32(Color.Yellow);
            }
            else if (healthPercentage > 0.4f)
            {
                // Orange for 40% - 80%
                barColor = ColorToUint32(Color.Orange);
            }
            else
            {
                // Red for 0% - 40%
                barColor = ColorToUint32(Color.Red);
            }

            // Draw the outline of the health bar (black) with rounded corners (radius 1)
            vList.AddRect(new Vector2(X - 1, Y - 1), new Vector2(X + 5, Y + height + 1), outlineColor, 1.0f);

            // Draw the background (empty) health bar (black) with rounded corners (radius 1)
            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + 4, Y + height), backgroundColor, 1.0f);

            // Draw the filled portion of the health bar based on the health percentage with rounded corners (radius 1)
            vList.AddRectFilled(new Vector2(X, Y + (height - barHeight)), new Vector2(X + 4, Y + height), barColor, 1.0f);
        }




        public void DrawHealthBar(short health, short maxHealth, float X, float Y, float height, float width)
        {
            var vList = ImGui.GetForegroundDrawList();

            // Prevent division by zero and ensure healthPercentage is between 0 and 1
            if (maxHealth <= 0) maxHealth = 100; // Fallback to a default max health
            float healthPercentage = Math.Clamp((float)health / maxHealth, 0f, 1f);
            float healthWidth = width * healthPercentage;

            // Determine the color based on health percentage
            Color healthColor;


            if (healthPercentage < 0.3f)
            {
                healthColor = Color.FromArgb((int)(1f * 255), 255, 0, 0); // Red for health < 20%
            }
            else if (healthPercentage < 0.8f)
            {
                healthColor = Color.FromArgb((int)(1f * 255), 255, 255, 0); // Yellow for health < 70%
            }
            else
            {
                healthColor = Color.FromArgb((int)(1f * 255), 86, 255, 43); // Green for health >= 70%
            }

            // Draw the full health bar background (unfilled part)
            vList.AddRectFilled(new Vector2(X, Y - height), new Vector2(X + width, Y), ColorToUint32(Color.FromArgb((int)(1f * 255), 99, 0, 0))); // Background for health bar

            // Draw the health portion representing current health
            vList.AddRectFilled(new Vector2(X, Y - height), new Vector2(X + healthWidth, Y), ColorToUint32(healthColor)); // Health portion

            // Draw the black outline around the health bar
            vList.AddRect(new Vector2(X, Y - height), new Vector2(X + width, Y), ColorToUint32(Color.Black), 1f); // Black outline
        }
        static uint ColorToUint32(Color color)
        {
            return ImGui.ColorConvertFloat4ToU32(new Vector4(
            (float)(color.R / 255.0),
                (float)(color.G / 255.0),
                (float)(color.B / 255.0),
                (float)(color.A / 255.0)));
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);


        const uint WDA_NONE = 0x00000000;
        const uint WDA_MONITOR = 0x00000001;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
        void CreateHandle()
        {
            RECT rect;
            GetWindowRect(Core.Handle, out rect);
            int x = rect.Left;
            int y = rect.Top;
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;
            ImGui.SetWindowSize(new Vector2((float)width, (float)height));
            ImGui.SetWindowPos(new Vector2((float)x, (float)y));
            Size = new Size(width, height);
            Position = new Point(x, y);

            Core.Width = width;
            Core.Height = height;
            if (Config.StreamMode)
            {
                SetWindowDisplayAffinity(hWnd, WDA_EXCLUDEFROMCAPTURE);
            }
            else
            {
                SetWindowDisplayAffinity(hWnd, WDA_NONE);
            }
        }
    }
}




