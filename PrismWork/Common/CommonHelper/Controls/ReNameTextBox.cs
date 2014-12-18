/* 迹I柳燕
 * 
 * FileName:   ReNameTextBox.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      ReNameTextBox
 * @extends    TextBox
 * 
 *             对于需要执行切换显示模式进行更改名称的TextBox
 *             此项未完成，需要重新修改关于名称判断和完成事件通知
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Jisons
{
    public interface ReNameOfUIElement : IInputElement
    {
        string ChangedName { get; set; }
        bool IsEditor { get; set; }
    }

    public class ReNameTextBox : TextBox
    {
        //public UIElement ParentUIElement
        //{
        //    get { return (UIElement)GetValue(ParentUCProperty); }
        //    set { SetValue(ParentUCProperty, value); }
        //}
        //public static readonly DependencyProperty ParentUCProperty =
        //    DependencyProperty.Register("ParentUC", typeof(UIElement), typeof(ReNameTextBox), new PropertyMetadata(null, ParentUCChanged));

        public ReNameTextBox()
        {
            this.Loaded += delegate
            {
                this.IsVisibleChanged += SPReNameTextBox_IsVisibleChanged;
            };

            this.Unloaded += delegate
            {
                this.IsVisibleChanged -= SPReNameTextBox_IsVisibleChanged;
            };

            this.AddHandler(TextBox.PreviewMouseDownEvent, new MouseButtonEventHandler(TextBoxReNameUC_PreviewMouseDown), true);
            this.AddHandler(TextBox.PreviewMouseUpEvent, new MouseButtonEventHandler(TextBoxReNameUC_PreviewMouseUp), true);
            this.AddHandler(TextBox.PreviewMouseMoveEvent, new MouseEventHandler(TextBoxReNameUC_PreviewMouseMove), true);
        }

        void TextBoxReNameUC_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OnPreviewMouseDown(e);
        }

        void TextBoxReNameUC_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            OnPreviewMouseMove(e);
        }

        void TextBoxReNameUC_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            OnPreviewMouseUp(e);
        }

        static void ParentUCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReNameTextBox tb = d as ReNameTextBox;
            UIElement uielement = e.NewValue as UIElement;
            if (tb != null && uielement != null)
            {
                uielement.PreviewMouseDown += (sender, args) => { args.Handled = true; };
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var caretIndex = this.CaretIndex;
            var selectionStart = this.SelectionStart;
            var selectLength = this.SelectionLength;
            if (!this.IsFocused)
            {
                this.Focusable = true;
                this.Focus();

                Mouse.Capture(this);

                #region 计算TextBox的光标位置

                if (this.CaretIndex < caretIndex) // 往左选择
                {
                    if (selectLength > this.SelectionLength)
                    {
                        this.SelectionStart = this.CaretIndex;
                    }
                    else if (selectLength == this.SelectionLength)
                    {
                        this.SelectionStart = this.SelectionStart + this.SelectionLength;
                    }
                    else
                    {
                        this.SelectionStart = this.CaretIndex;
                    }
                }
                else if (this.CaretIndex > caretIndex)
                {
                    if (selectLength > this.SelectionLength)
                    {
                        this.CaretIndex = this.SelectionStart;
                    }
                    else if (selectLength == this.SelectionLength)
                    {
                        this.SelectionStart = this.SelectionStart;
                    }
                    else
                    {
                        this.CaretIndex = this.SelectionStart + this.SelectionLength;
                    }
                }
                else //全选时控制操作
                {
                    if (selectLength == this.SelectionLength)
                    {
                        this.CaretIndex = this.SelectionStart;
                    }
                    else
                    {
                        this.CaretIndex = this.SelectionStart + this.SelectionLength;
                    }
                }

                this.SelectionLength = 0;

                #endregion
            }
            base.OnPreviewMouseDown(e);
        }

        void SPReNameTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                Mouse.Capture(null);
            }
            else
            {
                this.Focus();
                this.SelectAll();
            }
        }

        public void EndInput()
        {
            //if (ParentUC != null)
            //{
            //    string newname = this.Text.Trim();
            //    bool canchange = false;
            //    if (this.Text != null && !string.IsNullOrWhiteSpace(newname))
            //    {
            //        var control = ParentUC.FindVisualParent<BoneTreeUC>();
            //        if (control != null)
            //        {
            //            if (control.ViewModel.Items.FirstOrDefault(i => i.Name == newname) == null)
            //            {
            //                canchange = true;
            //            }
            //        }
            //    }

            //    if (canchange)
            //    {
            //        if (this.Text != ParentUC.Item.Name)
            //        {
            //            ParentUC.Item.Name = newname;
            //        }
            //    }
            //    else
            //    {
            //        if (ParentUC.Item.Name != newname)
            //        {
            //            if (ParentUC.IsEditor)
            //            {
            //                EditorCommon.Manager.LogConfig.Output.Info("Wrong Name...");
            //            }
            //            this.Text = ParentUC.Item.Name;
            //        }
            //    }

            //    ParentUC.IsEditor = false;
            //}
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            var hitVisual = VisualTreeHelper.HitTest(this, Mouse.GetPosition(this as IInputElement));
            if (this.Equals(hitVisual))
            {
                EndInput();
                return;
            }

            //if (this.Text != ParentUC.Item.Name)
            //{
            //    EndInput();
            //}
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                EndInput();
            }
            base.OnKeyDown(e);
        }

    }
}
