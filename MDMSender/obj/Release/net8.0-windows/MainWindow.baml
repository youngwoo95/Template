   M S B A M L   `   `   `  ����   	MDMSender    MDMSender.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  MDMSender_V02��$	��450��$
��1000��$
��1280��$	��800��$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)� � Icon$/ )pack://application:,,,/Images/run_ico.ico�� � 	Resources    AnimatedButtonTemplate �� 
TargetType ��Name �� CornerRadius �� Triggers :� RoutedEvent �� 
TargetName	 �� TargetProperty
 t� From	 t� To g� Duration	 �� To��%  &	        �� " ��.R��� $ Border��#��z��
������ $��0q�$ 5��._��� $��Center=�$��Center=� :� $ Button.MouseEnter��.[��� .���� .��t� $ Border��$	 BorderThickness�
 0 2$ 0:0:0.5D��� $ Border��$2	 ,(Border.BorderBrush).(SolidColorBrush.Color)�	 Beige$ 0:0:0.5D�:� $ Button.MouseLeave��.[��� .���� .��t� $ Border��$	 BorderThickness�
 2 0$ 0:0:0.5D��� $ Border��$2	 ,(Border.BorderBrush).(SolidColorBrush.Color)� Transparent$ 0:0:0.5D�.���  � ColumnDefinitions �� $��180px ��� $��* � � RowDefinitions �� $
��80px ��� $��* �.+�� $��0�����#223377.+��� $1��+pack://application:,,,/Images/Stec_Logo.png��$��Uniform=�$	��150��$��80��� $��1��$��0�����#223377.+��� $��MDM Sender V_02�� �� TextAlignment$ Center=�$��Center=�
�������$
��20px(�$��Heavy"��� -    ��Name$ LeftStackPanel�����#868e9c �� Orientation$ Vertical=�$��0��$��1��.+��� -    ��Name$ btnHome�����#223377
�������$��메인��$��Heavy"�$��1q�$
��50px�� � Cursor$
 Hand��#��[  $C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ btnStart�����#223377
�������$��시작��$��Heavy"�$��1q�$
��50px��$
 Hand��#��[  $C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ btnStop�����#223377
�������$��종료��$��Heavy"�$��1q�$
��50px��$
 Hand��#��[  $C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ 
btnSetting�����#223377
�������$��설정��$��Heavy"�$��1q�$
��50px��$
 Hand��#��[  $C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -    ��Name$ RightStackPanel�����#c4c9cf$��1��$��1��