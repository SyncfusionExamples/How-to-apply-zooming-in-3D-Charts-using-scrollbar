# How-to-apply-zooming-in-3D-Charts-using-scrollbar

This example explains how to zoom the SfChart3D using the ScrollBar.Please refer KB links for more details,

[How to zoom the SfChart3D using the ScrollBar?](https://www.syncfusion.com/kb/11667/how-to-apply-zooming-in-3d-charts-using-scrollbar)

The [Maximum](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.NumericalAxis3D~Maximum.html), [Minimum](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.NumericalAxis3D~Minimum.html) properties and [LabelCreated](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartAxis~LabelCreated_EV.html) event of [NumericalAxis3D](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.NumericalAxis3D.html) will help to achieve zooming the SfChart3D using the scrollbar by these following steps.

Step 1: Create Scrollbars and register the ValueChanged event for getting the position and factor for zooming. 

**XAML**
```
  <!--ZoomFactor-->
<Grid>
	<Grid.ColumnDefinitions>
		<ColumnDefinition Width="Auto"/>
		<ColumnDefinition/>
	</Grid.ColumnDefinitions>
		<TextBlock Text="X-Axis ZoomFactor    :"  VerticalAlignment="Center"/>
		<ScrollBar Height="15" Grid.Column="1" Orientation="Horizontal" Margin="35,5,10,5" x:Name="zoomFactor" Minimum="0" Maximum="1" Value="1"
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
Step 2: Update the [Maximum](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.NumericalAxis3D~Maximum.html) and [Minimum](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.NumericalAxis3D~Minimum.html) properties of [PrimaryAxis](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.SfChart3D~PrimaryAxis.html) based on Scrollbar values.

**C#**
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
Step 3: Update the [LabelContent](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartAxisLabel~LabelContent.html) property based on the value of [Position](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartAxisLabel.html) property of [ChartAxisLabel](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartAxisLabel.html) in [LabelCreated](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartAxis~LabelCreated_EV.html) event.

**XAML**
```
<chart:SfChart3D.PrimaryAxis>
	<chart:NumericalAxis3D  Interval="1" EnableAutoIntervalOnZooming="False" LabelCreated="xAxis_LabelCreated" LabelRotationAngle="-90" x:Name="xAxis" />
</chart:SfChart3D.PrimaryAxis>
```

**C#**
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
