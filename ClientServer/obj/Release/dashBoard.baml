   M S B A M L   `   `   `  ����   ClientServer     ChatClient.DashboardWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Main Dashboard��$	��600��$
��1000�����#FFF0F5.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��3* ��� $��2* ��� $
��Auto �.+��� $��Mortal Kombat X Lobby��$��40(�$
��Bold"����#212121$��	262,0,0,0q�$��Center=�$
��Left=�$��2���� -   $��	btnLogout��$��Logout��$��3��$��0�����#E57373
�������$
��Bold"�$
��12,6q�$��20,0,0,0q�$��Center=�$
��Left=�$��16(�����  �� 
TargetType" ��.���  �� Property � �� �� Value �#E57373��  � ��
 �������  � �� �#D32F2F��  � ��$ 2q���  � ��$
 12,6q���  � ��$ 16(���  � ��$
 Bold"���  �@�FocusVisualStyle Z� ��  � �� ��  �� 
TargetType" ��.R��� #��z��#��z��#��z�� �� Padding# z�� �� CornerRadius$ 8��._��� $��Center=�$��Center=��� -   	 ��Name$	 
lblWelcome��$�� ��$��1��$��0��$��3��$��36(�$
��Bold"����#212121$��	20,10,0,0q�$	��Top=�$
��Left=��� $��2��$��0��$��30,20,10,73q�$ 10q����#E8F5E9���#BDBDBD$��1q�$ 8��$��2��._��� .+��� $��Available Rooms��$��24(�$��SemiBold"����#424242�� -   
 ��Name$
 scrRooms��$��0,10,0,0q�$	��399��.���� -    ��Name$ roomStackPanel���� $��2��$��1��$��16,20,26,190q�$ 10q����#E8F5E9���#BDBDBD$��1q�$ 8��$��2��._��� $��0,0,0,-1q�.+��� $��New Room Name��$��16(�$��SemiBold"����#424242�� -    ��Name$ 
txtNewRoom��$	��0,5q�$��10q�$��39�����#FFFFFF���#BDBDBD$��1q��� -    ��Name$ btnCreateRoom��$��Create�����#66BB6A
�������$
��Bold"�$
��12,6q�$��16(�$��59��$	��371������ " ��.���  � �� �#66BB6A��  � ��
 �������  � ��
 ���� ��  � ��$ 0q���  � ��$
 12,6q���  � ��$ 16(���  � ��$
 Bold"���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� #��z��#��z��#��z��# z��$ 8��._��� $��Center=�$��Center=��� -   $��btnPrivateMessages��$��Private Messages��$��3��$��2�����#4CAF50
�������$
��Bold"�$
��12,6q�$��20q�$��Center=�$��Center=�$��16(����� " ��.���  � �� �#4CAF50��  � ��
 �������  � ��
 ���� ��  � ��$ 0q���  � ��$
 12,6q���  � ��$ 16(���  � ��$
 Bold"���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� #��z��#��z��#��z��# z��$ 8��._��� $��Center=�$��Center=�