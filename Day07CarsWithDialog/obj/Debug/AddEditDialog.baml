   M S B A M L   `   `   `  ����N  JDay07CarsWithDialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Day07CarsWithDialogDay07CarsWithDialog  5   	   '    !Day07CarsWithDialog.AddEditDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Day07CarsWithDialog     5   	     � WindowStartupLocation$  CenterOwner=�5       � Title$ AddEditDialog��5   	   $	��240��6   $	��500��6,   .��5	      �  � 	Resources 5
   
    �� 
TargetType �� Property �� Value��%%  '��       '�� I     '�� �     �� 5      " ��6   .�5      ��  � ��$	 0 5q�6+   5      �� 5      " ��6   .�5      ��  � ��$
 0 10q�6+   5      �� 5      " ��6   .�5      ��  � ��$	 100��6*   �� 5       � ��$ 	50 35 0 5q�6+   5      5       � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $	��150 �6   �� 5      $	��300 �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5       $
��auto �6   �� 5!      $
��auto �6   �� 5"      $��20 �6   .+�5$   
   �� $��1��6   $��1��6   $��
Make Model��6-   �� 5%   
   -   5         ��Name$ tbModel��5%   /   $��1��6   $��2��6   	 �� 	MaxLength$	 50��6@   �� 5&   
   $��2��6   $��1��6   .��5'      �� ��6   �� 
 �� ElementName$
 sldEngineSize�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=� �� StringFormat$ EngineSize    {0:#}L��5)      �� 5*   
   -   5        $��sldEngineSize��5+   -   $��2��5*      $��2��6    � Maximum$ 20Q�6.    � Minimum$ 0Q�6;    �� TickPlacement$ BottomRight=�5+       �� TickFrequency$	 0.5Q�5,       �� IsSnapToTickEnabled . 6%   �� 5-   
   $��3��6   $��1��6   $��	Fuel Type��6-   �� 5.   
   -   5         ��Name$ cmbFuelType��5.   0   $��3��6   $��2��6    60   �� 5/   
   -   5         ��Name$ btCancel��5/   .   $��4��6   $��1��6   $��Cancel��6@    �� IsCancel . 6Q   �� 50   
   -   5        $ btSave��50   .   $��4��6   $��2��6   $
��Save��6>    �� 	IsDefault . 6^   51      52      