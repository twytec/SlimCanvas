using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal class DXSwapDraw
    {
        Windows.UI.Xaml.Controls.SwapChainPanel swapChainPanel;

        private SharpDX.Direct3D11.Device2 d3dDevice;
        private SharpDX.Direct3D11.DeviceContext1 d3dDeviceContext;

        private Device d2dDevice;
        public DeviceContext d2dContext;

        private Bitmap1 d2dBitmapTarget;
        SharpDX.DXGI.Surface backBuffer;

        private SharpDX.DXGI.SwapChain2 swapChain;
        
        bool canDraw = false;

        public DXSwapDraw(Windows.UI.Xaml.Controls.SwapChainPanel scp)
        {
            swapChainPanel = scp;

            swapChainPanel.Loaded += SwapChainPanel_Loaded;
            swapChainPanel.Unloaded += SwapChainPanel_Unloaded;
            swapChainPanel.SizeChanged += SwapChainPanel_SizeChanged;

            Windows.UI.Xaml.Application.Current.Suspending += Current_Suspending;
        }

        #region Events
        
        private void SwapChainPanel_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (d3dDevice == null)
                CreateDeviceAndLoadContent();
        }

        private void SwapChainPanel_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StopRendering();
        }

        private void SwapChainPanel_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty)
                return;
            if (d3dDevice == null)
                return;

            Resize(e.NewSize);
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            if (d3dDevice != null)
            {
                using (var dxgiDevice = d3dDevice.QueryInterface<SharpDX.DXGI.Device3>())
                    dxgiDevice.Trim();
            }
        }

        #endregion

        #region CreateDeviceAndLoadContent

        void CreateDeviceAndLoadContent()
        {
            var creationFlags = SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport;
#if DEBUG

            creationFlags |= SharpDX.Direct3D11.DeviceCreationFlags.Debug;
#endif
            
            using (var dd = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, creationFlags))
            {
                d3dDevice = dd.QueryInterface<SharpDX.Direct3D11.Device2>();

            }
            d3dDeviceContext = d3dDevice.ImmediateContext2;

            var size = LocalTransform.GetSize(swapChainPanel.ActualWidth, swapChainPanel.ActualHeight);

            var scd = new SharpDX.DXGI.SwapChainDescription1()
            {
                AlphaMode = SharpDX.DXGI.AlphaMode.Ignore,
                BufferCount = 2,
                Flags = SharpDX.DXGI.SwapChainFlags.AllowModeSwitch,
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                Height = (int)size.Height,
                Width = (int)size.Width,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Scaling = SharpDX.DXGI.Scaling.Stretch,
                Stereo = false,
                SwapEffect = SharpDX.DXGI.SwapEffect.FlipSequential,
                Usage = SharpDX.DXGI.Usage.BackBuffer | SharpDX.DXGI.Usage.RenderTargetOutput
            };


            using (var dd3 = d3dDevice.QueryInterface<SharpDX.DXGI.Device3>())
            {
                using (var df3 = dd3.Adapter.GetParent<SharpDX.DXGI.Factory3>())
                {
                    using (var sc = new SharpDX.DXGI.SwapChain1(df3, d3dDevice, ref scd))
                    {
                        swapChain = sc.QueryInterface<SharpDX.DXGI.SwapChain2>();
                        d2dDevice = new SharpDX.Direct2D1.Device(dd3);
                        d2dContext = new SharpDX.Direct2D1.DeviceContext(d2dDevice, SharpDX.Direct2D1.DeviceContextOptions.EnableMultithreadedOptimizations);
                    }
                }
            }

            using (SharpDX.DXGI.ISwapChainPanelNative nativeObject = ComObject.As<SharpDX.DXGI.ISwapChainPanelNative>(swapChainPanel))
            {
                // Set its swap chain.
                nativeObject.SwapChain = this.swapChain;
            }

            CreateRenderTarget();
            Shared.D2dContext = d2dContext;
            canDraw = true;
        }

        void CreateRenderTarget()
        {
            var display = Windows.Graphics.Display.DisplayInformation.GetForCurrentView();

            SharpDX.Direct2D1.BitmapProperties1 bp1 = new SharpDX.Direct2D1.BitmapProperties1(
                new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                display.LogicalDpi, display.LogicalDpi, SharpDX.Direct2D1.BitmapOptions.Target | SharpDX.Direct2D1.BitmapOptions.CannotDraw);

            backBuffer = swapChain.GetBackBuffer<SharpDX.DXGI.Surface>(0);
            d2dBitmapTarget = new SharpDX.Direct2D1.Bitmap1(d2dContext, backBuffer, bp1);
            d2dContext.Target = d2dBitmapTarget;
        }

        public void StopRendering()
        {
            Dispose();
        }

        void DisposeRenderTarget()
        {
            d2dContext.Target = null;
            Utilities.Dispose(ref backBuffer);
            Utilities.Dispose(ref d2dBitmapTarget);
            d2dBitmapTarget = null;
        }

        void Resize(Windows.Foundation.Size s)
        {
            var size = LocalTransform.GetSize(s.Width, s.Height);

            canDraw = false;

            DisposeRenderTarget();

            swapChain.ResizeBuffers(
                swapChain.Description.BufferCount,
                (int)size.Width, (int)size.Height,
                swapChain.Description1.Format,
                swapChain.Description1.Flags);

            CreateRenderTarget();

            canDraw = true;

            swapChain.SourceSize = new Size2((int)size.Width, (int)size.Height);
        }

        #endregion

        #region Begin- Enddraw

        public void BeginDraw()
        {
            if (canDraw == false)
                return;

            d2dContext.BeginDraw();
        }

        public void EndDraw()
        {
            if (canDraw == false)
                return;

            d2dContext.EndDraw();
            swapChain.Present(1, SharpDX.DXGI.PresentFlags.None);
            d2dContext.Transform = Matrix3x2.Identity;
            
        }

        #endregion

        #region Draw Method

        #region Clear

        public void Clear(SharpDX.Color color)
        {
            if (canDraw == false)
                return;
            
            d2dContext.Clear(color);
        }

        #endregion

        public void DrawGeometry(Geometry g, Matrix3x2 trans, StrokeStyle ss, SolidColorBrush strokeColor, float strokeWidth, Brush FillBrush)
        {
            if (canDraw == false)
                return;

            d2dContext.Transform = trans;

            if (FillBrush != null)
            {
                d2dContext.FillGeometry(g, FillBrush);
            }
                

            d2dContext.DrawGeometry(g, strokeColor, strokeWidth, ss);

            d2dContext.Transform = Matrix3x2.Identity;
        }

        public void DrawBitmap(Bitmap bitmap, Matrix3x2 trans, SharpDX.RectangleF rect, float opacity)
        {
            if (canDraw == false)
                return;
            
            d2dContext.Transform = trans;
            d2dContext.DrawBitmap(bitmap, rect, opacity, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor, rect);
            d2dContext.Transform = Matrix3x2.Identity;
        }

        public void DrawText(SharpDX.DirectWrite.TextLayout tl, SolidColorBrush brush, Matrix3x2 trans)
        {
            if (canDraw == false)
                return;

            d2dContext.Transform = trans;
            d2dContext.DrawTextLayout(new RawVector2(), tl, brush);
            d2dContext.Transform = Matrix3x2.Identity;
        }

        #endregion

        public void Dispose()
        {
            canDraw = false;

            DisposeRenderTarget();

            Utilities.Dispose(ref swapChain);
            Utilities.Dispose(ref d2dContext);
            Utilities.Dispose(ref d2dDevice);
            Utilities.Dispose(ref d3dDeviceContext);
            Utilities.Dispose(ref d3dDevice);

            swapChain = null;
            d2dContext = null;
            d2dDevice = null;
            d3dDeviceContext = null;
            d3dDevice = null;
        }
    }
}
