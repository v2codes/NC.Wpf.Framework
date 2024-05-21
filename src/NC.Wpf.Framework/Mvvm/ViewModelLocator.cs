﻿using NC.Wpf.Core.Mvvm;
using System.ComponentModel;
using System.Windows;

namespace NC.Wpf.Framework.Mvvm
{
    /// <summary>
    /// 视图模型定位器，自动映射绑定View和ViewModel，来自Prism框架
    /// 使用示例，在View视图中添加如下设置：
    ///     <Window>
    ///         ...
    ///         xmlns:vm="clr-namespace:NC.Wpf.Framework.Mvvm;assembly= NC.Wpf.Framework"
    ///         vm:ViewModelLocator.AutoWireViewModel="True"
    ///         ...
    ///     </Window>
    /// This class defines the attached property and related change handler that calls the ViewModelLocator in Prism.Mvvm.
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// The AutoWireViewModel attached property.
        /// </summary>
        public static DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool?), typeof(ViewModelLocator), new PropertyMetadata(defaultValue: null, propertyChangedCallback: AutoWireViewModelChanged));

        /// <summary>
        /// Gets the value for the <see cref="AutoWireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="AutoWireViewModelProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static bool? GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool?)obj.GetValue(AutoWireViewModelProperty);
        }

        /// <summary>
        /// Sets the <see cref="AutoWireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The value to attach.</param>
        public static void SetAutoWireViewModel(DependencyObject obj, bool? value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                var value = (bool?)e.NewValue;
                if (value.HasValue && value.Value)
                {
                    ViewModelLocationProvider.AutoWireViewModelChanged(d, Bind);
                }
            }
        }

        /// <summary>
        /// Sets the DataContext of a View.
        /// </summary>
        /// <param name="view">The View to set the DataContext on.</param>
        /// <param name="viewModel">The object to use as the DataContext for the View.</param>
        static void Bind(object view, object viewModel)
        {
            if (view is FrameworkElement element)
                element.DataContext = viewModel;
        }
    }
}