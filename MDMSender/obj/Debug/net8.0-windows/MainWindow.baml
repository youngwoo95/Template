   M S B A M L   `   `   `  ����  	MDMSender    MDMSender.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��5   	   $
��1000��5   	   $
��1000��5   	   $	��600��5   	    � 	Resources 5         AnimatedButtonTemplate �� 
TargetType ��Name �� CornerRadius �� Triggers :� RoutedEvent �� 
TargetName �� TargetProperty	 t� From	
 t� To g� Duration	 �� To��%�  &	        �� 5   
   " ��69   .R�5      �� $ Border��6   #��z��6%   
������ $��0q�5   /   $ 5��6C   ._�5      �� $��Center=�6#   $��Center=�6@   5       5      :� 5      $ Button.MouseEnter��6   .[�5      �� .��5      �� .��5      t� $ Border��61   $ BorderThickness�5   1   	 05   1   
 26:   $ 0:0:0.5D�6A   �� 5      $ Border��6-   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�5   -   	 Beige5   -   $ 0:0:0.5D�68   5      5       5!      :� 5$      $ Button.MouseLeave��6   .[�5%      �� .��5&      �� .��5'      t� $ Border��61   $ BorderThickness�5(   1   	 25)   1   
 06:   $ 0:0:0.5D�6A   �� 5*      $ Border��6-   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�5+   -    Transparent5,   -   $ 0:0:0.5D�6>   5-      5.      5/      51      53      .��55      �  � ColumnDefinitions 56   
   �� 57      $��180px �6   �� 58      $��* �6    � RowDefinitions 5:   
   �� 5;      $
��80px �6   �� 5<      $��* �6   .+�5?   
   � $��0��6   ���#223377.+�5@      �� $1��+pack://application:,,,/Images/Stec_Logo.png��6   $��Uniform=�6I   $	��150��6[   $��80��6g   5A      � 5C   
   $��1��6   $��0��6   ���#223377.+�5D      �� $��MDM Sender V_02��6    �� TextAlignment$ Center=�6/   $��Center=�6F   
�������$
��20px(�6t   $��Heavy"�6�   5E      �� 5H   
   -   5         ��Name$ LeftStackPanel��5H      ���#868e9c �� Orientation$ Vertical=�6B   $��0��6Y   $��1��6i   .+�5I      �� -   5         ��Name$ btnStart��5I      ���#223377
�������$��시작��6O   $��Heavy"�6\   $��1q�6o   $
��50px��6z    � Cursor$
 Hand��6�   #��[  6�   5J      �� 5K      ���#223377
�������$��종료��6=   $��Heavy"�6J   $��1q�6]   $
��50px��6h   $
 Hand��6v   #��[  6�   5L      �� 5M      -   5        $ 
btnSetting��5M      ���#223377
�������$��설정��6Q   $��Heavy"�6^   $��1q�6q   $
��50px��6|   $
 Hand��6�   #��[  6�   6�   5N      �� 5S   
   -   5         ��Name$ RightStackPanel��5S      ���#c4c9cf$��1��6H   $��1��6X   5U      5V      