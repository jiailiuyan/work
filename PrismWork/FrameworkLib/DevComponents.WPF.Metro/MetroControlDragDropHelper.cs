using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevComponents.WPF.Controls;

namespace DevComponents.WPF.Metro
{
    internal class MetroControlDragDropHelper
    {
        #region Fields, Construction

        private MetroControl _MetroControl;
        private Panel _MetroControlPanel;
        private MetroStartPage _DragSourcePage;
        private MetroTile _DragTile;
        private object _DragData;
        private IDragDropAdorner _DragAdorner;
        private TileDropHelper _TileDropHelper;
        private AutoScrollHelper _ScrollHelper;
        private DragTileIntoNewPageHelper _NewPageHelper;
        private Point _AdornerOffset;
        private Point _MouseDownPosition;
        private Point _CenterOffset;
        private bool _IsEnabled;
        private bool _IsDragging;

        private FrameworkElement HitTestReferenceElement
        {
            get
            {
                return _MetroControlPanel;
            }
        }

        public MetroControlDragDropHelper(MetroControl metroControl)
        {
            _MetroControl = metroControl;
            IsEnabled = true; // Trigger the setter which sets up the event handlers.
        }

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (value != _IsEnabled)
                {
                    _IsEnabled = value;
                    if (_IsEnabled)
                    {
                        // Setting handler for handled events too because MetroTile is a Button and Button captures mouse on mouse down.
                        _MetroControl.AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(HandleMetroControlMouseDown), true);
                        _MetroControl.AddHandler(FrameworkElement.MouseMoveEvent, new MouseEventHandler(HandleMetroControlMouseMove), true);
                    }
                    else
                    {
                        _MetroControl.RemoveHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(HandleMetroControlMouseDown));
                        _MetroControl.RemoveHandler(FrameworkElement.MouseMoveEvent, new MouseEventHandler(HandleMetroControlMouseMove));
                    }
                }
            }
        }

        #endregion // Fields, Construction

        #region Private

        private Point TranslateHitPoint(Point hitPoint, FrameworkElement relativeTo)
        {
            if (relativeTo == HitTestReferenceElement)
                return hitPoint;
            return HitTestReferenceElement.TranslatePoint(hitPoint, relativeTo);
        }

        private void PrepareForDrag()
        {
            var window = Window.GetWindow(_MetroControl);
            window.AllowDrop = true;
            window.DragEnter += HandleDrag;
            window.DragOver += HandleDrag;
            window.DragLeave += HandleDrag;
            window.Drop += HandleDrop;
            window.GiveFeedback += HandleGiveFeedback;

            if (_MetroControlPanel == null)
                _MetroControlPanel = _MetroControl.GetItemsHost();

            VisualStateManager.GoToState(_DragTile, "IsDragSource", true);
            _DragSourcePage = _DragTile.GetVisualParent<MetroStartPage>();
            _DragData = _DragSourcePage != null ? _DragSourcePage.ItemContainerGenerator.ItemFromContainer(_DragTile) : null;
            
            var adornerTemplate = (DataTemplate)_MetroControl.FindResource(MetroControl.DragAdornerDataTemplateKey);
            _DragAdorner = new TemplatedDragDropAdorner(_MetroControl, adornerTemplate, _DragData);

            _AdornerOffset = _MetroControl.TranslatePoint(_MouseDownPosition, _DragTile);

            _CenterOffset = new Point();
            _CenterOffset.Offset((_DragTile.RenderSize.Width / 2) - _AdornerOffset.X, (_DragTile.RenderSize.Height / 2) - _AdornerOffset.Y);

            _TileDropHelper = new TileDropHelper(this);
            _ScrollHelper = new AutoScrollHelper(this);
            if (CanCreateNewPages())
                _NewPageHelper = new DragTileIntoNewPageHelper(this);
        }

        private void CleanUpAfterDrag()
        {
            var window = Window.GetWindow(_MetroControl);
            window.ClearValue(Window.AllowDropProperty);
            window.DragEnter -= HandleDrag;
            window.DragOver -= HandleDrag;
            window.DragLeave -= HandleDrag;
            window.Drop -= HandleDrop;
            window.GiveFeedback -= HandleGiveFeedback;

            VisualStateManager.GoToState(_DragTile, "IsNotDragSource", true);

            if (_DragAdorner != null)
                _DragAdorner.Detach();
            if (_ScrollHelper != null)
                _ScrollHelper.Stop();

            _DragAdorner = null;
            _DragSourcePage = null;
            _DragTile = null;
            _DragData = null;
            _TileDropHelper = null;
            _ScrollHelper = null;
            _NewPageHelper = null;
        }

        private bool CanCreateNewPages()
        {
            bool canCreateNewPages = _MetroControl.CanUserCreateNewStartPage;

            // If using ItemsSource, must be ensured that item types have parameterless constructor in order
            // to create new page by dropping between pages.
            if (canCreateNewPages && _MetroControl.ItemsSource != null)
            {
                if (!_MetroControl.ItemsSource.GetType().IsList())
                    canCreateNewPages = false;
                var dataItem = _MetroControl.ItemContainerGenerator.ItemFromContainer(_DragSourcePage);
                var constructor = dataItem.GetType().GetConstructor(System.Type.EmptyTypes);
                if (constructor == null)
                    canCreateNewPages = false;
            }
            return canCreateNewPages;
        }

        private void UpdateAdornerPosition(DragEventArgs e)
        {
            _DragAdorner.SetPosition(Point.Subtract(e.GetPosition(_MetroControl), new Vector(_AdornerOffset.X, _AdornerOffset.Y)));
        }

        private Point GetCenterAdjustedPosition(Point mousePosition)
        {
            return new Point(mousePosition.X + _CenterOffset.X, mousePosition.Y + _CenterOffset.Y);
        }

        private void HandleMetroControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            _MouseDownPosition = e.GetPosition(_MetroControl);
            _DragTile = e.OriginalSource as MetroTile; 
            if(_DragTile == null)
            {
                var fe = e.OriginalSource as FrameworkElement;
                if (fe != null)
                    _DragTile = fe.GetVisualParent<MetroTile>();
            }
        }

        private void HandleMetroControlMouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDragging || _DragTile == null)
                return;
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                _DragTile = null;
                return;
            }

            var pos = e.GetPosition(_MetroControl);
            bool doDrag = ((Math.Abs(pos.X - _MouseDownPosition.X) >= 4 ||
                            Math.Abs(pos.Y - _MouseDownPosition.Y) >= 4));

            if (doDrag)
            {
                PrepareForDrag();
                _IsDragging = true;
                System.Windows.DragDrop.DoDragDrop(_MetroControl, _DragData, DragDropEffects.Move);
                _IsDragging = false;
                CleanUpAfterDrag();
            }
        }

        private void HandleDrag(object sender, DragEventArgs e)
        {
            UpdateView(e);
        }

        private void HandleDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;

            var hitPoint = GetCenterAdjustedPosition(HitTestReferenceElement.TrueCursorPosition());

            if (_ScrollHelper != null)
                _ScrollHelper.Stop();

            bool dropped = false;
            if (_NewPageHelper != null)
                dropped = _NewPageHelper.HandleDropped(hitPoint);
            dropped |= _TileDropHelper.HandleDropped(hitPoint);

            if (dropped)
            {
                if (_DragSourcePage.Items.Count == 0)
                {
                    _MetroControl.RemoveContainer(_DragSourcePage);
                }
            }
        }

        private void HandleGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects != DragDropEffects.None)
            {
                e.UseDefaultCursors = false;
                Mouse.OverrideCursor = Cursors.Arrow;
                e.Handled = true;
            }
        }

        private void UpdateView(DragEventArgs e)
        {
            e.Handled = true;

            var hitPoint = GetCenterAdjustedPosition(HitTestReferenceElement.TrueCursorPosition());

            if (_ScrollHelper != null)
                _ScrollHelper.DoScroll(hitPoint);

            bool canDrop = false;
            if (_NewPageHelper != null)
                canDrop = _NewPageHelper.UpdateVisual(hitPoint, _ScrollHelper.IsScrolling);
            canDrop = canDrop | _TileDropHelper.UpdateView(hitPoint, canDrop | _ScrollHelper.IsScrolling);

            UpdateAdornerPosition(e);

            if (hitPoint.Y >= 0 && hitPoint.Y <= _MetroControlPanel.ActualHeight)
                e.Effects = DragDropEffects.Move;
            else
                e.Effects = DragDropEffects.None;
        }

        #endregion // Private

        /// <summary>
        /// Helper class for managing the dragging and dropping of Tiles over other Tiles and into Pages.
        /// </summary>
        private class TileDropHelper
        {
            MetroControlDragDropHelper _DragHelper;
            MetroStartPage _DropPage;
            private int _DropIndex = -1;
            private MetroTile _DropTargetTile;  // Identifies the tile which is under the center point of the drag adorner.
            private MetroTile _DropTargetTile2; // Will be set if drop tile 1 is a small tile with another small tile beside it.
            private MetroTile _SecondaryDropTargetTile;  // Idendifies the tile which is nest to drop target tile. Tiles are animated to indicate that the drop will be placed between the two drop tiles.
            private MetroTile _SecondaryDropTargetTile2; // Will be set if secondary drop tile 1 is a small tile with another small tile beside it.

            public TileDropHelper(MetroControlDragDropHelper dragHelper)
            {
                _DragHelper = dragHelper;
            }

            public bool UpdateView(Point hitPoint, bool clear)
            {
                if (clear)
                {
                    Clear();
                    return false;
                }

                Point testPoint = _DragHelper.TranslateHitPoint(hitPoint, _DragHelper._MetroControlPanel);
                _DropPage = _DragHelper._MetroControlPanel.HitTestForOne<MetroStartPage>(testPoint);
                if (_DropPage == null)
                {
                    Clear();
                    return false;
                }

                SetDropTargetTiles(_DragHelper.TranslateHitPoint(hitPoint, _DropPage), _DropPage);

                return true;
            }

            public bool HandleDropped(Point hitPoint)
            {
                if (_DropIndex < 0)
                    return false;

                int dragIndex = _DragHelper._DragSourcePage.ItemContainerGenerator.IndexFromContainer(_DragHelper._DragTile);
                bool targetEqualsSource = dragIndex == _DropIndex && _DropPage == _DragHelper._DragSourcePage;

                if (!targetEqualsSource)
                    DoDrop();

                var tile = _DropPage.ItemContainerGenerator.ContainerFromItem(_DragHelper._DragData) as MetroTile;
                if (tile != null)
                {
                    Size tileSize = _DragHelper._DragTile.RenderSize;
                    var pos = _DragHelper.TranslateHitPoint(hitPoint, _DropPage.MainContent != null ? _DropPage.MainContent : _DropPage);
                    pos.Offset(-tileSize.Width / 2, -tileSize.Height / 2);

                    tile.SetCurrentValue(Canvas.LeftProperty, pos.X);
                    tile.SetCurrentValue(Canvas.TopProperty, pos.Y);

                    if (targetEqualsSource)
                    {
                        // must manually refresh tile positioning since no changes were made to tile list to trigger an update.
                        _DropPage.RefreshLayout();
                    }
                }

                Clear();

                return true;
            }

            private void DoDrop()
            {
                if (_DropPage == _DragHelper._DragSourcePage)
                {
                    _DropPage.MoveItem(_DragHelper._DragData, _DropIndex);
                }
                else
                {
                    _DragHelper._DragSourcePage.RemoveItem(_DragHelper._DragData);
                    _DropPage.AddItem(_DragHelper._DragData, _DropIndex);
                }
            }

            private void SetDropTargetTiles(Point position, MetroStartPage dropTargetPage)
            {
                var dragTile = _DragHelper._DragTile;
                bool suppressAnimation = false;
                MetroTile secondaryTarget = null, target2 = null, secondaryTarget2 = null;
                var target = dropTargetPage.HitTestForOne<MetroTile>(position, _DragHelper._DragTile.Margin);
                if (target == null && dragTile.Type == MetroTileType.Small)
                {
                    target = dropTargetPage.HitTestForOne<MetroTile>(position, new Thickness(0, 0, dragTile.Width, 0));
                    if (target != null && target.Type == MetroTileType.Large)
                        target = null;
                    suppressAnimation = true;
                }

                Dock hitSide = Dock.Left;
                if (target != null)
                {
                    hitSide = GetHitSide(dropTargetPage, target, dragTile, dropTargetPage.TranslatePoint(position, target));

                    // If vertical orientation, and hitting a small tile, there might be another small tile beside it which also needs to be animated.
                    if (_DropPage.Orientation == Orientation.Vertical && target.Type == MetroTileType.Small && dragTile.Type == MetroTileType.Large)
                    {
                        int index, adjacentIndex;
                        target2 = FindAdjacentSmallTile(target, out index, out adjacentIndex);
                        if (target2 != null)
                        {
                            // Might need to switch target and target2 because target defines the insertion index.
                            if ((hitSide == Dock.Top && adjacentIndex < index) || (hitSide == Dock.Bottom && adjacentIndex > index))
                            {
                                var temp = target;
                                target = target2;
                                target2 = temp;
                            }
                        }
                    }

                    // Look for secondary drop target if not dropping onto self or not dropping one small tile onto another small tile.
                    secondaryTarget = FindSecondaryTargetTile(dropTargetPage, target, dragTile, hitSide, out _DropIndex);

                    if (secondaryTarget != null && _DropPage.Orientation == Orientation.Vertical && secondaryTarget.Type == MetroTileType.Small && dragTile.Type == MetroTileType.Large)
                    {
                        int index, adjacentIndex;
                        secondaryTarget2 = FindAdjacentSmallTile(secondaryTarget, out index, out adjacentIndex);
                    }
                }
                else
                {
                    _DropIndex = _DropPage.Items.Count;
                    if (_DragHelper._DragSourcePage == _DropPage)
                        _DropIndex--;
                }

                if (!suppressAnimation)
                    UpdateDropTargetTiles(target, target2, secondaryTarget, secondaryTarget2, hitSide);
                else
                    UpdateDropTargetTiles(null, null, null, null, Dock.Left);
            }

            private void Clear()
            {
                _DropIndex = -1;
                UpdateDropTargetTiles(null, null, null, null, Dock.Left);
            }

            private MetroTile FindAdjacentSmallTile(MetroTile smallTile, out int index, out int adjacentIndex)
            {
                double tileTop = Canvas.GetTop(smallTile);
                index = _DropPage.ItemContainerGenerator.IndexFromContainer(smallTile);
                if (index > 0)
                {
                    adjacentIndex = index - 1;
                    var otherTile = _DropPage.ItemContainerGenerator.ContainerFromIndex(adjacentIndex) as MetroTile;
                    if (otherTile != null && otherTile.Type == MetroTileType.Small && Canvas.GetTop(otherTile) == tileTop)
                        return otherTile;
                }
                if (index < _DropPage.Items.Count - 1)
                {
                    adjacentIndex = index + 1;
                    var otherTile = _DropPage.ItemContainerGenerator.ContainerFromIndex(adjacentIndex) as MetroTile;
                    if (otherTile != null && otherTile.Type == MetroTileType.Small && Canvas.GetTop(otherTile) == tileTop)
                        return otherTile;
                }
                adjacentIndex = -1;
                return null;
            }

            private void UpdateDropTargetTiles(MetroTile newDropTarget, MetroTile newDropTarget2, MetroTile newSecondary, MetroTile newSecondary2, Dock side)
            {
                if (!IsOneOfOrNull(_DropTargetTile, newDropTarget, newDropTarget2, newSecondary, newSecondary2))
                    ClearDropTargetState(_DropTargetTile);
                if (!IsOneOfOrNull(_DropTargetTile2, newDropTarget, newDropTarget2, newSecondary, newSecondary2))
                    ClearDropTargetState(_DropTargetTile2);
                if (!IsOneOfOrNull(_SecondaryDropTargetTile, newDropTarget, newDropTarget2, newSecondary, newSecondary2))
                    ClearDropTargetState(_SecondaryDropTargetTile);
                if (!IsOneOfOrNull(_SecondaryDropTargetTile2, newDropTarget, newDropTarget2, newSecondary, newSecondary2))
                    ClearDropTargetState(_SecondaryDropTargetTile2);

                _DropTargetTile = newDropTarget;
                _DropTargetTile2 = newDropTarget2;
                _SecondaryDropTargetTile = newSecondary;
                _SecondaryDropTargetTile2 = newSecondary2;

                if (_DropTargetTile != null)
                    SetDropTargetState(_DropTargetTile, side);
                if (_DropTargetTile2 != null)
                    SetDropTargetState(_DropTargetTile2, side);
                if (_SecondaryDropTargetTile != null)
                    SetDropTargetState(_SecondaryDropTargetTile, OppositeSide(side));
                if (_SecondaryDropTargetTile2 != null)
                    SetDropTargetState(_SecondaryDropTargetTile2, OppositeSide(side));
            }

            private static bool IsOneOfOrNull(object target, params object[] others)
            {
                if (target == null)
                    return true;
                foreach (var obj in others)
                {
                    if (target == obj)
                        return true;
                }

                return false;
            }


            private static Dock OppositeSide(Dock side)
            {
                if (side == Dock.Left)
                    return Dock.Right;
                if (side == Dock.Right)
                    return Dock.Left;
                if (side == Dock.Top)
                    return Dock.Bottom;
                return Dock.Top;
            }

            private MetroTile FindSecondaryTargetTile(MetroStartPage targetPage, MetroTile targetTile, MetroTile dragTile, Dock hitSide, out int dropIndex)
            {
                int targetIndex = targetPage.ItemContainerGenerator.IndexFromContainer(targetTile);
                dropIndex = targetIndex;

                if (targetTile == _DragHelper._DragTile)
                    return null;

                int sourceIndex = targetPage.ItemContainerGenerator.IndexFromContainer(dragTile);
                if (sourceIndex < 0)
                    sourceIndex = int.MaxValue;
                int secondaryIndex = -1;

                if (hitSide == Dock.Top)
                {
                    if (!IsTileOnPageTop(targetPage, targetTile))
                        secondaryIndex = targetIndex - 1;
                    if (sourceIndex < targetIndex)
                        dropIndex--;
                }
                else if (hitSide == Dock.Bottom)
                {
                    if (!IsTileOnPageBottom(targetPage, targetTile))
                        secondaryIndex = targetIndex + 1;
                    if (sourceIndex > targetIndex)
                        dropIndex++;
                }
                else if (hitSide == Dock.Left)
                {
                    if (!IsTileOnPageLeft(targetPage, targetTile))
                        secondaryIndex = targetIndex - 1;
                    if (sourceIndex < targetIndex)
                        dropIndex--;
                }
                else if (hitSide == Dock.Right)
                {
                    if (!IsTileOnPageRight(targetPage, targetTile))
                        secondaryIndex = targetIndex + 1;
                    if (sourceIndex > targetIndex)
                        dropIndex++;
                }

                MetroTile secondary = null;
                if (secondaryIndex >= 0 && secondaryIndex < targetPage.Items.Count)
                {
                    secondary = targetPage.ItemContainerGenerator.ContainerFromIndex(secondaryIndex) as MetroTile;
                    // special case handling for when dropping small tile into empty space beside another small tile,
                    // in this situation there should be no secondary.
                    if (secondary != null && secondary.Type != MetroTileType.Small && secondary != dragTile && targetTile.Type == MetroTileType.Small && dragTile.Type == MetroTileType.Small)
                        secondary = null;

                }

                return secondary;
            }

            private bool IsTileOnPageBottom(MetroStartPage targetPage, FrameworkElement tile)
            {
                var target = targetPage.MainContent != null ? targetPage.MainContent : targetPage;
                Point tileBottom = tile.TransformToAncestor(target).Transform(new Point(tile.Width, tile.Height));
                return tileBottom.Y >= target.ActualHeight - tile.Margin.Bottom;
            }

            private bool IsTileOnPageTop(MetroStartPage targetPage, FrameworkElement tile)
            {
                var target = targetPage.MainContent != null ? targetPage.MainContent : targetPage;
                Point tileTop = tile.TransformToAncestor(target).Transform(new Point(0, 0));
                return tileTop.Y <= tile.Margin.Top;
            }

            private bool IsTileOnPageLeft(MetroStartPage targetPage, FrameworkElement tile)
            {
                var target = targetPage.MainContent != null ? targetPage.MainContent : targetPage;
                Point tileLeft = tile.TransformToAncestor(target).Transform(new Point(0, 0));
                return tileLeft.X <= tile.Margin.Left;
            }

            private bool IsTileOnPageRight(MetroStartPage targetPage, FrameworkElement tile)
            {
                var target = targetPage.MainContent != null ? targetPage.MainContent : targetPage;
                Point tileRight = tile.TransformToAncestor(target).Transform(new Point(tile.Width, tile.Height));
                return tileRight.X >= target.ActualWidth - tile.Margin.Left;
            }

            private Dock GetHitSide(MetroStartPage targetPage, MetroTile hitTile, MetroTile dragTile, Point pos)
            {
                // For vertically oriented pages, if dragging a small tile and the hit tile is also small,
                // treat as if horizontal oriented.
                if (targetPage.Orientation == Orientation.Vertical && !(hitTile.Type == MetroTileType.Small && dragTile.Type == MetroTileType.Small))
                {
                    double dY = (hitTile.RenderSize.Height / 2) - pos.Y;
                    if (dY > 0)
                        return Dock.Top;
                    return Dock.Bottom;
                }
                else
                {
                    double dX = (hitTile.RenderSize.Width / 2) - pos.X;
                    if (dX > 0)
                        return Dock.Left;
                    return Dock.Right;
                }
            }

            private void SetDropTargetState(MetroTile tile, Dock side)
            {
                if (tile == _DragHelper._DragTile)
                    return;

                string state;
                if (side == Dock.Left)
                {
                    state = "DropOnLeft";
                }
                else if (side == Dock.Top)
                {
                    state = "DropOnTop";
                }
                else if (side == Dock.Right)
                {
                    state = "DropOnRight";
                }
                else
                {
                    state = "DropOnBottom";
                }

                VisualStateManager.GoToState(tile, state, true);
            }

            private void ClearDropTargetState(MetroTile tile)
            {
                VisualStateManager.GoToState(tile, "NoDrop", true);
            }
        }

        /// <summary>
        /// Private class to help out by managing the dragging and dropping of tiles over the area between pages
        /// where it is possible to drop a tile and place it into a newly created page.
        /// </summary>
        private class DragTileIntoNewPageHelper
        {
            private MetroControlDragDropHelper _DragHelper;
            private MetroStartPage _SpreadPageLeft;
            private MetroStartPage _SpreadPageRight;

            public DragTileIntoNewPageHelper(MetroControlDragDropHelper dragHelper)
            {
                _DragHelper = dragHelper;
            }

            public bool UpdateVisual(Point hitPoint, bool clear)
            {
                if (clear)
                {
                    ClearSpreadPageStates();
                    return false;
                }

                return SpreadPages(hitPoint);
            }

            public bool HandleDropped(Point hitPoint)
            {
                bool dropped = DoDrop();
                ClearSpreadPageStates();
                return dropped;
            }

            private bool SpreadPages(Point hitPoint)
            {
                MetroStartPage pageLeft = null, pageRight = null;
                bool doSearch = true;

                var testPoint = _DragHelper.TranslateHitPoint(hitPoint, _DragHelper._MetroControlPanel);
                if (testPoint.Y < 0 || testPoint.Y > _DragHelper._MetroControlPanel.ActualHeight)
                    doSearch = false;

                var hitPage = _DragHelper._MetroControlPanel.HitTestForOne<MetroStartPage>(testPoint);
                if (hitPage != null)
                {
                    if (hitPage.MainContent != null)
                    {
                        var contentPos = _DragHelper.TranslateHitPoint(hitPoint, hitPage.MainContent);
                        if (contentPos.X < 0)
                            pageRight = hitPage;
                        else if (contentPos.X > hitPage.MainContent.ActualWidth)
                            pageLeft = hitPage;
                    }
                }

                if (doSearch)
                {
                    int leftIndex = 0;
                    if (pageLeft != null)
                        leftIndex = _DragHelper._MetroControl.ItemContainerGenerator.IndexFromContainer(pageLeft) + 1;
                    int rightIndex = _DragHelper._MetroControl.Items.Count;
                    if (pageRight != null)
                        rightIndex = _DragHelper._MetroControl.ItemContainerGenerator.IndexFromContainer(pageRight);

                    for (int i = leftIndex; i < rightIndex; i++)
                    {
                        var page = _DragHelper._MetroControlPanel.Children[i] as MetroStartPage;

                        // Pages are separated by margins on grids defined in their templates. The 
                        // grids don't have background set so drag events make it through. Here we're
                        // getting the visible portion of the page against which to test mouse position.
                        var visible = page.MainContent;
                        if (visible == null)
                            visible = page;

                        var hitPos = _DragHelper.TranslateHitPoint(hitPoint, visible);
                        if (visible.IsPointWithin(hitPos))
                        {
                            pageLeft = pageRight = null;
                            break;
                        }
                        else if (hitPos.X < 0)
                        {
                            pageRight = page;
                            break;
                        }
                        else if (hitPos.X > visible.ActualWidth)
                            pageLeft = page;
                    }

                    if (_DragHelper._DragSourcePage.Items.Count == 1 && (pageLeft == _DragHelper._DragSourcePage || pageRight == _DragHelper._DragSourcePage))
                    {
                        pageRight = pageLeft = null;
                    }
                }

                if (pageLeft != _SpreadPageLeft)
                {
                    if (_SpreadPageLeft != null)
                        VisualStateManager.GoToState(_SpreadPageLeft, "NoSpread", true);
                    if (pageLeft != null)
                        VisualStateManager.GoToState(pageLeft, "SpreadLeft", true);
                    _SpreadPageLeft = pageLeft;
                }

                if (pageRight != _SpreadPageRight)
                {
                    if (_SpreadPageRight != null)
                        VisualStateManager.GoToState(_SpreadPageRight, "NoSpread", true);
                    if (pageRight != null)
                        VisualStateManager.GoToState(pageRight, "SpreadRight", true);
                    _SpreadPageRight = pageRight;
                }

                return pageLeft != null || pageRight != null;
            }

            private void ClearSpreadPageStates()
            {
                if (_SpreadPageLeft != null)
                {
                    VisualStateManager.GoToState(_SpreadPageLeft, "NoSpread", true);
                    _SpreadPageLeft = null;
                }
                if (_SpreadPageRight != null)
                {
                    VisualStateManager.GoToState(_SpreadPageRight, "NoSpread", true);
                    _SpreadPageRight = null;
                }
            }

            private bool DoDrop()
            {
                if (_SpreadPageLeft == null && _SpreadPageRight == null)
                    return false;

                // Can't rely on Parent Items.Count...
                int index = -1;
                if (_SpreadPageRight != null)
                {
                    index = _DragHelper._MetroControl.ItemContainerGenerator.IndexFromContainer(_SpreadPageRight);
                }

                object newPageItem = null;
                if (_DragHelper._MetroControl.ItemsSource == null)
                {
                    newPageItem = new MetroStartPage();
                    if (index >= 0)
                        _DragHelper._MetroControl.Items.Insert(index, newPageItem);
                    else
                    {
                        _DragHelper._MetroControl.Items.Add(newPageItem);
                    }
                }
                else
                {
                    // Can't insert into Items when ItemsSource is in use. Made check when setting up 
                    // MetroControl as drop target to ensure that the data type used for ItemsSource 
                    // has a default constructor.
                    var pageDataType = _DragHelper._MetroControl.ItemContainerGenerator.ItemFromContainer(_DragHelper._DragSourcePage).GetType();
                    var constructor = pageDataType.GetConstructor(System.Type.EmptyTypes);
                    dynamic page = constructor.Invoke(null);
                    // Also made check that ItemsSource is a list, but it might be generic and might not be generic,
                    // so use dynamic here so it won't matter when making call to insert.
                    dynamic list = _DragHelper._MetroControl.ItemsSource;
                    if (index >= 0)
                        list.Insert(index, page);
                    else
                    {
                        list.Add(page);
                    }

                    newPageItem = page;
                }

                _DragHelper._DragSourcePage.RemoveItem(_DragHelper._DragData);

                // Dispatch method call to add tile data to new page, so the new page has time to be created.
                _DragHelper._MetroControl.Dispatcher.BeginInvoke(
                    new Action<MetroControl, object, object, int>(AddDraggedDataToNewPageDispatcherCallback),
                    DispatcherPriority.ApplicationIdle,
                    _DragHelper._MetroControl, newPageItem, _DragHelper._DragData, 0);

                return true;
            }

            private static void AddDraggedDataToNewPageDispatcherCallback(MetroControl parentControl, object newPageItem, object tileData, int callCount = 0)
            {
                var newPage = parentControl.ItemContainerGenerator.ContainerFromItem(newPageItem) as MetroStartPage;
                if (newPage == null)
                {
                    if (callCount < 1)
                    {
                        parentControl.Dispatcher.BeginInvoke(
                        new Action<MetroControl, object, object, int>(AddDraggedDataToNewPageDispatcherCallback),
                        DispatcherPriority.ApplicationIdle,
                        parentControl, newPageItem, tileData, callCount + 1);
                    }
                    return;
                }

                // Must repeat process of checking for ItemsSource with the new page.
                if (newPage.ItemsSource == null)
                {
                    newPage.Items.Add(tileData);
                }
                else
                {
                    dynamic list = newPage.ItemsSource;
                    dynamic ddata = tileData;
                    list.Add(ddata);
                }
            }

        }

        /// <summary>
        /// Helper class for managing the auto scroll feature.
        /// </summary>           
        private class AutoScrollHelper
        {
            private MetroControlDragDropHelper _DragHelper;

            private ScrollViewer _ScrollViewer;
            private Storyboard _Storyboard;
            private double _EffectiveSpeedRatio;
            private double _LastMousePosition = Double.NaN;

            public AutoScrollHelper(MetroControlDragDropHelper dragHelper)
            {
                _DragHelper = dragHelper;
                _ScrollViewer = _DragHelper._MetroControl.GetFirstDescendent<ScrollViewer>();
            }

            public bool IsScrolling { get; private set; }

            public void Stop()
            {
                StopStoryboard(true);
            }

            public bool DoScroll(Point hitPoint)
            {
                Point pos = _DragHelper.TranslateHitPoint(hitPoint, _ScrollViewer);

                if (pos.Y >= 0 && pos.Y <= _ScrollViewer.ActualHeight)
                {
                    IsScrolling = UpdateScroll(pos.X);
                }
                else
                {
                    StopStoryboard(true);
                    IsScrolling = false;
                    _LastMousePosition = Double.NaN;
                }

                return IsScrolling;
            }

            private bool UpdateScroll(double centerX)
            {
                if (Double.IsNaN(_LastMousePosition))
                    _LastMousePosition = centerX;
                double mouseMovement = centerX - _LastMousePosition;
                _LastMousePosition = centerX;

                if (mouseMovement == 0) return IsScrolling;

                int scroll = 0;
                double buffer = _DragHelper._DragTile.ActualWidth / 2 + 5;

                if (centerX < buffer && _ScrollViewer.HorizontalOffset > 0 && mouseMovement < 0)
                {
                    scroll = (int)Math.Ceiling(centerX - buffer);
                }
                else if (mouseMovement > 0 && centerX > _ScrollViewer.ViewportWidth - buffer &&
                         _ScrollViewer.HorizontalOffset < _ScrollViewer.ExtentWidth - _ScrollViewer.ViewportWidth)
                {
                    scroll = (int)Math.Ceiling(centerX - (_ScrollViewer.ViewportWidth - buffer));
                }

                if (IsScrolling)
                {
                    if (scroll == 0)
                    {
                        StopStoryboard(false);
                    }
                    else
                    {
                        double maxSpeedRatio = Math.Abs(scroll) / 18d;
                        _EffectiveSpeedRatio = Math.Min(_EffectiveSpeedRatio += 0.2, maxSpeedRatio);
                        _Storyboard.SetSpeedRatio(_ScrollViewer, Math.Ceiling(_EffectiveSpeedRatio));
                    }
                }
                else if (scroll != 0)
                {
                    _EffectiveSpeedRatio = 1;
                    StartScrollStoryboard(scroll);
                }

                return scroll != 0;
            }

            private void StartScrollStoryboard(int scroll)
            {
                double distance;
                if (scroll > 0)
                {
                    distance = (int)(_ScrollViewer.ExtentWidth - _ScrollViewer.HorizontalOffset + _ScrollViewer.ViewportWidth);
                }
                else
                {
                    distance = -(int)(_ScrollViewer.HorizontalOffset);
                }

                DoubleAnimation animation;
                if (_Storyboard == null)
                {
                    animation = new DoubleAnimation();
                    Storyboard.SetTarget(animation, _ScrollViewer);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(ScrollViewerBehavior.HorizontalOffsetProperty));

                    _Storyboard = new Storyboard();
                    _Storyboard.Children.Add(animation);
                    _Storyboard.Completed += delegate
                    {
                        IsScrolling = false;
                    };
                }
                else
                {
                    animation = (DoubleAnimation)_Storyboard.Children[0];
                }

                animation.Duration = TimeSpan.FromMilliseconds(Math.Abs(distance) * 6);
                animation.To = _ScrollViewer.HorizontalOffset + distance;
                animation.From = _ScrollViewer.HorizontalOffset;
                _Storyboard.Begin(_ScrollViewer, true);
                IsScrolling = true;
            }

            private void StopStoryboard(bool immediate)
            {
                if (_Storyboard == null)
                    return;

                if (immediate)
                {
                    _Storyboard.Pause(_ScrollViewer);
                }
                else
                {
                    DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };

                    EventHandler handler = null;
                    handler = delegate
                    {
                        timer.Tick -= handler;
                        timer.Stop();
                        if (_ScrollViewer != null)
                            _Storyboard.Pause(_ScrollViewer);
                    };

                    timer.Tick += handler;
                    timer.Start();
                }
                IsScrolling = false;
            }
        }
    }
}
