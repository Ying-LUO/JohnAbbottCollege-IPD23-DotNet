   M S B A M L   `   `   `  ����H  DQuiz4PersonEF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Quiz4PersonEFQuiz4PersonEF  5   	   )    #Quiz4PersonEF.AddEditPassportDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Quiz4PersonEF     5   	     � WindowStartupLocation$  CenterOwner=�5       � Title$ AddEditPassport��5   	   $	��280��6!   $	��220��6.   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $	��150 �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��1��6   $
��Name��6-   �� 5   
   -   5         ��Name$ tblName��5   1   $��0��6   $��2��6!   $��WrapWithOverflow=�6B   �� 5   
   $��Passport No.��6   $��1��6'   $��1��64   �� 5   
   -   5         ��Name$ tbPassportNo��5      $��1��6(   $��2��65   $��5q�6E    �� 	MaxLength$ 8��6P   �� 5   
   -   5        $��2��5      $��1��6   $��2��6J   $��5q�6^   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ image��5      $	��150��6'   �� 5      -   5        $ tbImage��5   9   $��Click to select image��6   69   5      5      �� 5    
   $��Cancel��6   	 �� IsCancel	 . $��3��62   $��1��6?   $��5q�6O   �� 5!   
   -   5        
 ��Name$
 btSave��5!      $
��Save��66   $��3��6E   $��2��6R    �� 	IsDefault . $��75��6s   $��5q�6~   5"      5#      