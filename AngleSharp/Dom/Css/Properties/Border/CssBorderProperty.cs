﻿namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border
    /// </summary>
    sealed class CssBorderProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssBorderTopWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderTopStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderTopColorProperty>().TrySetValue(m.Item3);
            Get<CssBorderLeftWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderLeftStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderLeftColorProperty>().TrySetValue(m.Item3);
            Get<CssBorderRightWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderRightStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderRightColorProperty>().TrySetValue(m.Item3);
            Get<CssBorderBottomWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderBottomStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderBottomColorProperty>().TrySetValue(m.Item3);
         */
        internal static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option(),
            Converters.LineStyleConverter.Option(),
            Converters.CurrentColorConverter.Option());

        #endregion

        #region ctor

        internal CssBorderProperty()
            : base(PropertyNames.Border, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var leftColor = properties.OfType<CssBorderLeftColorProperty>().FirstOrDefault();
            var topColor = properties.OfType<CssBorderTopColorProperty>().FirstOrDefault();
            var rightColor = properties.OfType<CssBorderRightColorProperty>().FirstOrDefault();
            var bottomColor = properties.OfType<CssBorderBottomColorProperty>().FirstOrDefault();

            if (leftColor == null || rightColor == null || topColor == null || bottomColor == null)
                return String.Empty;
            else if (leftColor.OriginalValue != rightColor.OriginalValue || leftColor.OriginalValue != topColor.OriginalValue || leftColor.OriginalValue != bottomColor.OriginalValue)
                return String.Empty;

            var leftWidth = properties.OfType<CssBorderLeftWidthProperty>().FirstOrDefault();
            var topWidth = properties.OfType<CssBorderTopWidthProperty>().FirstOrDefault();
            var rightWidth = properties.OfType<CssBorderRightWidthProperty>().FirstOrDefault();
            var bottomWidth = properties.OfType<CssBorderBottomWidthProperty>().FirstOrDefault();

            if (leftWidth == null || rightWidth == null || topWidth == null || bottomWidth == null)
                return String.Empty;
            else if (leftWidth.OriginalValue != rightWidth.OriginalValue || leftWidth.OriginalValue != topWidth.OriginalValue || leftWidth.OriginalValue != bottomWidth.OriginalValue)
                return String.Empty;

            var leftStyle = properties.OfType<CssBorderLeftStyleProperty>().FirstOrDefault();
            var topStyle = properties.OfType<CssBorderTopStyleProperty>().FirstOrDefault();
            var rightStyle = properties.OfType<CssBorderRightStyleProperty>().FirstOrDefault();
            var bottomStyle = properties.OfType<CssBorderBottomStyleProperty>().FirstOrDefault();

            if (leftStyle == null || rightStyle == null || topStyle == null || bottomStyle == null)
                return String.Empty;
            else if (leftStyle.OriginalValue != rightStyle.OriginalValue || leftStyle.OriginalValue != topStyle.OriginalValue || leftStyle.OriginalValue != bottomStyle.OriginalValue)
                return String.Empty;

            return SerializeValue(leftWidth, leftStyle, leftColor);
        }

        static internal String SerializeValue(CssProperty width, CssProperty style, CssProperty color)
        {
            var result = new List<String>();

            if (width != null && width.HasValue)
                result.Add(width.SerializeValue());

            if (style != null && style.HasValue)
                result.Add(style.SerializeValue());

            if (color != null && color.HasValue)
                result.Add(color.SerializeValue());

            return String.Join(" ", result);
        }

        #endregion
    }
}
