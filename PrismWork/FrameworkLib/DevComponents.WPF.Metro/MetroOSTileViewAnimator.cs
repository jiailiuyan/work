using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.WPF.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using System.Windows;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// 3D transition animation provider for animating the view which contains the Tiles of MetroControl.
    /// </summary>
    public class MetroOSTileViewAnimator : TransitionAnimation3DProvider
    {
        /// <summary>
        /// Overriding to create animation.
        /// </summary>
        protected override Storyboard GetEnteringViewStoryboard(Rectangle2D3D viewport, TransitionAnimationFlags flags = TransitionAnimationFlags.None)
        {
            var storyboard = CreateEnteringViewStoryboard(viewport);
            return storyboard;
        }

        /// <summary>
        /// Overriding to create animation.
        /// </summary>
        protected override Storyboard GetLeavingViewStoryboard(Rectangle2D3D viewport, TransitionAnimationFlags flags = TransitionAnimationFlags.None)
        {
            var storyboard = CreateLeavingViewStoryboard(viewport);
            return storyboard;
        }

        private Storyboard CreateLeavingViewStoryboard(Rectangle2D3D viewport3D)
        {
            // Camera position animation.
            Point3DAnimation cameraPositionAnimation = new Point3DAnimation 
            { 
                Duration = Duration,
                To = new Point3D(0, 0, 200),
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn },
                FillBehavior = FillBehavior.Stop
            };
            cameraPositionAnimation.To = new Point3D(0, 0, 200);
            Storyboard.SetTarget(cameraPositionAnimation, viewport3D);
            Storyboard.SetTargetProperty(cameraPositionAnimation, new PropertyPath("(0).(1)", Rectangle2D3D.CameraProperty, PerspectiveCamera.PositionProperty));

            // Opacity amination.
            DoubleAnimation opacityAnimation = new DoubleAnimation 
            { 
                Duration = Duration, 
                To = 0, 
                From = 1,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            // Create storyboard.
            var storyboard = new Storyboard();
            storyboard.Children.Add(cameraPositionAnimation);
            storyboard.Children.Add(opacityAnimation);

            return storyboard;
        }

        private Storyboard CreateEnteringViewStoryboard(Rectangle2D3D viewport3D)
        {
            // Camera position animation.
            Point3DAnimation cameraPositionAnimation = new Point3DAnimation 
            { 
                Duration = Duration, 
                From = new Point3D(0, 0, 200),
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTarget(cameraPositionAnimation, viewport3D);
            Storyboard.SetTargetProperty(cameraPositionAnimation, new PropertyPath("(0).(1)", Rectangle2D3D.CameraProperty, PerspectiveCamera.PositionProperty));

            // Opacity amination.
            DoubleAnimation opacityAnimation = new DoubleAnimation { Duration = Duration, From = 0, To = 1 };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            // Create storyboard.
            var storyboard = new Storyboard();
            storyboard.Children.Add(cameraPositionAnimation);
            storyboard.Children.Add(opacityAnimation);

            return storyboard;
        }
    }
}
