<!--
 * @Author: 顾名思义
 * @Date: 2025-01-07 09:42:17
 * @LastEditors: 顾名思义
 * @LastEditTime: 2025-01-07 09:57:28
 * @FilePath: \com.myxmu.SCADASystem\readme.md
 * @Description:
 *
-->

# WPF 框架搭建

## 1. Nuget 包安装

- CommunityToolkit.MVVM
- Microsoft.Extensions.DependencyInjection
- MaterialDesignThemes
- MaterialDesignThemes.MahApps
- Masuit.Tools.Core https://www.masuit.tools/api.html-Db 层
- Microsoft.Extensions.Configuration-Db 层
- Microsoft.Extensions.Configuration.Json-Db 层
- Microsoft.Extensions.Configuration.Binder - Db 层
- Microsoft.Extensions.Options - Db 层
- SqlSugarCore-Db 层
- NLog.Extensions.Logging-Db 层
- 关于 MaterialDesignThemes 使用可以直接参考课件资料下的 Demo，官网链接 https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit

## 2. 框架预准备

1. 删除 MainWindow.xaml，App.xaml 中 StartUri 也删除
2. 创建三层架构 Views ViewModels Models
3. 在 Views 中创建 MainView.xaml，ViewModels 创建 MainViewModel.cs，MainViewModel 继承自 ObservableObject
4. MaterialDesign 主题包使用
5. MainView.xaml 常用配置，设置窗体导航，方便能够点出 ViewModel.xxx，加上 d:DataContext="{d:DesignInstance vm:ShellViewModel}"并设置一些字体属性
6. 改造 App.xaml.cs 文件将依赖注入开启（[microsoft 推荐写法](https://learn.microsoft.com/zh-cn/dotnet/communitytoolkit/mvvm/ioc，)）
7. 设置窗体内部弹出对话框要用到的组件，里面嵌入一个 ContentControl 可以填充其他窗体。

## 3 搭建数据库交互层

1. 创建 TulingScadaSystem.Db 类库项目，安装 Nuget 包：
   - CommunityToolkit.Mvvm
   - SqlSugarCore
2. 创建 Helpers-SqlSugarHelper.cs 类

# CodeSnape

Xaml's UI，style,Ref...

```html
xmlns:vm="clr-namespace:TulingScadaSystem.ViewModels"
xmlns:i="http://schemas.microsoft.com/xaml/behaviors"
xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
d:DataContext="{d:DesignInstance vm:MainViewModel}" d:DesignHeight="800"
d:DesignWidth="1200" Background="{DynamicResource
MaterialDesign.Brush.Background}" FontFamily="Microsoft YaHei" FontSize="16"
TextElement.FontSize="16" TextElement.FontWeight="Regular"
TextElement.Foreground="{DynamicResource MaterialDesignBody}"
```

图标文字 Template

```html
<StackPanel Margin="10" HorizontalAlignment="Center" Orientation="Horizontal">
  <materialDesign:PackIcon
    Width="50"
    Height="50"
    Margin="5"
    VerticalAlignment="Center"
    Foreground="#1b2755"
    Kind="ChartScatterPlotHexbin"
  />
  <TextBlock HorizontalAlignment="Center" VerticalAlignment="Center"
  FontSize="40" Foreground="#1b2755" Style="{StaticResource
  MaterialDesignTitleSmallTextBlock}"
</StackPanel>
```

behavior

```html
<i:Interaction.Triggers>
  <i:EventTrigger EventName="PreviewMouseUp">
    <i:InvokeCommandAction
      Command="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=UserControl}, Path=DataContext.NavigationCommand}"
      CommandParameter="{Binding}"
    />
  </i:EventTrigger>
</i:Interaction.Triggers>
```
