using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    /// <summary>
    /// Implementation of a control which represents a single Page of Metro Tiles in the MetroControl.
    /// </summary>
    [TemplatePart(Name = "MainContent", Type = typeof(FrameworkElement))]
    [DesignTimeVisible(true)]
    public class MetroStartPage : ItemsControl
    {
        #region Fields, Construction

        /// <summary>
        /// Identifies the DataTemplate used for drag adorner when drag/drop of Tiles is enabled.
        /// </summary>
        public static readonly ComponentResourceKey DragAdornerTemplateKey = new ComponentResourceKey(typeof(MetroStartPage), "DragAdornerTemplateKey");

        static MetroStartPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroStartPage), new FrameworkPropertyMetadata(typeof(MetroStartPage)));
        }

        // Used to remember available height from last time MeasureOverride was called for arranging
        // the tiles when no MaximumRows or MaximumCols has been specified.
        private double? _MeasuredHeightConstraint;
        // The items host canvas.
        private Canvas _Canvas;

        private Storyboard _Storyboard;
        private bool _IsAnimating;
        private bool _TilePositionPending;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MetroStartPage()
        {
            Loaded += delegate { PositionTiles(); };
        }
        
        #endregion // Fields, Construction

        #region Clr properties

        /// <summary>
        /// Returns an enumeration of the MetroTiles which are the containers of the items in this ItemsControl.
        /// </summary>
        public IEnumerable<MetroTile> Tiles
        {
            get
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var tile = ItemContainerGenerator.ContainerFromIndex(i) as MetroTile;
                    if (tile != null)
                        yield return tile;
                }
            }
        }

        /// <summary>
        /// Element which represents or contains the main, visible, content of the page,
        /// not including margins or padding or other spacing elements which are used to 
        /// provide separation esp. when page is first or last page and for when dragging between
        /// pages and a space is created for visual cue to user.
        /// Made available specifically for Drag/Drop hit testing.
        /// </summary>
        public FrameworkElement MainContent { get; protected set; }

        #endregion // Clr properties

        /// <summary>
        /// Using a DevComponents RoutedEvent for Repositioned.
        /// </summary>
        public static readonly RoutedEvent RepositionedEvent =
            EventManager.RegisterRoutedEvent("Repositioned", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MetroStartPage));
        /// <summary>
        /// Routed event raised when the tiles in this page are repositioned.
        /// </summary>
        public event RoutedEventHandler Repositioned
        {
            add { this.AddHandler(RepositionedEvent, value); }
            remove { this.RemoveHandler(RepositionedEvent, value); }
        }
        
        #region Dependency Properties

        /// <summary>
        // Using a DependencyProperty as the backing store for MaximumRows.
        /// </summary>
        public static readonly DependencyProperty MaximumRowsProperty =
            DependencyProperty.Register("MaximumRows", typeof(int), typeof(MetroStartPage), new UIPropertyMetadata(0));
        /// <summary>
        /// Get or set the maximum number of rows in this page. A value of 0 or less means undefined. 
        /// This is a dependency property. The default value is 3.
        /// </summary>
        public int MaximumRows
        {
            get { return (int)GetValue(MaximumRowsProperty); }
            set { SetCurrentValue(MaximumRowsProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for MaximumColumns.
        /// </summary>
        public static readonly DependencyProperty MaximumColumnsProperty =
            DependencyProperty.Register("MaximumColumns", typeof(int), typeof(MetroStartPage), new UIPropertyMetadata(0));
        /// <summary>
        /// Get or set the maximum number of columns in this page. A value of 0 or less means undefined. 
        /// This property is ignored if Orientation == Vertical.
        /// This is a dependency property. The default value is 0.
        /// </summary>
        public int MaximumColumns
        {
            get { return (int)GetValue(MaximumColumnsProperty); }
            set { SetCurrentValue(MaximumColumnsProperty, value); }
        }

        /// <summary>
        // Using a DependencyProperty as the backing store for Orientation.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MetroStartPage), new UIPropertyMetadata(Orientation.Vertical));
        /// <summary>
        /// Get or Set the direction in which items are added to the view. 
        /// A value of Horizontal results in items being layout in order first by row, second by column. Visa-versa for Vertical orientation.
        /// This is a dependency property. The default value is Vertical.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetCurrentValue(OrientationProperty, value); }
        }

        #endregion // Dependency Properties

        #region Public Methods

        /// <summary>
        /// Forces a fresh layout of the Tiles.
        /// </summary>
        public void RefreshLayout()
        {
            Dispatcher.BeginInvoke(new Action(PositionTiles), DispatcherPriority.ContextIdle);
        }

        #endregion // Public Methods

        #region Overrides

        /// <summary>
        /// Overriding to setup state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MainContent = GetTemplateChild("MainContent") as FrameworkElement;

            _Canvas = this.GetItemsHost() as Canvas;
            if (_Canvas == null)
            {
                Loaded += delegate
                {
                    _Canvas = this.GetItemsHost() as Canvas;
                };
            }
        }

        /// <summary>
        /// Overriding to return MetroTile as container.
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MetroTile();
        }

        /// <summary>
        /// Overriding to ensure item is UIElement.
        /// </summary>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is UIElement;
        }

        /// <summary>
        /// Overriding to ensure Tiles are positioned correctly.
        /// </summary>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (IsLoaded)
            {
                // Dispatch to give time for container to be generated.
                Dispatcher.BeginInvoke(new Action(PositionTiles), DispatcherPriority.ContextIdle);
            }
        }

        /// <summary>
        /// Overriding to do tile layout when the number or rows and columns is determined 
        /// automatically based on available height.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            if (AutoLayout)
            {
                // If orientation is vertical, a value of 0 for max rows indicates auto layout. (max cols is ignored.)
                // If horizontal, then either max cols or max rows can be used to specify layout.
                if (!Double.IsInfinity(constraint.Height))
                    _MeasuredHeightConstraint = constraint.Height;
                else 
                    _MeasuredHeightConstraint = null;
                PositionTiles();
            }
            return base.MeasureOverride(constraint);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            PositionTiles();
        }

        #endregion // Overrides

        #region Protected

        protected virtual void OnRepositioned()
        {
            var args = new RoutedEventArgs(RepositionedEvent, this);
            RaiseEvent(args);
        }

        #endregion

        #region Private

        private bool AutoLayout
        {
            get
            {
                return ((Orientation == Orientation.Vertical && MaximumRows <= 0) || (Orientation == Orientation.Horizontal && MaximumColumns <= 0 && MaximumRows <= 0));
            }
        }

        private double GetAutoLayoutHeight()
        {
            if (_MeasuredHeightConstraint != null)
                return _MeasuredHeightConstraint.Value;
            var sv = this.GetVisualParent<ScrollViewer>();
            if (sv != null)
                return sv.ActualHeight;
            return ActualHeight;
        }        

        private void PositionTiles()
        {
            if (!IsLoaded)
                return;

            _TilePositionPending = _IsAnimating;
            if (_IsAnimating)
                return;

            double width = 0;
            double height = 0;

            if (_Storyboard == null)
            {
                _Storyboard = new Storyboard();
                _Storyboard.Completed += delegate 
                { 
                    _IsAnimating = false;
                    if (_TilePositionPending)
                        PositionTiles();
                };
            }
            else
            {
                if (_IsAnimating)
                    _Storyboard.Pause(this);
                _Storyboard.Children.Clear();
            }

            if (Orientation == Orientation.Vertical)
                ArrangeTilesVertically(_Storyboard, ref width, ref height);
            else
                ArrangeTilesHorizontally(_Storyboard, ref width, ref height);

            if (_Canvas != null)
            {
                // Explicitly set canvas width and height since it doesn't size itself to its contents.
                _Canvas.Height = height;
                _Canvas.Width = width;
            }

            if (_Storyboard.Children.Count > 0)
            {
                _TilePositionPending = false;
                _IsAnimating = true;
                _Storyboard.Begin(this);
            }

            OnRepositioned();
        }

        private void ArrangeTilesVertically(Storyboard storyboard, ref double width, ref double height)
        {
            int row = 0;
            double toTop = 0;
            double colWidth = 0, rowHeight = 0;
            double cellOffset = 0; // A cell might contain 2 small tiles or 1 large tile.
            int smallTiles = 0;
            int maxRows = MaximumRows;

            IEnumerable<MetroTile> tiles = Tiles;
            if (tiles.Count() == 0)
                return;

            if (maxRows <= 0)
            {
                tiles = tiles.ToList();
                double availableHeight = GetAutoLayoutHeight();
                double maxHeight = availableHeight - Padding.Top - Padding.Bottom;
                maxRows = (int)Math.Floor(maxHeight / tiles.First().DesiredSize.Height);
                if (maxRows == 0)
                    maxRows = 1;
            }

            foreach (var tile in tiles)
            {
                if (!tile.IsMeasureValid)
                    tile.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                double tileHeight = tile.DesiredSize.Height;
                double tileWidth = tile.DesiredSize.Width;

                bool newRow = tile.Type == MetroTileType.Large || smallTiles % 2 == 0;

                // If new row.
                if (newRow)
                {
                    row++;

                    if (row > maxRows)
                    {
                        width += colWidth;
                        height = Math.Max(height, rowHeight);
                        rowHeight = 0;
                        colWidth = 0;
                        row = 1;
                    }

                    cellOffset = 0;
                    toTop = rowHeight;
                    rowHeight += tileHeight;
                }

                colWidth = Math.Max(colWidth, tileWidth + cellOffset);

                double currLeft = Canvas.GetLeft(tile);
                double currTop = Canvas.GetTop(tile);
                double toLeft = width + cellOffset;

                bool adjusted = AdjustFromPositionVertical(ref currLeft, ref currTop, toLeft, toTop, tileWidth, tileHeight, row - 1, maxRows);
                if (adjusted)
                {
                    // Minimizes flicker.
                    tile.SetCurrentValue(Canvas.TopProperty, currTop);
                    tile.SetCurrentValue(Canvas.LeftProperty, currLeft);
                }

                if (adjusted || currLeft != toLeft)
                    CreateAnimation(storyboard, tile, Canvas.LeftProperty, currLeft, toLeft);
                if (adjusted || currTop != toTop)
                    CreateAnimation(storyboard, tile, Canvas.TopProperty, currTop, toTop);

                cellOffset = tileWidth;
                if (tile.Type == MetroTileType.Small)
                    smallTiles++;
                else
                    smallTiles = 0;
            }

            if (row <= maxRows)
            {
                width += colWidth;
                height = Math.Max(height, rowHeight);
            }
        }

        private void ArrangeTilesHorizontally(Storyboard storyboard, ref double width, ref double height)
        {
            double col = 0;
            double left = 0;
            double rowHeight = 0;
            double maxCols = MaximumColumns;
            int maxRows = MaximumRows;

            // Arranging horizontally, but available size will still be determined by height not width since the 
            // containing metro control is always oriented horizontally.

            var tiles = Tiles.ToList();
            if (tiles.Count == 0)
                return;

            if (maxCols <= 0 && maxRows <= 0)
            {
                double availableHeight = GetAutoLayoutHeight();
                // Specify maxRows based on available height. Assuming all tiles are the same height.
                double maxHeight = availableHeight - Padding.Top - Padding.Bottom;
                maxRows = (int)Math.Floor(maxHeight / tiles[0].DesiredSize.Height);
                if (maxRows == 0)
                    maxRows = 1;
            }
            if (maxCols <= 0 && maxRows > 0)
            {
                // With maxRows specified, max columns can be calculated based on the number of tiles,
                // taking into account small tiles equaling 1/2 a column width.
                tiles = tiles.ToList();
                double total = 0;
                foreach (var tile in tiles)
                {
                    if (tile.Type == MetroTileType.Large)
                        total = Math.Ceiling(total) + 1;
                    else
                        total += 0.5;
                }
                maxCols = Math.Floor(total / maxRows); //Math.Ceiling((total * 2) / maxRows) / 2;
                while (!WillFitHorizontalLayout(tiles, maxRows, maxCols))
                    maxCols += 0.5;
            }

            foreach (var tile in tiles)
            {
                var test = tile.Type == MetroTileType.Large ? col + 1 : col + 0.5;
                if (test > maxCols)
                {
                    height += rowHeight;
                    width = Math.Max(width, left);
                    col = 0;
                    left = rowHeight = 0;
                }

               if (!tile.IsMeasureValid)
                    tile.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                double tileHeight = tile.DesiredSize.Height;
                double tileWidth = tile.DesiredSize.Width;

                double currLeft = Canvas.GetLeft(tile);
                double currTop = Canvas.GetTop(tile);

                bool adjusted = AdjustFromPositionHorizontal(ref currLeft, ref currTop, left, height, tileWidth, tileHeight, col, maxCols, maxRows);
                if (adjusted)
                {
                    // Minimizes flicker.
                    tile.SetCurrentValue(Canvas.TopProperty, currTop);
                    tile.SetCurrentValue(Canvas.LeftProperty, currLeft);
                }

                if (currLeft != left)
                    CreateAnimation(storyboard, tile, Canvas.LeftProperty, currLeft, left);
                if (currTop != height)
                    CreateAnimation(storyboard, tile, Canvas.TopProperty, currTop, height);

                left += tileWidth;
                rowHeight = Math.Max(rowHeight, tileHeight);

                col = tile.Type == MetroTileType.Large ? col + 1 : col + 0.5;
            }

            //if (col < maxCols)
            //{
                height += rowHeight;
                width = Math.Max(width, left);
            //}
        }

        private bool WillFitHorizontalLayout(List<MetroTile> tiles, int maxRows, double numCols)
        {
            double col = 0;
            double row = 1;

            foreach (var tile in tiles)
            {
                var test = tile.Type == MetroTileType.Large ? col + 1 : col + 0.5;
                if (test > numCols)
                {
                    col = 0;
                    row++;
                }

                col = tile.Type == MetroTileType.Large ? col + 1 : col + 0.5;
            }

            return row <= maxRows;
        }


        private void CreateAnimation(Storyboard storyboard, FrameworkElement target, DependencyProperty property, double from, double to)
        {
            if (Double.IsNaN(from))
                target.SetCurrentValue(property, to);
            else
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(200),
                    To = to,
                    From = from,
                    EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(animation, target);
                Storyboard.SetTargetProperty(animation, new PropertyPath(property));
                storyboard.Children.Add(animation);
            }
        }

        private bool AdjustFromPositionVertical(ref double currLeft, ref double currTop, double toLeft, double toTop, double width, double height, int toRow, int maxRows)
        {
            if (toRow > 0 && toRow < maxRows - 1)
                return false;
            double fromRow = currTop / height;
            if (fromRow > 0.01 && fromRow < maxRows - 1.10)
                return false;

            double toColumn = toLeft / width;
            double fromColumn = currLeft / width;

            if (toRow == 0 && Math.Abs(fromRow - (maxRows - 1)) < 0.01 && Math.Abs(toColumn - (fromColumn + 1)) < 0.01)
            {
                currLeft = toLeft;
                currTop = toTop - height * 0.85;
                return true;
            }
            if (toRow == maxRows - 1 && fromRow <= 0.01 && Math.Abs((fromColumn - 1) - toColumn) < 0.01)
            {
                currLeft = toLeft;
                currTop = toTop + height * 0.85;
                return true;
            }

            return false;
        }

        private bool AdjustFromPositionHorizontal(ref double currLeft, ref double currTop, double toLeft, double toTop, double width, double height, double toCol, double maxCols, int maxRows)
        {
            if (toCol > 0 && toCol < maxCols - 1)
                return false;

            double toRow = toTop / height;
            double fromRow = currTop / height;
            double fromCol = currLeft / width;

            if (toCol == 0 && Math.Abs(maxCols - (fromCol + 1)) < 0.01 && toRow - fromRow > 0.98)
            {
                currTop = toTop;
                currLeft = toLeft - width * 0.5;
                return true;
            }


            return false;
        }

        #endregion // Private
    }
}
