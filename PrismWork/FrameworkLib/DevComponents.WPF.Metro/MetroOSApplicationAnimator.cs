using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// 3D transition animation provider for animating the items in the TransitioningSelector of a MetroControl to achieve look and feel
    /// of Metro OS.
    /// </summary>
    public class MetroOSApplicationAnimator : TransitionAnimation3DProvider
    {
        private const string TranslateTransform3DName = "TranslateTransform3D";
        private const string RotateTransform3DName = "RotateTransform3D";

        /// <summary>
        /// Overriding to create animation.
        /// </summary>
        protected override Storyboard GetEnteringViewStoryboard(Rectangle2D3D viewport3D, TransitionAnimationFlags flags = TransitionAnimationFlags.None)
        {
            if ((flags & TransitionAnimationFlags.SuccessiveRepeat) == TransitionAnimationFlags.SuccessiveRepeat)
                return CreateRepeatEnteringViewStoryboard(viewport3D);
            else
                return CreateFirstTimeEnteringViewStoryboard(viewport3D);
        }

        /// <summary>
        /// Overriding to create animation.
        /// </summary>
        protected override Storyboard GetLeavingViewStoryboard(Rectangle2D3D viewport3D, TransitionAnimationFlags flags = TransitionAnimationFlags.None)
        {
            return CreateLeavingViewStoryboard(viewport3D);
        }

        private Storyboard CreateFirstTimeEnteringViewStoryboard(Rectangle2D3D viewport3D)
        {
            var viewport2D = viewport3D.Viewport2D;

            var translateTransform = GetTransform<TranslateTransform3D>(viewport3D, viewport2D, TranslateTransform3DName);
            translateTransform.OffsetX = translateTransform.OffsetY = 0;

            var rotationTransform = GetTransform<RotateTransform3D>(viewport3D, viewport3D.Viewport2D, RotateTransform3DName);
            rotationTransform.CenterX = Side == Dock.Left ? viewport2D.Geometry.Bounds.SizeX / 2 : -viewport2D.Geometry.Bounds.SizeX / 2;
            rotationTransform.Rotation = new AxisAngleRotation3D 
            {
                Angle = Side == Dock.Left ? -180 : 180, 
                Axis = new Vector3D(0, 1, 0) 
            };

            // Rotation animation    
            var duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds / 1.5));
            var beginTime = TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds - duration.TimeSpan.TotalMilliseconds);
            var rotationAnimation = new DoubleAnimation 
            { 
                To = 0, 
                From = Side == Dock.Left ? -180 : 180,
                BeginTime = beginTime, 
                Duration = duration 
            };

            Storyboard.SetTargetName(rotationAnimation, RotateTransform3DName);
            Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(0).(1)", RotateTransform3D.RotationProperty, AxisAngleRotation3D.AngleProperty));

            // Create storyboard.
            var storyboard = new Storyboard();
            storyboard.Children.Add(rotationAnimation);

            return storyboard;
        }

        private Storyboard CreateRepeatEnteringViewStoryboard(Rectangle2D3D viewport3D)
        {
            var viewport2D = viewport3D.Viewport2D;

            var translateTransform = GetTransform<TranslateTransform3D>(viewport3D, viewport2D, TranslateTransform3DName);
            translateTransform.OffsetX = Side == Dock.Left ? -viewport2D.Geometry.Bounds.SizeX : viewport2D.Geometry.Bounds.SizeX;
            translateTransform.OffsetY = -viewport2D.Geometry.Bounds.SizeY / 14;

            // OffsetX animation.
            var duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds / 1.2));
            var beginTime = TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds - duration.TimeSpan.TotalMilliseconds);
            var translateXAnimation = new DoubleAnimation 
            { 
                BeginTime = beginTime, 
                Duration = duration, 
                To = 0,
                From = Side == Dock.Left ? -viewport2D.Geometry.Bounds.SizeX : viewport2D.Geometry.Bounds.SizeX,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTargetName(translateXAnimation, TranslateTransform3DName);
            Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath("OffsetX"));

            // OffsetY animation.
            duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds / 3));
            beginTime = TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds - duration.TimeSpan.TotalMilliseconds);
            var translateYAnimation = new DoubleAnimation
            {
                Duration = duration,
                BeginTime = beginTime,
                To = 0,
                From = -viewport2D.Geometry.Bounds.SizeY / 14,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTargetName(translateYAnimation, TranslateTransform3DName);
            Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("OffsetY"));
            
            // Create storyboard.
            var storyboard = new Storyboard() { FillBehavior = FillBehavior.Stop };
            storyboard.Children.Add(translateXAnimation);
            storyboard.Children.Add(translateYAnimation);

            return storyboard;
        }

        private Storyboard CreateLeavingViewStoryboard(Rectangle2D3D viewport3D)
        {
            var viewport2D = viewport3D.Viewport2D;

            var translateTransform = GetTransform<TranslateTransform3D>(viewport3D, viewport2D, TranslateTransform3DName);
            translateTransform.OffsetX = translateTransform.OffsetY = 0;

            // Translate transform animation.
            var translateXAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds / 1.5)),
                From = 0,
                To = Side == Dock.Left ? -viewport2D.Geometry.Bounds.SizeX : viewport2D.Geometry.Bounds.SizeX,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTargetName(translateXAnimation, TranslateTransform3DName);
            Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath("OffsetX"));

            var translateYAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds / 3)),
                From = 0,
                To = -viewport2D.Geometry.Bounds.SizeY / 14,
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTargetName(translateYAnimation, TranslateTransform3DName);
            Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("OffsetY"));

            // Create storyboard.
            var storyboard = new Storyboard { Duration = Duration };
            storyboard.Children.Add(translateXAnimation);
            storyboard.Children.Add(translateYAnimation);

            return storyboard;
        }
    }
}
