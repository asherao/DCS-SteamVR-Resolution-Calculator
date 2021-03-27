using System;
using System.Windows;
using System.Windows.Input;

/*
Welcome to DCS SteaVR Resolution Calculator!
Based on Jabbers research here: https://youtu.be/t1gcgAXhgtA
Based on Jabbers spreadsheet here: https://docs.google.com/spreadsheets/d/1ygjuleAivvA4C-EnHU2Hc6AddoRzvBvpsxe8Wse8feE/edit#gid=0
*/

namespace DCS_SteamVR_Resolution_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isEverythingLoaded;
        int maxTextLength = 6;//determines the length of the final number to include the decimal
        public MainWindow()
        {
            InitializeComponent();
            isEverythingLoaded = true;
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void SomethingChanged_RecalculateEverything()
        {
            //Aspect Ratio
            TextBlock_aspectRatio_Scenario1.Text = AspectRatioFormula(DoubleUpDown_HmdNativeResWidth_Scenario1.Value, DoubleUpDown_HmdNativeResHeight_Scenario1.Value).ToString();
            TextBlock_aspectRatio_Scenario2.Text = AspectRatioFormula(DoubleUpDown_HmdNativeResWidth_Scenario2.Value, DoubleUpDown_HmdNativeResHeight_Scenario2.Value).ToString();
            TextBlock_aspectRatio_Scenario3.Text = AspectRatioFormula(DoubleUpDown_HmdNativeResWidth_Scenario3.Value, DoubleUpDown_HmdNativeResHeight_Scenario3.Value).ToString();

            //Lens Distortion Multiplier Width
            TextBlock_widthLensDistMulti_Scenario1.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResWidth_Scenario1.Value).ToString();
            TextBlock_widthLensDistMulti_Scenario2.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResWidth_Scenario2.Value).ToString();
            TextBlock_widthLensDistMulti_Scenario3.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResWidth_Scenario3.Value).ToString();

            //Lens Distortion Multiplier Height
            TextBlock_heightLensDistMulti_Scenario1.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResHeight_Scenario1.Value).ToString();
            TextBlock_heightLensDistMulti_Scenario2.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResHeight_Scenario2.Value).ToString();
            TextBlock_heightLensDistMulti_Scenario3.Text = LensDistortionMultiplier(DoubleUpDown_HmdNativeResHeight_Scenario3.Value).ToString();

            //Resolution Area
            TextBlock_resArea_Scenario1.Text = ResArea(Convert.ToInt32(TextBlock_widthLensDistMulti_Scenario1.Text), Convert.ToInt32(TextBlock_heightLensDistMulti_Scenario1.Text)).ToString();
            TextBlock_resArea_Scenario2.Text = ResArea(Convert.ToInt32(TextBlock_widthLensDistMulti_Scenario2.Text), Convert.ToInt32(TextBlock_heightLensDistMulti_Scenario2.Text)).ToString();
            TextBlock_resArea_Scenario3.Text = ResArea(Convert.ToInt32(TextBlock_widthLensDistMulti_Scenario3.Text), Convert.ToInt32(TextBlock_heightLensDistMulti_Scenario3.Text)).ToString();

            //RPE Multiplier Height
            //Console.WriteLine(Convert.ToInt32(TextBlock_resArea_Scenario1.Text) + " , " + DoubleUpDown_SteamVrResPerEye_Scenario1.Value + " , " + (TextBlock_aspectRatio_Scenario1.Text));
            TextBlock_heightRpeMulti_Scenario1.Text = (RPE_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario1.Text), DoubleUpDown_SteamVrResPerEye_Scenario1.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario1.Text))).ToString();
            TextBlock_heightRpeMulti_Scenario2.Text = (RPE_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario2.Text), DoubleUpDown_SteamVrResPerEye_Scenario2.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario2.Text))).ToString();
            TextBlock_heightRpeMulti_Scenario3.Text = (RPE_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario3.Text), DoubleUpDown_SteamVrResPerEye_Scenario3.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario3.Text))).ToString();
           

            //RPE Multiplier Width
            TextBlock_widthRpeMulti_Scenario1.Text = RPE_multiplier_width(Convert.ToInt32(TextBlock_heightRpeMulti_Scenario1.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario1.Text)).ToString();
            TextBlock_widthRpeMulti_Scenario2.Text = RPE_multiplier_width(Convert.ToInt32(TextBlock_heightRpeMulti_Scenario2.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario2.Text)).ToString();
            TextBlock_widthRpeMulti_Scenario3.Text = RPE_multiplier_width(Convert.ToInt32(TextBlock_heightRpeMulti_Scenario3.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario3.Text)).ToString();

            //CRM Multiplier Height
            TextBlock_heightCrmMulti_Scenario1.Text = CRM_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario1.Text), DoubleUpDown_SteamVrResPerEye_Scenario1.Value, DoubleUpDown_SteamVrCustResMulti_Scenario1.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario1.Text)).ToString();
            TextBlock_heightCrmMulti_Scenario2.Text = CRM_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario2.Text), DoubleUpDown_SteamVrResPerEye_Scenario2.Value, DoubleUpDown_SteamVrCustResMulti_Scenario2.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario2.Text)).ToString();
            TextBlock_heightCrmMulti_Scenario3.Text = CRM_multiplier_height(Convert.ToInt32(TextBlock_resArea_Scenario3.Text), DoubleUpDown_SteamVrResPerEye_Scenario3.Value, DoubleUpDown_SteamVrCustResMulti_Scenario3.Value, Convert.ToDouble(TextBlock_aspectRatio_Scenario3.Text)).ToString();

            //CRM Multiplier Width
            TextBlock_widthCrmMulti_Scenario1.Text = CRM_multiplier_width(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario1.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario1.Text)).ToString();
            TextBlock_widthCrmMulti_Scenario2.Text = CRM_multiplier_width(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario2.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario2.Text)).ToString();
            TextBlock_widthCrmMulti_Scenario3.Text = CRM_multiplier_width(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario3.Text), Convert.ToDouble(TextBlock_aspectRatio_Scenario3.Text)).ToString();

            //Dcs Frame Res Width
            TextBlock_dcsFrameResWidth_Scenario1.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_widthCrmMulti_Scenario1.Text), DoubleUpDown_DcsPixelDensity_Scenario1.Value).ToString();
            TextBlock_dcsFrameResWidth_Scenario2.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_widthCrmMulti_Scenario2.Text), DoubleUpDown_DcsPixelDensity_Scenario2.Value).ToString();
            TextBlock_dcsFrameResWidth_Scenario3.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_widthCrmMulti_Scenario3.Text), DoubleUpDown_DcsPixelDensity_Scenario3.Value).ToString();

            //Dcs Frame Res Height
            TextBlock_dcsFrameResHeight_Scenario1.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario1.Text), DoubleUpDown_DcsPixelDensity_Scenario1.Value).ToString();
            TextBlock_dcsFrameResHeight_Scenario2.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario2.Text), DoubleUpDown_DcsPixelDensity_Scenario2.Value).ToString();
            TextBlock_dcsFrameResHeight_Scenario3.Text = DcsFrameResWidth(Convert.ToInt32(TextBlock_heightCrmMulti_Scenario3.Text), DoubleUpDown_DcsPixelDensity_Scenario3.Value).ToString();

            //Effective PD
            TextBlock_effectivePd_Scenario1.Text = EffectivePd(Convert.ToInt32(TextBlock_dcsFrameResWidth_Scenario1.Text), Convert.ToInt32(TextBlock_widthRpeMulti_Scenario1.Text)).ToString();
            TextBlock_effectivePd_Scenario2.Text = EffectivePd(Convert.ToInt32(TextBlock_dcsFrameResWidth_Scenario2.Text), Convert.ToInt32(TextBlock_widthRpeMulti_Scenario2.Text)).ToString();
            TextBlock_effectivePd_Scenario3.Text = EffectivePd(Convert.ToInt32(TextBlock_dcsFrameResWidth_Scenario3.Text), Convert.ToInt32(TextBlock_widthRpeMulti_Scenario3.Text)).ToString();

            //https://stackoverflow.com/questions/9779858/set-the-maximum-chr-length-of-a-textblock-in-xaml
            if (TextBlock_effectivePd_Scenario1.Text.Length > maxTextLength)
            {
                TextBlock_effectivePd_Scenario1.Text = TextBlock_effectivePd_Scenario1.Text.Substring(0, maxTextLength);
            }
            else
            {
                TextBlock_effectivePd_Scenario1.Text = TextBlock_effectivePd_Scenario1.Text;
            }

            if (TextBlock_effectivePd_Scenario2.Text.Length > maxTextLength)
            {
                TextBlock_effectivePd_Scenario2.Text = TextBlock_effectivePd_Scenario2.Text.Substring(0, maxTextLength);
            }
            else
            {
                TextBlock_effectivePd_Scenario2.Text = TextBlock_effectivePd_Scenario2.Text;
            }

            if (TextBlock_effectivePd_Scenario3.Text.Length > maxTextLength)
            {
                TextBlock_effectivePd_Scenario3.Text = TextBlock_effectivePd_Scenario3.Text.Substring(0, maxTextLength);
            }
            else
            {
                TextBlock_effectivePd_Scenario3.Text = TextBlock_effectivePd_Scenario3.Text;
            }

        }

        //private int CRM_multiplier_width(int v1, double v2)
        //{
        //    return Convert.ToInt32(v1 * (v2));
        //}

        public int CRM_multiplier_width(int CRM_height, double aspectRatio)
        {
            return Convert.ToInt32(CRM_height * (aspectRatio));
        }

        private int CRM_multiplier_height(int v1, int? v2, int? value, double v3)
        {
            double result1 = (double)(v1 * (v2/100.0) * (value/100.0));
            //Console.WriteLine("Result 1 is: " + v1 + " * " + (double)(v2/100.0) + " * " + (double)(value / 100.0) + " = " + result1);
            
            double result2 = result1 / (v3);
            //Console.WriteLine("Result 2 is: " + result1 + "/" +  (v3) + " = " + result2);

            double result3 = Math.Sqrt(result2);
            //Console.WriteLine("Result 3 is: " + result3);
            return (int)result3;
        }

        //public int CRM_multiplier_height(int resArea, int resPerEye, int? steamVrCustRes, double aspectRatio)
        //{
        //    return Convert.ToInt32(Math.Sqrt((double)((resArea * resPerEye * steamVrCustRes) / aspectRatio)));
        //}

        private int RPE_multiplier_height(int v1, double? value, double v2)
        {
            //return Convert.ToInt32(Math.Sqrt((resArea * resPerEye) / aspectRatio));
            return Convert.ToInt32(Math.Sqrt((double)((v1 * (value / 100)) / v2)));//divide by 100 because of the percent
           
        }

        private int LensDistortionMultiplier(int? value)
        {
            return Convert.ToInt32(value * 1.4);
        }

        private double AspectRatioFormula(int? value1, int? value2)
        {
            double val1_double = Convert.ToDouble(value1);
            double val2_double = Convert.ToDouble(value2);
            //Console.WriteLine(val1_double + " / " + val2_double + " = " + (val1_double / val2_double));
            
            return val1_double / val2_double;
        }

        //==Calculation Methods==//

        public double AspectRatioFormula(int topNumber, int bottomNumber)
        {
            return topNumber / bottomNumber;
        }

        public int LensDistortionMultiplier(int resNumber)
        {
            return Convert.ToInt32(resNumber * 1.4);
        }

        public int ResArea(int res1,int res2)
        {
            return res1 * res2;
        }

        public int RPE_multiplier_width(int RPE_height, double aspectRatio)
        {
            return Convert.ToInt32(RPE_height * (aspectRatio));
        }

        public int DcsFrameResWidth(int CRM_width, double? DcsPixelDensity)
        {
            return Convert.ToInt32(CRM_width * DcsPixelDensity);
        }

        public int DcsFrameResHeight(int CRM_height, double? DcsPixelDensity)
        {
            return Convert.ToInt32(CRM_height * DcsPixelDensity);
        }

        public double EffectivePd(int DcsFrameResWidth, int RPE_multiplierWidth)
        {
            return (double)DcsFrameResWidth / RPE_multiplierWidth;
        }

        private void DoubleUpDown_HmdNativeResWidth_Scenario1_changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }


        private void effectivePdLabel_mouseDown(object sender, MouseButtonEventArgs e)//debug
        {
            SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_HmdNativeResWidth_Scenario2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_HmdNativeResWidth_Scenario3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isEverythingLoaded) SomethingChanged_RecalculateEverything();
        }

        private void button_close_click(object sender, RoutedEventArgs e)
        {
            Close();//closes the program when you click the close "button"
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this moves the window when the titlebar is clicked and held down
            //I made the custom title bar
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TitleRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this moves the window when the titlebar is clicked and held down
            //I made the custom title bar
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TextBlock_scenario1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TextBlock_scenario2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TextBlock_scenario3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        //https://stackoverflow.com/questions/3941293/how-to-forbid-backspace-key-in-wpf
        //precents null handeled when the backspace key is pressed because the value will have been empty
        private void DoubleUpDown_HmdNativeResWidth_Scenario1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_HmdNativeResWidth_Scenario2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_HmdNativeResWidth_Scenario3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_HmdNativeResHeight_Scenario3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrResPerEye_Scenario3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_SteamVrCustResMulti_Scenario3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void DoubleUpDown_DcsPixelDensity_Scenario3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }
    }
}
