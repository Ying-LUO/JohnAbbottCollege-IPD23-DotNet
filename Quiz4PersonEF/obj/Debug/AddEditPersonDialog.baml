   M S B A M L   `   `   `  ����H  DQuiz4PersonEF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Quiz4PersonEFQuiz4PersonEF  5   	   '    !Quiz4PersonEF.AddEditPersonDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Quiz4PersonEF     5   	     � WindowStartupLocation$  CenterOwner=�5       � Title$ AddEditPerson��5   	   $	��200��6   $	��350��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��100 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��0��6   $
��Name��6-   $��10q�6<   �� 5   
   -   5         ��Name$ tbName��5   /   $��0��6   $��1��6   $��10q�6?    �� 	MaxLength$ 50��6K   �� 5   
   $��1��6   $��0��6   $	��Age��6-   $��10q�6;   �� 5   
   -   5        $ tbAge��5   /   $��1��6   $��1��6   $��10q�6>   $ 3��6J   �� 5   
    �� IsCancel . $��Cancel��6!   $��2��62   $��0��6?   $��20q�6O   �� 5   
   -   5         ��Name$ btAdd��5   "    �� 	IsDefault . $	��Add��6E   $��2��6S   $��1��6`   $��70��6p   $��20q�6{   5      5      