using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace DateScheduler_MAUI.CustomContentView
{
    public class WaveView : SKCanvasView
    {
        // Bindable properties
        public static readonly BindableProperty Parameter1Property =
            BindableProperty.Create(nameof(Parameter1), typeof(double), typeof(WaveView), default(double));

        public static readonly BindableProperty Parameter2Property =
            BindableProperty.Create(nameof(Parameter2), typeof(int), typeof(WaveView), default(int));

        // Properties for XAML binding
        public double Parameter1
        {
            get { return (double)GetValue(Parameter1Property); }
            set { SetValue(Parameter1Property, value); }
        }

        public int Parameter2
        {
            get { return (int)GetValue(Parameter2Property); }
            set { SetValue(Parameter2Property, value); }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            var info = e.Info;

            // Set up paint for the wave
            var wavePaint = new SKPaint
            {
                IsAntialias = true,
            };

            // Draw a centered width-wise wave pattern with gradient above the wave
            DrawWave(canvas, wavePaint, info);
        }

        private void DrawWave(SKCanvas canvas, SKPaint paint, SKImageInfo info)
        {
            var wavePosition = Parameter1 <= 1.0 ? info.Height * Parameter1 : Parameter1;
            var waveHeight = Parameter2 <= 1.0 ? info.Width * Parameter2 : Parameter2; // Adjust this for the amplitude of the wave
            var waveLength = info.Width; // Adjust this for the length of the wave
            var frequency = 2 * Math.PI / waveLength;

            // Calculate the vertical position to center the wave
            var centerY = (int)(wavePosition);

            using (var path = new SKPath())
            {
                path.MoveTo(0, centerY);

                for (int x = 0; x <= info.Width; x++)
                {
                    var y = waveHeight * Math.Sin(frequency * x);
                    path.LineTo(x, (float)(centerY + y));
                }

                path.LineTo(info.Width, centerY);
                path.LineTo(info.Width, 0);
                path.LineTo(0, 0);
                path.Close();

                // Create a shader for the gradient effect
                var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(waveLength, centerY + waveHeight),
                    new SKColor[] { SKColor.Parse("#dec2fd"), SKColor.Parse("#92c5fc") },
                    null,
                    SKShaderTileMode.Clamp);

                paint.Shader = shader;

                // Fill the path with the gradient
                canvas.DrawPath(path, paint);

                // Clear the shader to avoid affecting other drawings
                paint.Shader = null;
            }
        }
    }
}
