   M S B A M L   `   `   `  ����M  IDay04ScoopSelector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Day04ScoopSelectorDay04ScoopSelector  5   	   #    Day04ScoopSelector.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Day04ScoopSelector     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��600��6)   .��5	      �  � 	Resources 5
   
    �� 
TargetType �� Property �� Value��%�   '��       '�� H     �� 5      " ��6   .�5      ��  � ��$ 22��6+   5      �� 5      " ��6   .�5      ��  � ��$	 0 5q�6+   5      5       � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $	��210 �6   �� 5      $��20 �6   �� 5      $��80 �6   �� 5      $��20 �6   �� 5      $	��210 �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $	��330 �6   �� 5      $
��auto �6   �� 5       $��20 �6   .+�5"   
   �� $��1��6   $��1��6   $��Flavours��6-   �� 5#   
   -   5         ��Name$ flavourListBinding��5#   0   $��2��6   $��1��6    6L   �� 5$   
   -   5         ��Name$ btAdd��5$      $��2��6    $��3��6-   $��Add=>��6=   $��22��6M   6Y   �� 5%   
   -   5        $ selectListBinding��5%   0   $��2��6   $��5��6    6K   �� 5&   
   $��1��6   $��5��6   $��Selected��6-   �� 5'   
   -   5        $ btDelete��5'      $��3��6#   $��5��60   $��Delete Scoop��6@   6W   �� 5(   
   -   5        $ btClear��5(      $��3��6"   $��1��6/   $��	Clear All��6?   6S   5*      5+      