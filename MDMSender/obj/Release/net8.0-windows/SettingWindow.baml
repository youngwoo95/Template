   M S B A M L   `   `   `  ���� D  @MDMSender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MDMSender	MDMSender      MDMSender.SettingWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:MDMSender     -   
�������.���   � ColumnDefinitions  �� $��3* ��� $��3* �.+���  �� Orientation$ Vertical=�$��0��.+��� $ Vertical=�
������ $��10q�.+��� $��* Database IP��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -    ��Name$ txtDBIpAddress��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�.������  �� 
TargetType" ��.R���  ��Name$ border�� �� CornerRadius$ 5��
���OO/�$��2q����#ebeff5._���  ��Name$ PART_ContentHost��$��5q� �� Triggers P�  P� Property �@J�IsMouseOver	 P� Value$
	 True��. ��� 
 �� 
TargetName$
 border�� �� Property � �� �� Value
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377.���� $ Vertical=�
������ $��10q�.+��� $��* Database Port��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ 	txtDBPort��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�.������ " ��.R��� $ border��$ 5��
���OO/�$��2q����#ebeff5._��� $ PART_ContentHost��$��5q� P�  �@J�IsMouseOver$
	 True��. ��� $
 border�� � ��
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377.���� $ Vertical=�
������ $��10q�.+��� $��* Database User��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ 	txtDBUser��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�.������ " ��.R��� $ border��$ 5��
���OO/�$��2q����#ebeff5._��� $ PART_ContentHost��$��5q� P�  �@J�IsMouseOver$
	 True��. ��� $
 border�� � ��
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377.���� $ Vertical=�
������ $��10q�.+��� $��* Database Password��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�G� -    G�Name$ txtDBPW��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)� G� PasswordChar$	 ●������ " G�.R��� $ border��$ 5��
���OO/�$��2q����#ebeff5._��� $ PART_ContentHost��$��5q� P�  �@J�IsMouseOver$
	 True��. ��� $
 border�� � ��
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377�� $ Vertical=�
������ $��10q�.+��� $��* Database Name��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ 	txtDBName��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�.������ " ��.R��� $ border��$ 5��
���OO/�$��2q����#ebeff5._��� $ PART_ContentHost��$��5q� P�  �@J�IsMouseOver$
	 True��. ��� $
 border�� � ��
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377.���� $ Vertical=�
������ $��10q�.+��� $��* Destination��$
��Left=�$��Center=�$��18(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� -   $ txtDestination��$��0 2 0 0q�$��Stretch=�$��50��$��Center=�$��18(�$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�.������ " ��.R��� $ border��$ 5��
���OO/�$��2q����#ebeff5._��� $ PART_ContentHost��$��5q� P�  �@J�IsMouseOver$
	 True��. ��� $
 border�� � ��
 ����P�  �@J�	IsFocused$
	 True��. ��� $
 border�� � �� �#223377.���� $ Vertical=�
������ $��10q�.+��   �� $��* ��� $��* �.+��� -    ��Name$ 
btnDBCheck��$��0��$��연결테스트��$��50�����#db5e56
�������$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)����� " ��.R��� $ Border��#��z��
������ $��0q�$ 5��._��� $��Center=�$��Center=� :�  :� RoutedEvent$ Button.MouseEnter��.[��� .���� .��t�  �� 
TargetName$ Border�� �� TargetProperty$ BorderThickness� t� From 0	 t� To 2 g� Duration$ 0:0:0.5D��� $ Border��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)�	 �� To	 Beige$ 0:0:0.5D�:� $ Button.MouseLeave��.[��� .���� .��t� $ Border��$ BorderThickness� 2 0$ 0:0:0.5D��� $ Border��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)� Transparent$ 0:0:0.5D��� -	   $ btnSave��$��1��$��저장��$��50�����#71a1e3
�������$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)����� " ��.R��� $ Border��#��z��
������ $��0q�$ 5��._��� $��Center=�$��Center=� :� $ Button.MouseEnter��.[��� .���� .��t� $ Border��$ BorderThickness� 0 2$ 0:0:0.5D��� $ Border��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)�	 Beige$ 0:0:0.5D�:� $ Button.MouseLeave��.[��� .���� .��t� $ Border��$ BorderThickness� 2 0$ 0:0:0.5D��� $ Border��$2 ,(Border.BorderBrush).(SolidColorBrush.Color)� Transparent$ 0:0:0.5D�� $��1��$��
0 70 10 10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��RUN TIME��$��0��$��Center=�$��Center=�$��80(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)��� $��1��$��Center=�$��Center=�$��40(�
���   �$C��=pack://application:,,,/Fonts/#나눔바른고딕OTF 옛한글)�$��0 20 0 0q�����    MDMSender.CommonModel   RunTime �� Source# Z 