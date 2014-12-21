using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// ItemsControl containing MetroTileFrame controls. Responsible for managing the transition between frames in a multi-frame MetroTile.
    /// </summary>
    public class MetroTileFramesPresenter : ItemsControl
    {
        static MetroTileFramesPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTileFramesPresenter), new FrameworkPropertyMetadata(typeof(MetroTileFramesPresenter)));
        }

        private MetroTileFrame _CurrentFrame;
        private Storyboard _TransitionAnimation;
        private DispatcherTimer _Timer;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MetroTileFramesPresenter()
        {
            Loaded += delegate { EnsureTimer(); };
        }

        public IEnumerable<MetroTileFrame> Frames
        {
            get
            {
                for(int i = 0; i < Items.Count; i++)
                {
                    var frame = ItemContainerGenerator.ContainerFromIndex(i) as MetroTileFrame;
                    if (frame != null)
                        yield return frame;
                }
            }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(Duration), typeof(MetroTileFramesPresenter), new UIPropertyMetadata(new Duration(TimeSpan.FromSeconds(0.25))));
        /// <summary>
        /// Gets or sets the duration of the animation for transitioning between frames.
        /// </summary>
        public Duration AnimationDuration
        {
            get { return (Duration)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }
        
        /// <summary>
        /// Overriding to ensure container is MetroTileFrameItem.
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MetroTileFrame;
        }

        /// <summary>
        /// Overriding to return MetroTileFrameItem.
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MetroTileFrame();
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
 	         base.PrepareContainerForItemOverride(element, item);

             var frame = (MetroTileFrame)element;
             var translateTransform = frame.GetTransform<TranslateTransform>(true);
             if (_CurrentFrame == null)
             {
                 _CurrentFrame = frame;
             }
             else
             {
                 translateTransform.Bind(TranslateTransform.YProperty, this, "ActualHeight");
             }
        }

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (IsLoaded)
                EnsureTimer();
        }

        /// <summary>
        /// Virtual method called to animate the transition between frames.
        /// </summary>
        protected virtual void AnimateTransition(MetroTileFrame currentFrame, MetroTileFrame nextFrame)
        {
            if (_TransitionAnimation == null)
                _TransitionAnimation = CreateTransitionAnimation();

            Storyboard.SetTarget(_TransitionAnimation.Children[0], currentFrame);
            Storyboard.SetTarget(_TransitionAnimation.Children[1], nextFrame);

            _TransitionAnimation.Begin();
        }

        private Storyboard CreateTransitionAnimation()
        {
            var animation = new Storyboard { Duration = AnimationDuration };

            var leavingAnimation = new DoubleAnimation
            {
                FillBehavior = FillBehavior.HoldEnd,
                Duration = AnimationDuration,
                To = -ActualHeight,
            };
            Storyboard.SetTargetProperty(leavingAnimation, new PropertyPath("(0).(1)", MetroTileFrame.RenderTransformProperty, TranslateTransform.YProperty));
            animation.Children.Add(leavingAnimation);

            var enteringAnimation = new DoubleAnimation
            {
                FillBehavior = FillBehavior.HoldEnd,
                Duration = AnimationDuration,
                To = 0,
                From = ActualHeight,
            };
            Storyboard.SetTargetProperty(enteringAnimation, new PropertyPath("(0).(1)", MetroTileFrame.RenderTransformProperty, TranslateTransform.YProperty));
            animation.Children.Add(enteringAnimation);

            return animation;
        }

        private void EnsureTimer()
        {
            if (Items.Count <= 1)
            {
                if (_Timer != null)
                    _Timer.Stop();
                _Timer = null;
            }
            else if(_Timer == null)
            {
                _Timer = new DispatcherTimer();
                _Timer.Tick += HandleTimerTick;
                StartTimerForCurrentFrame();
            }
        }

        private void HandleTimerTick(object sender, EventArgs e)
        {
            var nextFrameIndex = ItemContainerGenerator.IndexFromContainer(_CurrentFrame) + 1;
            if (nextFrameIndex >= Items.Count)
                nextFrameIndex = 0;

            var nextFrame = (MetroTileFrame)ItemContainerGenerator.ContainerFromIndex(nextFrameIndex);

            AnimateTransition(_CurrentFrame, nextFrame);
            _CurrentFrame = nextFrame;

            StartTimerForCurrentFrame();
        }

        private void StartTimerForCurrentFrame()
        {
            if(_Timer.IsEnabled)
                _Timer.Stop();
            _Timer.Interval = _CurrentFrame.DisplayDuration.TimeSpan;
            _Timer.Start();
        }
    }
}
