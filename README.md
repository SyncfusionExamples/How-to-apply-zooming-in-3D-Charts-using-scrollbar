# How to apply zooming in 3D Charts using scrollbar?

This example demonstrates how to zoom the [SfChart3D](https://help.syncfusion.com/wpf/sfchart3d/gettingstarted) using the ScrollBar.

The [Maximum](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html#Syncfusion_UI_Xaml_Charts_NumericalAxis3D_Maximum), [Minimum](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html#Syncfusion_UI_Xaml_Charts_NumericalAxis3D_Minimum) properties and [LabelCreated](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.ChartAxis.html) event of [NumericalAxis3D](https://help.syncfusion.com/cr/Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html) will help to achieve zooming the SfChart3D using the scrollbar by these following steps.

**Step 1:** Create Scrollbars and register the ValueChanged event for getting the position and factor for zooming.
```
<!--ZoomFactor-->
<Grid>
 <Grid.ColumnDefinitions>
  <ColumnDefinition Width="Auto"/>
  <ColumnDefinition/>
 </Grid.ColumnDefinitions>
  <TextBlock Text="X-Axis ZoomFactor    :"  VerticalAlignment="Center"/>
  <ScrollBar Height="15" Grid.Column="1" Orientation="Horizontal"    Margin="35,5,10,5" x:Name="zoomFactor" Minimum="0" Maximum="1" Value="1"
  ValueChanged="zoomFactor_ValueChanged"/>
</Grid>
<!--ZoomPosition-->
<Grid>
 <Grid.ColumnDefinitions>
  <ColumnDefinition Width="Auto"/>
  <ColumnDefinition/>
 </Grid.ColumnDefinitions>
  <TextBlock Text="X-Axis ZoomPosition :"  VerticalAlignment="Center"/>
  <ScrollBar Height="15" Grid.Column="1" Orientation="Horizontal" 
  x:Name="zoomPosition" Margin="35,5,10,5" Minimum="0" Maximum="1"
  ValueChanged="zoomPosition_ValueChanged"/>
</Grid>
```

**Step 2:** Update the [Maximum](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html#Syncfusion_UI_Xaml_Charts_NumericalAxis3D_Maximum) and [Minimum](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html#Syncfusion_UI_Xaml_Charts_NumericalAxis3D_Minimum) properties of [PrimaryAxis](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.SfChart3D.html#Syncfusion_UI_Xaml_Charts_SfChart3D_PrimaryAxis) based on Scrollbar values.
```
//Calculating the Maximum and Minimum properties of Axis.
private void UpdateRange()
{
    if (zoomPosition != null && zoomFactor != null)
    {
        double start = minimum + zoomPosition.Value * maximum;
        double end = start + zoomFactor.Value * maximum;

        if (end > maximum)
        {
            start = start - (end - maximum);
            end = maximum;
        }

        if (start < minimum)
            start = minimum;

        xAxis.Minimum = start;
        xAxis.Maximum = end;
    }
}
```

**Step 3:** Update the [LabelContent](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.ChartAxisLabel.html#Syncfusion_UI_Xaml_Charts_ChartAxisLabel_LabelContent) property based on the value of [Position](https://help.syncfusion.com/cr/Syncfusion.UI.Xaml.Charts.ChartAxisLabel.html) property of [ChartAxisLabel](https://help.syncfusion.com/cr/Syncfusion.UI.Xaml.Charts.ChartAxisLabel.html) in [LabelCreated](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Charts.ChartAxis.html) event.
```
<chart:SfChart3D.PrimaryAxis>
 <chart:NumericalAxis3D  Interval="1" EnableAutoIntervalOnZooming="False"   LabelCreated="xAxis_LabelCreated" LabelRotationAngle="-90" x:Name="xAxis" />
</chart:SfChart3D.PrimaryAxis>
```
```
// Update LabelContent property of Position of AxisLabel
private void xAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
{
    if (e.AxisLabel.Position - (int)e.AxisLabel.Position < 0.5)
    {
        int position = (int)Math.Floor(e.AxisLabel.Position);

        if (position < months.Count() && position >= 0)
            e.AxisLabel.LabelContent = months[position].ToString();
        else
            e.AxisLabel.LabelContent = "";
    }
    else
    {
        int position = (int)Math.Ceiling(e.AxisLabel.Position);

        if (position < months.Count() && position >= 0)
            e.AxisLabel.LabelContent = months[position].ToString();
        else
            e.AxisLabel.LabelContent = "";
    }
}
```

![x-axis scrolling](https://github.com/user-attachments/assets/73d3f639-a7dc-4a04-9368-9c663b4a94de)


Refer to this KB [article](https://www.syncfusion.com/kb/2904/how-to-set-default-zoom-pan-position-for-charts), for applying default zoom and pan position in SfChart.

KB article - [How to apply zooming in 3D Charts using scrollbar?](https://www.syncfusion.com/kb/11667/how-to-apply-zooming-in-3d-charts-using-scrollbar)

## See also

[How to display the axis labels in a particular format](https://www.syncfusion.com/kb/3318/how-to-display-the-axis-labels-in-a-particular-format)

[How to define ticker labels of custom axis](https://www.syncfusion.com/kb/2588/how-to-define-ticker-labels-of-custom-axis)

[How to display the visible range of labels while zooming](https://www.syncfusion.com/kb/2712/how-to-display-the-visible-range-of-labels-while-zooming)
