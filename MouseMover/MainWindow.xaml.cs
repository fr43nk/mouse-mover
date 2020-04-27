using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace MouseMover
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer m_timer = new System.Timers.Timer();
        public MainWindow()
        {
            InitializeComponent();
            this.m_timer.Interval = this.slInterval.Value * 1000;
            this.m_timer.AutoReset = true;
            this.m_timer.Elapsed += handeTimerEvent;
            this.m_timer.Enabled = true;

            this.lblInterval.Content = this.slInterval.Value;
        }

        private void handeTimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Point p = GetMousePosition();
            this.SetPosition((int)p.X+3, (int)p.Y+4);
            //this.SetPosition((int)p.X, (int)p.Y);
            Console.WriteLine("Timer fired");
        }

        private void SetPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = sender as Slider;
            this.m_timer.Enabled = false;
            this.m_timer.Interval = s.Value * 1000;
            this.m_timer.Enabled = true;
            if( this.lblInterval != null )
                this.lblInterval.Content = $"{this.slInterval.Value}s";
        }
    }
}
